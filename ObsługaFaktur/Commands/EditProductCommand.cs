using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using ObsługaFaktur.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ObsługaFaktur.Stores;

namespace ObsługaFaktur.Commands
{
    public class EditProductCommand : AsyncCommandBase
    {
        private readonly CreateProductViewModel _editProductViewModel;
        private readonly Business _business;
        private readonly ProductStore _productStore;

        public EditProductCommand(CreateProductViewModel editProductViewModel, Business business,
            INavigationService navigationBackService, ProductStore productStore)
        {
            _editProductViewModel = editProductViewModel;
            _business = business;
            _productStore = productStore;

            NavigateBackCommand = new NavigateCommand(navigationBackService);

            //_editProductViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public ICommand NavigateBackCommand { get; }

        public override async Task ExecuteAsync(object? parameter)
        {
            Product product = new Product(_editProductViewModel.Name, _editProductViewModel.VatPercent,
                _editProductViewModel.Unit, _editProductViewModel.UnitNetPrice, _editProductViewModel.Comment);

            await _business.EditProduct(product, _productStore);
            NavigateBackCommand.Execute(null);
        }

        //private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == nameof(CreateProductViewModel.Name))
        //    {
        //        OnCanExecutedChanged();
        //    }
        //}
    }
}
