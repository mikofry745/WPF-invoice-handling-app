using ObsługaFaktur.Services.Navigation;
using System;
using ObsługaFaktur.Models;
using ObsługaFaktur.Stores;
using ObsługaFaktur.ViewModels;

namespace ObsługaFaktur.Commands
{
    public class NavigateToEditProductCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private readonly ProductStore _productStore;

        public NavigateToEditProductCommand(INavigationService navigationService, ProductStore productStore)
        {
            _navigationService = navigationService;
            _productStore = productStore;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is not ProductViewModel productViewModel)
            {
                return;
            }

            Product product = new Product(productViewModel.Name, productViewModel.VatPercent,
                (Unit)Enum.Parse(typeof(Unit), productViewModel.Unit), productViewModel.NetPrice, productViewModel.Comment);

            _productStore.CurrentEditedProduct = product;
            _productStore.IsEditMode = true;
            _navigationService.Navigate();
        }
    }
}
