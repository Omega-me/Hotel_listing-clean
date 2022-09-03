namespace Hotel_listing.Application.DTO.Hotels;

public class BaseHotelDto
{
    public string? Name { get; set; }       
    public string? Adrsess { get; set; }
    public double Rating { get; set; }
    public int? CountryId { get; set; }
}