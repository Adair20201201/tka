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
using System.Windows.Shapes;
using System.Threading;
using System.Speech.Synthesis;
using TKA.ViewModel;
using System.Timers;

namespace TKA.View
{
    /// <summary>
    /// FingerprintWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FingerprintWindow : Window
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();
        System.Timers.Timer timer = new System.Timers.Timer();
        public FingerprintWindow()
        {
            InitializeComponent();
            IsShow = false;
            this.Loaded += new RoutedEventHandler(FingerprintWindow_Loaded);


            timer.Interval = 10000;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            synth.Speak("请录入指纹");
        }

        public bool IsShow;

        void FingerprintWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ((FingerViewModel)this.DataContext).PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(FingerprintWindow_PropertyChanged);
        }

        void FingerprintWindow_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (e.PropertyName == "CloseWindow" && ((FingerViewModel)this.DataContext).CloseWindow == true)
                {
                    IsShow = false;
                    timer.Stop();
                    timer.Elapsed -= timer_Elapsed;
                    this.Hide();
                }
            }));
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
