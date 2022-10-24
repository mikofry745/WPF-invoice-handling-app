using Microsoft.EntityFrameworkCore;
using ObsługaFaktur.DbContext;
using ObsługaFaktur.Dtos;
using ObsługaFaktur.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ObsługaFaktur.Services.ConflictValidators.CustomerConflictValidators
{
    public class DatabaseCustomerConflictValidator : ICustomerConflictValidator
    {
        private readonly ObslugaFakturDbContextFactory _dbContextFactory;

        public DatabaseCustomerConflictValidator(ObslugaFakturDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Customer> GetConflictingCustomer(Customer customer)
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                CustomerDTO? customerDto = await dbContext.Customers
                    .Where(c => c.Name == customer.Name)
                    .FirstOrDefaultAsync();

                if (customerDto == null)
                {
                    return null;
                }

                return ToCustomer(customerDto);
            }
        }

        private static Customer ToCustomer(CustomerDTO customerDto)
        {
            return new Customer(customerDto.Name, customerDto.Street, customerDto.Town, customerDto.ZipCode,
                customerDto.Country, customerDto.PhoneNumber, customerDto.EmailAddress, customerDto.BankName,
                customerDto.Iban, customerDto.Nip);
        }
    }
}
