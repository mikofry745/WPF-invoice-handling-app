using ObsługaFaktur.Commands;
using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ObsługaFaktur.Stores;

namespace ObsługaFaktur.ViewModels
{
    public class ProductListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ProductViewModel> _products;

        public ProductListingViewModel(INavigationService addProductNavigationService, 
            Business business, ProductStore productStore)
        {
            _products = new ObservableCollection<ProductViewModel>();
            ProductStore = productStore;

            NavigateToCreateProductViewCommand = new NavigateCommand(addProductNavigationService);
            NavigateToEditProductViewCommand = new NavigateToEditProductCommand(addProductNavigationService, productStore);
            LoadProductsCommand = new LoadProductsCommand(business, this);
            DeleteCommand = new DeleteProductCommand(business, this);

            LoadProductsCommand.Execute(null);
        }

        public string SearchedProductName { get; set; }
        public ProductStore ProductStore { get; set; }
        public ProductViewModel SelectedProduct { get; set; }

        public IEnumerable<ProductViewModel> Products
        {
            get
            {
                if (SearchedProductName != "" && SearchedProductName != null)
                {
                    return _products.Where(p => p.Name.Contains(SearchedProductName));
                }

                return _products;
            }
            set => Products = value;
        }

        public ICommand NavigateToCreateProductViewCommand { get; }
        public ICommand NavigateToEditProductViewCommand { get; }
        public ICommand LoadProductsCommand { get; }
        public ICommand DeleteCommand { get; }

        public void UpdateProducts(IEnumerable<Product> products)
        {
            _products.Clear();

            foreach (Product product in products)
            {
                ProductViewModel productViewModel = new ProductViewModel(product);
                _products.Add(productViewModel);
            }
        }
    }
}
