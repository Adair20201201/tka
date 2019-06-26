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
        private int[] iChannelNum = new int[96];
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
                VGVM.WestThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat.Channel + dwDCStartChannelNum, WestThroatVideo.Handle, 0);
                VGVM.WestTrackVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[4].West.Channel + dwDCStartChannelNum, WestTrackVideo.Handle, 0);
                VGVM.WestTrackZoomVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[4].West.Channel + dwDCStartChannelNum, WestTrackZoomVideo.Handle, 0);
                VGVM.EastTrackZoomVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[4].East.Channel + dwDCStartChannelNum, EastTrackZoomVideo.Handle, 0);
                VGVM.EastTrackVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[4].East.Channel + dwDCStartChannelNum, EastTrackVideo.Handle, 0);
                VGVM.EastThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat.Channel + dwDCStartChannelNum, EastThroatVideo.Handle, 0);
                VGVM.PlayAll();
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
        //private void UserControl_Loaded(object sender, RoutedEventArgs e)
        //{
        //    //return;
        //    VGVM = (VideoGroupViewModel)this.DataContext;

        //    rb1.IsChecked = true;

        //    string HCIP = ConfigurationManager.AppSettings["HCIP"];
        //    ushort HCPort = Convert.ToUInt16(ConfigurationManager.AppSettings["HCPort"]);
        //    string HCUserName = ConfigurationManager.AppSettings["HCUserName"];
        //    string HCPassWord = ConfigurationManager.AppSettings["HCPassWord"];

        //    HC_SDKFactory.DVR_NET_INIT();
        //    HCUserID = HC_SDKFactory.Login(HCIP, HCPort, HCUserName, HCPassWord);
        //    if (HCUserID != -1)
        //    {
        //        VGVM.WestThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat.Channel, WestThroatVideo.Handle, 0);
        //        VGVM.WestTrackVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].West.Channel, WestTrackVideo.Handle, 0);
        //        VGVM.WestTrackZoomVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].West.Channel, WestTrackZoomVideo.Handle, 0);
        //        VGVM.EastTrackZoomVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].East.Channel, EastTrackZoomVideo.Handle, 0);
        //        VGVM.EastTrackVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].East.Channel, EastTrackVideo.Handle, 0);
        //        VGVM.EastThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat.Channel, EastThroatVideo.Handle, 0);

        //        VGVM.PlayAll();
        //    }
        //}

        public VideoGroupControl()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (VGVM != null)
            {
                VGVM.ChangeTrack(Convert.ToInt32(((RadioButton)sender).Tag));
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
    }
}
