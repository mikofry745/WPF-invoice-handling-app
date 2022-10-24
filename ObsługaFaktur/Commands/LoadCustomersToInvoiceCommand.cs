using ObsługaFaktur.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using ObsługaFaktur.Stores;

namespace ObsługaFaktur.Commands
{
    public class LoadCustomersToInvoiceCommand : AsyncCommandBase
    {
        private readonly Business _business;
        private readonly InvoiceDataStore _invoiceDataStore;


        public LoadCustomersToInvoiceCommand(Business business, InvoiceDataStore invoiceDataStore)
        {
            _business = business;
            _invoiceDataStore = invoiceDataStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                IEnumerable<Customer> customers = await _business.GetAllCustomers();

                _invoiceDataStore.LoadCustomers(customers);
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to load customers", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
