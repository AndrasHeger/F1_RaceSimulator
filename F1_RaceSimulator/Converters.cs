using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace F1_RaceSimulator
{
    public class ProggressBarToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double progressValue)
            {
                if (progressValue < 40)
                    return Brushes.Red;
                else if (progressValue < 70)
                    return Brushes.Orange;
                else
                    return Brushes.Green;
            }
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ReverseProgressBarColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double progressValue)
            {
                if (progressValue < 40)
                    return Brushes.Green;
                else if (progressValue < 70)
                    return Brushes.Orange;
                else
                    return Brushes.Red;
            }
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class RecklessToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isReckless)
            {
                return isReckless ? "Caution! This driver drives\nrecklessly, this means he is a bit\nfaster but more likely to crash"
                    : "This driver drives safe,\nhe doesn't take high risks";
            }
            return "N/A";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WeatherColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string selectedValue)
            {
                switch (value)
                {
                    case "Rain":
                        return Brushes.LightBlue;
                    case "Sunny":
                        return Brushes.LightYellow;
                    default:
                        return Brushes.LightGray;
                }
            }
            return Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TeamToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string teamName)
            {
                switch (teamName)
                {
                    case "Ferrari":
                        return Brushes.Red;
                    case "McLaren":
                        return Brushes.Orange;
                    case "Mercedes":
                        return Brushes.Gray;
                    case "Aston Martin":
                        return Brushes.DarkGreen;
                    case "Red Bull":
                        return Brushes.DarkBlue;
                    default:
                        return Brushes.Black;
                }
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
