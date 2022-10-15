using System.ComponentModel.DataAnnotations.Schema;
using Hotel_listing.Domain.Entitites.Base;

namespace Hotel_listing.Domain.Entitites;

public class Hotel : BaseEntity
{
    public string Name { get; set; }       
    public string Addrsess { get; set; }
    public double Rating { get; set; }
    [ForeignKey(nameof(Country))]
    public int? CountryId { get; set; }
    public virtual Country? Country { get; set; }
}