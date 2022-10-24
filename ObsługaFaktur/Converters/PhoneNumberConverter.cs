using System;
using System.Globalization;
using System.Windows.Data;
using Accessibility;

namespace ObsługaFaktur.Converters
{
    public class PhoneNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().Length == 9)
            {
                double number = System.Convert.ToDouble(value);
                return number.ToString("###-###-###");
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString().Length == 11)
            {
                string first = value.ToString().Substring(0, 3);
                string second = value.ToString().Substring(3, 3);
                string third = value.ToString().Substring(7, 3);

                return first + second + third;
            }
            else
            {
                return value;
            }
        }
    }
}
