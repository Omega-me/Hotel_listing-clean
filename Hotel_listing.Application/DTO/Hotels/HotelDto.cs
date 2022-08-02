using Hotel_listing.Application.DTO.Country;

namespace Hotel_listing.Application.DTO.Hotels;

public class HotelDto : BaseHotelDto
{
    public int HotelId { get; set; }       
    public virtual CountryDto? Country { get; set; }
}