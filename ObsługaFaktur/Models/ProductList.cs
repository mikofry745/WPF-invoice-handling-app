using ObsługaFaktur.Exceptions;
using ObsługaFaktur.Services.ConflictValidators.ProductConflictValidators;
using ObsługaFaktur.Services.Navigation.Creators.ProductCreators;
using ObsługaFaktur.Services.Navigation.Providers.ProductProviders;
using System.Collections.Generic;
using System.Threading.Tasks;
using ObsługaFaktur.Stores;

namespace ObsługaFaktur.Models
{
    public class ProductList
    {
        private readonly IProductProvider _productProvider;
        private readonly IProductCreator _productCreator;
        private readonly IProductConflictValidator _productConflictValidator;

        public ProductList(IProductProvider productProvider, IProductCreator productCreator, 
            IProductConflictValidator productConflictValidator)
        {
            _productProvider = productProvider;
            _productCreator = productCreator;
            _productConflictValidator = productConflictValidator;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productProvider.GetAllProducts();
        }

        public async Task AddProduct(Product product)
        {
            Product conflictingProduct = await _productConflictValidator.GetConflictingProduct(product);

            if (conflictingProduct != null)
            {
                throw new ProductConflictException(conflictingProduct, product);
            }

            await _productCreator.CreateProduct(product);
        }

        public async Task DeleteProduct(Product product)
        {
            await _productCreator.DeleteProduct(product);
        }

        public async Task EditProduct(Product product, ProductStore productStore)
        {
            await _productCreator.EditProduct(product, productStore);
        }
    }
}
