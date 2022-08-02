using AutoMapper;
using Hotel_listing.Application.Configurations.Response;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Domain.Entitites;
using Microsoft.AspNetCore.JsonPatch;

namespace Hotel_listing.Presantation.Managers;

public static class CountryManager
{
    #region Managers and response builders
    public static async Task<CountryResponse> GetCountries(IQuery query)
    {
        IList<Country> countries =await query.Countries.GetAll(null,null,new List<string>{"Hotels"});
        return new CountryResponse().BuildCountryResult(r =>
        {
            r.Token = "1A8E9C32-E49B-49DD-9483-19D44A05B87A";
            r.Data = countries;
            r.StatusCode = StatusCodes.Status200OK;
            r.Success = true;
            r.Results = countries.Count;
        });
    }
    public static async Task<BaseResponse<object, object>> GetCountry(int id, IQuery query)
    {
        Country country = await query.Countries.Get(c => c.CountryId == id);
        if (country == null)
        {
            return new BaseResponse<object, object>().BuildResult<BaseResponse<object,object>>(r =>
            {
                r.Success = false;
                r.StatusCode = StatusCodes.Status404NotFound;
                r.Errors = new List<string>
                {
                    "The Country you are looking for does not exists."
                };
            });
            
        }
        
        return new BaseResponse<object, object>().BuildResult<BaseResponse<object,object>>(r =>
        {
            r.Data = country;
            r.Success = true;
            r.StatusCode = StatusCodes.Status200OK;
            r.Results = 1;
        });
    }
    public static async Task<BaseResponse<object, object>> CreateCountry(CreateCountryDto data,ICommands command,IMapper mapper)
    {
        Country country = mapper.Map<Country>(data);
        await command.Countries.Insert(country);
        await command.Save();
        return new BaseResponse<object, object>().BuildResult<BaseResponse<object, object>>(r =>
        {
            r.Data = data;
            r.StatusCode = StatusCodes.Status201Created;
        });
    }
    public static async Task<BaseResponse<object, object>> DeleteCountry(int id,IQuery query,ICommands command)
    {
        Country country = await query.Countries.Get(c => c.CountryId == id);
        if (country == null)
        {
            return new BaseResponse<object, object>().BuildResult<BaseResponse<object,object>>(r =>
            {
                r.StatusCode = StatusCodes.Status404NotFound;
                r.Success = false;
                r.Errors = new List<string>
                {
                    "No country found with that id to delete."
                };
            });
        }
        await command.Countries.Delete(id);
        await command.Save();
        return new BaseResponse<object, object>().BuildResult<BaseResponse<object,object>>(r =>
        {
            r.StatusCode = StatusCodes.Status204NoContent;
        });
    }
    //TODO
    public static async Task<BaseResponse<object, object>> DeleteCountries(List<int> ids,IQuery query, ICommands command)
    {
        IEnumerable<Country> countries = Array.Empty<Country>();
        foreach (var id in ids)
        {
            countries.ToList().Add(await query.Countries.Get(c=>c.CountryId==id));
        }
        if (countries.ToList().Count == 0)
        {
            return new BaseResponse<object, object>().BuildResult<BaseResponse<object,object>>(r =>
            {
                r.StatusCode = StatusCodes.Status404NotFound;
                r.Success = false;
                r.Errors = new List<string>
                {
                    "No country found with that list of ids to delete."
                };
            });
        }
        command.Countries.DeleteRange(countries);
        await command.Save();
        return new BaseResponse<object, object>().BuildResult<BaseResponse<object,object>>(r =>
        {
            r.StatusCode = StatusCodes.Status204NoContent;
        });
    }
    public static async Task<BaseResponse<object, object>> UpdateCountry(int id, CountryDto data, IQuery query, ICommands command, IMapper mapper)
    {
        if (id != data.CountryId)
        {
            return new BaseResponse<object, object>().BuildResult<BaseResponse<object, object>>(r =>
            {
                r.StatusCode = StatusCodes.Status400BadRequest;
                r.Success = false;
                r.Errors = new List<string>
                {
                    "Provided Id in the url does not match the body Id."
                };
            });
        }
        Country country = await query.Countries.Get(c => c.CountryId == id);
        if (country == null)
        {
            return new BaseResponse<object, object>().BuildResult<BaseResponse<object, object>>(r =>
            {
                r.StatusCode = StatusCodes.Status404NotFound;
                r.Success = false;
                r.Errors = new List<string>()
                {
                    "No Country with tht Id was found."
                };
            });
        }
        mapper.Map(data, country);
        command.Countries.Update(country);
        await command.Save();
        return new BaseResponse<object, object>().BuildResult<BaseResponse<object,object>>(r =>
        {
            r.Data = country;
            r.StatusCode = StatusCodes.Status200OK;
            r.Success = true;
        });
    }
    public static async Task<BaseResponse<object, object>> UpdateCountryPartial(int id, JsonPatchDocument data, IQuery query, ICommands command,IMapper mapper)
    {
        var test = data.Operations;
        Country country = await query.Countries.Get(c => c.CountryId == id);
        if (country == null)
        {
            return new BaseResponse<object, object>().BuildResult<BaseResponse<object, object>>(r =>
            {
                r.StatusCode = StatusCodes.Status404NotFound;
                r.Success = false;
                r.Errors = new List<string>()
                {
                    "No Country with tht Id was found."
                };
            });
        }
        command.Countries.UpdatePartial(country,data);
        await command.Save();
        return new BaseResponse<object, object>().BuildResult<BaseResponse<object,object>>(r =>
        {
            r.Data = country;
            r.StatusCode = StatusCodes.Status200OK;
            r.Success = true;
        });
    }
    #endregion
}