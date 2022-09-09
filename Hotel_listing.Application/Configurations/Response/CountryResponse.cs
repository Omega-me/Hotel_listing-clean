using Hotel_listing.Application.Contracts.Response;

namespace Hotel_listing.Application.Configurations.Response;

public class CountryResponse<T> : BaseResponse<T, object>, ICountryResponse<T>
{
    public string? Token { get; set; }
}
