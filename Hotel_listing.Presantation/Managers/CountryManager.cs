using AutoMapper;
using Hotel_listing.Application.Configurations.Response;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Domain.Entitites;

namespace Hotel_listing.Presantation.Managers;

public static class CountryManager
{
    #region Response Builders
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
    #endregion

    #region Managers
    // public static async Task<Country> CreateCountry(CreateCountryDTO data,ICommands command,IMapper mapper)
    // {
    //     var country = mapper.Map<Country>(data);
    //     await command.Countries.Insert(country);
    //     await command.Save();
    //     return country;
    // }
    public static async Task<BaseResponse<object, object>> CreateCountry(CreateCountryDTO data,ICommands command,IMapper mapper)
    {
        if (data == null)
        {
            return new BaseResponse<object, object>()
                .BuildResult<BaseResponse<object, object>>(r =>
                {
                    r.Success = false;
                    r.StatusCode = StatusCodes.Status404NotFound;
                    r.Errors = "There was a problem with data object";
                });
        }
        
        var country = mapper.Map<Country>(data);
        await command.Countries.Insert(country);
        await command.Save();
        return new BaseResponse<object, object>().BuildResult<BaseResponse<object, object>>(r =>
        {
            r.Data = data;
            r.Errors = null;
            r.Results = 1;
            r.Success = true;
            r.StatusCode = StatusCodes.Status200OK;
        });
    }
    
    public static async Task<IList<Country>> CreateCountries(IList<CreateCountryDTO> data,ICommands command,IMapper mapper)
    {
        var countries = mapper.Map<IList<Country>>(data);
        await command.Countries.InsertRange(countries);
        await command.Save();
        return countries;
    }
    #endregion
}