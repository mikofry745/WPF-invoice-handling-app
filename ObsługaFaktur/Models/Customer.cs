namespace ObsługaFaktur.Models
{
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(string name, string street, string town, string zipCode, 
            string country, string phoneNumber, string emailAddress, string bankName, 
            string iban, string nip)
        {
            Name = name;
            Street = street;
            Town = town;
            ZipCode = zipCode;
            Country = country;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            BankName = bankName;
            Iban = iban;
            Nip = nip;
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

        public bool Conflicts(Customer customer)
        {
            if (customer.Name != Name && customer.PhoneNumber != PhoneNumber && customer.EmailAddress != EmailAddress
                && customer.Iban != Iban && customer.Nip != Nip)
            {
                return false;
            }

            return true;
        }
    }
}

