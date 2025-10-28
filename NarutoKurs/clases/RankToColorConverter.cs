using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NarutoKurs.clases
{
    public class RankToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string rank)
            {
                switch (rank)
                {
                    case "Ранг-E":
                        return new SolidColorBrush(Colors.Gray);
                    case "Ранг-D":
                        return new SolidColorBrush(Colors.Brown);
                    case "Ранг-C":
                        return new SolidColorBrush(Colors.Blue);
                    case "Ранг-B":
                        return new SolidColorBrush(Colors.Red);
                    case "Ранг-A":
                        return new SolidColorBrush(Colors.Gold);
                    case "Ранг-S":
                        // Для Ранг-S используем градиент
                        LinearGradientBrush gradientBrush = new LinearGradientBrush();
                        gradientBrush.GradientStops.Add(new GradientStop(Colors.Red, 0));
                        gradientBrush.GradientStops.Add(new GradientStop(Colors.Orange, 0.2));
                        gradientBrush.GradientStops.Add(new GradientStop(Colors.Yellow, 0.4));
                        gradientBrush.GradientStops.Add(new GradientStop(Colors.Green, 0.6));
                        gradientBrush.GradientStops.Add(new GradientStop(Colors.Blue, 0.8));
                        gradientBrush.GradientStops.Add(new GradientStop(Colors.Violet, 1));
                        return gradientBrush;
                    default:
                        return new SolidColorBrush(Colors.Transparent);
                }
            }

            return new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}