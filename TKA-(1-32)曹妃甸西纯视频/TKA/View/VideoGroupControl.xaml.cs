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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using TKA.ViewModel;
using System.Runtime.InteropServices;
using System.Windows.Forms.Integration;
using System.Threading;

namespace TKA.View
{
    /// <summary>
    /// VideoGroupControl.xaml 的交互逻辑
    /// </summary>
    public partial class VideoGroupControl : UserControl
    {
        private int HCUserID;
        private VideoGroupViewModel VGVM;
        private HCNetSDK_X64.NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40;
        private uint dwAChanTotalNum = 0;
        private int dwDCStartChannelNum = 0;
        private int[] iChannelNum = new int[96];
        /// <summary>
        /// 关闭所有的视频
        /// </summary>
        public void CloseAll()
        {
            if (VGVM != null)
            {
                VGVM.CloseAll();
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            VGVM = (VideoGroupViewModel)this.DataContext;
            //rb1.IsChecked = true;

            string HCIP = ConfigurationManager.AppSettings["HCIP"];
            ushort HCPort = Convert.ToUInt16(ConfigurationManager.AppSettings["HCPort"]);
            string HCUserName = ConfigurationManager.AppSettings["HCUserName"];
            string HCPassWord = ConfigurationManager.AppSettings["HCPassWord"];
            HC_SDKFactory.DVR_NET_INIT();
            HCUserID = HC_SDKFactory.Login(HCIP, HCPort, HCUserName, HCPassWord);
            dwAChanTotalNum = HC_SDKFactory.lpDeviceInfo_X64.byChanNum;
            if (HC_SDKFactory.lpDeviceInfo_X64.byIPChanNum > 0)
            {
                InfoIPChannel();
            }

            if (HCUserID != -1)
            {
                //VGVM.w0 = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat.Channel + dwDCStartChannelNum, WestThroatVideo.Handle, 0);
                //VGVM.w1 = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].West.Channel + dwDCStartChannelNum, WestTrackVideo.Handle, 0);
                //VGVM.w2 = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].West.Channel + dwDCStartChannelNum, WestTrackZoomVideo.Handle, 0);

                //VGVM.w3 = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].East.Channel + dwDCStartChannelNum, EastTrackZoomVideo.Handle, 0);
                //VGVM.e0 = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].East.Channel + dwDCStartChannelNum, EastTrackVideo.Handle, 0);
                //VGVM.e1 = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat.Channel + dwDCStartChannelNum, EastThroatVideo.Handle, 0);
                //VGVM.PlayAll();

                VGVM.wt0 = HC_SDKFactory.factory(HCUserID, 0 + dwDCStartChannelNum, wt0.Handle, 0);
                VGVM.et0 = HC_SDKFactory.factory(HCUserID, 1 + dwDCStartChannelNum, et0.Handle, 0);
                VGVM.wt0.Play();
                VGVM.et0.Play();

                VGVM.wt1 = HC_SDKFactory.factory(HCUserID, 2 + dwDCStartChannelNum, wt1.Handle, 0);
                VGVM.et1 = HC_SDKFactory.factory(HCUserID, 3 + dwDCStartChannelNum, et1.Handle, 0);
                VGVM.wt1.Play();
                VGVM.et1.Play();

                VGVM.wt2 = HC_SDKFactory.factory(HCUserID, 4 + dwDCStartChannelNum, wt0.Handle, 0);
                VGVM.et2 = HC_SDKFactory.factory(HCUserID, 5 + dwDCStartChannelNum, et0.Handle, 0);
                VGVM.wt2.Play();
                VGVM.et2.Play();

                VGVM.wt3 = HC_SDKFactory.factory(HCUserID, 6 + dwDCStartChannelNum, wt0.Handle, 0);
                VGVM.et3 = HC_SDKFactory.factory(HCUserID, 7 + dwDCStartChannelNum, et0.Handle, 0);
                VGVM.wt3.Play();
                VGVM.et3.Play();
            }
        }

        private void InfoIPChannel()
        {
            uint dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(m_struIpParaCfgV40, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            if (!HCNetSDK_X64.NET_DVR_GetDVRConfig(HCUserID, HCNetSDK_X64.NET_DVR_GET_IPPARACFG_V40, 0, ptrIpParaCfgV40, dwSize, ref dwReturn))
            {
                MessageBox.Show("Net NVR Failed! Code :" + HCNetSDK_X64.NET_DVR_GetLastError());
            }
            else
            {
                m_struIpParaCfgV40 = (HCNetSDK_X64.NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HCNetSDK_X64.NET_DVR_IPPARACFG_V40));
                //byte byStreamType;
                for (int i = 0; i < m_struIpParaCfgV40.dwDChanNum; i++)
                {
                    iChannelNum[i + dwAChanTotalNum] = i + (int)m_struIpParaCfgV40.dwStartDChan;
                    dwDCStartChannelNum = (int)m_struIpParaCfgV40.dwStartDChan;
                    //byStreamType = m_struIpParaCfgV40.struStreamMode[i].byGetStreamType;
                    //switch (byStreamType)
                    //{
                    //    //目前NVR仅支持0- 直接从设备取流一种方式 NVR supports only one mode: 0- get stream from device directly
                    //    case 0:
                    //        dwSize = (uint)Marshal.SizeOf(m_struStreamMode);
                    //        IntPtr ptrChanInfo = Marshal.AllocHGlobal((Int32)dwSize);
                    //        Marshal.StructureToPtr(m_struIpParaCfgV40.struStreamMode[i].uGetStream, ptrChanInfo, false);
                    //        m_struChanInfo = (HCNetSDK_X64.NET_DVR_IPCHANINFO)Marshal.PtrToStructure(ptrChanInfo, typeof(HCNetSDK_X64.NET_DVR_IPCHANINFO));

                    //        //列出IP通道 List the IP channel
                    //        ListIPChannel(i + 1, m_struChanInfo.byEnable, m_struChanInfo.byIPID);
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
            }
        }
        public VideoGroupControl()
        {
            InitializeComponent();
        }
        List<string> list = new List<string>();
        List<RadioButton> btnList = new List<RadioButton>();
        Thread thread;
        bool isOver = true;
        private void RadioButton_Cick(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (btnList.Contains(radioButton))
            {
                radioButton.Background = new SolidColorBrush(Color.FromRgb(107, 123, 128));
                btnList.Remove(radioButton);
            }
            else
            {
                radioButton.Background = new SolidColorBrush(Color.FromRgb(58, 155, 176));
                btnList.Add(radioButton);
                if (btnList.Count > 4)
                {
                    RadioButton firstRadioButton = btnList[0];
                    firstRadioButton.Background = new SolidColorBrush(Color.FromRgb(107, 123, 128));
                    btnList.RemoveAt(0);
                }
            }

            if (VGVM != null)
            {
                tb0.Text = "";
                tb1.Text = "";
                tb2.Text = "";
                tb3.Text = "";
                object tag = (radioButton).Tag;
                if (!list.Contains(tag.ToString()))
                {
                    list.Add(tag.ToString());
                    if (list.Count > 4)
                    {
                        list.RemoveAt(0);
                    }
                }
                else
                {
                    list.Remove(tag.ToString());
                }

                w0.Visible = false;
                w1.Visible = false;
                w2.Visible = false;
                w3.Visible = false;


                e0.Visible = false;
                e1.Visible = false;
                e2.Visible = false;
                e3.Visible = false;
               
                VGVM.CloseAll();

                if (isOver)
                {
                    thread = new Thread(PlayVideo);
                    thread.IsBackground = true;
                    thread.Start();
                }
                else
                {
                    MessageBox.Show("加载中...");
                }
            }
        }
        private void PlayVideo()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                isOver = false;
                for (int i = 0; i < list.Count; i++)
                {
                    string track = list[i].ToString();
                    string[] arr = track.Split(',');
                    if (i == 0)
                    {
                        w0.Visible = true;
                        e0.Visible = true;
                        tb0.Text = arr[0];
                        VGVM.w0 = HC_SDKFactory.factory(HCUserID, int.Parse(arr[1]) + dwDCStartChannelNum, w0.Handle, 0);
                        VGVM.e0 = HC_SDKFactory.factory(HCUserID, int.Parse(arr[2]) + dwDCStartChannelNum, e0.Handle, 0);
                        VGVM.w0.Play();
                        VGVM.e0.Play();

                        w0.Tag = arr[1];
                        e0.Tag = arr[2];
                    }
                    if (i == 1)
                    {
                        w1.Visible = true;
                        e1.Visible = true;
                        tb1.Text = arr[0];
                        VGVM.w1 = HC_SDKFactory.factory(HCUserID, int.Parse(arr[1]) + dwDCStartChannelNum, w1.Handle, 0);
                        VGVM.e1 = HC_SDKFactory.factory(HCUserID, int.Parse(arr[2]) + dwDCStartChannelNum, e1.Handle, 0);
                        VGVM.w1.Play();
                        VGVM.e1.Play();

                        w1.Tag = arr[1];
                        e1.Tag = arr[2];
                    }
                    if (i == 2)
                    {
                        w2.Visible = true;
                        e2.Visible = true;
                        tb2.Text = arr[0];
                        VGVM.w2 = HC_SDKFactory.factory(HCUserID, int.Parse(arr[1]) + dwDCStartChannelNum, w2.Handle, 0);
                        VGVM.e2 = HC_SDKFactory.factory(HCUserID, int.Parse(arr[2]) + dwDCStartChannelNum, e2.Handle, 0);
                        VGVM.w2.Play();
                        VGVM.e2.Play();

                        w2.Tag = arr[1];
                        e2.Tag = arr[2];
                    }
                    if (i == 3)
                    {
                        w3.Visible = true;
                        e3.Visible = true;
                        tb3.Text = arr[0];
                        VGVM.w3 = HC_SDKFactory.factory(HCUserID, int.Parse(arr[1]) + dwDCStartChannelNum, w3.Handle, 0);
                        VGVM.e3 = HC_SDKFactory.factory(HCUserID, int.Parse(arr[2]) + dwDCStartChannelNum, e3.Handle, 0);
                        VGVM.w3.Play();
                        VGVM.e3.Play();

                        w3.Tag = arr[1];
                        e3.Tag = arr[2];
                    }
                }
                isOver = true;
                thread.Abort();
            }));
        }
        public void PlayedZoom(IHCControl HCControl)
        {
            HCControl.ZoomOut(175, 140, 350, 280);
        }
        /// <summary>
        /// 弹出视频窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WestThroatVideo_Click(object sender, EventArgs e)
        {
            if (HCUserID != -1)
            {
                VideoZoomInWindow vziw = new VideoZoomInWindow();
                vziw.HCUserID = this.HCUserID;
                switch (((System.Windows.Forms.Control)sender).Tag.ToString())
                {
                    case "WestThroatVideo":
                        vziw.Channle = iChannelNum[0];
                        break;
                    case "EastThroatVideo":
                        vziw.Channle = iChannelNum[1];
                        break;
                }
                vziw.Show();
            }
        }

        private void w0_Click(object sender, EventArgs e)
        {
            if (HCUserID != -1)
            {
                VideoZoomInWindow vziw = new VideoZoomInWindow();
                vziw.HCUserID = this.HCUserID;
                //switch (((System.Windows.Forms.Control)sender).Tag.ToString())
                //{
                //    case "WestThroatVideo":
                //        vziw.Channle = iChannelNum[0];
                //        break;
                //    case "EastThroatVideo":
                //        vziw.Channle = iChannelNum[1];
                //        break;
                //}
                vziw.Channle = dwDCStartChannelNum + int.Parse(((System.Windows.Forms.Control)sender).Tag.ToString());
                vziw.Show();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VGVM.CloseAll();
            VGVM.w0 = HC_SDKFactory.factory(HCUserID, 31 + dwDCStartChannelNum, w0.Handle, 0);
            w0.Text = "结束";
            VGVM.w0.Play();
        }
    }
}
