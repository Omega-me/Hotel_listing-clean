using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Application.DTO.Hotels;
using Hotel_listing.Domain.Entitites;

namespace Hotel_listing.Application.Common;
public partial class MapperConfig
{
    public MapperConfig()
    {
        #region CountryDto
        CreateMap<Country,CountryDto>().ReverseMap();
        CreateMap<Country,BaseCountryDto>().ReverseMap();
        CreateMap<Country, CreateCountryDto>().ReverseMap();
        CreateMap<Country, UpdateCountryDto>().ReverseMap();
        #endregion
    
        #region HotelDto
        CreateMap<Hotel,HotelDto>().ReverseMap();
        CreateMap<Hotel,BaseHotelDto>().ReverseMap();
        CreateMap<Hotel,CreateHotelDto>().ReverseMap();
        CreateMap<Hotel,UpdateHotelDto>().ReverseMap();
        #endregion
    }
}