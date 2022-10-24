using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObsługaFaktur.Stores;

namespace ObsługaFaktur.Models
{
    public class Business
    {
        private readonly ProductList _products;
        private readonly CustomerList _customers;
        private readonly InvoiceList _invoices;

        public string Name { get; set; }

        public Business(string name, ProductList products, CustomerList customers, InvoiceList invoices)
        {
            _products = products;
            _customers = customers;
            _invoices = invoices;

            Name = name;
        }

        #region Product Commands
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _products.GetAllProducts();
        }

        public async Task AddProduct(Product product)
        {
            await _products.AddProduct(product);
        }

        public async Task DeleteProduct(Product product)
        {
            await _products.DeleteProduct(product);
        }

        public async Task EditProduct(Product product, ProductStore productStore)
        {
            await _products.EditProduct(product, productStore);
        }
        #endregion

        #region Customer Commands
        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await _customers.GetALlCustomers();
        }

        public async Task AddCustomer(Customer customer)
        {
            await _customers.AddCustomer(customer);
        }

        public async Task DeleteCustomer(Customer customer)
        {
            await _customers.DeleteCustomer(customer);
        }

        public async Task EditCustomer(Customer customer, CustomerStore customerStore)
        {
            await _customers.EditCustomer(customer, customerStore);
        }
        #endregion

        #region Invoice Commands
        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            return await _invoices.GetAllInvoices();
        }

        public async Task AddInvoice(Invoice invoice)
        {
            await _invoices.AddInvoice(invoice);
        }

        public async Task DeleteInvoice(Invoice invoice)
        {
            await _invoices.DeleteInvoice(invoice);
        }
        #endregion
    }
}
