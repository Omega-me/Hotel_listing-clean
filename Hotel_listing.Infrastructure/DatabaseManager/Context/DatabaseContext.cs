using Hotel_listing.Domain.Entitites;
using Hotel_listing.Infrastructure.DatabaseManager.Configurations.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel_listing.Infrastructure.DatabaseManager.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options):base(options)
    { }
    
    public DbSet<Country>? Countries { get; set; }
    public DbSet<Hotel>? Hotels { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CountryConfiguration());
        modelBuilder.ApplyConfiguration(new HotelConfiguration());
    }
}