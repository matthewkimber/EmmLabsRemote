using System;
using System.Globalization;
using System.Windows.Data;

namespace Pre2
{
    public class IntegerToDoubleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (int)value;
            var result = System.Convert.ToDouble(val);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = (double)value;
            var result = System.Convert.ToInt32(val);

            return result;
        }
    }
}
