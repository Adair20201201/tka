using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TKA.Business;
using System.Windows.Forms;

namespace TKA.View
{

    public partial class LogWindow : Window
    {
        public LogWindow()
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

        private void PopupStartcal_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PopupStart.IsOpen)
            {
                PopupStart.IsOpen = false;
            }
        }

        private void PopupEndcal_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PopupEnd.IsOpen)
            {
                PopupEnd.IsOpen = false;
            }
        }

        private void StartTimeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!PopupStart.IsOpen)
            {
                PopupStart.IsOpen = true;
            }
        }

        private void EndTimeButton_Click(object sender, RoutedEventArgs e)
        {
            if (!PopupEnd.IsOpen)
            {
                PopupEnd.IsOpen = true;
            }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            Access DataBase = new Access();
            DataTable dataSource;
            if (LogStartTimeAndEndTime.StartTime != "" && LogStartTimeAndEndTime.EndTime != "")
            {
                dataSource = DataBase.Select(LogStartTimeAndEndTime.StartTime, LogStartTimeAndEndTime.EndTime, MoldBox.SelectedItem.ToString());
            }
            else
            {
                dataSource = DataBase.Select(MoldBox.SelectedItem.ToString());
            }



            if (null != dataSource)
            {

                LogGrid.ItemsSource = dataSource.Rows;
            }
        }


        public static class LogStartTimeAndEndTime
        {
            public static string StartTime = "";
            public static string EndTime = "";
        }

        private void btn_Getout_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.Filter = "CSV文件(*.csv)|*.csv";
            saveDlg.FileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
            if (saveDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (LogGrid.ItemsSource != null)
                {
                    DataRowCollection DRC = (DataRowCollection)LogGrid.ItemsSource;
                
                    if (!File.Exists(saveDlg.FileName))
                    {
                        FileStream fs1 = new FileStream(saveDlg.FileName, FileMode.Create, FileAccess.Write);//创建写入文件 
                        StreamWriter sw = new StreamWriter(fs1, System.Text.Encoding.Default);

                        foreach (DataRow DR in DRC)
                        {
                            sw.WriteLine(DR.ItemArray[1] + "," + DR.ItemArray[2] + "," + DR.ItemArray[3]);//开始写入值
                        }

                        sw.Close();
                        fs1.Close();

                    }
                    else
                    {
                        FileStream fs = new FileStream(saveDlg.FileName, FileMode.Open, FileAccess.Write);
                        StreamWriter sr = new StreamWriter(fs, System.Text.Encoding.Default);
                        foreach (DataRow DR in DRC)
                        {
                            sr.WriteLine(DR.ItemArray[1] + "," + DR.ItemArray[2] + "," + DR.ItemArray[3]);//开始写入值
                        }
                        sr.Close();
                        fs.Close();
                    }
                }
            }
        }
    }
}
