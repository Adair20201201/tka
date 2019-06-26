using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TKA.Model;
using TKA.ViewModel;

namespace TKA.View.ViewModel
{
    public class VideoZoomInWindowViewModel : ViewModelBase
    {
        public VideoZoomInWindowViewModel()
        {

        }
        public IHCControl ZoomInVideoControl;

        public TrackVideo TV { get; set; }

        public ICommand OpenWindowCommand { get { return new BaseCommand(OnOpenWindow); } }

        private void OnOpenWindow(object para)
        {
            string commandParameter = para.ToString();
            switch (commandParameter)
            {
                case "设置":
                    SetPitchRotateWindow pitchrotateWindow = new SetPitchRotateWindow();
                    pitchrotateWindow.Show();
                    break;
                case "历史回放":
                    SelectReplayDateTimeWindow selectReplayDateTimeWindow = new SelectReplayDateTimeWindow();
                    selectReplayDateTimeWindow.Show();
                    break;
            }

        }
    }
}
