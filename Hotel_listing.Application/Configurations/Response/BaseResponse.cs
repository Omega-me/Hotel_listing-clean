using Hotel_listing.Application.Contracts.Response;
using Microsoft.AspNetCore.Http;

namespace Hotel_listing.Application.Configurations.Response;

public class BaseResponse<TData,TError> : IBaseResponse<TData, TError>
{
    public bool Success { get; set; }
    public int? Count { get; set; }
    public TData? Results { get; set; }
    public TError? Errors { get; set; }
    public int StatusCode { get; set; }

    public T BuildResult<T>(Action<T>? responseBuilder) where T : new()
    {
        var response = new T();
        responseBuilder?.Invoke(response);
        return response;
    }
}

