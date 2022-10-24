using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using ObsługaFaktur.Stores;
using ObsługaFaktur.ViewModels;

namespace ObsługaFaktur.Commands
{
    public class NavigateToEditCustomerCommand : CommandBase
    {
        private readonly INavigationService _navigationService;
        private readonly CustomerStore _customerStore;

        public NavigateToEditCustomerCommand(INavigationService navigationService, CustomerStore customerStore)
        {
            _navigationService = navigationService;
            _customerStore = customerStore;
        }

        public override void Execute(object? parameter)
        {
            if (parameter is not CustomerViewModel customerViewModel)
            {
                return;
            }

            Customer customer = new Customer(customerViewModel.Name, customerViewModel.Street, customerViewModel.Town,
                customerViewModel.ZipCode, customerViewModel.Country, customerViewModel.PhoneNumber,
                customerViewModel.EmailAddress, customerViewModel.BankName, customerViewModel.Iban,
                customerViewModel.Nip);

            _customerStore.CurrentEditedCustomer = customer;
            _customerStore.IsEditMode = true;
            _navigationService.Navigate();
        }
    }
}
