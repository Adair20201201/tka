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
using TKA.Business;

namespace TKA.View
{
    /// <summary>
    /// RotationLog.xaml 的交互逻辑
    /// </summary>
    public partial class RotationLog : Window
    {
        public RotationLog()
        {
            InitializeComponent();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Access access = new Access();
            access.InsertOperation(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),"人员:"+tb_Name.Text+"|班次:"+tb_Num.Text + "|交班记录:"+tb_Record.Text);
            this.Close();
        }
    }
}
