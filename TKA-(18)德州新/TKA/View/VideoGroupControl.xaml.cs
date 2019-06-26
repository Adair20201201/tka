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
            //if (HC_SDKFactory.lpDeviceInfo_X64.byIPChanNum > 0)
            //{
            //    InfoIPChannel();
            //}

            if (HCUserID != -1)
            {
                VGVM.WestThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat.Channel + dwDCStartChannelNum+1, WestThroatVideo.Handle, 0);

                VGVM.WestTrackVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].West.Channel + dwDCStartChannelNum + 1, WestTrackVideo.Handle, 0);
                VGVM.WestTrackZoomVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].West.Channel + dwDCStartChannelNum + 1, WestTrackZoomVideo.Handle, 0);

                VGVM.EastTrackZoomVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].East.Channel + dwDCStartChannelNum + 1, EastTrackZoomVideo.Handle, 0);
                VGVM.EastTrackVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.Track[0].East.Channel + dwDCStartChannelNum + 1, EastTrackVideo.Handle, 0);

                VGVM.EastThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat.Channel + dwDCStartChannelNum + 1, EastThroatVideo.Handle, 0);
                VGVM.PlayAll();
            }
            //string DVRIPAddress = HCIP; //设备IP地址或者域名
            //Int16 DVRPortNumber = Convert.ToInt16(HCPort);//设备服务端口号
            //string DVRUserName = HCUserName;//设备登录用户名
            //string DVRPassword = HCPassWord;//设备登录密码

            //HCNetSDK_X64.NET_DVR_DEVICEINFO_V30 DeviceInfo = new HCNetSDK_X64.NET_DVR_DEVICEINFO_V30();

            ////登录设备 Login the device
            //HCUserID = HCNetSDK_X64.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
            //if (HCUserID < 0)
            //{
            //    uint iLastErr = HCNetSDK_X64.NET_DVR_GetLastError();

            //    MessageBox.Show("NET_DVR_Login_V30 failed, error code= " + iLastErr);
            //    return;
            //}
            //else
            //{
            //    //登录成功
            //    //MessageBox.Show("Login Success!");
            //    HCNetSDK_X64.NET_DVR_PREVIEWINFO lpPreviewInfo = new HCNetSDK_X64.NET_DVR_PREVIEWINFO();
            //    lpPreviewInfo.hPlayWnd = WestThroatVideo.Handle;//预览窗口
            //    lpPreviewInfo.lChannel = 2;//预te览的设备通道
            //    lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
            //    lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
            //    lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
            //    lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
            //    lpPreviewInfo.byProtoType = 0;
            //    lpPreviewInfo.byPreviewMode = 0;
            //    IntPtr pUser = new IntPtr();//用户数据
            //    //打开预览 Start live view 
            //    int m_lRealHandle = HCNetSDK_X64.NET_DVR_RealPlay_V40(HCUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
            //    if (m_lRealHandle < 0)
            //    {
            //        uint iLastErr = HCNetSDK_X64.NET_DVR_GetLastError();
            //        MessageBox.Show("NET_DVR_RealPlay_V40 failed, error code= " + iLastErr);
            //        return;
            //    }
            //}
        }
        private void InfoIPChannel()
        {
            uint dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(m_struIpParaCfgV40, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            if (!HCNetSDK_X64.NET_DVR_GetDVRConfig(HCUserID, HCNetSDK_X64.NET_DVR_GET_IPPARACFG_V40, 0, ptrIpParaCfgV40, dwSize, ref dwReturn))
            {
               // MessageBox.Show("Net NVR Failed! Code :" + HCNetSDK_X64.NET_DVR_GetLastError());
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
                System.Windows.Forms.Control con = ((System.Windows.Forms.Control)sender);
                switch (con.Tag.ToString())
                {
                    case "WestThroatVideo":
                        //vziw.Channle = iChannelNum[0];
                        con.Tag = "WestThroatVideo2";
                        VGVM.WestThroatVideoControl.Stop();
                        VGVM.WestThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat2.Channel + dwDCStartChannelNum+1, WestThroatVideo.Handle, 0);
                        VGVM.WestThroatVideoControl.Play();
                        break;
                    case "WestThroatVideo2":
                        //vziw.Channle = iChannelNum[0];
                        con.Tag = "WestThroatVideo";
                        VGVM.WestThroatVideoControl.Stop();
                        VGVM.WestThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.WestThroat.Channel + dwDCStartChannelNum + 1, WestThroatVideo.Handle, 0);
                        VGVM.WestThroatVideoControl.Play();
                        break;
                    case "EastThroatVideo":
                        //vziw.Channle = iChannelNum[1];
                        con.Tag = "WestThroatVideo2";
                        VGVM.EastThroatVideoControl.Stop();
                        VGVM.EastThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat2.Channel + dwDCStartChannelNum + 1, EastThroatVideo.Handle, 0);
                        VGVM.EastThroatVideoControl.Play();
                        break;
                    case "EastThroatVideo2":
                        //vziw.Channle = iChannelNum[1];
                        con.Tag = "WestThroatVideo";
                        VGVM.EastThroatVideoControl.Stop();
                        VGVM.EastThroatVideoControl = HC_SDKFactory.factory(HCUserID, VGVM.TV.EastThroat.Channel + dwDCStartChannelNum + 1, EastThroatVideo.Handle, 0);
                        VGVM.EastThroatVideoControl.Play();
                        break;
                }
                //vziw.Show();
            }
        }
    }
}
