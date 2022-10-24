using ObsługaFaktur.Commands;
using ObsługaFaktur.Services.Navigation;
using System.Windows.Input;

namespace ObsługaFaktur.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateProductListingCommand { get; }
        public ICommand NavigateCustomerListingCommand { get; }
        public ICommand NavigateInvoiceListingCommand { get; }

        public NavigationBarViewModel(INavigationService productListingNavigationService,
            INavigationService customerListingNavigationService,
            INavigationService invoiceListingNavigationService)
        {
            NavigateProductListingCommand = new NavigateCommand(productListingNavigationService);
            NavigateCustomerListingCommand = new NavigateCommand(customerListingNavigationService);
            NavigateInvoiceListingCommand = new NavigateCommand(invoiceListingNavigationService);
        }
    }
}
