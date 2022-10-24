using System.Threading.Tasks;
using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using ObsługaFaktur.Stores;
using ObsługaFaktur.ViewModels;
using System.Windows.Input;

namespace ObsługaFaktur.Commands
{
    public class EditCustomerCommand : AsyncCommandBase
    {
        private readonly CreateCustomerViewModel _editCustomerViewModel;
        private readonly Business _business;
        private readonly CustomerStore _customerStore;

        public EditCustomerCommand(CreateCustomerViewModel editCustomerViewModel, Business business,
            INavigationService navigationBackService, CustomerStore customerStore)
        {
            _editCustomerViewModel = editCustomerViewModel;
            _business = business;
            _customerStore = customerStore;

            NavigateBackCommand = new NavigateCommand(navigationBackService);
        }

        public ICommand NavigateBackCommand { get; }

        public override async Task ExecuteAsync(object? parameter)
        {
            Customer customer = new Customer(_editCustomerViewModel.Name, _editCustomerViewModel.Street, _editCustomerViewModel.Town,
                _editCustomerViewModel.ZipCode, _editCustomerViewModel.Country, _editCustomerViewModel.PhoneNumber,
                _editCustomerViewModel.EmailAddress, _editCustomerViewModel.BankName, _editCustomerViewModel.Iban,
                _editCustomerViewModel.Nip);

            await _business.EditCustomer(customer, _customerStore);
            NavigateBackCommand.Execute(null);
        }
    }
}
