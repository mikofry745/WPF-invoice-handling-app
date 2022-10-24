using ObsługaFaktur.Models;
using ObsługaFaktur.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ObsługaFaktur.Commands
{
    public class LoadProductsToInvoiceCommand : AsyncCommandBase
    {
        private readonly Business _business;
        private readonly InvoiceDataStore _invoiceDataStore;

        public LoadProductsToInvoiceCommand(Business business, InvoiceDataStore invoiceDataStore)
        {
            _business = business;
            _invoiceDataStore = invoiceDataStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                IEnumerable<Product>? products = await _business.GetAllProducts();

                _invoiceDataStore.LoadProducts(products);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load products", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
