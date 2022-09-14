using Hotel_listing.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel_listing.Persistence.Configurations.Entities;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasData(
            new Hotel
            {
                Id = 1,
                Name = "Hotel 1",
                Addrsess = "Hotel 1 street",
                Rating = 4.8,
                CountryId = 1,
            },
            new Hotel
            {
                Id = 2,
                Name = "Hotel 2",
                Addrsess = "Hotel 2 street",
                Rating = 4.8,
                CountryId = 2,
            },
            new Hotel
            {
                Id = 3,
                Name = "Hotel 3",
                Addrsess = "Hotel 3 street",
                Rating = 4.8,
                CountryId = 3,
            },
            new Hotel
            {
                Id = 4,
                Name = "Hotel 4",
                Addrsess = "Hotel 4 street",
                Rating = 4.8,
                CountryId = 4,
            }
        );
    }
}