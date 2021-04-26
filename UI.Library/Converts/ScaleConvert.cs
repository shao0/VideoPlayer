using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UI.Library.Converts
{
    public class ScaleConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 && values[0] is double v && values[1] is double m && values[2] is double width)
            {
                return width * v / m;
            }
            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
