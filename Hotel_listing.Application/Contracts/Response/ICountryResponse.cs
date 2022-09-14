using Hotel_listing.Application.Common.Response;

namespace Hotel_listing.Application.Contracts.Response;
public interface ICountryResponse<TData>:IBaseResponse<TData,List<BaseError>>
{
    string? Token { get; set; }
}