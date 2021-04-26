using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UI.Library.Converts
{
    public class CornerRadiusConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1 && values[0] is double height)
            {
                return new CornerRadius(height / 2);
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
