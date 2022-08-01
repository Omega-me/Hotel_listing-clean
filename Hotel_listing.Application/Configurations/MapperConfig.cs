using AutoMapper;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Domain.Entitites;

namespace Hotel_listing.Application.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        #region CountryDTO
        CreateMap<Country,BaseCountryDTO>().ReverseMap();
        CreateMap<Country, CreateCountryDTO>().ReverseMap();
        CreateMap<Country,CountryDTO>().ReverseMap();
        #endregion

        #region HotelDTO

        

        #endregion
    }
}