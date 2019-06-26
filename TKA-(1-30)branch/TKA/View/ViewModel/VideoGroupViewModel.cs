using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TKA.Model;
using TKA.Business;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Windows;
using System.Windows.Forms;

namespace TKA.ViewModel
{
    public class VideoGroupViewModel : ViewModelBase
    {
        private HCNetSDK_X64.NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40;
        private int dwDCStartChannelNum = 0;
        public IHCControl WestThroatVideoControl,
            WestTrackVideoControl, 
            WestTrackZoomVideoControl, 

            EastTrackZoomVideoControl, 
            EastTrackVideoControl, 
            EastThroatVideoControl;

        public TrackVideo TV { get; set; }

        private int m_isAllPlayed = 0;
        public int isAllPlayed
        {
            get
            {
                return m_isAllPlayed;
            }
            set
            {
                if (value != m_isAllPlayed)
                {
                    m_isAllPlayed = value;
                    RaisePropertyChanged(() => isAllPlayed);

                }
            }
        }

        private string m_TrackNum = ConfigurationManager.AppSettings["DefaultTrackNum"];
        public string TrackNum
        {
            get
            {
                return m_TrackNum;
            }
            set
            {
                if (value != m_TrackNum)
                {
                    m_TrackNum = value;
                    RaisePropertyChanged(() => TrackNum);

                }
            }
        }
        Control m_WestThroatVideo;
        Control m_EastThroatVideo;
        public void PlayAll(Control westThroatVideo, Control eastThroatVideo)
        {
            m_WestThroatVideo = westThroatVideo;
            m_EastThroatVideo = eastThroatVideo;
            //WestThroatVideoControl1.Play();
            //WestThroatVideoControl2.Play();
            WestThroatVideoControl.Play();
            EastThroatVideoControl.Play();

            WestTrackVideoControl.Played += PlayedWest;
            WestTrackVideoControl.Play();

            WestTrackZoomVideoControl.TrackNum = ConfigurationManager.AppSettings["PlayTrackNum"];//1;
            WestTrackZoomVideoControl.Played += PlayedWestZoom;
            WestTrackZoomVideoControl.Play();

            EastTrackZoomVideoControl.TrackNum = ConfigurationManager.AppSettings["PlayTrackNum"];//1;
            EastTrackZoomVideoControl.Played += PlayedEastZoom;
            EastTrackZoomVideoControl.Play();

            EastTrackVideoControl.Played += PlayedEast;
            EastTrackVideoControl.Play();


            //EastThroatVideoControl1.Play();
            //EastThroatVideoControl2.Play();
            //EastThroatVideoControl3.Play();
            //EastThroatVideoControl4.Play();
        }
        /// <summary>
        /// 关闭所有的视频
        /// </summary>
        public void CloseAll()
        {
            if (WestThroatVideoControl != null)
            {
                WestThroatVideoControl.Stop();
            }
           

            if (WestTrackVideoControl != null)
            {
                WestTrackVideoControl.Stop();
            }
            if (WestTrackZoomVideoControl != null)
            {
                WestTrackZoomVideoControl.Stop();
            }
            if (EastTrackZoomVideoControl != null)
            {
                EastTrackZoomVideoControl.Stop();
            }
            if (EastTrackVideoControl != null)
            {
                EastTrackVideoControl.Stop();

            }
           
        }
        public void PlayedWest(IHCControl HCControl)
        {
            isAllPlayed++;
        }

        public void PlayedEast(IHCControl HCControl)
        {
            isAllPlayed++;
        }

        public void PlayedWestZoom(IHCControl HCControl)
        {
            TwoCamera tc;
            tc = TV.TakeCameraConfigByTrackNum(HCControl.TrackNum);
            HCControl.ZoomOut((int)tc.West.ZoomRect.Left, (int)tc.West.ZoomRect.Top, (int)tc.West.ZoomRect.Right, (int)tc.West.ZoomRect.Bottom);
            isAllPlayed++;
        }

        public void PlayedEastZoom(IHCControl HCControl)
        {
            TwoCamera tc;
            tc = TV.TakeCameraConfigByTrackNum(HCControl.TrackNum);
            HCControl.ZoomOut((int)tc.East.ZoomRect.Left, (int)tc.East.ZoomRect.Top, (int)tc.East.ZoomRect.Right, (int)tc.East.ZoomRect.Bottom);
            isAllPlayed++;
        }

        public void ChangeTrack(string TrackNum)
        {
            InfoIPChannel();
            if (WestTrackVideoControl != null && WestTrackZoomVideoControl != null && EastTrackZoomVideoControl != null && EastTrackVideoControl != null)
            {
                TwoCamera tc = TV.TakeCameraConfigByTrackNum(TrackNum);
                if (TrackNum == "A09" || TrackNum == "A10" || TrackNum == "A11" || TrackNum == "A12" || TrackNum == "A13" || TrackNum == "A14" || TrackNum == "A15" || TrackNum == "A16")
                {
                    m_WestThroatVideo.Tag = "WestThroatVideo3";
                    m_EastThroatVideo.Tag = "WestThroatVideo4";
                    WestThroatVideoControl.ChangePlay(TV.WestThroat3.Channel + dwDCStartChannelNum);
                    EastThroatVideoControl.ChangePlay(TV.WestThroat4.Channel + dwDCStartChannelNum);
                }
                else if (TrackNum == "A01" || TrackNum == "A02" || TrackNum == "A03" || TrackNum == "A04" || TrackNum == "A05" || TrackNum == "A06" || TrackNum == "A07" || TrackNum == "A08")
                {
                    m_WestThroatVideo.Tag = "WestThroatVideo1";
                    m_EastThroatVideo.Tag = "WestThroatVideo2";
                    WestThroatVideoControl.ChangePlay(TV.WestThroat1.Channel + dwDCStartChannelNum);
                    EastThroatVideoControl.ChangePlay(TV.WestThroat2.Channel + dwDCStartChannelNum);
                }
                else if (TrackNum == "B09" || TrackNum == "B10" || TrackNum == "B11" || TrackNum == "B12" || TrackNum == "B13" || TrackNum == "B14" || TrackNum == "B15" || TrackNum == "B16")
                {
                    m_WestThroatVideo.Tag = "EastThroatVideo1";
                    m_EastThroatVideo.Tag = "EastThroatVideo2";
                    WestThroatVideoControl.ChangePlay(TV.EastThroat1.Channel + dwDCStartChannelNum);
                    EastThroatVideoControl.ChangePlay(TV.EastThroat2.Channel + dwDCStartChannelNum);
                }
                else if (TrackNum == "B01" || TrackNum == "B02" || TrackNum == "B03" || TrackNum == "B04" || TrackNum == "B05" || TrackNum == "B06" || TrackNum == "B07" || TrackNum == "B08")
                {
                    m_WestThroatVideo.Tag = "EastThroatVideo3";
                    m_EastThroatVideo.Tag = "EastThroatVideo4";
                    WestThroatVideoControl.ChangePlay(TV.EastThroat3.Channel + dwDCStartChannelNum);
                    EastThroatVideoControl.ChangePlay(TV.EastThroat4.Channel + dwDCStartChannelNum);
                }

                WestTrackVideoControl.ChangePlay(tc.West.Channel + dwDCStartChannelNum);
                WestTrackZoomVideoControl.TrackNum = TrackNum;
                WestTrackZoomVideoControl.ChangePlay(tc.West.Channel + dwDCStartChannelNum);

                EastTrackZoomVideoControl.TrackNum = TrackNum;
                EastTrackZoomVideoControl.ChangePlay(tc.East.Channel + dwDCStartChannelNum);
                EastTrackVideoControl.ChangePlay(tc.East.Channel + dwDCStartChannelNum);

                this.TrackNum = TrackNum + "道";
            }
            //}
        }

        private void InfoIPChannel()
        {
            string HCIP = ConfigurationManager.AppSettings["HCIP"];
            ushort HCPort = Convert.ToUInt16(ConfigurationManager.AppSettings["HCPort"]);
            string HCUserName = ConfigurationManager.AppSettings["HCUserName"];
            string HCPassWord = ConfigurationManager.AppSettings["HCPassWord"];
            HC_SDKFactory.DVR_NET_INIT();
            int HCUserID = HC_SDKFactory.Login(HCIP, HCPort, HCUserName, HCPassWord);

            uint dwSize = (uint)Marshal.SizeOf(m_struIpParaCfgV40);
            IntPtr ptrIpParaCfgV40 = Marshal.AllocHGlobal((Int32)dwSize);
            Marshal.StructureToPtr(m_struIpParaCfgV40, ptrIpParaCfgV40, false);
            uint dwReturn = 0;
            if (!HCNetSDK_X64.NET_DVR_GetDVRConfig(HCUserID, HCNetSDK_X64.NET_DVR_GET_IPPARACFG_V40, 0, ptrIpParaCfgV40, dwSize, ref dwReturn))
            {
                //System.Windows.MessageBox.Show("Net NVR Failed! Code :" + HCNetSDK_X64.NET_DVR_GetLastError());
            }
            else
            {
                m_struIpParaCfgV40 = (HCNetSDK_X64.NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HCNetSDK_X64.NET_DVR_IPPARACFG_V40));
                dwDCStartChannelNum = (int)m_struIpParaCfgV40.dwStartDChan;
            }
        }
    }
}
