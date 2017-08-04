using System;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Kinect;
using WindowsInput;

namespace KinectV2MouseControl
{
    class KinectControl
    {
        /// Active Kinect sensor
        KinectSensor sensor;
        /// Reader for body frames
        BodyFrameReader bodyFrameReader;
        /// Array for the bodies
        private Body[] bodies = null;
        /// Screen width and height for determining the exact mouse sensitivity
        int screenWidth, screenHeight;
        /// timer for pause-to-click feature
        DispatcherTimer timer = new DispatcherTimer();
        /// How far the cursor move according to your hand's movement
        public float mouseSensitivity = MOUSE_SENSITIVITY;
        /// Time required as a pause-clicking
        public float timeRequired = TIME_REQUIRED;
        /// The radius range your hand move inside a circle for [timeRequired] seconds would be regarded as a pause-clicking
        public float pauseThresold = PAUSE_THRESOLD;
        /// Decide if the user need to do clicks or only move the cursor
        public bool doClick = DO_CLICK;
        /// Use Grip gesture to click or not
        public bool useGripGesture = USE_GRIP_GESTURE;
        /// Value 0 - 0.95f, the larger it is, the smoother the cursor would move
        public float cursorSmoothing = CURSOR_SMOOTHING;
        // Default values
        public const float MOUSE_SENSITIVITY = 3.5f;
        public const float TIME_REQUIRED = 2f;
        public const float PAUSE_THRESOLD = 60f;
        public const bool DO_CLICK = true;
        public const bool USE_GRIP_GESTURE = true;
        public const float CURSOR_SMOOTHING = 0.2f;
        /// magclosed variable is used to determine whether or not if the magnify program is rnning or not
        private bool magclosed = true;
        /// Determine if we have tracked the hand and used it to move the cursor,
        /// If false, meaning the user may not lift their hands, we don't get the last hand position and some actions like pause-to-click won't be executed.
        bool alreadyTrackedPos = false;
        /// for storing the time passed for pause-to-click
        float timeCount = 0;
        /// For storing last cursor position
        Point lastCurPos = new Point(0, 0);
        /// If true, user did a left/right hand Grip gesture
        bool wasLRGrip = false;
        /// Program init
        public KinectControl()
        {
            // get Active Kinect Sensor
            sensor = KinectSensor.GetDefault();
            // open the reader for the body frames
            bodyFrameReader = sensor.BodyFrameSource.OpenReader();
            bodyFrameReader.FrameArrived += bodyFrameReader_FrameArrived;
            // get screen with and height
            screenWidth = (int)SystemParameters.PrimaryScreenWidth;
            screenHeight = (int)SystemParameters.PrimaryScreenHeight;
            // set up timer, execute every 0.1s
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100); 
　　　　    timer.Tick += new EventHandler(Timer_Tick);
　　　　    timer.Start();
            // open the sensor
            sensor.Open();
        }
        /// Pause to click timer
        void Timer_Tick(object sender, EventArgs e)
        {
            if (!doClick || useGripGesture)
                return;

            if (!alreadyTrackedPos)
            {
                timeCount = 0;
                return;
            }
            
            Point curPos = MouseControl.GetCursorPosition();

            if ((lastCurPos - curPos).Length < pauseThresold)
            {
                if ((timeCount += 0.1f) > timeRequired)
                {
                    MouseControl.DoMouseClick();
                    timeCount = 0;
                }
            }
            else
                timeCount = 0;

            lastCurPos = curPos;
        }
        /// Read body frames
        void bodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null)
                    {
                        this.bodies = new Body[bodyFrame.BodyCount];
                    }

                    // The first time GetAndRefreshBodyData is called, Kinect will allocate each Body in the array.
                    // As long as those body objects are not disposed and not set to null in the array,
                    // those body objects will be re-used.
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }
            }
            if (!dataReceived) 
            {
                alreadyTrackedPos = false;
                return;
            }
            foreach (Body body in this.bodies)
            {
                // get first tracked body only, notice there's a break below.
                if (body.IsTracked)
                {
                    // get various skeletal positions
                    CameraSpacePoint handLeft = body.Joints[JointType.HandLeft].Position;
                    CameraSpacePoint handRight = body.Joints[JointType.HandRight].Position;
                    CameraSpacePoint spineBase = body.Joints[JointType.SpineBase].Position;

                    //if both hands lift up
                    if ((handRight.Z - spineBase.Z < -0.15f) && (handLeft.Z - spineBase.Z < -0.15f))
                        dualhands(body);
                    // if right hand lift forward
                    else if (handRight.Z - spineBase.Z < -0.15f)
                        setcursor(handRight.X - spineBase.X + 0.05f, spineBase.Y - handRight.Y + 0.51f, body, 1);
                    // if left hand lift forward
                    else if (handLeft.Z - spineBase.Z < -0.15f)
                        setcursor(handLeft.X - spineBase.X + 0.3f, spineBase.Y - handLeft.Y + 0.51f, body, 2);
                    else
                    {
                        wasLRGrip = true;
                        alreadyTrackedPos = false;
                    }
                    // get first tracked body only
                    break;
                }
            }
        }
        /// dual hands
        private void dualhands(Body body)
        {
            //this portion (if clause) was added to add another guesture to the original program
            //specifically the windows magnification tool using keyboard shortcut 'windows'+'+' or 'windows'+'-'
            alreadyTrackedPos = true;
            if (this.magclosed == true)
            {
                InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.ADD);
                this.magclosed = false;
            }
            bool righth_closed = body.HandRightState == HandState.Closed;
            bool lefth_closed = body.HandLeftState == HandState.Closed;

            if (righth_closed && lefth_closed)
                killmag();
            else if (righth_closed)
                InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.ADD);
            else if (lefth_closed)
                InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.RWIN, VirtualKeyCode.SUBTRACT);
        }
        /// this function is used to set the cursor relative to the left/right hand position towards the screen
        private void setcursor(float x, float y, Body body, int hand)
        {
            Point curPos = MouseControl.GetCursorPosition();
            // smoothing for using should be 0 - 0.95f. The way we smooth the cusor is: oldPos + (newPos - oldPos) * smoothValue
            float smoothing = 1 - cursorSmoothing;
            // set cursor position
            int cursor_x = (int)(curPos.X + (x * mouseSensitivity * screenWidth - curPos.X) * smoothing);
            int cursor_y = (int)(curPos.Y + ((y + 0.25f) * mouseSensitivity * screenHeight - curPos.Y) * smoothing);
            MouseControl.SetCursorPos(cursor_x, cursor_y);
            alreadyTrackedPos = true;
            // Grip gesture
            /// the following is used to click
            if (doClick && useGripGesture)
            {
                if ((body.HandRightState == HandState.Closed || (body.HandLeftState == HandState.Closed)) && !wasLRGrip)
                {
                    MouseControl.MouseLeftDown();
                    wasLRGrip = true;
                }
                else if ((body.HandRightState == HandState.Open || body.HandRightState == HandState.Lasso ||
                          body.HandLeftState == HandState.Open || body.HandLeftState == HandState.Lasso) && wasLRGrip)
                {
                    MouseControl.MouseLeftUp();
                    wasLRGrip = false;
                }
            }
        }
        /// this killmag funciton is used to kill the magnify application when not using the magnify application
        private void killmag()
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("Magnify");
            if (processes.Length > 0)
                foreach (System.Diagnostics.Process p in processes)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch//if cannot kill Magnify
                    {/*do nothing*/}
                }
            this.magclosed = true;
            //sleep for 2 seconds after closing the magnify application
            System.Threading.Thread.Sleep(2000);
        }
        /// this close function is used to stop the timer
        /// and disconnect from the sensor when the program closes
        public void Close()
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
            if (this.sensor != null)
            {
                this.sensor.Close();
                this.sensor = null;
            }
        }
    }
}
