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
using TKA.View.ViewModel;

namespace TKA.View
{
    /// <summary>
    /// SetPitchRotateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SetPitchRotateWindow : Window
    {
        public SetPitchRotateWindow()
        {
            InitializeComponent();
            this.DataContext = new SetPitchRotateWindowViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
