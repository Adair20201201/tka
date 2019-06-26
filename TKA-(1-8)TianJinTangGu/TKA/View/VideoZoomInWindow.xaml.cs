using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TKA.View.ViewModel;

namespace TKA.View
{
    /// <summary>
    /// VideoZoomInWindow.xaml 的交互逻辑
    /// </summary>
    public partial class VideoZoomInWindow : Window
    {
        public VideoZoomInWindow()
        {
            InitializeComponent();
            this.DataContext = new VideoZoomInWindowViewModel();
            this.Loaded += VideoZoomInWindow_Loaded;

        }
        private VideoZoomInWindowViewModel VZWVM;
        public int HCUserID;
        public int Channle;
        private void VideoZoomInWindow_Loaded(object sender, RoutedEventArgs e)
        {
            VZWVM = (VideoZoomInWindowViewModel)this.DataContext;
            //string HCIP = ConfigurationManager.AppSettings["HCIP"];
            //ushort HCPort = Convert.ToUInt16(ConfigurationManager.AppSettings["HCPort"]);
            //string HCUserName = ConfigurationManager.AppSettings["HCUserName"];
            //string HCPassWord = ConfigurationManager.AppSettings["HCPassWord"];
            //VZWVM.TV = new Model.TrackVideo();
            //VZWVM.TV = new MainWindowViewModel().LoadCameraConfig();

            //HC_SDKFactory.DVR_NET_INIT();
            //HCUserID = HC_SDKFactory.Login(HCIP, HCPort, HCUserName, HCPassWord);

            if (HCUserID != -1)
            {
                VZWVM.ZoomInVideoControl = HC_SDKFactory.factory(HCUserID, Channle, ZoomInVideo.Handle, 0);
                VZWVM.ZoomInVideoControl.Play();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VZWVM.ZoomInVideoControl.Stop();
            this.ZoomInVideo = null;
            this.Close();
        }

        /// <summary>
        /// 向上移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Camera_Start_Move(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "Up":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.TILT_UP, 0, (uint)2);
                    break;
                case "Left":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.PAN_LEFT, 0, (uint)2);
                    break;
                case "Right":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.PAN_RIGHT, 0, (uint)2);
                    break;
                case "Down":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.TILT_DOWN, 0, (uint)2);
                    break;
                case "ZoomIn":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.ZOOM_IN, 0, (uint)2);
                    break;
                case "ZoomOut":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.ZOOM_OUT, 0, (uint)2);
                    break;
            }

        }
        /// <summary>
        /// 向上移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Camera_Stop_Move(object sender, MouseButtonEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "Up":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.TILT_UP, 1, (uint)2);
                    break;
                case "Left":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.PAN_LEFT, 1, (uint)2);
                    break;
                case "Right":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.PAN_RIGHT, 1, (uint)2);
                    break;
                case "Down":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.TILT_DOWN, 1, (uint)2);
                    break;
                case "ZoomIn":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.ZOOM_IN, 1, (uint)2);
                    break;
                case "ZoomOut":
                    HCNetSDK_X64.NET_DVR_PTZControlWithSpeed_Other(HCUserID, Channle, HCNetSDK_X64.ZOOM_OUT, 1, (uint)2);
                    break;
            }
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
    }
}
