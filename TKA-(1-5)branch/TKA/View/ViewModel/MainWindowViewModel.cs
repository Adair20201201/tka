using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using TKA.Helper;
using TKA.Model;
using TKA.View;
using System.Windows.Threading;
using System.Windows;

namespace TKA.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public WarningViewModel WarningVM { get; set; }
        public ObservableCollection<TrackViewModel> TracksVM { get; set; }
        public VideoGroupViewModel VideoGroupVM { get; set; }
        public FingerViewModel FingerVM { get; set; }
        public ConfigModel CM { get; set; }

        /// <summary>
        /// 指纹检测窗口
        /// </summary>
        public FingerprintWindow FingerWindow = null;
        private bool m_IndoorOperation = false;
        /// <summary>
        /// 室内操作
        /// </summary>
        public bool IndoorOperation
        {
            get { return m_IndoorOperation; }
            set
            {
                if (m_IndoorOperation != value)
                {
                    m_IndoorOperation = value;
                    RaisePropertyChanged(() => IndoorOperation);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        if (m_IndoorOperation)
                        {
                            if (FingerWindow == null)
                            {
                                FingerWindow = new FingerprintWindow();
                            }
                            FingerWindow.DataContext = FingerVM;
                            FingerWindow.Topmost = true;
                            if (!FingerWindow.IsShow)
                            {
                                FingerWindow.Show();
                                FingerWindow.IsShow = true;
                            }
                        }
                    }));
                }
            }
        }
        //private bool m_Auto = false;
        ///// <summary>
        ///// 自动
        ///// </summary>
        //public bool Auto
        //{
        //    get { return m_Auto; }
        //    set
        //    {
        //        if (m_Auto != value)
        //        {
        //            m_Auto = value;
        //            RaisePropertyChanged(() => Auto);
        //            Application.Current.Dispatcher.Invoke(new Action(() =>
        //            {
        //                if (m_Auto)
        //                {
        //                    if (FingerWindow == null)
        //                    {
        //                        FingerWindow = new FingerprintWindow();
        //                    }
        //                    FingerWindow.DataContext = FingerVM;
        //                    FingerWindow.Topmost = true;
        //                    if (!FingerWindow.IsShow)
        //                    {
        //                        FingerWindow.Show();
        //                        FingerWindow.IsShow = true;
        //                    }
        //                }
        //            }));
        //        }
        //    }
        //}
        private bool m_CanSpeek = false;
        public bool CanSpeek
        {
            get { return m_CanSpeek; }
            set
            {
                if (m_CanSpeek != value)
                {
                    m_CanSpeek = value;
                    RaisePropertyChanged(() => CanSpeek);
                }
            }
        }

        public MainWindowViewModel()
        {
            VideoGroupVM = new VideoGroupViewModel();
            VideoGroupVM.TV = new TrackVideo();
            VideoGroupVM.TV = LoadCameraConfig();

            CM = LoadTrackModel();

            WarningVM = new WarningViewModel(CM);
            FingerVM = new FingerViewModel();

            TracksVM = new ObservableCollection<TrackViewModel>();
            for (int i = 0; i < CM.LTM.Count; i++)
            {
                TracksVM.Add(new TrackViewModel(VideoGroupVM)
                {
                    TrackNum = int.Parse(CM.LTM[i].TrackID)
                });
            }
        }

        public TrackVideo LoadCameraConfig()
        {
            return (TrackVideo)XMLHelper.Read("TrackCameraConfig.xml", typeof(TrackVideo));
        }

        public ConfigModel LoadTrackModel()
        {
            return (ConfigModel)XMLHelper.Read("TrackModelConfig.xml", typeof(ConfigModel));
        }
    }
}
