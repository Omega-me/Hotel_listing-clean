using Hotel_listing.Domain.Entitites;

namespace Hotel_listing.Application.DTO.Country;
public class CountryDto:BaseCountryDto
{
    public int Id { get; init; }
    public ICollection<Hotel>? Hotels { get; set; }
}
