using ObsługaFaktur.Exceptions;
using ObsługaFaktur.Services.Providers.InvoiceProviders;
using System.Collections.Generic;
using System.Threading.Tasks;
using ObsługaFaktur.Services.ConflictValidators.InvoiceConflictValidators;
using ObsługaFaktur.Services.Creators.InvoiceCreators;

namespace ObsługaFaktur.Models
{
    public class InvoiceList
    {
        private readonly IInvoiceProvider _invoiceProvider;
        private readonly IInvoiceConflictValidator _invoiceConflictValidator;
        private readonly IInvoiceCreator _invoiceCreator;

        public InvoiceList(IInvoiceProvider invoiceProvider, IInvoiceConflictValidator invoiceConflictValidator, 
            IInvoiceCreator invoiceCreator)
        {
            _invoiceProvider = invoiceProvider;
            _invoiceConflictValidator = invoiceConflictValidator;
            _invoiceCreator = invoiceCreator;
        }

        public async Task<IEnumerable<Invoice>> GetAllInvoices()
        {
            return await _invoiceProvider.GetAllInvoices();
        }

        public async Task AddInvoice(Invoice invoice)
        {
            Invoice conflictingInvoice = await _invoiceConflictValidator.GetConflictingInvoice(invoice);

            if (conflictingInvoice != null)
            {
                throw new InvoiceConflictException(conflictingInvoice, invoice);
            }

            await _invoiceCreator.CreateInvoice(invoice);
        }

        public async Task DeleteInvoice(Invoice invoice)
        {
            await _invoiceCreator.DeleteInvoice(invoice);
        }
        
    }
}
