using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ObsługaFaktur.Commands;
using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using ObsługaFaktur.Stores;

namespace ObsługaFaktur.ViewModels
{
    public class CustomerListingViewModel : ViewModelBase
    {
        private ObservableCollection<CustomerViewModel> _customers;

        public CustomerListingViewModel(INavigationService addCustomerNavigationService, 
            Business business, CustomerStore customerStore)
        {
            CustomerStore = customerStore;
            _customers = new ObservableCollection<CustomerViewModel>();

            NavigateToCreateCustomerViewCommand = new NavigateCommand(addCustomerNavigationService);
            NavigateToEditCustomerViewCommand = new NavigateToEditCustomerCommand(addCustomerNavigationService, customerStore);
            LoadCustomersCommand = new LoadCustomersCommand(business, this);
            DeleteCommand = new DeleteCustomerCommand(business, this);

            LoadCustomersCommand.Execute(null);
        }

        public string SearchedCustomerName { get; set; }
        public CustomerStore CustomerStore { get; set; }
        public CustomerViewModel SelectedCustomer { get; set; }

        public IEnumerable<CustomerViewModel> Customers
        {
            get
            {
                if (SearchedCustomerName != "" && SearchedCustomerName != null)
                {
                    return _customers.Where(c => c.Name.Contains(SearchedCustomerName));
                }

                return _customers;
            }
            set
            {
                Customers = value;
            }
        }

        public ICommand NavigateToCreateCustomerViewCommand { get; }
        public ICommand NavigateToEditCustomerViewCommand { get; }
        public ICommand LoadCustomersCommand { get; }
        public ICommand DeleteCommand { get; }

        public void UpdateCustomers(IEnumerable<Customer> customers)
        {
            _customers.Clear();

            foreach (Customer customer in customers)
            {
                CustomerViewModel customerViewModel = new CustomerViewModel(customer);
                _customers.Add(customerViewModel);
            }
        }
    }
}
