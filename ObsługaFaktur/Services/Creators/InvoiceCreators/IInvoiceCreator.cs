using ObsługaFaktur.Models;
using ObsługaFaktur.Stores;
using System.Threading.Tasks;

namespace ObsługaFaktur.Services.Creators.InvoiceCreators
{
    public interface IInvoiceCreator
    {
        Task CreateInvoice(Invoice invoice);
        Task DeleteInvoice(Invoice invoice);
    }
}
