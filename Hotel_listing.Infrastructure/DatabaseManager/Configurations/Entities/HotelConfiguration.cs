using Hotel_listing.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hotel_listing.Infrastructure.DatabaseManager.Configurations.Entities;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasData(
            new Hotel
            {
                HotelId = 1,
                Name = "Hotel 1",
                Adrsess = "Hotel 1 street",
                Rating = 4.8,
                CountryId = 1,
            },
            new Hotel
            {
                HotelId = 2,
                Name = "Hotel 2",
                Adrsess = "Hotel 2 street",
                Rating = 4.8,
                CountryId = 2,
            },
            new Hotel
            {
                HotelId = 3,
                Name = "Hotel 3",
                Adrsess = "Hotel 3 street",
                Rating = 4.8,
                CountryId = 3,
            },
            new Hotel
            {
                HotelId = 4,
                Name = "Hotel 4",
                Adrsess = "Hotel 4 street",
                Rating = 4.8,
                CountryId = 4,
            }
        );
    }
}