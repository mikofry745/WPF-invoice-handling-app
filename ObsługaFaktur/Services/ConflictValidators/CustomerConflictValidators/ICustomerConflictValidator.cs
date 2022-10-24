using ObsługaFaktur.Models;
using System.Threading.Tasks;

namespace ObsługaFaktur.Services.ConflictValidators.CustomerConflictValidators
{
    public interface ICustomerConflictValidator
    {
        Task<Customer> GetConflictingCustomer(Customer customer);
    }
}
