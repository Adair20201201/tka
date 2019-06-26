using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Timers;
using TKA.View;

namespace TKA.ViewModel
{
    public class FingerViewModel:ViewModelBase
    {
        public FingerViewModel()
        {
        }

        private bool m_PremFingerprintCheckSuccess = false;
        /// <summary>
        /// 主值班指纹检测通过
        /// </summary>
        public bool PremFingerprintCheckSuccess
        {
            get { return m_PremFingerprintCheckSuccess; }
            set
            {
                if (m_PremFingerprintCheckSuccess != value)
                {
                    m_PremFingerprintCheckSuccess = value;
                    if (value)
                    {
                  
                        if (m_ViceFingerprintCheckSuccess && m_PremFingerprintCheckSuccess)
                        {
                            Pass();
                        }
                    }
                    RaisePropertyChanged(() => PremFingerprintCheckSuccess);
                }
            }
        }

        private bool m_ViceFingerprintCheckSuccess = false;
        /// <summary>
        /// 副值班指纹检测通过
        /// </summary>
        public bool ViceFingerprintCheckSuccess
        {
            get { return m_ViceFingerprintCheckSuccess; }
            set
            {
                if (m_ViceFingerprintCheckSuccess != value)
                {
                    m_ViceFingerprintCheckSuccess = value;
                    if (value)
                    {           
                        if (m_ViceFingerprintCheckSuccess && m_PremFingerprintCheckSuccess)
                        {
                            Pass();
                        }
                    }
                    RaisePropertyChanged(() => ViceFingerprintCheckSuccess);
                }
            }
        }

        private bool m_CloseWindow = false;
        public bool CloseWindow
        {
            get { return m_CloseWindow; }
            set
            {
                m_CloseWindow = value;
                RaisePropertyChanged(() => CloseWindow);
            }
        }

        Timer timer;

        public void Pass()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            CloseWindow = true;
            timer.Stop();
        }
    }
}
