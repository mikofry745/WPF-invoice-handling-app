using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;

namespace ObsługaFaktur.Dtos
{
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime PaymentTerm { get; set; }
        public string PlaceOfIssue { get; set; }
        public string NameOfTheIssuer { get; set; }
        public string PaymentMethod { get; set; }
        public string BankName { get; set; }
        public string Iban { get; set; }
        public string Remarks { get; set; }
        public decimal TotalPrice { get; set; }

        public List<InvoiceItemDTO> Items { get; set; }

        public string IssuerName { get; set; }
        public string IssuerStreet { get; set; }
        public string IssuerTown { get; set; }
        public string IssuerZipCode { get; set; }
        public string IssuerCountry { get; set; }
        public string IssuerPhoneNumber { get; set; }
        public string IssuerEmailAddress { get; set; }
        public string IssuerBankName { get; set; }
        public string IssuerIban { get; set; }
        public string IssuerNip { get; set; }

        public string RecipientName { get; set; }
        public string RecipientStreet { get; set; }
        public string RecipientTown { get; set; }
        public string RecipientZipCode { get; set; }
        public string RecipientCountry { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string RecipientEmailAddress { get; set; }
        public string RecipientBankName { get; set; }
        public string RecipientIban { get; set; }
        public string RecipientNip { get; set; }
    }
}
