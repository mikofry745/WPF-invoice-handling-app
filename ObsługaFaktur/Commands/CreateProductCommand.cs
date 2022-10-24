using ObsługaFaktur.Exceptions;
using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using ObsługaFaktur.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ObsługaFaktur.Commands
{
    public class CreateProductCommand : AsyncCommandBase
    {
        private readonly CreateProductViewModel _createProductViewModel;
        private readonly Business _business;

        public CreateProductCommand(CreateProductViewModel productViewModel, Business business, 
            INavigationService navigationBackService)
        {
            _createProductViewModel = productViewModel;
            _business = business;

            NavigateBackCommand = new NavigateCommand(navigationBackService);
        }

        public ICommand NavigateBackCommand { get; }

        public override async Task ExecuteAsync(object? parameter)
        {
            Product product = new Product(_createProductViewModel.Name, _createProductViewModel.VatPercent,
                _createProductViewModel.Unit, _createProductViewModel.UnitNetPrice, _createProductViewModel.Comment);

            try
            {
                _createProductViewModel.ConflictErrorVisibility = false;
                _createProductViewModel.InvalidDataErrorVisibility = false;

                if (String.IsNullOrEmpty(product.Name) || String.IsNullOrEmpty(product.VatPercent.ToString())
                    || String.IsNullOrEmpty(product.Unit.ToString()) || String.IsNullOrEmpty(product.NetPrice.ToString())
                    || String.IsNullOrEmpty(product.Comment))
                {
                    throw new ArgumentNullException();
                }

                if (product.NetPrice == 0 || product.VatPercent == 0)
                {
                    throw new InvalidDataException();
                }

                await _business.AddProduct(product);
                NavigateBackCommand.Execute(null);
            }
            catch (ProductConflictException)
            {
                _createProductViewModel.ConflictErrorVisibility = true;
                _createProductViewModel.InvalidDataErrorVisibility = false;
                _createProductViewModel.NullOrEmptyDataErrorVisibility = false;
            }
            catch (ArgumentNullException)
            {
                _createProductViewModel.ConflictErrorVisibility = false;
                _createProductViewModel.InvalidDataErrorVisibility = false;
                _createProductViewModel.NullOrEmptyDataErrorVisibility = true;
            }
            catch (Exception)
            {
                _createProductViewModel.ConflictErrorVisibility = false;
                _createProductViewModel.InvalidDataErrorVisibility = true;
                _createProductViewModel.NullOrEmptyDataErrorVisibility = false;
            }
        }
    }
}
