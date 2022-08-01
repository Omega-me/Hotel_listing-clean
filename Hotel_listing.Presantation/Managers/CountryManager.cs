using AutoMapper;
using Hotel_listing.Application.Configurations.Response;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Domain.Entitites;

namespace Hotel_listing.Presantation.Managers;

public static class CountryManager
{
    #region Managers and response builders
    public static async Task<BaseResponse<object, object>> GetCountries(IQuery query)
    {
        IList<Country> countries =await query.Countries.GetAll();
        return new BaseResponse<object, object>().BuildResult<BaseResponse<object,object>>(r =>
        {
            r.Data = countries;
            r.StatusCode = StatusCodes.Status200OK;
            r.Success = true;
            r.Errors = null;
            r.Results = countries.Count;
        });
    }
    public static async Task<BaseResponse<object, object>> GetCountry(int id, IQuery query)
    {
        Country country = await query.Countries.Get(c => c.CountryId == id);

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (country == null)
        {
            return new BaseResponse<object, object>().BuildResult<BaseResponse<object,object>>(r =>
            {
                r.Data = null;
                r.Success = false;
                r.StatusCode = StatusCodes.Status404NotFound;
                r.Errors = "The Country you are looking fore does not exists";
            });
            
        }
        
        return new BaseResponse<object, object>().BuildResult<BaseResponse<object,object>>(r =>
        {
            r.Data = country;
            r.Success = true;
            r.StatusCode = StatusCodes.Status200OK;
            r.Results = 1;
            r.Errors = null;
        });
    }
    public static async Task<BaseResponse<object, object>> CreateCountry(CreateCountryDTO data,ICommands command,IMapper mapper)
    {
        var country = mapper.Map<Country>(data);
        await command.Countries.Insert(country);
        await command.Save();
        return new BaseResponse<object, object>().BuildResult<BaseResponse<object, object>>(r =>
        {
            r.Data = data;
            r.StatusCode = StatusCodes.Status201Created;
        });
    }
    public static async Task<CountryResponse> CreateCountries(IList<CreateCountryDTO> data,ICommands command,IMapper mapper)
    {
        var countries = mapper.Map<IList<Country>>(data);
        await command.Countries.InsertRange(countries);
        await command.Save();
        return new CountryResponse().BuildResult<CountryResponse>(r =>
        {
            r.Data = countries;
            r.StatusCode = StatusCodes.Status201Created;
        });
    }
    #endregion
}