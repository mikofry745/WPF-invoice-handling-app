using ObsługaFaktur.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObsługaFaktur.Services.Providers.InvoiceProviders
{
    public interface IInvoiceProvider
    {
        Task<IEnumerable<Invoice>> GetAllInvoices();
    }
}
