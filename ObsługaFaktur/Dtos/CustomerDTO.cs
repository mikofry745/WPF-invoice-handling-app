using System;
using System.ComponentModel.DataAnnotations;

namespace ObsługaFaktur.Dtos
{
    public class CustomerDTO
    {
        public int Id { get; set; }
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
    }
}
