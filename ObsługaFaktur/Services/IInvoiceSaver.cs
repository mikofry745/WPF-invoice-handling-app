using ObsługaFaktur.ViewModels;

namespace ObsługaFaktur.Services
{
    public interface IInvoiceSaver
    {
        public void Save(InvoiceViewModel invoiceViewModel);
    }
}
