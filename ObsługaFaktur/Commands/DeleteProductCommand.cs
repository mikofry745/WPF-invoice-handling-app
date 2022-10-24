using ObsługaFaktur.Models;
using ObsługaFaktur.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using ObsługaFaktur.Services.Navigation;

namespace ObsługaFaktur.Commands
{
    public class DeleteProductCommand : AsyncCommandBase
    {
        private readonly Business _business;
        private readonly ProductListingViewModel _productListingViewModel;

        public DeleteProductCommand(Business business, ProductListingViewModel productListingViewModel)
        {
            _business = business;
            _productListingViewModel = productListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            ProductViewModel? productViewModel = (ProductViewModel)parameter;

            if (productViewModel != null)
            {
                Product product = new Product(productViewModel.Name, productViewModel.VatPercent,
                    (Unit)Enum.Parse(typeof(Unit), productViewModel.Unit), productViewModel.NetPrice, productViewModel.Comment);

                await _business.DeleteProduct(product);
                _productListingViewModel.LoadProductsCommand.Execute(null);
            }
        }
    }
}
