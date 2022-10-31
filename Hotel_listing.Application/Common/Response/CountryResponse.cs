using Hotel_listing.Application.Contracts.Response;

namespace Hotel_listing.Application.Common.Response;
public class CountryResponse<TData> : BaseResponse<TData,List<BaseError>>, ICountryResponse<TData>
{
    public string? Token { get; set; }
}
