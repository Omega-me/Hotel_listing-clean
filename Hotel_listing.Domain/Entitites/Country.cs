using Hotel_listing.Domain.Entitites.Base;
using Hotel_listing.Domain.ValueObjects;

namespace Hotel_listing.Domain.Entitites;

public class Country : BaseEntity
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public bool IsEuropeCountry { get; set; }
    public ICollection<Hotel>? Hotels { get; set; }
}