using System.Windows;
using System.Windows.Input;

namespace KinectV2MouseControl
{
    public partial class MainWindow : Window
    {
        KinectControl kinectCtrl = new KinectControl();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MouseSensitivity.Value = Properties.Settings.Default.MouseSensitivity;
            PauseToClickTime.Value = Properties.Settings.Default.PauseToClickTime;
            PauseThresold.Value = Properties.Settings.Default.PauseThresold;
            chkNoClick.IsChecked = !Properties.Settings.Default.DoClick;
            CursorSmoothing.Value = Properties.Settings.Default.CursorSmoothing;
            if (Properties.Settings.Default.GripGesture)
                rdiGrip.IsChecked = true;
            else
                rdiPause.IsChecked = true;
        }

        private void MouseSensitivity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (MouseSensitivity.IsLoaded)
            {
                kinectCtrl.mouseSensitivity = (float)MouseSensitivity.Value;
                txtMouseSensitivity.Text = kinectCtrl.mouseSensitivity.ToString("f2");
                
                Properties.Settings.Default.MouseSensitivity = kinectCtrl.mouseSensitivity;
                Properties.Settings.Default.Save();
            }
        }

        private void PauseToClickTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PauseToClickTime.IsLoaded)
            {
                kinectCtrl.timeRequired = (float)PauseToClickTime.Value;
                txtTimeRequired.Text = kinectCtrl.timeRequired.ToString("f2");

                Properties.Settings.Default.PauseToClickTime = kinectCtrl.timeRequired;
                Properties.Settings.Default.Save();
            }
        }

        private void txtMouseSensitivity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                float v;
                if (float.TryParse(txtMouseSensitivity.Text, out v))
                {
                    MouseSensitivity.Value = v;
                    kinectCtrl.mouseSensitivity = (float)MouseSensitivity.Value;
                }
            }
        }

        private void txtTimeRequired_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                float v;
                if (float.TryParse(txtTimeRequired.Text, out v))
                {
                    PauseToClickTime.Value = v;
                    kinectCtrl.timeRequired = (float)PauseToClickTime.Value;
                }
            }
        }

        private void PauseThresold_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PauseThresold.IsLoaded)
            {
                kinectCtrl.pauseThresold = (float)PauseThresold.Value;
                txtPauseThresold.Text = kinectCtrl.pauseThresold.ToString("f2");

                Properties.Settings.Default.PauseThresold = kinectCtrl.pauseThresold;
                Properties.Settings.Default.Save();
            }
        }

        private void txtPauseThresold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                float v;
                if (float.TryParse(txtPauseThresold.Text, out v))
                {
                    PauseThresold.Value = v;
                    kinectCtrl.timeRequired = (float)PauseThresold.Value;
                }
            }
        }

        private void btnDefault_Click(object sender, RoutedEventArgs e)
        {
            MouseSensitivity.Value = KinectControl.MOUSE_SENSITIVITY;
            PauseToClickTime.Value = KinectControl.TIME_REQUIRED;
            PauseThresold.Value = KinectControl.PAUSE_THRESOLD;
            CursorSmoothing.Value = KinectControl.CURSOR_SMOOTHING;

            chkNoClick.IsChecked = !KinectControl.DO_CLICK;
            rdiGrip.IsChecked = KinectControl.USE_GRIP_GESTURE;
        }

        private void chkNoClick_Checked(object sender, RoutedEventArgs e)
        {
            chkNoClickChange();
        }


        public void chkNoClickChange()
        {
            kinectCtrl.doClick = !chkNoClick.IsChecked.Value;
            Properties.Settings.Default.DoClick = kinectCtrl.doClick;
            Properties.Settings.Default.Save();
        }

        private void chkNoClick_Unchecked(object sender, RoutedEventArgs e)
        {
            chkNoClickChange();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            kinectCtrl.Close();
        }

        public void rdiGripGestureChange()
        {
            kinectCtrl.useGripGesture = rdiGrip.IsChecked.Value;
            Properties.Settings.Default.GripGesture = kinectCtrl.useGripGesture;
            Properties.Settings.Default.Save();
        }

        private void rdiGrip_Checked(object sender, RoutedEventArgs e)
        {
            rdiGripGestureChange();
        }

        private void rdiPause_Checked(object sender, RoutedEventArgs e)
        {
            rdiGripGestureChange();
        }

        private void CursorSmoothing_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (CursorSmoothing.IsLoaded)
            {
                kinectCtrl.cursorSmoothing = (float)CursorSmoothing.Value;
                txtCursorSmoothing.Text = kinectCtrl.cursorSmoothing.ToString("f2");

                Properties.Settings.Default.CursorSmoothing = kinectCtrl.cursorSmoothing;
                Properties.Settings.Default.Save();
            }
        }//mouse sensitivity
        private void btn_msn_Click(object sender, RoutedEventArgs e)
        {
            MouseSensitivity.Value = kinectCtrl.mouseSensitivity = (float)kinectCtrl.mouseSensitivity - (float)0.1;
            if (kinectCtrl.mouseSensitivity < 1)
                MouseSensitivity.Value = kinectCtrl.mouseSensitivity = (float)1;
            txtMouseSensitivity.Text = kinectCtrl.mouseSensitivity.ToString("f2");
        }

        private void btn_msp_Click(object sender, RoutedEventArgs e)
        {
            MouseSensitivity.Value = kinectCtrl.mouseSensitivity = (float)kinectCtrl.mouseSensitivity + (float)0.1;
            if (kinectCtrl.mouseSensitivity > 5)
                MouseSensitivity.Value = kinectCtrl.mouseSensitivity = (float)5;
            txtMouseSensitivity.Text = kinectCtrl.mouseSensitivity.ToString("f2");
        }
        //time required for pause to click
        private void btn_trn_Click(object sender, RoutedEventArgs e)
        {
            PauseToClickTime.Value = kinectCtrl.timeRequired = (float)kinectCtrl.timeRequired - (float)0.1;
            if (kinectCtrl.timeRequired < 0.3)
                PauseToClickTime.Value = kinectCtrl.timeRequired = (float)0.3;
            txtTimeRequired.Text = kinectCtrl.timeRequired.ToString("f2");
        }

        private void btn_trp_Click(object sender, RoutedEventArgs e)
        {
            PauseToClickTime.Value = kinectCtrl.timeRequired = (float)kinectCtrl.timeRequired + (float)0.1;
            if (kinectCtrl.timeRequired > 5)
                PauseToClickTime.Value = kinectCtrl.timeRequired = (float)5;
            txtTimeRequired.Text = kinectCtrl.timeRequired.ToString("f2");
        }
        // pause movement threshold
        private void btn_mtn_Click(object sender, RoutedEventArgs e)
        {
            PauseThresold.Value = kinectCtrl.pauseThresold = (float)kinectCtrl.pauseThresold - (float)1;
            if (kinectCtrl.pauseThresold < 10)
                PauseThresold.Value = kinectCtrl.pauseThresold = (float)10;
            txtPauseThresold.Text = kinectCtrl.pauseThresold.ToString("f2");
        }

        private void btn_mtp_Click(object sender, RoutedEventArgs e)
        {
            PauseThresold.Value = kinectCtrl.pauseThresold = (float)kinectCtrl.pauseThresold + (float)1;
            if (kinectCtrl.pauseThresold > 160)
                PauseThresold.Value = kinectCtrl.pauseThresold = (float)160;
            txtPauseThresold.Text = kinectCtrl.pauseThresold.ToString("f2");
        }
        //cursor smoothing
        private void btn_csn_Click(object sender, RoutedEventArgs e)
        {
            CursorSmoothing.Value = kinectCtrl.cursorSmoothing = (float)kinectCtrl.cursorSmoothing - (float)0.05;
            if (kinectCtrl.cursorSmoothing < 0)
                CursorSmoothing.Value = kinectCtrl.cursorSmoothing = (float)0;
            txtCursorSmoothing.Text = kinectCtrl.cursorSmoothing.ToString("f2");
        }

        private void btn_csp_Click(object sender, RoutedEventArgs e)
        {
            CursorSmoothing.Value = kinectCtrl.cursorSmoothing = (float)kinectCtrl.cursorSmoothing + (float)0.05;
            if (kinectCtrl.cursorSmoothing > 1)
                CursorSmoothing.Value = kinectCtrl.cursorSmoothing = (float)1;
            txtCursorSmoothing.Text = kinectCtrl.cursorSmoothing.ToString("f2");
        }
    }
}
