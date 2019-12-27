using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using TKA.Business;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TKA.View
{
    public class BoolToWarningColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            System.Windows.Media.SolidColorBrush Result;
            if ((bool)value == true)
            {
                Result = GolbalColors.Red;
            }
            else
            {
                Result = GolbalColors.Blue;
            }
            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringNullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility Result = Visibility.Visible;

            string s = value.ToString();
            if (String.IsNullOrEmpty(s))
            {
                Result = Visibility.Collapsed;
            }
            else
            {
                if (s == "hide")
                {
                    Result = Visibility.Collapsed;
                }
                else
                {
                    Result = Visibility.Visible;
                }
               
            }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility Result = Visibility.Visible;

            if ((bool)value)
            {
                Result = Visibility.Visible;
            }
            else
            {
                Result = Visibility.Hidden;
            }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ReverseBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility Result = Visibility.Visible;

            if (!(bool)value)
            {
                Result = Visibility.Visible;
            }
            else
            {
                Result = Visibility.Hidden;
            }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class DateToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                DateTime dt = (DateTime)value;
                return dt.ToString("yyyy-MM-dd");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StartDateToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                DateTime dt = (DateTime)value;
                TKA.View.LogWindow.LogStartTimeAndEndTime.StartTime = dt.ToString("yyyy-MM-dd");
                return dt.ToString("yyyy-MM-dd");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class EndDateToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                DateTime dt = (DateTime)value;
                TKA.View.LogWindow.LogStartTimeAndEndTime.EndTime = dt.AddDays(1).AddSeconds(-1).ToString("yyyy-MM-dd");
                return dt.ToString("yyyy-MM-dd");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PlayIntegerToBool : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if ((int)value >= 4)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TrackColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush Result;
            switch ((int)value)
            {
                case 0:
                    Result = new SolidColorBrush(Colors.White);
                    break;
                case 1:
                    Result = GolbalColors.Green;
                    break;
                case 2:
                    Result = GolbalColors.Red;
                    break;
                default:
                    Result = new SolidColorBrush();
                    break;
            }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BreakLightColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BitmapImage Result = new BitmapImage();

            switch ((int)value)
            {
                case 0:
                    Result = new BitmapImage();
                    break;
                case 1:
                    Result = new BitmapImage(new Uri("pack://application:,,,/Style/Image/CircleLight.png"));
                    break;
                case 2:
                    Result = new BitmapImage(new Uri("pack://application:,,,/Style/Image/CircleLightGreen.png"));
                    break;
                default:
                    Result = new BitmapImage();
                    break;
            }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BreakLightTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string Result = "";

            switch ((int)value)
            {
                case 0:
                    Result = "";
                    break;
                case 1:
                    Result = "申请上脱";
                    break;
                case 2:
                    Result = "申请下脱";
                    break;
                default:
                    Result = "";
                    break;
            }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BreakLightForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush Result;
            switch ((int)value)
            {
                case 0:
                    Result = new SolidColorBrush();
                    break;
                case 1:
                    Result = GolbalColors.Red;
                    break;
                case 2:
                    Result = GolbalColors.Green;
                    break;
                default:
                    Result = new SolidColorBrush();
                    break;
            }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TrackNumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string Result = value.ToString() + "道";

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TrackMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string Result = value.ToString();
            if (Result == "Operation")
            {
                return "操作";
            }
            else if (Result == "Warning")
            {
                return "警告";
            }
            else
            {
                return "操作";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeOutToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush Result;

            if ((bool)value)
            {
                Result = GolbalColors.Red;
            }
            else
            {
                Result = new SolidColorBrush(Colors.White);
            }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// 允许，供电，撤 *灯颜色转换器
    /// </summary>
    public class LightColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush Result;

            if ((bool)value)
            {
                switch (parameter.ToString())
                {
                    case "允许":
                        Result = GolbalColors.Red;
                        break;
                    case "供电":
                        Result = GolbalColors.Blue;
                        break;
                    case "撤":
                        Result = GolbalColors.Green;
                        break;
                    default:
                        Result = new SolidColorBrush(Colors.Gray);
                        break;
                }

            }
            else
            {
                Result = new SolidColorBrush(Colors.Gray);
            }

            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class GetTwoTrackNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string Result = ((int)value).ToString("d2");
            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 获得轨道号是否被选中
    /// </summary>
    public class GetIschecked : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.ToString() == parameter.ToString())
            {
                return "#3A9BB0";
            }
            return "#6B7B80";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
