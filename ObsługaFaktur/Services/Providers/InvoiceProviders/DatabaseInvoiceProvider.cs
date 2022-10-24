using ObsługaFaktur.DbContext;
using ObsługaFaktur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObsługaFaktur.Dtos;

namespace ObsługaFaktur.Services.Providers.InvoiceProviders
{
    public class DatabaseInvoiceProvider : IInvoiceProvider
    {
        private readonly ObslugaFakturDbContextFactory _dbContextFactory;

        public DatabaseInvoiceProvider(ObslugaFakturDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                List<InvoiceDTO> invoiceDtos = await dbContext.Invoices.Include(i => i.Items).ToListAsync();

                return invoiceDtos.Select(c => ToInvoice(c));
            }
        }

        private static Invoice ToInvoice(InvoiceDTO invoiceDto)
        {
            return new Invoice((InvoiceType)Enum.Parse(typeof(InvoiceType), invoiceDto.Type), invoiceDto.Number,
                invoiceDto.DateOfIssue, invoiceDto.PaymentTerm, new Customer(invoiceDto.IssuerName, invoiceDto.IssuerStreet,
                    invoiceDto.IssuerTown, invoiceDto.IssuerZipCode, invoiceDto.IssuerCountry, invoiceDto.IssuerPhoneNumber,
                    invoiceDto.IssuerEmailAddress, invoiceDto.IssuerBankName, invoiceDto.IssuerIban, invoiceDto.IssuerNip),
                new Customer(invoiceDto.RecipientName, invoiceDto.RecipientStreet,
                    invoiceDto.RecipientTown, invoiceDto.RecipientZipCode, invoiceDto.RecipientCountry, invoiceDto.RecipientPhoneNumber,
                    invoiceDto.RecipientEmailAddress, invoiceDto.RecipientBankName, invoiceDto.RecipientIban, invoiceDto.RecipientNip),
                invoiceDto.Items.Select(i => ToInvoiceItem(i)).ToList(),
                invoiceDto.PlaceOfIssue, invoiceDto.NameOfTheIssuer,
                (PaymentMethod)Enum.Parse(typeof(PaymentMethod), invoiceDto.PaymentMethod), 
                invoiceDto.BankName, invoiceDto.Iban, invoiceDto.Remarks, invoiceDto.TotalPrice);
        }

        private static Customer ToCustomer(CustomerDTO customerDto)
        {
            return new Customer(customerDto.Name, customerDto.Street, customerDto.Town, customerDto.ZipCode,
                customerDto.Country, customerDto.PhoneNumber, customerDto.EmailAddress, customerDto.BankName,
                customerDto.Iban, customerDto.Nip);
        }

        private static InvoiceItem ToInvoiceItem(InvoiceItemDTO invoiceItemDto)
        {
            return new InvoiceItem(invoiceItemDto.Name, invoiceItemDto.VatPercent,
                (Unit) Enum.Parse(typeof(Unit), invoiceItemDto.Unit), invoiceItemDto.Quantity,
                invoiceItemDto.NetPrice, invoiceItemDto.Comment);
        }
    }
}
