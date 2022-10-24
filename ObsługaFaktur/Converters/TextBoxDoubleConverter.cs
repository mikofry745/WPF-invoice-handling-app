using System;
using System.Globalization;
using System.Windows.Data;

namespace ObsługaFaktur.Converters
{
    public class TextBoxDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double number = (double)value;

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
