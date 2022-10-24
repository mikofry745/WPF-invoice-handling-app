using System;
using System.Linq;
using ObsługaFaktur.DbContext;
using ObsługaFaktur.Dtos;
using ObsługaFaktur.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObsługaFaktur.Stores;

namespace ObsługaFaktur.Services.Navigation.Creators.ProductCreators
{
    public class DatabaseProductCreator : IProductCreator
    {
        private readonly ObslugaFakturDbContextFactory _dbContextFactory;

        public DatabaseProductCreator(ObslugaFakturDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task CreateProduct(Product product)
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                ProductDTO productDto = ToProductDto(product);

                dbContext.Products.Add(productDto);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteProduct(Product product)
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                ProductDTO? productToRemove =
                    await dbContext.Products.SingleOrDefaultAsync(p => p.Name == product.Name && p.NetPrice == product.NetPrice 
                        && p.VatPercent == product.VatPercent && p.Comment == p.Comment);


                if (productToRemove != null)
                {
                    dbContext.Products.Attach(productToRemove);
                    dbContext.Products.Remove(productToRemove);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public async Task EditProduct(Product product, ProductStore productStore)
        {
            using (ObslugaFakturDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                ProductDTO? productToEdit = await dbContext.Products.SingleOrDefaultAsync(p => 
                    p.Name == productStore.CurrentEditedProduct.Name && 
                    p.NetPrice == productStore.CurrentEditedProduct.NetPrice && 
                    p.VatPercent == productStore.CurrentEditedProduct.VatPercent && 
                    p.Comment == productStore.CurrentEditedProduct.Comment);

                if (productToEdit != null)
                {
                    productToEdit.Name = product.Name;
                    productToEdit.Unit = product.Unit.ToString();
                    productToEdit.VatPercent = product.VatPercent;
                    productToEdit.NetPrice = product.NetPrice;
                    productToEdit.Comment = product.Comment;

                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private static ProductDTO ToProductDto(Product product)
        {
            return new ProductDTO()
            {
                Name = product.Name,
                VatPercent = product.VatPercent,
                Unit = product.Unit.ToString(),
                Comment = product.Comment,
                NetPrice = product.NetPrice
            };
        }
    }
}