using System.Windows.Input;
using ObsługaFaktur.Commands;
using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using ObsługaFaktur.Stores;

namespace ObsługaFaktur.ViewModels
{
    public class CreateCustomerViewModel : ViewModelBase
    {
        public CreateCustomerViewModel(INavigationService cancelNavigationService,
            Business business, CustomerStore customerStore)
        {
            InitializeViewModel(customerStore);

            SubmitCommand = new CreateCustomerCommand(this, business, cancelNavigationService);
            EditCommand = new EditCustomerCommand(this, business, cancelNavigationService, customerStore);
            CancelCommand = new NavigateCommand(cancelNavigationService);
        }

        public string Name { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string BankName { get; set; }
        public string Iban { get; set; }
        public string Nip { get; set; }

        public bool IsEditButtonVisible { get; set; }
        public bool IsCreateButtonVisible { get; set; }

        public bool ConflictErrorVisibility { get; set; } = false;
        public bool InvalidDataErrorVisibility { get; set; } = false;
        public bool NullOrEmptyDataErrorVisibility { get; set; } = false;

        public ICommand SubmitCommand { get; }
        public ICommand EditCommand { get; set; }
        public ICommand CancelCommand { get; }

        private void InitializeViewModel(CustomerStore customerStore)
        {
            if (customerStore.IsEditMode)
            {
                RefillForm(customerStore);
                MakeEditButtonVisible();

                customerStore.IsEditMode = false;
            }
            else
            {
                MakeCreateButtonVisible();
            }
        }

        private void RefillForm(CustomerStore customerStore)
        {
            Name = customerStore.CurrentEditedCustomer.Name;
            Street = customerStore.CurrentEditedCustomer.Street;
            Town = customerStore.CurrentEditedCustomer.Town;
            ZipCode = customerStore.CurrentEditedCustomer.ZipCode;
            Country = customerStore.CurrentEditedCustomer.Country;
            PhoneNumber = customerStore.CurrentEditedCustomer.PhoneNumber;
            EmailAddress = customerStore.CurrentEditedCustomer.EmailAddress;
            BankName = customerStore.CurrentEditedCustomer.BankName;
            Iban = customerStore.CurrentEditedCustomer.Iban;
            Nip = customerStore.CurrentEditedCustomer.Nip;

        }

        private void MakeEditButtonVisible()
        {
            IsEditButtonVisible = true;
            IsCreateButtonVisible = false;
        }

        private void MakeCreateButtonVisible()
        {
            IsEditButtonVisible = false;
            IsCreateButtonVisible = true;
        }
    }
}
