using System;
using ObsługaFaktur.Commands;
using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using ObsługaFaktur.Stores;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ObsługaFaktur.ViewModels
{
    public class CreateProductViewModel : ViewModelBase
    {
        public CreateProductViewModel(INavigationService cancelNavigationService, 
            Business business, ProductStore productStore)
        {
            InitializeViewModel(productStore);

            ProductUnits = Enum.GetValues(typeof(Unit));

            SubmitCommand = new CreateProductCommand(this, business, cancelNavigationService);
            EditCommand = new EditProductCommand(this, business, cancelNavigationService, productStore);
            CancelCommand = new NavigateCommand(cancelNavigationService);
        }

        public string Name { get; set; } 
        public double VatPercent { get; set; }
        public Unit Unit { get; set; }
        public decimal UnitNetPrice { get; set; }
        public string Comment { get; set; }

        public Array ProductUnits { get; set; }
        public bool IsEditButtonVisible { get; set; }
        public bool IsCreateButtonVisible { get; set; }

        public bool ConflictErrorVisibility { get; set; } = false;
        public bool InvalidDataErrorVisibility { get; set; } = false;
        public bool NullOrEmptyDataErrorVisibility { get; set; } = false;

        public ICommand SubmitCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private void InitializeViewModel(ProductStore productStore)
        {
            if (productStore.IsEditMode)
            {
                RefillForm(productStore);
                MakeEditButtonVisible();

                productStore.IsEditMode = false;
            }
            else
            {
                MakeCreateButtonVisible();
            }
        }

        private ObservableCollection<Unit> RefillUnits()
        {
            return new ObservableCollection<Unit>
            {
                Unit.Gram,
                Unit.Sztuka,
            };
        }

        private void RefillForm(ProductStore productStore)
        {
            Name = productStore.CurrentEditedProduct.Name;
            VatPercent = productStore.CurrentEditedProduct.VatPercent;
            Unit = productStore.CurrentEditedProduct.Unit;
            UnitNetPrice = productStore.CurrentEditedProduct.NetPrice;
            Comment = productStore.CurrentEditedProduct.Comment;
        }

        private void MakeEditButtonVisible()
        {
            IsEditButtonVisible = true;
            IsCreateButtonVisible = false;
        }

        private void MakeCreateButtonVisible()
        {
            IsEditButtonVisible = false;
            IsCreateButtonVisible = true;
        }
    }
}
