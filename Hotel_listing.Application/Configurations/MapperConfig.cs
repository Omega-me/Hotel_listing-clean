using AutoMapper;
using Hotel_listing.Domain.Entitites;

namespace Hotel_listing.Application.Configurations;
public partial class MapperConfig : Profile
{
    public MapperConfig(string mapper)
    {
        CreateMap<Country, Country>().ReverseMap();
        CreateMap<Hotel, Hotel>().ReverseMap();
    }
}