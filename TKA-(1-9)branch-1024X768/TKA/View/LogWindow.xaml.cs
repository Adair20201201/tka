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
       
    //public class CsvOutPut
    //{
    //    public static void OutPutCsvFile(ref DataGrid DataGridName, string fileName)
    //    {
    //            Microsoft.Win32.SaveFileDialog saveDlg = new SaveFileDialog();
    //            saveDlg.Filter = "CSV文件(*.csv)|*.csv";
    //            saveDlg.FileName = fileName;
    //                string message = "";
    //                try
    //                {
    //                    StreamWriter write = new StreamWriter(saveDlg.FileName, false, Encoding.Default, 20480);
    //                    //标题行
    //                    for (int t = 0; t < 3; t++)
    //                    {
    //                        write.Write(DataGridName.Columns[t].Header.ToString() + ",");
    //                    }

    //                    write.WriteLine();
    //                    write.Flush();
    //                    write.Close();
    //                }
    //                catch (Exception)
    //                {
    //                    message = "CSV文件导出失败。" + "\n" + "文件：" + saveDlg.FileName.ToString().Trim() + "\n" + "可能处于打开状态或被其他程序中用中。";
    //                    MessageBox.Show(message, "确认");
    //                    return;
    //                }
    //                message = "CSV文件导出成功。请确认。" + "\n" + "文件位置：" + saveDlg.FileName.ToString().Trim();
    //                MessageBox.Show(message, "确认");
    //                return;
                
    //        }
    //    }



    }
}
