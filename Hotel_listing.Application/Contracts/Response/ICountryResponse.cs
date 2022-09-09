namespace Hotel_listing.Application.Contracts.Response;
public interface ICountryResponse<T>:IBaseResponse<T,object>
{
    string? Token { get; set; }
}