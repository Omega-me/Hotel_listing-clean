using Hotel_listing.Domain.ValueObjects;

namespace Hotel_listing.Domain.Entitites;

public class Country
{
    public int Id { get; init; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public ICollection<Hotel>? Hotels { get; set; }
}