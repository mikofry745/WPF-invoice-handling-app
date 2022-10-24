using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObsługaFaktur.DbContext;
using ObsługaFaktur.Dtos;
using ObsługaFaktur.Models;

namespace ObsługaFaktur.Services.ConflictValidators.ProductConflictValidators
{
    public class DatabaseProductConflictValidator : IProductConflictValidator
    {
        private readonly ObslugaFakturDbContextFactory _dbContextFactory;

        public DatabaseProductConflictValidator(ObslugaFakturDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Product> GetConflictingProduct(Product product)
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                ProductDTO? productDto =  await dbContext.Products
                    .Where(p => p.Name == product.Name)
                    .Where(p => p.VatPercent == product.VatPercent)
                    .Where(p => p.NetPrice == product.NetPrice)
                    .FirstOrDefaultAsync();

                if (productDto == null)
                {
                    return null;
                }

                return ToProduct(productDto);
            }
        }

        private static Product ToProduct(ProductDTO productDto)
        {
            return new Product(productDto.Name, productDto.VatPercent, (Unit)Enum.Parse(typeof(Unit), productDto.Unit), productDto.NetPrice,
                productDto.Comment);
        }
    }
}
