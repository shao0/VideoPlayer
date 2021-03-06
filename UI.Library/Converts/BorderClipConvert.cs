using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace UI.Library.Converts
{
    public class BorderClipConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 && values[0] is double width && values[1] is double height && values[2] is CornerRadius radius)
            {

                if (width < double.Epsilon || height < double.Epsilon)
                {
                    return Geometry.Empty;
                }
                var clip = new RectangleGeometry(new Rect(0, 0, width, height), radius.TopLeft, radius.TopLeft);
                clip.Freeze();

                return clip;
            }

            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (value is Border border)
        //    {
        //        RectangleGeometry clip = new RectangleGeometry(new Rect(0, 0, border.ActualWidth, border.ActualHeight), border.CornerRadius.TopLeft, border.CornerRadius.TopLeft);
        //        clip.Freeze();
        //        return clip;

        //    }
        //    return DependencyProperty.UnsetValue;
        //}

        //public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
