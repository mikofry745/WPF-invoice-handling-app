using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsługaFaktur.Models;

namespace ObsługaFaktur.ViewModels
{
    public class InvoiceItemViewModel : ViewModelBase
    {
        public InvoiceItemViewModel()
        {
        }

        public InvoiceItemViewModel(InvoiceItem invoiceItem)
        {
            Name = invoiceItem.Name;
            VatPercent = invoiceItem.VatPercent;
            Unit = invoiceItem.Unit.ToString();
            NetPrice = invoiceItem.NetPrice;
            Comment = invoiceItem.Comment;
            Quantity = invoiceItem.Quantity;
        }

        public InvoiceItemViewModel(string name, double vatPercent, string unit,
            double quantity, decimal netPrice, string comment)
        {
            Name = name;
            VatPercent = vatPercent;
            Unit = unit;
            NetPrice = netPrice;
            Comment = comment;
            Quantity = quantity;
        }

        public string Name { get; set; }
        public double VatPercent { get; set; }
        public string Unit { get; set; }
        public decimal NetPrice { get; set; }
        public string Comment { get; set; }
        public double Quantity { get; set; }

        public decimal GrossPrice
        {
            get => NetPrice * VatFactor * (decimal)Quantity;
            set => NetPrice = (value / VatFactor) * (decimal)Quantity;
        }

        private decimal VatFactor => (1.0m + (decimal)VatPercent / 100m);
    }
}
