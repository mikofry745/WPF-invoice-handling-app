using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ObsługaFaktur.DbContext
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ObslugaFakturDbContext>
    {
        public ObslugaFakturDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=ObslugaFaktur.db").Options;

            return new ObslugaFakturDbContext(options);
        }
    }
}
