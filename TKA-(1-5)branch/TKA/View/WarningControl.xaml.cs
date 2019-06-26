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

namespace TKA.View
{
    /// <summary>
    /// WarningControl.xaml 的交互逻辑
    /// </summary>
    public partial class WarningControl : UserControl
    {
        public WarningControl()
        {
            InitializeComponent();
        }

        public bool IsWarning
        {
            get { return (bool)GetValue(IsWarningProperty); }
            set { SetValue(IsWarningProperty, value); }
        }
        public static readonly DependencyProperty IsWarningProperty =
            DependencyProperty.Register("IsWarning", typeof(bool), typeof(WarningControl), new UIPropertyMetadata(false));

        
    }
}