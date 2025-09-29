using Microsoft.EntityFrameworkCore;
using WebApiDay5Lab.Models;

namespace WebApiDay5Lab.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                   new Department { DepartmentId = 1, Name = "HR" },
                   new Department { DepartmentId = 2, Name = "Software" }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
