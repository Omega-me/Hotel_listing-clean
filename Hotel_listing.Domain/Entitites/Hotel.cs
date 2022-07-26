using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_listing.Domain.Entitites;

public class Hotel
{
    public int HotelId { get; set; }       
    public string Name { get; set; }       
    public string Adrsess { get; set; }
    public double Rating { get; set; }
    [ForeignKey(nameof(Country))]
    public int? CountryId { get; set; }
    public virtual Country Country { get; set; }
}