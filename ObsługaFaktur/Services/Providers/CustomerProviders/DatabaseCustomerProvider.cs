using Microsoft.EntityFrameworkCore;
using ObsługaFaktur.DbContext;
using ObsługaFaktur.Dtos;
using ObsługaFaktur.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObsługaFaktur.Services.Providers.CustomerProviders
{
    public class DatabaseCustomerProvider : ICustomerProvider
    {
        private readonly ObslugaFakturDbContextFactory _dbContextFactory;

        public DatabaseCustomerProvider(ObslugaFakturDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<CustomerDTO> customerDtos = await dbContext.Customers.ToListAsync();

                return customerDtos.Select(c => ToCustomer(c));
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
