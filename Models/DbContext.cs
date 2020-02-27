using Microsoft.EntityFrameworkCore;

namespace tryout_gigamin1_nugi_mulya_nugraha.Models
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options): base(options) {}

        public DbSet<Customer> customers {get; set;}
        public DbSet<Order> orders {get; set;}
        public DbSet<Product> products {get; set;}
        public DbSet<Driver> drivers {get; set;}
    }
}