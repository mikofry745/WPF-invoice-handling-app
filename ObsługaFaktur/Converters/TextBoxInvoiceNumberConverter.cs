using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ObsługaFaktur.Converters
{
    public class TextBoxInvoiceNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string invoiceNumber = (string)value;

            if (string.IsNullOrEmpty(invoiceNumber))
            {
                return "";
            }
            else if (invoiceNumber.All(char.IsDigit) && invoiceNumber.Length <= 8)
            {
                int number = int.Parse(invoiceNumber);
                string invoiceNumberWithLeadingZeros = number.ToString("D8");

                return $"#{invoiceNumberWithLeadingZeros}";
            }
            else if (invoiceNumber.All(char.IsDigit) && invoiceNumber.Length > 8 && invoiceNumber.Length <= 16)
            {
                long number = long.Parse(invoiceNumber);
                string invoiceNumberWithLeadingZeros = number.ToString("D16");

                return $"#{invoiceNumberWithLeadingZeros}";
            }
            else
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
