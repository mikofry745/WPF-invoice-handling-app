using ObsługaFaktur.Models;
using System;

namespace ObsługaFaktur.Stores
{
    public class InvoiceStore
    {
        private Invoice _currentlyCorrectedInvoice;

        public Invoice CurrentCorrectedInvoice
        {
            get => _currentlyCorrectedInvoice;
            set
            {
                _currentlyCorrectedInvoice = value;
                CurrentEditedCustomerChanged?.Invoke();
            }
        }

        //public bool IsEditMode { get; set; }

        public event Action CurrentEditedCustomerChanged;
    }
}
