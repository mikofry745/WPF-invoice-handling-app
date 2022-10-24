using ObsługaFaktur.Models;
using ObsługaFaktur.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ObsługaFaktur.Commands
{
    public class LoadInvoicesCommand : AsyncCommandBase
    {
        private readonly Business _business;
        private readonly InvoiceListingViewModel _invoiceListingViewModel;

        public LoadInvoicesCommand(Business business, InvoiceListingViewModel invoiceListingViewModel)
        {
            _business = business;
            _invoiceListingViewModel = invoiceListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                IEnumerable<Invoice> invoices = await _business.GetAllInvoices();

                _invoiceListingViewModel.UpdateInvoices(invoices);
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to load invoices", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
