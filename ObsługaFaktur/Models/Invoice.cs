using System;
using System.Collections.Generic;

namespace ObsługaFaktur.Models
{
    public class Invoice
    {
        public Invoice()
        {
        }

        public Invoice(InvoiceType type, string number, DateTime dateOfIssue, DateTime paymentTerm,
            Customer issuer, Customer recipient, IList<InvoiceItem> items, string placeOfIssue, 
            string nameOfTheIssuer, PaymentMethod paymentMethod, string bankName, string iban, 
            string remarks, decimal totalPrice)
        {
            Type = type;
            Number = number;
            DateOfIssue = dateOfIssue;
            PaymentTerm = paymentTerm;
            Issuer = issuer;
            Recipient = recipient;
            Items = items;
            PlaceOfIssue = placeOfIssue;
            NameOfTheIssuer = nameOfTheIssuer;
            PaymentMethod = paymentMethod;
            BankName = bankName;
            Iban = iban;
            Remarks = remarks;
            TotalPrice = totalPrice;
        }

        public InvoiceType Type { get; set; }
        public string Number { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime PaymentTerm { get; set; }
        public Customer Issuer { get; set; }
        public Customer Recipient { get; set; }
        public IList<InvoiceItem> Items { get; set; }
        public string PlaceOfIssue { get; set; }
        public string NameOfTheIssuer { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string BankName { get; set; }
        public string Iban { get; set; }
        public string Remarks { get; set; }
        public decimal TotalPrice { get; set; }

        public bool Conflicts(Invoice invoice)
        {
            if (invoice.Number != Number)
            {
                return false;
            }

            return true;
        }
    }
}
