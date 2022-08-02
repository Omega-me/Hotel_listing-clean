using Hotel_listing.Application.DTO.Hotels;

namespace Hotel_listing.Application.DTO.Country;
public class UpdateCountryDto : BaseCountryDto
{
    public ICollection<HotelDto>? Hotels { get; set; }
}