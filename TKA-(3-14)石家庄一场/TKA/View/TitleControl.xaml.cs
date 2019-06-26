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
using System.Windows.Threading;

namespace TKA.View
{
    /// <summary>
    /// TitleControl.xaml 的交互逻辑
    /// </summary>
    public partial class TitleControl : UserControl
    {
        public DateTime DateTime
        {
            get { return (DateTime)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }
        public static readonly DependencyProperty DateTimeProperty =
            DependencyProperty.Register("DateTime", typeof(DateTime), typeof(TitleControl), new UIPropertyMetadata(DateTime.Now));

        void dt_Tick(object sender, EventArgs e)
        {
            DateTime = DateTime.Now;
        }

        public TitleControl()
        {
            InitializeComponent();
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += new EventHandler(dt_Tick);
            dt.Start();
        }
    }
}
