using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using TKA.Helper;
using System.Windows;
using System.Threading;
using TKA.Business;

namespace TKA
{
    public interface IHCControl
    {
        void Play();
        void ZoomOut(int zoomleft, int zoomup, int zoomright, int zoomdown);
        void ZoomIn();
        void Stop();
        void ChangePlay(int channel, int userDate = -1);
        Action<IHCControl> Played { get; set; }
        int TrackNum { get; set; }
    }

    public struct HC_SDK_CLIENTINFO
    {
        /// <summary>
        /// 通道号
        /// </summary>
        public int lChannel;
        /// <summary>
        /// 最高位(31)为0表示主码流，为1表示子，0－30位表示码流连接方式: 0：TCP方式,1：UDP方式,2：多播方式,3 - RTP方式，4-音视频分开(TCP)
        /// </summary>
        public int lLinkMode;
        /// <summary>
        /// 播放窗口的句柄,为NULL表示不播放图象
        ///     IntPtr hPlayWnd;
        /// </summary>
        public IntPtr hPlayWnd;
        /// <summary>
        /// 多播组地址
        ///     char* sMultiCastIP;
        /// </summary>
        public string sMultiCastIP;
    }

    public class HC_SDKFactory
    {
        //private static HCNetSDK_X86.NET_DVR_DEVICEINFO lpDeviceInfo;
        public static HCNetSDK_X64.NET_DVR_DEVICEINFO_V30 lpDeviceInfo_X64;

        public static IHCControl factory(int userID, int channel, System.Windows.Forms.Control control, int userDate = -1)
        {
            IHCControl ihc;
            switch (SystemHelper.GetOSBit())
            {
                case OSBit.OS64Bit:
                    ihc = new HC_SDK_X64(userID, channel, control, userDate);
                    break;
                //case OSBit.OS32Bit:
                //    ihc = new HCControl_X86(userID, channel, wndhandle, userDate);
                //    break;
                default:
                    ihc = null;
                    break;
            }
            return ihc;
        }

        public static bool DVR_NET_INIT()
        {
            return HCNetSDK_X64.NET_DVR_Init();
        }

        public static int Login(string IP, ushort Port, string Name, string Password, int time = 1)
        {
            int UserID = 0;
            for (int i = 0; i <= time; i++)
            {
                UserID = HCNetSDK_X64.NET_DVR_Login_V30(IP, Port, Name, Password, ref lpDeviceInfo_X64);
                if (UserID != -1)
                {
                    break;
                }
            }
            return UserID;
        }

    }
    public class HC_SDK_X64 : IHCControl
    {
        private int UserDate = -1;
        private int UserID = -1;
        private int ClientPort = -1;
        private int RealHandle = -1;
        private IntPtr WndHandle = IntPtr.Zero;
        private System.Windows.Forms.Control control;
        private bool IsTurnStart = false;
        public HCNetSDK_X64.REALDATACALLBACK CallBackHandle = null;
        public HCNetSDK_X64.NET_DVR_CLIENTINFO lpClientInfo;
        //public HCNetSDK_X64.NET_DVR_PREVIEWINFO lpPreviewInfo; 
        //public HCNetSDK_X64.REALDATACALLBACK RealData = null;
        private PlayCtrl_X64.tagRECT pSrcRect;

        public HC_SDK_X64(int userid, int channel, System.Windows.Forms.Control control, int userDate)
        {
            //HCNetSDK_X64.NET_DVR_SetShowMode(HCNetSDK_X64.DISPLAY_MODE.OVERLAYMODE, 0);
            //if (channel < 3)
            //{
            //    lpClientInfo.lChannel = channel;
            //    lpClientInfo.lLinkMode = 1;
            //    lpClientInfo.sMultiCastIP = "";
            //    lpClientInfo.hPlayWnd = IntPtr.Zero;
            //}
            //else
            //{
            //    lpClientInfo.lChannel = channel;
            //    lpClientInfo.lLinkMode = 1 << 31 + 1;
            //    lpClientInfo.sMultiCastIP = "";
            //    lpClientInfo.hPlayWnd = IntPtr.Zero;
            //}
            //HCNetSDK_X64.NET_DVR_PREVIEWINFO lpPreviewInfo = new HCNetSDK_X64.NET_DVR_PREVIEWINFO();
            //lpPreviewInfo.hPlayWnd = wndhandle;//预览窗口
            //lpPreviewInfo.lChannel = channel;//预te览的设备通道
            //lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
            //lpPreviewInfo.dwLinkMode = 1;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
            //lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
            //lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
            //lpPreviewInfo.byProtoType = 0;
            //lpPreviewInfo.byPreviewMode = 0;
            ////RealDataCallBack_V40
            //CallBackHandle = new HCNetSDK_X64.REALDATACALLBACK(Playback);

            //UserID = userid;
            //UserDate = userDate;
            //WndHandle = wndhandle;
            //pSrcRect.Init();


            lpClientInfo.lChannel = channel;
            lpClientInfo.lLinkMode = 1;
            lpClientInfo.sMultiCastIP = "";
            lpClientInfo.hPlayWnd = IntPtr.Zero;
            //RealDataCallBack
            CallBackHandle = new HCNetSDK_X64.REALDATACALLBACK(Playback);

            UserID = userid;
            UserDate = userDate;
            WndHandle = control.Handle;
            this.control = control;
            pSrcRect.Init();
        }
        public void Trun(string Direction)
        {
            uint m_direction = 0;
            switch (Direction)
            {
                case "Up":
                    {
                        m_direction = 21;
                        break;
                    }
                case "Down":
                    {
                        m_direction = 22;
                        break;
                    }
                case "Left":
                    {
                        m_direction = 23;
                        break;
                    }
                case "Right":
                    {
                        m_direction = 24;
                        break;
                    }
                case "Zoom":
                    {
                        m_direction = 11;
                        break;
                    }
                case "ZoomOut":
                    {
                        m_direction = 12;
                        break;
                    }
            }

            if (!IsTurnStart)
            {

                HCNetSDK_X64.NET_DVR_PTZControl_Other(UserID, lpClientInfo.lChannel, m_direction, 0);
                IsTurnStart = !IsTurnStart;
            }
            else
            {
                HCNetSDK_X64.NET_DVR_PTZControl_Other(UserID, lpClientInfo.lChannel, m_direction, 1);
                IsTurnStart = !IsTurnStart;
            }
        }
        private void Playback(int lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, IntPtr pUser)
        {
            switch (dwDataType)
            {
                case 1:     // sys head
                    RealHandle = lRealHandle;
                    PlayCtrl_X64.PlayM4_GetPort(ref ClientPort);
                    if (dwBufSize > 0)
                    {
                        PlayCtrl_X64.PlayM4_SetStreamOpenMode(ClientPort, PlayCtrl_X64.STREAME_REALTIME);
                        PlayCtrl_X64.PlayM4_OpenStream(ClientPort, pBuffer, dwBufSize, 1024 * 1024);
                        PlayCtrl_X64.PlayM4_Play(ClientPort, WndHandle);
                        PlayCtrl_X64.PlayM4_SetDisplayBuf(ClientPort, 15);
                        PlayCtrl_X64.PlayM4_SetOverlayMode(ClientPort, 0, 0);
                        Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            if (Played != null)
                            {
                                Played(this);
                            }
                        }));
                    }
                    break;
                case 2:     // video stream data
                    PlayCtrl_X64.PlayM4_InputData(ClientPort, pBuffer, dwBufSize);

                    break;
                case 3:     //  Audio Stream Data
                    PlayCtrl_X64.PlayM4_InputVideoData(ClientPort, pBuffer, dwBufSize);
                    break;
                default:
                    break;
            }
        }

        public void Play()
        {
            int i = HCNetSDK_X64.NET_DVR_RealPlay_V30(UserID, ref lpClientInfo, CallBackHandle, (IntPtr)UserDate, 1);
            //int i= HCNetSDK_X64.NET_DVR_RealPlay_V30(UserID, ref lpClientInfo, CallBackHandle, (IntPtr)UserDate,1);
            //int i = HCNetSDK_X64.NET_DVR_RealPlay_V30(UserID, ref lpClientInfo, null, (IntPtr)UserDate, 1);
            //IntPtr pUser = new IntPtr();
            //if (RealData == null)
            //{
            //    RealData = new HCNetSDK_X64.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
            //}
            //int i = HCNetSDK_X64.NET_DVR_RealPlay_V40(UserID, ref lpClientInfo, RealData, (IntPtr)UserDate);
            //HCNetSDK_X64.NET_DVR_SetPlayerBufNumber(i, 50);
            //HCNetSDK_X64.NET_DVR_ThrowBFrame(i, 10);
        }
        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
            RealHandle = lRealHandle;
            Application.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            if (Played != null)
                            {
                                Played(this);
                            }
                        }));
            //switch (dwDataType)
            //{
            //    case 1:     // sys head
            //        RealHandle = lRealHandle;
            //        PlayCtrl_X64.PlayM4_GetPort(ref ClientPort);
            //        if (dwBufSize > 0)
            //        {
            //            PlayCtrl_X64.PlayM4_SetStreamOpenMode(ClientPort, PlayCtrl_X64.STREAME_REALTIME);
            //            PlayCtrl_X64.PlayM4_OpenStream(ClientPort, pBuffer, dwBufSize, 1024 * 1024);
            //            PlayCtrl_X64.PlayM4_Play(ClientPort, WndHandle);
            //            PlayCtrl_X64.PlayM4_SetDisplayBuf(ClientPort, 15);
            //            PlayCtrl_X64.PlayM4_SetOverlayMode(ClientPort, 0, 0);
            //            Application.Current.Dispatcher.Invoke(new Action(() =>
            //            {
            //                if (Played != null)
            //                {
            //                    Played(this);
            //                }
            //            }));
            //        }
            //        break;
            //    case 2:     // video stream data
            //        PlayCtrl_X64.PlayM4_InputData(ClientPort, pBuffer, dwBufSize);

            //        break;
            //    case 3:     //  Audio Stream Data
            //        PlayCtrl_X64.PlayM4_InputVideoData(ClientPort, pBuffer, dwBufSize);
            //        break;
            //    default:
            //        break;
            //}
            //if (dwBufSize > 0)
            //{
            //    byte[] sData = new byte[dwBufSize];
            //    Marshal.Copy(pBuffer, sData, 0, (Int32)dwBufSize);

            //    string str = "实时流数据.ps";
            //    FileStream fs = new FileStream(str, FileMode.Create);
            //    int iLen = (int)dwBufSize;
            //    fs.Write(sData, 0, iLen);
            //    fs.Close();
            //}
        }
        //public delegate void MyDebugInfo(string str);
        //private Int32 m_lPort = -1;
        //private uint iLastErr = 0;
        //private string str;
        //private PlayCtrl_X64.DECCBFUN m_fDisplayFun = null;
        //private IntPtr m_ptrRealHandle;
        //public void DebugInfo(string str)
        //{
        //    if (str.Length > 0)
        //    {
        //        str += "\n";
        //        //TextBoxInfo.AppendText(str);
        //    }
        //}
        //public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser)
        //{
        //    //下面数据处理建议使用委托的方式
        //    MyDebugInfo AlarmInfo = new MyDebugInfo(DebugInfo);
        //    switch (dwDataType)
        //    {
        //        case HCNetSDK_X64.NET_DVR_SYSHEAD:     // sys head
        //            if (dwBufSize > 0)
        //            {
        //                if (m_lPort >= 0)
        //                {
        //                    return; //同一路码流不需要多次调用开流接口
        //                }

        //                //获取播放句柄 Get the port to play
        //                if (!PlayCtrl_X64.PlayM4_GetPort(ref m_lPort))
        //                {
        //                    iLastErr = PlayCtrl_X64.PlayM4_GetLastError(m_lPort);
        //                    str = "PlayM4_GetPort failed, error code= " + iLastErr;
        //                    //this.BeginInvoke(AlarmInfo, str);
        //                    break;
        //                }

        //                //设置流播放模式 Set the stream mode: real-time stream mode
        //                if (!PlayCtrl_X64.PlayM4_SetStreamOpenMode(m_lPort, PlayCtrl_X64.STREAME_REALTIME))
        //                {
        //                    iLastErr = PlayCtrl_X64.PlayM4_GetLastError(m_lPort);
        //                    str = "Set STREAME_REALTIME mode failed, error code= " + iLastErr;
        //                    //this.BeginInvoke(AlarmInfo, str);
        //                }

        //                //打开码流，送入头数据 Open stream
        //                if (!PlayCtrl_X64.PlayM4_OpenStream(m_lPort, pBuffer, dwBufSize, 2 * 1024 * 1024))
        //                {
        //                    iLastErr = PlayCtrl_X64.PlayM4_GetLastError(m_lPort);
        //                    str = "PlayM4_OpenStream failed, error code= " + iLastErr;
        //                    //this.BeginInvoke(AlarmInfo, str);
        //                    break;
        //                }


        //                //设置显示缓冲区个数 Set the display buffer number
        //                if (!PlayCtrl_X64.PlayM4_SetDisplayBuf(m_lPort, 15))
        //                {
        //                    iLastErr = PlayCtrl_X64.PlayM4_GetLastError(m_lPort);
        //                    str = "PlayM4_SetDisplayBuf failed, error code= " + iLastErr;
        //                    //this.BeginInvoke(AlarmInfo, str);
        //                }

        //                //设置显示模式 Set the display mode
        //                if (!PlayCtrl_X64.PlayM4_SetOverlayMode(m_lPort, 0, 0/* COLORREF(0)*/)) //play off screen 
        //                {
        //                    iLastErr = PlayCtrl_X64.PlayM4_GetLastError(m_lPort);
        //                    str = "PlayM4_SetOverlayMode failed, error code= " + iLastErr;
        //                    //this.BeginInvoke(AlarmInfo, str);
        //                }

        //                //设置解码回调函数，获取解码后音视频原始数据 Set callback function of decoded data
        //                m_fDisplayFun = new PlayCtrl_X64.DECCBFUN(DecCallbackFUN);
        //                if (!PlayCtrl_X64.PlayM4_SetDecCallBackEx(m_lPort, m_fDisplayFun, IntPtr.Zero, 0))
        //                {
        //                    //this.BeginInvoke(AlarmInfo, "PlayM4_SetDisplayCallBack fail");
        //                }

        //                //开始解码 Start to play                       
        //                if (!PlayCtrl_X64.PlayM4_Play(m_lPort, m_ptrRealHandle))
        //                {
        //                    iLastErr = PlayCtrl_X64.PlayM4_GetLastError(m_lPort);
        //                    str = "PlayM4_Play failed, error code= " + iLastErr;
        //                    //this.BeginInvoke(AlarmInfo, str);
        //                    break;
        //                }
        //            }
        //            break;
        //        case HCNetSDK_X64.NET_DVR_STREAMDATA:     // video stream data
        //            if (dwBufSize > 0 && m_lPort != -1)
        //            {
        //                for (int i = 0; i < 999; i++)
        //                {
        //                    //送入码流数据进行解码 Input the stream data to decode
        //                    if (!PlayCtrl_X64.PlayM4_InputData(m_lPort, pBuffer, dwBufSize))
        //                    {
        //                        iLastErr = PlayCtrl_X64.PlayM4_GetLastError(m_lPort);
        //                        str = "PlayM4_InputData failed, error code= " + iLastErr;
        //                        Thread.Sleep(2);
        //                    }
        //                    else
        //                    {
        //                        break;
        //                    }
        //                }
        //            }
        //            break;
        //        default:
        //            if (dwBufSize > 0 && m_lPort != -1)
        //            {
        //                //送入其他数据 Input the other data
        //                for (int i = 0; i < 999; i++)
        //                {
        //                    if (!PlayCtrl_X64.PlayM4_InputData(m_lPort, pBuffer, dwBufSize))
        //                    {
        //                        iLastErr = PlayCtrl_X64.PlayM4_GetLastError(m_lPort);
        //                        str = "PlayM4_InputData failed, error code= " + iLastErr;
        //                        Thread.Sleep(2);
        //                    }
        //                    else
        //                    {
        //                        break;
        //                    }
        //                }
        //            }
        //            break;
        //    }
        //}

        ////解码回调函数
        //private void DecCallbackFUN(int nPort, IntPtr pBuf, int nSize, ref PlayCtrl_X64.FRAME_INFO pFrameInfo, int nReserved1, int nReserved2)
        //{
        //    // 将pBuf解码后视频输入写入文件中（解码后YUV数据量极大，尤其是高清码流，不建议在回调函数中处理）
        //    if (pFrameInfo.nType == 3) //#define T_YV12	3
        //    {
        //        //    FileStream fs = null;
        //        //    BinaryWriter bw = null;
        //        //    try
        //        //    {
        //        //        fs = new FileStream("DecodedVideo.yuv", FileMode.Append);
        //        //        bw = new BinaryWriter(fs);
        //        //        byte[] byteBuf = new byte[nSize];
        //        //        Marshal.Copy(pBuf, byteBuf, 0, nSize);
        //        //        bw.Write(byteBuf);
        //        //        bw.Flush();
        //        //    }
        //        //    catch (System.Exception ex)
        //        //    {
        //        //        MessageBox.Show(ex.ToString());
        //        //    }
        //        //    finally
        //        //    {
        //        //        bw.Close();
        //        //        fs.Close();
        //        //    }
        //    }
        //}


        public void ZoomOut(int zoomleft, int zoomup, int zoomright, int zoomdown)
        {
            pSrcRect.left = zoomleft;
            pSrcRect.top = zoomup;
            pSrcRect.right = zoomright;
            pSrcRect.bottom = zoomdown;
            bool isSet = PlayCtrl_X64.PlayM4_SetDisplayRegion(ClientPort, 0, ref pSrcRect, (IntPtr)0, true);
        }

        public void ZoomIn()
        {
            pSrcRect.Init();
            pSrcRect.right = 352;
            pSrcRect.bottom = 288;
            PlayCtrl_X64.PlayM4_SetDisplayRegion(ClientPort, 0, ref pSrcRect, (IntPtr)0, true);
        }

        public void Stop()
        {
            PlayCtrl_X64.PlayM4_Stop(ClientPort);
            PlayCtrl_X64.PlayM4_CloseStream(ClientPort);
            PlayCtrl_X64.PlayM4_FreePort(ClientPort);
            HCNetSDK_X64.NET_DVR_StopRealPlay(RealHandle);
            this.control.Invalidate();
        }


        public void ChangePlay(int channel, int userDate = -1)
        {
            this.Stop();

            lpClientInfo.lChannel = channel;
            UserDate = userDate;
            ClientPort = -1;
            RealHandle = -1;
            pSrcRect.Init();

            this.Play();
        }


        public Action<IHCControl> Played
        {
            get;
            set;
        }


        public int TrackNum
        {
            get;
            set;
        }
    }
}