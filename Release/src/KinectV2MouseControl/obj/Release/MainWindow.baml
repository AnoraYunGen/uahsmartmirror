   M S B A M L   `   `   `  ����   KinectV2MouseControl%    KinectV2MouseControl.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Kinect v2 Mouse Control��$��392.197��$��458.276�� � 
ResizeMode$ CanMinimize=� � WindowStartupLocation$ CenterScreen=�.��� .+�� -   $��0,0,0.5,-0.5q�.+��� -    ��Name$ 
btnDefault��$��Default��$��Right=�$��0,323,25.5,0q�$��74��$��24��$	��Top=��� -    ��Name$ 
chkNoClick��$!��No clicks, move cursor only��$
��Left=�$��214,291,0,0q�$��16��$	��Top=�	� -    	�Name$ rdiGrip��$��Grip Gesture��$
��Left=�$��214,312,0,0q�$	��Top=� 	� 	GroupName$ GestureMode��	� -   $ rdiPause��$��Pause To Click��$
��Left=�$��214,332,0,0q�$	��Top=�$ GestureMode���� -    ��Name$ UAH_blue_png��$��UAH_blue.png��$
��Fill=�$��0,268,243.5,-0.5q��� $��Mouse Sensitivity��$��10,10,139,0q�$	��Top=�$��25���� $"��Pause-To-Click Time Required��$��10,78,139,0q�$	��Top=�$��24���� $��Pause Movement Thresold��$��10,146,139,0q�$	��Top=�$��24���� $��Cursor Smoothing��$��10,215,139,0q�$	��Top=�$��24���� -    ��Name$ txtMouseSensitivity��$��22��$��	0,10,26,0q�	 �� TextWrapping$
	 Wrap=�$	��Top=�$
��3.50��$��15(�
 �� TextAlignment$
 Right=�$��Right=�$��78�� J� RenderTransformOrigin$ 
-0.179,2.5&��� -	   $ txtTimeRequired��$��22��$��	0,79,26,0q�$
	 Wrap=�$	��Top=�$
��2.00��$��15(�$
 Right=�$��Right=�$��78���� -
   $ txtPauseThresold��$��21��$��
0,147,26,0q�$
	 Wrap=�$	��Top=�$��60.00��$��15(�$
 Right=�$��Right=�$��78���� -   $ txtCursorSmoothing��$��22��$��
0,217,26,0q�$
	 Wrap=�$	��Top=�$
��0.40��$��15(�$
 Right=�$��Right=�$��78��$ 
0.679,-0.5&��� -    ��Name$ PauseToClickTime��$��46,107,61,0q�$	��Top=�$��25�� � Maximum$ 5Q� � SmallChange$
 0.01Q� � Value$ 2Q� � Minimum$	 0.3Q��� -   $ PauseThresold��$��46,175,61,0q�$	��Top=�$��25��$	 160Q�$
 0.01Q�$ 60Q�$ 10Q��� -   $ CursorSmoothing��$��46,244,61,0q�$	��Top=�$��24��$ 1Q�$
 0.01Q�$	 0.2Q��� -   $ MouseSensitivity��$��
46,39,61,0q�$	��Top=�$��25��$ 5Q�$
 0.01Q�$ 2Q�$ 1Q��� -   $ btn_msp��$��+��$
��Left=�$��
396,39,0,0q�$	��Top=�$��30���� -   $ btn_msn��$��-��$
��Left=�$��	10,39,0,0q�$	��Top=�$��31���� -   $ btn_trp��$��+��$
��Left=�$��396,107,0,0q�$	��Top=�$��30���� -   $ btn_trn��$��-��$
��Left=�$��
10,107,0,0q�$	��Top=�$��31���� -   $ btn_mtp��$��+��$
��Left=�$��396,175,0,0q�$	��Top=�$��30���� -   $ btn_mtn��$��-��$
��Left=�$��
10,175,0,0q�$	��Top=�$��31���� -   $ btn_csp��$��+��$
��Left=�$��396,244,0,0q�$	��Top=�$��30���� -   $ btn_csn��$��-��$
��Left=�$��
10,244,0,0q�$	��Top=�$��31��