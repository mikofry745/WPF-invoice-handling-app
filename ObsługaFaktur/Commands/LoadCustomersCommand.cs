using ObsługaFaktur.Models;
using ObsługaFaktur.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace ObsługaFaktur.Commands
{
    public class LoadCustomersCommand : AsyncCommandBase
    {
        private readonly Business _business;
        private readonly CustomerListingViewModel _customerListingViewModel;

        public LoadCustomersCommand(Business business, CustomerListingViewModel customerListingViewModel)
        {
            _business = business;
            _customerListingViewModel = customerListingViewModel;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                IEnumerable<Customer> customers = await _business.GetAllCustomers();

                _customerListingViewModel.UpdateCustomers(customers);
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to load customers", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
