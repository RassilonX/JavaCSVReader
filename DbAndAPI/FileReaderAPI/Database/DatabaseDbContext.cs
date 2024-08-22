using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class DatabaseDbContext : DbContext
{
    public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customer { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasKey(c => c.CustomerRef);

        modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerRef = "TEST", CustomerName = "John Tester", AddressLine1 = "Test House", AddressLine2 = "Test Building", Town = "Testmouth", County = "Testshire", Country = "Testland", Postcode = "TE573RS" }
        );
    }
}
