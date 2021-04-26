using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace UI.Library.Converts
{
    public class PositionConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 1 && values[0] is double height)
            {
                return new Thickness(-height, 0, 0, 0);
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
