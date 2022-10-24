using ObsługaFaktur.Exceptions;
using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using ObsługaFaktur.ViewModels;
using System;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ObsługaFaktur.Commands
{
    public class CreateCustomerCommand : AsyncCommandBase
    {
        private readonly CreateCustomerViewModel _createCustomerViewModel;
        private readonly Business _business;

        public CreateCustomerCommand(CreateCustomerViewModel createCustomerViewModel, Business business, 
            INavigationService navigationBackService)
        {
            _createCustomerViewModel = createCustomerViewModel;
            _business = business;

            NavigateBackCommand = new NavigateCommand(navigationBackService);

            _createCustomerViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public ICommand NavigateBackCommand { get; }

        public override async Task ExecuteAsync(object? parameter)
        {
            Customer customer = new Customer(_createCustomerViewModel.Name, _createCustomerViewModel.Street,
                _createCustomerViewModel.Town, _createCustomerViewModel.ZipCode, _createCustomerViewModel.Country,
                _createCustomerViewModel.PhoneNumber, _createCustomerViewModel.EmailAddress, _createCustomerViewModel.BankName,
                _createCustomerViewModel.Iban, _createCustomerViewModel.Nip);

            try
            {
                _createCustomerViewModel.ConflictErrorVisibility = false;
                _createCustomerViewModel.InvalidDataErrorVisibility = false;
                _createCustomerViewModel.NullOrEmptyDataErrorVisibility = false;

                if (String.IsNullOrEmpty(customer.Name) || String.IsNullOrEmpty(customer.Nip) 
                    || String.IsNullOrEmpty(customer.Country) || String.IsNullOrEmpty(customer.EmailAddress) 
                    || String.IsNullOrEmpty(customer.PhoneNumber) || String.IsNullOrEmpty(customer.BankName)
                    || String.IsNullOrEmpty(customer.Iban) || String.IsNullOrEmpty(customer.ZipCode) 
                    || String.IsNullOrEmpty(customer.Street) || String.IsNullOrEmpty(customer.Town))
                {
                    throw new ArgumentNullException();
                }

                if (!customer.PhoneNumber.All(char.IsDigit) || !customer.Nip.All(char.IsDigit)
                                                            || !customer.Iban.All(char.IsDigit))
                {
                    throw new InvalidDataException();
                }

                await _business.AddCustomer(customer);
                NavigateBackCommand.Execute(null);
            }
            catch (CustomerConflictException)
            {
                _createCustomerViewModel.ConflictErrorVisibility = true;
                _createCustomerViewModel.InvalidDataErrorVisibility = false;
                _createCustomerViewModel.NullOrEmptyDataErrorVisibility = false;
            }
            catch (ArgumentNullException)
            {
                _createCustomerViewModel.ConflictErrorVisibility = false;
                _createCustomerViewModel.InvalidDataErrorVisibility = false;
                _createCustomerViewModel.NullOrEmptyDataErrorVisibility = true;
            }
            catch (Exception)
            {
                _createCustomerViewModel.ConflictErrorVisibility = false;
                _createCustomerViewModel.InvalidDataErrorVisibility = true;
                _createCustomerViewModel.NullOrEmptyDataErrorVisibility = false;
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_createCustomerViewModel.Name))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
