using ObsługaFaktur.Models;
using System.Threading.Tasks;

namespace ObsługaFaktur.Services.ConflictValidators.ProductConflictValidators
{
    public interface IProductConflictValidator
    {
        Task<Product> GetConflictingProduct(Product product);
    }
}
