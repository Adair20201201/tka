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
        private static HCNetSDK_X86.NET_DVR_DEVICEINFO lpDeviceInfo;
        public static HCNetSDK_X64.NET_DVR_DEVICEINFO_V30 lpDeviceInfo_X64;

        public static IHCControl factory(int userID, int channel, IntPtr wndhandle, int userDate = -1)
        {
            IHCControl ihc;
            switch (SystemHelper.GetOSBit())
            {
                case OSBit.OS64Bit:
                    ihc = new HC_SDK_X64(userID, channel, wndhandle, userDate);
                    break;
                case OSBit.OS32Bit:
                    ihc = new HCControl_X86(userID, channel, wndhandle, userDate);
                    break;
                default:
                    ihc = null;
                    break;
            }
            return ihc;
        }

        public static bool DVR_NET_INIT()
        {
            if (SystemHelper.GetOSBit() == OSBit.OS64Bit)
            {
                return HCNetSDK_X64.NET_DVR_Init();
            }
            else
            {
                return HCNetSDK_X86.NET_DVR_Init();
            }
        }

        public static int Login(string IP, ushort Port, string Name, string Password, int time = 1)
        {
            int UserID = 0;
            if (SystemHelper.GetOSBit() == OSBit.OS64Bit)
            {
                for (int i = 0; i <= time; i++)
                {
                    UserID = HCNetSDK_X64.NET_DVR_Login_V30(IP, Port, Name, Password, out lpDeviceInfo_X64);
                    if (UserID != -1)
                    {
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= time; i++)
                {

                    UserID = HCNetSDK_X86.NET_DVR_Login(IP, Port, Name, Password, out lpDeviceInfo);
                    if (UserID != -1)
                    {
                        break;
                    }
                }
            }

            return UserID;
        }

    }

    public class HCControl_X86 : IHCControl
    {
        private int UserDate = -1;
        private int UserID = -1;
        private int ClientPort = -1;
        private int RealHandle = -1;
        private IntPtr WndHandle = IntPtr.Zero;

        private HCNetSDK_X86.RealDataCallBack_V30 CallBackHandle = null;
        private HCNetSDK_X86.NET_DVR_CLIENTINFO lpClientInfo;
        private PlayCtrl_X86.tagRECT pSrcRect;

        public HCControl_X86(int userID, int channel, IntPtr wndhandle, int userDate)
        {
            lpClientInfo.lChannel = channel;
            lpClientInfo.lLinkMode = 1;
            lpClientInfo.sMultiCastIP = "";
            lpClientInfo.hPlayWnd = IntPtr.Zero;

            CallBackHandle = new HCNetSDK_X86.RealDataCallBack_V30(Playback);

            UserID = userID;
            UserDate = userDate;
            WndHandle = wndhandle;
            pSrcRect.Init();
        }

        private void Playback(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUser)
        {
            switch (dwDataType)
            {
                case 1:     // sys head
                    RealHandle = lRealHandle;
                    PlayCtrl_X86.PlayM4_GetPort(ref ClientPort);
                    if (dwBufSize > 0)
                    {
                        PlayCtrl_X86.PlayM4_SetStreamOpenMode(ClientPort, PlayCtrl_X86.STREAME_REALTIME);
                        PlayCtrl_X86.PlayM4_OpenStream(ClientPort, ref pBuffer, dwBufSize, 1024 * 1024);
                        PlayCtrl_X86.PlayM4_Play(ClientPort, WndHandle);
                        PlayCtrl_X86.PlayM4_SetDisplayBuf(ClientPort, 15);
                        PlayCtrl_X86.PlayM4_SetOverlayMode(ClientPort, 0, 0);
                    }
                    break;
                case 2:     // video stream data
                    PlayCtrl_X86.PlayM4_InputData(ClientPort, ref pBuffer, dwBufSize);
                    break;
                case 3:     //  Audio Stream Data
                    PlayCtrl_X86.PlayM4_InputVideoData(ClientPort, ref pBuffer, dwBufSize);
                    break;
                default:
                    break;
            }
        }

        public void Play()
        {
            HCNetSDK_X86.NET_DVR_RealPlay_V30(UserID, ref lpClientInfo, CallBackHandle, UserDate, true);
        }

        public void ZoomOut(int zoomleft, int zoomup, int zoomright, int zoomdown)
        {
            pSrcRect.left = zoomleft;
            pSrcRect.top = zoomup;
            pSrcRect.right = zoomright;
            pSrcRect.bottom = zoomdown;
            PlayCtrl_X86.PlayM4_SetDisplayRegion(ClientPort, 0, ref pSrcRect, (IntPtr)0, true);
        }

        public void ZoomIn()
        {
            pSrcRect.Init();
            pSrcRect.right = 352;
            pSrcRect.bottom = 288;
            PlayCtrl_X86.PlayM4_SetDisplayRegion(ClientPort, 0, ref pSrcRect, (IntPtr)0, true);
        }

        public void Stop()
        {
            PlayCtrl_X86.PlayM4_Stop(ClientPort);
            PlayCtrl_X86.PlayM4_CloseStream(ClientPort);
            PlayCtrl_X86.PlayM4_FreePort(ClientPort);
            HCNetSDK_X86.NET_DVR_StopRealPlay(RealHandle);
        }

        private bool IsTurnStart = false;
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

                HCNetSDK_X86.NET_DVR_PTZControl_Other(UserID, lpClientInfo.lChannel, m_direction, 0);
                IsTurnStart = !IsTurnStart;
            }
            else
            {
                HCNetSDK_X86.NET_DVR_PTZControl_Other(UserID, lpClientInfo.lChannel, m_direction, 1);
                IsTurnStart = !IsTurnStart;
            }
        }
        public void ChangePlay(int channel, int userDate = -1)
        {
            throw new NotImplementedException();
        }

        Action<IHCControl> IHCControl.Played
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

    public class HC_SDK_X64 : IHCControl
    {
        private int UserDate = -1;
        private int UserID = -1;
        private int ClientPort = -1;
        private int RealHandle = -1;
        private IntPtr WndHandle = IntPtr.Zero;
        private bool IsTurnStart = false;
        public HCNetSDK_X64.RealDataCallBack_V40 CallBackHandle = null;
        public HCNetSDK_X64.NET_DVR_CLIENTINFO lpClientInfo;
        private PlayCtrl_X64.tagRECT pSrcRect;

        public HC_SDK_X64(int userid, int channel, IntPtr wndhandle, int userDate)
        {
            //HCNetSDK_X64.NET_DVR_SetShowMode(HCNetSDK_X64.DISPLAY_MODE.OVERLAYMODE, 0);
            if (channel < 3)
            {
                lpClientInfo.lChannel = channel;
                lpClientInfo.lLinkMode = 1;
                lpClientInfo.sMultiCastIP = "";
                lpClientInfo.hPlayWnd = IntPtr.Zero;
            }
            else
            {
                lpClientInfo.lChannel = channel;
                lpClientInfo.lLinkMode = 1 << 31 + 1;
                lpClientInfo.sMultiCastIP = "";
                lpClientInfo.hPlayWnd = IntPtr.Zero;
            }

            CallBackHandle = new HCNetSDK_X64.RealDataCallBack_V40(Playback);

            UserID = userid;
            UserDate = userDate;
            WndHandle = wndhandle;
            pSrcRect.Init();
        }
        public void Trun(string Direction)
        {
            uint m_direction=0;
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
        private void Playback(int lRealHandle, uint dwDataType, ref byte pBuffer, uint dwBufSize, IntPtr pUser)
        {
            switch (dwDataType)
            {
                case 1:     // sys head
                    RealHandle = lRealHandle;
                    PlayCtrl_X64.PlayM4_GetPort(ref ClientPort);
                    if (dwBufSize > 0)
                    {
                        PlayCtrl_X64.PlayM4_SetStreamOpenMode(ClientPort, PlayCtrl_X64.STREAME_REALTIME);
                        PlayCtrl_X64.PlayM4_OpenStream(ClientPort, ref pBuffer, dwBufSize, 1024 * 1024);
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
                    PlayCtrl_X64.PlayM4_InputData(ClientPort, ref pBuffer, dwBufSize);

                    break;
                case 3:     //  Audio Stream Data
                    PlayCtrl_X64.PlayM4_InputVideoData(ClientPort, ref pBuffer, dwBufSize);
                    break;
                default:
                    break;
            }
        }

        public void Play()
        {
            int i= HCNetSDK_X64.NET_DVR_RealPlay_V40(UserID, ref lpClientInfo, CallBackHandle, UserDate, true);
            //HCNetSDK_X64.NET_DVR_SetPlayerBufNumber(i, 50);
            //HCNetSDK_X64.NET_DVR_ThrowBFrame(i, 10);
        }

        public void ZoomOut(int zoomleft, int zoomup, int zoomright, int zoomdown)
        {
            pSrcRect.left = zoomleft;
            pSrcRect.top = zoomup;
            pSrcRect.right = zoomright;
            pSrcRect.bottom = zoomdown;
            PlayCtrl_X64.PlayM4_SetDisplayRegion(ClientPort, 0, ref pSrcRect, (IntPtr)0, true);
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