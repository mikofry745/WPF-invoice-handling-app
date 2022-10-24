using ObsługaFaktur.Models;
using ObsługaFaktur.ViewModels;
using System.Threading.Tasks;

namespace ObsługaFaktur.Commands
{
    public class DeleteCustomerCommand : AsyncCommandBase
    {
        private readonly Business _business;
        private readonly CustomerListingViewModel _customerListingViewModel;

        public DeleteCustomerCommand(Business business, CustomerListingViewModel customerListingViewModel)
        {
            _business = business;
            _customerListingViewModel = customerListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            CustomerViewModel? customerViewModel = (CustomerViewModel)parameter;

            if (customerViewModel != null)
            {
                Customer customer = new Customer(customerViewModel.Name, customerViewModel.Street,
                    customerViewModel.Town, customerViewModel.ZipCode, customerViewModel.Country, 
                    customerViewModel.PhoneNumber, customerViewModel.EmailAddress, customerViewModel.BankName, 
                    customerViewModel.Iban, customerViewModel.Nip);

                await _business.DeleteCustomer(customer);
                _customerListingViewModel.LoadCustomersCommand.Execute(null);
            }
        }
    }
}
