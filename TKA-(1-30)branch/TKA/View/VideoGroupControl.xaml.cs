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
        private int[] iChannelNum = new int[128];
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
            //return;
            VGVM = (VideoGroupViewModel)this.DataContext;

            rb1.IsChecked = true;

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
                int defaultIndex;
                bool b = int.TryParse(ConfigurationManager.AppSettings["DefaultIndex"].ToString(), out defaultIndex);
                if (!b)
                {
                    defaultIndex = 0;
                }
                //VGVM.WestThroatVideoControl1 = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat1.Channel + dwDCStartChannelNum, WestThroatVideo1.Handle, 0);
                //VGVM.WestThroatVideoControl2 = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat2.Channel + dwDCStartChannelNum, WestThroatVideo2.Handle, 0);
                VGVM.WestThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat3.Channel + dwDCStartChannelNum, WestThroatVideo.Handle, 0);
                VGVM.EastThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat4.Channel + dwDCStartChannelNum, EastThroatVideo.Handle, 0);

                VGVM.WestTrackVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[defaultIndex].West.Channel + dwDCStartChannelNum, WestTrackVideo.Handle, 0);
                VGVM.WestTrackZoomVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[defaultIndex].West.Channel + dwDCStartChannelNum, WestTrackZoomVideo.Handle, 0);
                VGVM.EastTrackZoomVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[defaultIndex].East.Channel + dwDCStartChannelNum, EastTrackZoomVideo.Handle, 0);
                VGVM.EastTrackVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[defaultIndex].East.Channel + dwDCStartChannelNum, EastTrackVideo.Handle, 0);

                //VGVM.EastThroatVideoControl1 = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat1.Channel + dwDCStartChannelNum, EastThroatVideo1.Handle, 0);
                //VGVM.EastThroatVideoControl2 = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat2.Channel + dwDCStartChannelNum, EastThroatVideo2.Handle, 0);
                //VGVM.EastThroatVideoControl3 = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat3.Channel + dwDCStartChannelNum, EastThroatVideo3.Handle, 0);
                //VGVM.EastThroatVideoControl4 = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat4.Channel + dwDCStartChannelNum, EastThroatVideo4.Handle, 0);
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    VGVM.PlayAll(WestThroatVideo, EastThroatVideo);
                });

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
                //MessageBox.Show("Net NVR Failed! Code :" + HCNetSDK_X64.NET_DVR_GetLastError());
            }
            else
            {
                m_struIpParaCfgV40 = (HCNetSDK_X64.NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HCNetSDK_X64.NET_DVR_IPPARACFG_V40));
                //byte byStreamType;
                for (int i = 0; i < m_struIpParaCfgV40.dwDChanNum; i++)
                {
                    iChannelNum[i + dwAChanTotalNum] = i + (int)m_struIpParaCfgV40.dwStartDChan;
                    dwDCStartChannelNum = (int)m_struIpParaCfgV40.dwStartDChan;
                }
            }
        }
        public VideoGroupControl()
        {
            InitializeComponent();
        }
        bool isFirst = true;
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (VGVM != null)
            {
                if (!isFirst)
                {
                    VGVM.ChangeTrack(((RadioButton)sender).Tag.ToString());
                }
                isFirst = false;
            }
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
                    case "WestThroatVideo1":
                        vziw.Channle = iChannelNum[0];
                        break;
                    case "WestThroatVideo2":
                        vziw.Channle = iChannelNum[1];
                        break;
                    case "WestThroatVideo3":
                        vziw.Channle = iChannelNum[2];
                        break;
                    case "WestThroatVideo4":
                        vziw.Channle = iChannelNum[3];
                        break;
                    case "EastThroatVideo1":
                        vziw.Channle = iChannelNum[4];
                        break;
                    case "EastThroatVideo2":
                        vziw.Channle = iChannelNum[5];
                        break;
                    case "EastThroatVideo3":
                        vziw.Channle = iChannelNum[6];
                        break;
                    case "EastThroatVideo4":
                        vziw.Channle = iChannelNum[7];
                        break;
                }
                vziw.Show();
            }
        }
    }
}
