using System;
using System.Globalization;
using System.Windows.Data;

namespace ObsługaFaktur.Converters
{
    public class TextBoxDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal number = (decimal)value;

            if (number == 0)
            {
                return null;
            }
            else
            {
                return number;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
