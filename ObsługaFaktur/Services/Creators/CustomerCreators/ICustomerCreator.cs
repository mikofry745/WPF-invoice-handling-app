using ObsługaFaktur.Models;
using System.Threading.Tasks;
using ObsługaFaktur.Stores;

namespace ObsługaFaktur.Services.Creators.CustomerCreators
{
    public interface ICustomerCreator
    {
        Task CreateCustomer(Customer customer);
        Task DeleteCustomer(Customer customer);
        Task EditCustomer(Customer customer, CustomerStore customerStore);
    }
}
