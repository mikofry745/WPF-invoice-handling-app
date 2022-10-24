using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObsługaFaktur.DbContext;
using ObsługaFaktur.Dtos;
using ObsługaFaktur.Models;

namespace ObsługaFaktur.Services.Navigation.Providers.ProductProviders
{
    public class DatabaseProductProvider : IProductProvider
    {
        private readonly ObslugaFakturDbContextFactory _dbContextFactory;

        public DatabaseProductProvider(ObslugaFakturDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ProductDTO> productDtos =  await dbContext.Products.ToListAsync();

                return productDtos.Select(p => ToProduct(p));
            }
        }

        private static Product ToProduct(ProductDTO productDto)
        {
            return new Product(productDto.Name, productDto.VatPercent, (Unit)Enum.Parse(typeof(Unit), productDto.Unit), productDto.NetPrice,
                productDto.Comment);
        }
    }
}
