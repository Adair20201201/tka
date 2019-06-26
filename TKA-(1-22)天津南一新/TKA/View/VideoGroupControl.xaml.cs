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
                VGVM.WestThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat.Channel + dwDCStartChannelNum, WestThroatVideo, 0);
                VGVM.WestTrackVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].West.Channel + dwDCStartChannelNum, WestTrackVideo, 0);
                VGVM.WestTrackZoomVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].West.Channel + dwDCStartChannelNum, WestTrackZoomVideo, 0);

                VGVM.EastTrackZoomVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].East.Channel + dwDCStartChannelNum, EastTrackZoomVideo, 0);
                VGVM.EastTrackVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].East.Channel + dwDCStartChannelNum, EastTrackVideo, 0);
                VGVM.EastThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat.Channel + dwDCStartChannelNum, EastThroatVideo, 0);
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
                //MessageBox.Show("Net NVR Failed! Code :" + HCNetSDK_X64.NET_DVR_GetLastError());
            }
            else
            {
                m_struIpParaCfgV40 = (HCNetSDK_X64.NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HCNetSDK_X64.NET_DVR_IPPARACFG_V40));
                //byte byStreamType;
                dwDCStartChannelNum = (int)m_struIpParaCfgV40.dwStartDChan;
                for (int i = 0; i < m_struIpParaCfgV40.dwDChanNum; i++)
                {
                    iChannelNum[i + dwAChanTotalNum] = i + (int)m_struIpParaCfgV40.dwStartDChan;
                }
            }
        }
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
                //VideoZoomInWindow vziw = new VideoZoomInWindow();
                //vziw.HCUserID = this.HCUserID;
                System.Windows.Forms.Control con = ((System.Windows.Forms.Control)sender);
                switch (con.Tag.ToString())
                {
                    case "WestThroatVideo":
                        con.Tag = "WestThroatVideo2";
                        //VGVM.WestThroatVideoControl.Stop();
                        //VGVM.WestThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat2.Channel + dwDCStartChannelNum, WestThroatVideo.Handle, 0);
                        //VGVM.WestThroatVideoControl.Play();
                        VGVM.WestThroatVideoControl.ChangePlay(VGVM.TV.WestThroat2.Channel + dwDCStartChannelNum);
                        break;
                    case "WestThroatVideo2":
                        con.Tag = "WestThroatVideo";
                        VGVM.WestThroatVideoControl.ChangePlay(VGVM.TV.WestThroat.Channel + dwDCStartChannelNum);
                        //VGVM.WestThroatVideoControl.Stop();
                        //VGVM.WestThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat.Channel + dwDCStartChannelNum, WestThroatVideo.Handle, 0);
                        //VGVM.WestThroatVideoControl.Play();
                        break;
                    case "EastThroatVideo":
                        con.Tag = "EastThroatVideo2";
                        VGVM.EastThroatVideoControl.ChangePlay(VGVM.TV.EastThroat2.Channel + dwDCStartChannelNum);
                        //VGVM.EastThroatVideoControl.Stop();
                        //VGVM.EastThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat2.Channel + dwDCStartChannelNum, EastThroatVideo.Handle, 0);
                        //VGVM.EastThroatVideoControl.Play();
                        break;
                    case "EastThroatVideo2":
                        con.Tag = "EastThroatVideo";
                        VGVM.EastThroatVideoControl.ChangePlay(VGVM.TV.EastThroat.Channel + dwDCStartChannelNum);
                        //VGVM.EastThroatVideoControl.Stop();
                        //VGVM.EastThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat.Channel + dwDCStartChannelNum, EastThroatVideo.Handle, 0);
                        //VGVM.EastThroatVideoControl.Play();
                        break;
                }
            }
        }
    }
}
