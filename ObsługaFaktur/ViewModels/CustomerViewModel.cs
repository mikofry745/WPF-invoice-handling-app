using ObsługaFaktur.Models;

namespace ObsługaFaktur.ViewModels
{
    public class CustomerViewModel : ViewModelBase
    {
        private Customer _customer;

        public CustomerViewModel(Customer customer)
        {
            _customer = customer;
        }

        public string Name => _customer.Name;
        public string Street => _customer.Street;
        public string Town => _customer.Town;
        public string ZipCode => _customer.ZipCode;
        public string Country => _customer.Country;
        public string PhoneNumber => _customer.PhoneNumber;
        public string EmailAddress => _customer.EmailAddress;
        public string BankName => _customer.BankName;
        public string Iban => _customer.Iban;
        public string Nip => _customer.Nip;
    }
}
