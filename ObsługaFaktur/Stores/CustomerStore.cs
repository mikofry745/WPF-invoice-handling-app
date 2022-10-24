using ObsługaFaktur.Models;
using System;

namespace ObsługaFaktur.Stores
{
    public class CustomerStore
    {
        private Customer _currentlyEditedCustomer;

        public Customer CurrentEditedCustomer
        {
            get => _currentlyEditedCustomer;
            set
            {
                _currentlyEditedCustomer = value;
                CurrentEditedCustomerChanged?.Invoke();
            }
        }

        public bool IsEditMode { get; set; }

        public event Action CurrentEditedCustomerChanged;
    }
}
