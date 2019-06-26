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
using TKA.ViewModel;
using TKA.Business;
using TKA.Model;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows.Threading;

namespace TKA.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Access access;
        MainWindowViewModel mainWindowViewModel;
        public MainWindow()
        {
            InitializeComponent();
            mainWindowViewModel = new MainWindowViewModel();
            this.DataContext = mainWindowViewModel;

            PLCControler.Instence.InitPLC();
            access = new Access();
            //tg_canspeek.IsChecked = true;
        }
        /// <summary>
        /// 程序退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Exit_Click(object sender, RoutedEventArgs e)
        {
            if (videogroup != null)
            {
                videogroup.CloseAll();
            }
            Environment.Exit(0);
            Application.Current.Shutdown();
        }
        /// <summary>
        /// 日志查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Log_Click(object sender, RoutedEventArgs e)
        {
            LogWindow lw = new LogWindow();
            lw.ShowDialog();
        }
        /// <summary>
        /// 海康监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_HC_Click(object sender, RoutedEventArgs e)
        {
            string Path = ConfigurationManager.AppSettings["HCPath"];
            Process.Start(Path);
        }
        /// <summary>
        /// 交接日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Roation_Click(object sender, RoutedEventArgs e)
        {
            RotationLog rw = new RotationLog();
            rw.ShowDialog();
        }
    }
}
