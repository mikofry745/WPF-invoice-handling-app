using ObsługaFaktur.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ObsługaFaktur.Services.Providers.CustomerProviders
{
    public interface ICustomerProvider
    {
        Task<IEnumerable<Customer>> GetAllCustomers();
    }
}
