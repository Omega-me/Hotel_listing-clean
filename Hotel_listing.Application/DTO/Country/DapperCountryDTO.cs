namespace Hotel_listing.Application.DTO.Country;

public class DapperCountryDTO
{
    public string CountryName { get; set; }   
    public string ShortName { get; set; }   
    public bool IsEuropeCountry { get; set; }   
    public string Address { get; set; }
    public string HotelName { get; set; }   
    public int Rating { get; set; }
    public int Id { get; set; }
    public int CountryId { get; set; }
}