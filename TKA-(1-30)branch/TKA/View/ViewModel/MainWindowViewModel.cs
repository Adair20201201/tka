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
        public FingerprintWindow FingerWindow;

        private bool m_Scene=false;
        /// <summary>
        /// 现场
        /// </summary>
        public bool Scene
        {
            get { return m_Scene; }
            set
            {
                if (m_Scene != value)
                {
                    m_Scene = value;
                    RaisePropertyChanged(() => Scene);
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        if (m_Scene)
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
                    TrackNum = CM.LTM[i].TrackID
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
