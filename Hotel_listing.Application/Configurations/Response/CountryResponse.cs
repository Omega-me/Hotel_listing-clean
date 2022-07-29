using Hotel_listing.Application.Contracts.Response;

namespace Hotel_listing.Application.Configurations.Response;

public class CountryResponse : BaseResponse<object, object>, ICountryResponse
{
    public string? Token { get; set; }
    
    public CountryResponse BuildCountryResult(Action<CountryResponse>? responseBuilder)
    {
        var response = new CountryResponse();
        responseBuilder?.Invoke(response);
        return response;
    }
}
