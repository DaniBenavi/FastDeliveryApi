using FastDeliveryAPI.Data.Configurations;
using FastDeliveryAPI.Entity;
using Microsoft.EntityFrameworkCore;


namespace FastDeliveryAPI.Data;
public class FastDeliveryDbContext : DbContext
{
    public FastDeliveryDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }

}