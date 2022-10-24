using ObsługaFaktur.Models;
using System.Threading.Tasks;

namespace ObsługaFaktur.Services.ConflictValidators.InvoiceConflictValidators
{
    public interface IInvoiceConflictValidator
    {
        Task<Invoice> GetConflictingInvoice(Invoice invoice);
    }
}
