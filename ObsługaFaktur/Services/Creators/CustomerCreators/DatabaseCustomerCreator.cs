using Microsoft.EntityFrameworkCore;
using ObsługaFaktur.DbContext;
using ObsługaFaktur.Dtos;
using ObsługaFaktur.Models;
using ObsługaFaktur.Stores;
using System.Threading.Tasks;

namespace ObsługaFaktur.Services.Creators.CustomerCreators
{
    public class DatabaseCustomerCreator : ICustomerCreator
    {
        private readonly ObslugaFakturDbContextFactory _dbContextFactory;

        public DatabaseCustomerCreator(ObslugaFakturDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateCustomer(Customer customer)
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                CustomerDTO customerDto = ToCustomerDto(customer);

                dbContext.Customers.Add(customerDto);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteCustomer(Customer customer)
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                CustomerDTO? customerToRemove =
                    await dbContext.Customers.SingleOrDefaultAsync(c => c.Name == customer.Name && c.Nip == customer.Nip 
                        && c.BankName == customer.BankName && c.Country == customer.Country && c.EmailAddress == customer.EmailAddress
                        && c.Iban == customer.Iban && c.PhoneNumber == customer.PhoneNumber && c.ZipCode == customer.ZipCode
                        && c.Street == customer.Street && c.Town == customer.Town);

                if (customerToRemove != null)
                {
                    dbContext.Customers.Attach(customerToRemove);
                    dbContext.Customers.Remove(customerToRemove);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task EditCustomer(Customer customer, CustomerStore customerStore)
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                CustomerDTO? customerToEdit =
                await dbContext.Customers.SingleOrDefaultAsync(c => 
                    c.Name == customerStore.CurrentEditedCustomer.Name && 
                    c.Nip == customerStore.CurrentEditedCustomer.Nip &&
                    c.BankName == customerStore.CurrentEditedCustomer.BankName &&
                    c.Country == customerStore.CurrentEditedCustomer.Country &&
                    c.EmailAddress == customerStore.CurrentEditedCustomer.EmailAddress &&
                    c.Iban == customerStore.CurrentEditedCustomer.Iban &&
                    c.PhoneNumber == customerStore.CurrentEditedCustomer.PhoneNumber &&
                    c.ZipCode == customerStore.CurrentEditedCustomer.ZipCode && 
                    c.Street == customerStore.CurrentEditedCustomer.Street &&
                    c.Town == customerStore.CurrentEditedCustomer.Town);

                if (customerToEdit != null)
                {
                    customerToEdit.Name = customer.Name;
                    customerToEdit.BankName = customer.BankName;
                    customerToEdit.Country = customer.Country;
                    customerToEdit.EmailAddress = customer.EmailAddress;
                    customerToEdit.Iban = customer.Iban;
                    customerToEdit.PhoneNumber = customer.PhoneNumber;
                    customerToEdit.ZipCode = customer.ZipCode;
                    customerToEdit.Street = customer.Street;
                    customerToEdit.Town = customer.Town;
                    customerToEdit.Nip = customer.Nip;

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private static CustomerDTO ToCustomerDto(Customer customer)
        {
            return new CustomerDTO()
            {
                Name = customer.Name,
                Street = customer.Street,
                Town = customer.Town,
                ZipCode = customer.ZipCode,
                PhoneNumber = customer.PhoneNumber,
                BankName = customer.BankName,
                Country = customer.Country,
                EmailAddress = customer.EmailAddress,
                Iban = customer.Iban,
                Nip = customer.Nip
            };
        }
    }
}
