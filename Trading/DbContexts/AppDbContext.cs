using Microsoft.EntityFrameworkCore;
using Trading.Models;

namespace Trading.DbContexts
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Trader> Traderlar { get; set; }

        public DbSet<Savdo> Savdolar { get; set; }

        public DbSet<SaleObject> SaleObjects { get; set; }
    }
}
