using ObsługaFaktur.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObsługaFaktur.Services.Navigation.Providers.ProductProviders
{
    public interface IProductProvider
    {
        Task<IEnumerable<Product>> GetAllProducts();
    }
}
