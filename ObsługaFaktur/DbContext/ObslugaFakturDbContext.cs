using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ObsługaFaktur.Dtos;
using ObsługaFaktur.Models;

namespace ObsługaFaktur.DbContext
{
    public class ObslugaFakturDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ObslugaFakturDbContext(DbContextOptions options) : base(options) {}

        public DbSet<InvoiceDTO> Invoices { get; set; }
        public DbSet<CustomerDTO> Customers { get; set; }
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<InvoiceItemDTO> InvoiceItems { get; set; }
    }
}
