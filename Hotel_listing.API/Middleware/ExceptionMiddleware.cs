using System.Net;
using System.Text.Json;
using Hotel_listing.API.Common;
using Hotel_listing.Application.Exceptions;
using Serilog;

namespace Hotel_listing.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next,IHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            Log.Fatal(e,e.Message);
            context.Response.ContentType = API_Const.PRODUCES_JSON;
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment()
                ? new AppException(context.Response.StatusCode,e.Message,e.StackTrace.ToString())
                : new AppException(context.Response.StatusCode,API_Const.SERVER_ERROR);
            var option = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
            var json = JsonSerializer.Serialize(response, option);

            await context.Response.WriteAsync(json);
        }
    }
}