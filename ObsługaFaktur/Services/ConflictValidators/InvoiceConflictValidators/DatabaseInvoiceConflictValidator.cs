using Microsoft.EntityFrameworkCore;
using ObsługaFaktur.DbContext;
using ObsługaFaktur.Dtos;
using ObsługaFaktur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObsługaFaktur.Services.ConflictValidators.InvoiceConflictValidators
{
    public class DatabaseInvoiceConflictValidator : IInvoiceConflictValidator
    {
        private readonly ObslugaFakturDbContextFactory _dbContextFactory;

        public DatabaseInvoiceConflictValidator(ObslugaFakturDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Invoice> GetConflictingInvoice(Invoice invoice)
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                InvoiceDTO? invoiceDto = await dbContext.Invoices
                    .Where(i => i.Number == invoice.Number)
                    .FirstOrDefaultAsync();

                if (invoiceDto == null)
                {
                    return null;
                }

                List<InvoiceItem> items = new List<InvoiceItem>();

                Customer recipient = new Customer(invoiceDto.RecipientName, invoiceDto.RecipientStreet,
                    invoiceDto.RecipientTown, invoiceDto.RecipientZipCode, invoiceDto.RecipientCountry,
                    invoiceDto.RecipientPhoneNumber,
                    invoiceDto.RecipientEmailAddress, invoiceDto.RecipientBankName, invoiceDto.RecipientIban,
                    invoiceDto.RecipientNip);

                Customer issuer = new Customer(invoiceDto.IssuerName, invoiceDto.IssuerStreet,
                    invoiceDto.IssuerTown, invoiceDto.IssuerZipCode, invoiceDto.IssuerCountry,
                    invoiceDto.IssuerPhoneNumber,
                    invoiceDto.IssuerEmailAddress, invoiceDto.IssuerBankName, invoiceDto.IssuerIban,
                    invoiceDto.IssuerNip);

                Invoice result = new Invoice((InvoiceType)Enum.Parse(typeof(InvoiceType), invoiceDto.Type), invoiceDto.Number,
                    invoiceDto.DateOfIssue, invoiceDto.PaymentTerm, issuer, recipient, items,
                    invoiceDto.PlaceOfIssue, invoiceDto.NameOfTheIssuer,
                    (PaymentMethod)Enum.Parse(typeof(PaymentMethod), invoiceDto.PaymentMethod),
                    invoiceDto.BankName, invoiceDto.Iban, invoiceDto.Remarks, invoiceDto.TotalPrice);

                return result;
            }
        }
    }
}
