using System;
using ObsługaFaktur.Models;

namespace ObsługaFaktur.Stores
{
    public class ProductStore
    {
        private Product _currentlyEditedProduct;

        public Product CurrentEditedProduct
        {
            get => _currentlyEditedProduct;
            set
            {
                _currentlyEditedProduct = value;
                CurrentEditedProductChanged?.Invoke();
            }
        }

        public bool IsEditMode { get; set; }

        public event Action CurrentEditedProductChanged;
    }
}
