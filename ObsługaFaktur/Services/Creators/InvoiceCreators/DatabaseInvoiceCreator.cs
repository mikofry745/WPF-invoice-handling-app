using ObsługaFaktur.DbContext;
using ObsługaFaktur.Dtos;
using ObsługaFaktur.Models;
using ObsługaFaktur.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ObsługaFaktur.Services.Creators.InvoiceCreators
{
    public class DatabaseInvoiceCreator : IInvoiceCreator
    {
        private readonly ObslugaFakturDbContextFactory _dbContextFactory;

        public DatabaseInvoiceCreator(ObslugaFakturDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateInvoice(Invoice invoice)
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                InvoiceDTO invoiceDto = ToInvoiceDto(invoice);
                invoiceDto.Items = new List<InvoiceItemDTO>();

                foreach (var invoiceItem in invoice.Items)
                {
                    InvoiceItemDTO itemDto = ToInvoiceItemDto(invoiceItem);
                    itemDto.InvoiceDto = invoiceDto;
                    itemDto.InvoiceDtoId = invoiceDto.Id;
                    
                    invoiceDto.Items.Add(itemDto);
                }

                dbContext.Invoices.Add(invoiceDto);
                await dbContext.SaveChangesAsync();
            };
        }

        public async Task DeleteInvoice(Invoice invoice)
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                InvoiceDTO? invoiceToRemove = await dbContext.Invoices.SingleOrDefaultAsync(i =>
                    i.Number == invoice.Number && i.DateOfIssue == invoice.DateOfIssue && 
                    i.NameOfTheIssuer == invoice.NameOfTheIssuer &&
                    i.PaymentMethod == invoice.PaymentMethod.ToString() && i.Iban == invoice.Iban &&
                    i.BankName == invoice.BankName && i.RecipientName == invoice.Recipient.Name &&
                    i.RecipientBankName == invoice.Recipient.BankName &&
                    i.RecipientCountry == invoice.Recipient.Country &&
                    i.RecipientEmailAddress == invoice.Recipient.EmailAddress &&
                    i.RecipientIban == invoice.Recipient.Iban &&
                    i.RecipientNip == invoice.Recipient.Nip &&
                    i.RecipientPhoneNumber == invoice.Recipient.PhoneNumber &&
                    i.RecipientStreet == invoice.Recipient.Street && i.RecipientTown == invoice.Recipient.Town &&
                    i.RecipientZipCode == invoice.Recipient.ZipCode && i.IssuerName == invoice.Issuer.Name &&
                    i.IssuerBankName == invoice.Issuer.BankName && i.IssuerCountry == invoice.Issuer.Country &&
                    i.IssuerEmailAddress == invoice.Issuer.EmailAddress && i.IssuerIban == invoice.Issuer.Iban &&
                    i.IssuerNip == invoice.Issuer.Nip && i.IssuerPhoneNumber == invoice.Issuer.PhoneNumber &&
                    i.IssuerStreet == invoice.Issuer.Street && i.IssuerTown == invoice.Issuer.Town &&
                    i.IssuerZipCode == invoice.Issuer.ZipCode);

                if (invoiceToRemove != null)
                {
                    dbContext.Invoices.Attach(invoiceToRemove);
                    dbContext.Invoices.Remove(invoiceToRemove);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private static InvoiceDTO ToInvoiceDto(Invoice invoice)
        {
            return new InvoiceDTO()
            {
                Type = invoice.Type.ToString(),
                Number = invoice.Number,
                DateOfIssue = invoice.DateOfIssue,
                PaymentTerm = invoice.PaymentTerm,
                BankName = invoice.BankName,
                Iban = invoice.Iban,

                IssuerName = invoice.Issuer.Name,
                IssuerBankName = invoice.Issuer.BankName,
                IssuerCountry = invoice.Issuer.Country,
                IssuerEmailAddress = invoice.Issuer.EmailAddress,
                IssuerIban = invoice.Issuer.Iban,
                IssuerNip = invoice.Issuer.Nip,
                IssuerPhoneNumber = invoice.Issuer.PhoneNumber,
                IssuerStreet = invoice.Issuer.Street,
                IssuerTown = invoice.Issuer.Town,
                IssuerZipCode = invoice.Issuer.ZipCode,

                RecipientName = invoice.Recipient.Name,
                RecipientBankName = invoice.Recipient.BankName,
                RecipientCountry = invoice.Recipient.Country,
                RecipientEmailAddress = invoice.Recipient.EmailAddress,
                RecipientIban = invoice.Recipient.Iban,
                RecipientNip = invoice.Recipient.Nip,
                RecipientPhoneNumber = invoice.Recipient.PhoneNumber,
                RecipientStreet = invoice.Recipient.Street,
                RecipientTown = invoice.Recipient.Town,
                RecipientZipCode = invoice.Recipient.ZipCode,

                NameOfTheIssuer = invoice.NameOfTheIssuer,
                PaymentMethod = invoice.PaymentMethod.ToString(),
                PlaceOfIssue = invoice.PlaceOfIssue,
                Remarks = invoice.Remarks,
                TotalPrice = invoice.TotalPrice
            };
        }

        private static InvoiceItemDTO ToInvoiceItemDto(InvoiceItem invoiceItem)
        {
            return new InvoiceItemDTO()
            {
                Name = invoiceItem.Name,
                VatPercent = invoiceItem.VatPercent,
                Unit = invoiceItem.Unit.ToString(),
                NetPrice = invoiceItem.NetPrice,
                Comment = invoiceItem.Comment,
                Quantity = invoiceItem.Quantity
            };
        }
    }
}
