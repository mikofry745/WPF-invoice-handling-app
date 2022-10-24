using ObsługaFaktur.Models;
using System.Threading.Tasks;
using ObsługaFaktur.Stores;

namespace ObsługaFaktur.Services.Navigation.Creators.ProductCreators
{
    public interface IProductCreator
    {
        Task CreateProduct(Product product);
        Task DeleteProduct(Product product);
        Task EditProduct(Product product, ProductStore productStore);
    }
}
