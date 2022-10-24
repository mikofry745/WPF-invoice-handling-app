using ObsługaFaktur.Exceptions;
using ObsługaFaktur.Services.ConflictValidators.CustomerConflictValidators;
using ObsługaFaktur.Services.Creators.CustomerCreators;
using ObsługaFaktur.Services.Providers.CustomerProviders;
using System.Collections.Generic;
using System.Threading.Tasks;
using ObsługaFaktur.Stores;

namespace ObsługaFaktur.Models
{
    public class CustomerList
    {
        private readonly ICustomerCreator _customerCreator;
        private readonly ICustomerConflictValidator _customerConflictValidator;
        private readonly ICustomerProvider _customerProvider;

        public CustomerList(ICustomerCreator customerCreator, ICustomerProvider customerProvider,
            ICustomerConflictValidator customerConflictValidator)
        {
            _customerCreator = customerCreator;
            _customerProvider = customerProvider;
            _customerConflictValidator = customerConflictValidator;
        }

        public async Task<IEnumerable<Customer>> GetALlCustomers()
        {
            return await _customerProvider.GetAllCustomers();
        }

        public async Task AddCustomer(Customer customer)
        {
            Customer confilctingCustomer = await _customerConflictValidator.GetConflictingCustomer(customer);

            if (confilctingCustomer != null)
            {
                throw new CustomerConflictException(confilctingCustomer, customer);
            }

            await _customerCreator.CreateCustomer(customer);
        }

        public async Task DeleteCustomer(Customer customer)
        {
            await _customerCreator.DeleteCustomer(customer);
        }

        public async Task EditCustomer(Customer customer, CustomerStore customerStore)
        {
            await _customerCreator.EditCustomer(customer, customerStore);
        }
    }
}
