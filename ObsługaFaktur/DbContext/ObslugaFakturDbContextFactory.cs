using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ObsługaFaktur.DbContext
{
    public class ObslugaFakturDbContextFactory
    {
        private readonly string _connectionString;

        public ObslugaFakturDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ObslugaFakturDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new ObslugaFakturDbContext(options);
        }
    }
}
