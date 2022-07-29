namespace Hotel_listing.Application.Contracts.Response;

public interface IBaseResponse<TData, TError>
{
    bool Success { get; set; }
    int? Results { get; set; }
    TData? Data { get; set; }
    TError? Errors { get; set; }
    int StatusCode { get; set; }
    T BuildResult<T>(Action<T>? responseBuilder) where T:new();
}