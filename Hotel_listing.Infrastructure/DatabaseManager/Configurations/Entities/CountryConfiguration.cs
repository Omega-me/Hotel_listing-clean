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
                CountryId = 1,
                Name = "Albania",
                ShortName = "AL"
            },
            new Country
            {
                CountryId = 2, 
                Name = "Germany", 
                ShortName = "DU"
            },
            new Country 
            {
                CountryId = 3, 
                Name = "Italy", 
                ShortName = "IT"
            }, 
            new Country 
            {
                CountryId = 4, 
                Name = "England", 
                ShortName = "EN"
            }
        );
    }
}