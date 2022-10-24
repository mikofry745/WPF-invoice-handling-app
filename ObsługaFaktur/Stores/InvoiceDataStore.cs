using ObsługaFaktur.Models;
using System;
using System.Collections.Generic;

namespace ObsługaFaktur.Stores
{
    public class InvoiceDataStore
    {
        public event Action<IEnumerable<Product>> ProductsLoaded;
        public event Action<IEnumerable<Customer>> CustomersLoaded;

        public void LoadProducts(IEnumerable<Product> products)
        {
            ProductsLoaded?.Invoke(products);
        }

        public void LoadCustomers(IEnumerable<Customer> customers)
        {
            CustomersLoaded?.Invoke(customers);
        }
    }
}
