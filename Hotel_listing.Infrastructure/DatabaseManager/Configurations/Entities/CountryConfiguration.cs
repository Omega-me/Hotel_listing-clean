using Hotel_listing.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel_listing.Infrastructure.DatabaseManager.Configurations.Entities;

public class CountryConfiguration :IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasData(
            new Country 
            {
                Id = 1,
                Name = "Albania",
                ShortName = "AL"
            },
            new Country
            {
                Id = 2, 
                Name = "Germany", 
                ShortName = "DU"
            },
            new Country 
            {
                Id = 3, 
                Name = "Italy", 
                ShortName = "IT"
            }, 
            new Country 
            {
                Id = 4, 
                Name = "England", 
                ShortName = "EN"
            }
        );
    }
}