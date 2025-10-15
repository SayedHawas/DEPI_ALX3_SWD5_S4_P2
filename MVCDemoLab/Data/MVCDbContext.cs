using Microsoft.EntityFrameworkCore;
using MVCDemoLab.Models;

namespace MVCDemoLab.Data
{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext()
        {

        }

        public MVCDbContext(DbContextOptions<MVCDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
