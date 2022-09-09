using AutoMapper;
using Hotel_listing.Application.Configurations.Response;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Options;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Domain.Entitites;
using Microsoft.AspNetCore.JsonPatch;

namespace Hotel_listing.Presantation.Managers;
public static class CountryManager
{
    #region Managers and response builders
    public static async Task<CountryResponse> GetCountries(IQuery query,QueryOptions<Country> queryOptions)
    {
        IList<Country> countries =await query.Country.GetAll(new QueryOptions<Country>()
        {
            Includes = new List<string>{"Hotels"},
            Pagination = queryOptions.Pagination,
            OrderBy = o=>o.OrderBy(country=>country.Id)
        });
        return new CountryResponse().BuildResult<CountryResponse>(r =>
        {
            r.Results = countries;
            r.StatusCode = StatusCodes.Status200OK;
            r.Success = true;
            r.PageSize = queryOptions.Pagination.PageSize;
            r.PageNumber = queryOptions.Pagination.PageNumber;
            r.MaxPageSize = queryOptions.Pagination.MaxPageSize;
            r.Count = countries.Count;
        });
    }
    
    public static async Task<CountryResponse> GetCountry(int id, IQuery query)
    {
        Country country = await query.Country.Get(c => c.Id == id,new List<string>{"Hotels"});
        if (country == null)
        {
            return new CountryResponse().BuildResult<CountryResponse>(r =>
            {
                r.Success = false;
                r.StatusCode = StatusCodes.Status404NotFound;
                r.Errors = new List<BaseError>
                {   
                    new BaseError()
                    {
                        ErrorMessage = "The Country you are looking for does not exists."
                    }
                };
            });
            
        }
        
        return new CountryResponse().BuildResult<CountryResponse>(r =>
        {
            r.Results = country;
            r.Success = true;
            r.StatusCode = StatusCodes.Status200OK;
        });
    }
    public static async Task<CountryResponse> CreateCountry(CreateCountryDto data,ICommands command,IMapper mapper)
    {
        Country country = mapper.Map<Country>(data);
        await command.Country.Insert(country);
        await command.Save();
        return new CountryResponse().BuildResult<CountryResponse>(r =>
        {
            r.Results = data;
            r.StatusCode = StatusCodes.Status201Created;
        });
    }
    public static async Task<CountryResponse> DeleteCountry(int id,IQuery query,ICommands command)
    {
        Country country = await query.Country.Get(c => c.Id == id);
        if (country == null)
        {
            return new CountryResponse().BuildResult<CountryResponse>(r =>
            {
                r.StatusCode = StatusCodes.Status404NotFound;
                r.Success = false;
                r.Errors = new List<BaseError>
                {   
                    new BaseError()
                    {
                        ErrorMessage = "The Country you are looking for does not exists."
                    }
                };
            });
        }
        await command.Country.Delete(id);
        await command.Save();
        return new CountryResponse().BuildResult<CountryResponse>(r =>
        {
            r.StatusCode = StatusCodes.Status204NoContent;
        });
    }
    //TODO
    public static async Task<CountryResponse> DeleteCountries(List<int> ids,IQuery query, ICommands command)
    {
        IEnumerable<Country> countries = Array.Empty<Country>();
        foreach (var id in ids)
        {
            countries.ToList().Add(await query.Country.Get(c=>c.Id==id));
        }
        if (countries.ToList().Count == 0)
        {
            return new CountryResponse().BuildResult<CountryResponse>(r =>
            {
                r.StatusCode = StatusCodes.Status404NotFound;
                r.Success = false;
                r.Errors = new List<BaseError>
                {   
                    new BaseError()
                    {
                        ErrorMessage = "The Country you are looking for does not exists."
                    }
                };
            });
        }
        command.Country.DeleteRange(countries);
        await command.Save();
        return new CountryResponse().BuildResult<CountryResponse>(r =>
        {
            r.StatusCode = StatusCodes.Status204NoContent;
        });
    }
    public static async Task<CountryResponse> UpdateCountry(int id, CountryDto data, IQuery query, ICommands command, IMapper mapper)
    {
        if (id != data.Id)
        {
            return new CountryResponse().BuildResult<CountryResponse>(r =>
            {
                r.StatusCode = StatusCodes.Status400BadRequest;
                r.Success = false;
                r.Errors = new List<BaseError>
                {   
                    new BaseError()
                    {
                        ErrorMessage = "The Country you are looking for does not exists."
                    }
                };
            });
        }
        Country country = await query.Country.Get(c => c.Id == id);
        if (country == null)
        {
            return new CountryResponse().BuildResult<CountryResponse>(r =>
            {
                r.StatusCode = StatusCodes.Status404NotFound;
                r.Success = false;
                r.Errors = new List<BaseError>
                {   
                    new BaseError()
                    {
                        ErrorMessage = "The Country you are looking for does not exists."
                    }
                };
            });
        }
        mapper.Map(data, country);
        command.Country.Update(country);
        await command.Save();
        return new CountryResponse().BuildResult<CountryResponse>(r =>
        {
            r.Results = country;
            r.StatusCode = StatusCodes.Status200OK;
            r.Success = true;
        });
    }
    public static async Task<CountryResponse> UpdateCountryPartial(int id, JsonPatchDocument data, IQuery query, ICommands command,IMapper mapper)
    {
        Country country = await query.Country.Get(c => c.Id == id);
        if (country == null)
        {
            return new CountryResponse().BuildResult<CountryResponse>(r =>
            {
                r.StatusCode = StatusCodes.Status404NotFound;
                r.Success = false;
                r.Errors = new List<BaseError>
                {   
                    new BaseError()
                    {
                        ErrorMessage = "The Country you are looking for does not exists."
                    }
                };
            });
        }
        command.Country.UpdatePartial(country,data);
        await command.Save();
        return new CountryResponse().BuildResult<CountryResponse>(r =>
        {
            r.Results = country;
            r.StatusCode = StatusCodes.Status200OK;
            r.Success = true;
        });
    }
    #endregion
}