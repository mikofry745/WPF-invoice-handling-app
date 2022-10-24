using ObsługaFaktur.Models;
using ObsługaFaktur.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ObsługaFaktur.Commands
{
    public class LoadProductsCommand : AsyncCommandBase
    {
        private readonly Business _business;
        private readonly ProductListingViewModel _productListingViewModel;

        public LoadProductsCommand(Business business, ProductListingViewModel productListingViewModel)
        {
            _business = business;
            _productListingViewModel = productListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                IEnumerable<Product> products = await _business.GetAllProducts();

                _productListingViewModel.UpdateProducts(products);
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to load products", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
