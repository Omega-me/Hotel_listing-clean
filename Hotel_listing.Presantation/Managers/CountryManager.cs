using Hotel_listing.Application.Configurations.Response;
using Hotel_listing.Domain.Entitites;

namespace Hotel_listing.Presantation.Managers;

public static class CountryManager
{
    public static CountryResponse Response(IList<Country>? resultObject)
    {
        if (resultObject == null)
        {
            return new CountryResponse().BuildCountryResult(r =>
            {
                r.Success = false;
                r.StatusCode = StatusCodes.Status400BadRequest;
                r.Errors = "There wa a problem";
            });
        }
        return new CountryResponse().BuildCountryResult(r =>
        {
            r.StatusCode = StatusCodes.Status200OK;
            r.Results = resultObject.Count;
            r.Success = true;
            r.Token = "6225DCE5-59C6-4657-A8C0-7AFC87E6B9D4";
            r.Errors = null;
            r.Data = resultObject;
        });
    }
    public static BaseResponse<object,object> Response(Country? resultObject)
    {
        if (resultObject == null)
        {
            return new BaseResponse<object, object>()
            .BuildResult<BaseResponse<object, object>>(r =>
            {
                r.Success = false;
                r.StatusCode = StatusCodes.Status404NotFound;
            });
        }

        return new BaseResponse<object, object>().BuildResult<BaseResponse<object, object>>(r =>
        {
            r.Data = resultObject;
            r.Errors = null;
            r.Results = 1;
            r.Success = true;
            r.StatusCode = StatusCodes.Status200OK;
        });
    }
}