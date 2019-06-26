using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TKA.Model;
using TKA.Business;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Windows;

namespace TKA.ViewModel
{
    public class VideoGroupViewModel : ViewModelBase
    {
        private HCNetSDK_X64.NET_DVR_IPPARACFG_V40 m_struIpParaCfgV40;
        private int dwDCStartChannelNum = 0;
        public IHCControl w0,w1, w2, w3,w4,w5,w6,w7, e0,e1,e2,e3,e4,e5,e6,e7, wt0, wt1, wt2, wt3, et0, et1, et2, et3;
                        
                       
                         
                         
                        

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

        private string m_TrackNum = "01道";
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

        public void PlayAll()
        {
            w0.Play();
            w1.Play();

            w2.Play();
            
            w3.Play();

            e0.Play();
            e1.Play();
        }
        /// <summary>
        /// 关闭所有的视频
        /// </summary>
        public void CloseAll()
        {
            if (w0 != null)
            {
                w0.Stop();
            }
            if (w1 != null)
            {
                w1.Stop();
            }
            if (w2 != null)
            {
                w2.Stop();
            }
            if (w3 != null)
            {
                w3.Stop();
            }
            if (w4 != null)
            {
                w4.Stop();
            }
            if (w5 != null)
            {
                w5.Stop();
            }
            if (w6 != null)
            {
                w6.Stop();
            }
            if (w7 != null)
            {
                w7.Stop();
            }
            if (e0 != null)
            {
                e0.Stop();
            }
            if (e1 != null)
            {
                e1.Stop();
            }
            if (e2 != null)
            {
                e2.Stop();
            }
            if (e3 != null)
            {
                e3.Stop();
            }
            if (e4 != null)
            {
                e4.Stop();
            }
            if (e5 != null)
            {
                e5.Stop();
            }
            if (e6 != null)
            {
                e6.Stop();
            }
            if (e7 != null)
            {
                e7.Stop();
            }
        }

        public void ChangeTrack(int TrackNum)
        {
            InfoIPChannel();
            //if (WestTrackVideoControl != null && w2 != null && w3 != null && e0 != null)
            //{
            //    TwoCamera tc = TV.TakeCameraConfigByTrackNum(TrackNum);

            //    WestTrackVideoControl.ChangePlay(tc.West.Channel + dwDCStartChannelNum);
            //    w2.TrackNum = TrackNum;
            //    w2.ChangePlay(tc.West.Channel + dwDCStartChannelNum);

            //    w3.TrackNum = TrackNum;
            //    w3.ChangePlay(tc.East.Channel + dwDCStartChannelNum);
            //    e0.ChangePlay(tc.East.Channel + dwDCStartChannelNum);

            //    this.TrackNum = TrackNum.ToString("d2") + "道";
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
                MessageBox.Show("Net NVR Failed! Code :" + HCNetSDK_X64.NET_DVR_GetLastError());
            }
            else
            {
                m_struIpParaCfgV40 = (HCNetSDK_X64.NET_DVR_IPPARACFG_V40)Marshal.PtrToStructure(ptrIpParaCfgV40, typeof(HCNetSDK_X64.NET_DVR_IPPARACFG_V40));
                dwDCStartChannelNum = (int)m_struIpParaCfgV40.dwStartDChan;
            }
        }
    }
}
