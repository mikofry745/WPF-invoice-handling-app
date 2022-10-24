using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsługaFaktur.Models;
using ObsługaFaktur.ViewModels;

namespace ObsługaFaktur.Commands
{
    public class DeleteInvoiceCommand : AsyncCommandBase
    {
        private readonly Business _business;
        private readonly InvoiceListingViewModel _invoiceListingViewModel;

        public DeleteInvoiceCommand(Business business, InvoiceListingViewModel invoiceListingViewModel)
        {
            _business = business;
            _invoiceListingViewModel = invoiceListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            InvoiceViewModel? invoiceViewModel = (InvoiceViewModel)parameter;

            if (invoiceViewModel != null)
            {
                Customer issuer = new Customer(invoiceViewModel.Issuer.Name, invoiceViewModel.Issuer.Street,
                    invoiceViewModel.Issuer.Town, invoiceViewModel.Issuer.ZipCode,
                    invoiceViewModel.Issuer.Country,
                    invoiceViewModel.Issuer.PhoneNumber, invoiceViewModel.Issuer.EmailAddress,
                    invoiceViewModel.Issuer.BankName,
                    invoiceViewModel.Issuer.Iban, invoiceViewModel.Issuer.Nip);

                Customer recipient = new Customer(invoiceViewModel.Recipient.Name,
                    invoiceViewModel.Recipient.Street,
                    invoiceViewModel.Recipient.Town, invoiceViewModel.Recipient.ZipCode,
                    invoiceViewModel.Recipient.Country,
                    invoiceViewModel.Recipient.PhoneNumber, invoiceViewModel.Recipient.EmailAddress,
                    invoiceViewModel.Recipient.BankName,
                    invoiceViewModel.Recipient.Iban, invoiceViewModel.Recipient.Nip);

                List<InvoiceItem> items = invoiceViewModel.Items.Select(i => new InvoiceItem(i.Name,
                    i.VatPercent, (Unit)Enum.Parse(typeof(Unit), i.Unit), i.Quantity, i.NetPrice, i.Comment)).ToList();

                Invoice invoice = new Invoice((InvoiceType)Enum.Parse(typeof(InvoiceType), invoiceViewModel.Type), invoiceViewModel.Number, invoiceViewModel.DateOfIssue, invoiceViewModel.PaymentTerm, issuer,
                    recipient, items, invoiceViewModel.PlaceOfIssue, invoiceViewModel.NameOfTheIssuer, 
                    (PaymentMethod)Enum.Parse(typeof(PaymentMethod), invoiceViewModel.PaymentMethod), 
                    invoiceViewModel.BankName, invoiceViewModel.Iban, 
                    invoiceViewModel.Remarks, invoiceViewModel.TotalPrice);

                await _business.DeleteInvoice(invoice);
                _invoiceListingViewModel.LoadInvoicesCommand.Execute(null);
            }
        }
    }
}
