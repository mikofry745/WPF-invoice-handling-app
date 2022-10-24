using ObsługaFaktur.Commands;
using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ObsługaFaktur.Services;

namespace ObsługaFaktur.ViewModels
{
    public class InvoiceListingViewModel : ViewModelBase
    {
        private ObservableCollection<InvoiceViewModel> _invoices;

        public InvoiceListingViewModel(INavigationService addInvoiceNavigationService, 
            Business business)
        {
            _invoices = new ObservableCollection<InvoiceViewModel>();

            NavigateToCreateInvoiceViewCommand = new NavigateCommand(addInvoiceNavigationService);
            LoadInvoicesCommand = new LoadInvoicesCommand(business, this);
            DeleteCommand = new DeleteInvoiceCommand(business, this);

            LoadInvoicesCommand.Execute(null);

            if (Invoices.Any())
            {
                SelectedInvoice = Invoices.First();
            }
        }

        public string SearchedInvoiceNumber { get; set; }
        public InvoiceViewModel SelectedInvoice { get; set; }

        public IEnumerable<InvoiceViewModel> Invoices
        {
            get
            {
                if (SearchedInvoiceNumber != "" && SearchedInvoiceNumber != null)
                {
                    return _invoices.Where(i => i.Number.Contains(SearchedInvoiceNumber));
                }

                return _invoices;
            }
            set
            {
                Invoices = value;
            }
        }

        public ICommand NavigateToCreateInvoiceViewCommand { get; }
        public ICommand NavigateToCreateCorrectionViewCommand { get; }
        public ICommand LoadInvoicesCommand { get; }
        public ICommand DeleteCommand { get; }

        public void UpdateInvoices(IEnumerable<Invoice> invoices)
        {
            _invoices.Clear();

            foreach (Invoice invoice in invoices)
            {
                InvoiceViewModel invoiceViewModel = new InvoiceViewModel(invoice);
                _invoices.Add(invoiceViewModel);
            }
        }

        public void SaveInvoiceToPdf()
        {
            IInvoiceSaver saver = new PdfInvoiceSaver();

            saver.Save(SelectedInvoice);
        }
    }
}
