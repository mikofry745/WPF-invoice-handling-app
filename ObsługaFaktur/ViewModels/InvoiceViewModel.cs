using ObsługaFaktur.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ObsługaFaktur.ViewModels
{
    public class InvoiceViewModel : ViewModelBase
    {
        private readonly Invoice _invoice;

        public InvoiceViewModel(Invoice invoice)
        {
            _invoice = invoice;
        }

        public string Type => _invoice.Type.ToString();
        public string Number => _invoice.Number;
        public DateTime DateOfIssue => _invoice.DateOfIssue;
        public DateTime PaymentTerm => _invoice.PaymentTerm;
        public CustomerViewModel Issuer => new CustomerViewModel(_invoice.Issuer);
        public CustomerViewModel Recipient => new CustomerViewModel(_invoice.Recipient);
        public IList<InvoiceItemViewModel> Items => _invoice.Items.Select(i => new InvoiceItemViewModel(i)).ToList();
        public string PlaceOfIssue => _invoice.PlaceOfIssue;
        public string NameOfTheIssuer => _invoice.NameOfTheIssuer;
        public string PaymentMethod => _invoice.PaymentMethod.ToString();
        public string BankName => _invoice.BankName;
        public string Iban => _invoice.Iban;
        public string Remarks => _invoice.Remarks;
        public decimal TotalPrice => _invoice.TotalPrice;
    }
}
