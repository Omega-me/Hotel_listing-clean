using AutoMapper;
using Hotel_listing.Application.Common.RepositoryOptions;
using Hotel_listing.Application.Common.Response;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Domain.Entitites;
using Microsoft.AspNetCore.JsonPatch;

namespace Hotel_listing.API.Managers;
public static class CountryManager
{
    #region Managers and response builders
    public static async Task<CountryResponse<List<Country>>> GetCountries(IQuery query,Options<Country>? options)
    {
        var countries =await query.Country.GetAll(options);
        
        return new CountryResponse<List<Country>>
        {
            StatusCode = StatusCodes.Status200OK,
            Results = countries,
            PageSize = options.Pagination.PageSize,
            Success = true,
            Count = countries.Count,
            PageNumber = options.Pagination.PageNumber,
            MaxPageSize = options.Pagination.MaxPageSize,
        };
    }
    public static async Task<CountryResponse<Country>> GetCountry(IQuery query, int id, string? includes)
    {
        Country country = await query.Country.Get(c => c.Id == id, includes);
        if (country == null)
        {
            return new CountryResponse<Country>
            {
                Success = false,
                StatusCode = StatusCodes.Status404NotFound,
                Errors = new List<BaseError>
                {   
                    new BaseError()
                    {
                        ErrorMessage = "The Country you are looking for does not exists."
                    }
                },
            };
        }

        return new CountryResponse<Country>
        {
            Results = country,
            Success = true,
            StatusCode = StatusCodes.Status200OK,
        };
    }
    public static async Task<CountryResponse<Country>> CreateCountry(CreateCountryDto data,ICommands command,IMapper mapper)
    {
        Country country = mapper.Map<Country>(data);
        await command.Country.Insert(country);
        await command.Save();
        return new CountryResponse<Country>()
        {
            Results = country,
            StatusCode = StatusCodes.Status201Created
        };
    }
    public static async Task<CountryResponse<Country>> DeleteCountry(int id,IQuery query,ICommands command)
    {
        Country country = await query.Country.Get(c => c.Id == id);
        if (country == null)
        {
            return new CountryResponse<Country>
            {
                StatusCode = StatusCodes.Status404NotFound,
                Success = false,
                Errors = new List<BaseError>
                {   
                    new BaseError()
                    {
                        ErrorMessage = "The Country you are looking for does not exists."
                    },
                },
            };
        }
        await command.Country.Delete(id);
        await command.Save();
        return new CountryResponse<Country>
        {
            StatusCode = StatusCodes.Status204NoContent
        };
    }
    public static async Task<CountryResponse<Country>> UpdateCountry(int id, CountryDto data, IQuery query, ICommands command, IMapper mapper)
    {
        if (id != data.Id)
        {
            return new CountryResponse<Country>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Success = false,
                Errors = new List<BaseError>
                {   
                    new BaseError()
                    {
                        ErrorMessage = "The Country you are looking for does not exists."
                    }
                }
            };
        }
        Country country = await query.Country.Get(c => c.Id == id);
        if (country == null)
        {
            return new CountryResponse<Country>
            {
                StatusCode = StatusCodes.Status404NotFound,
                Success = false,
                Errors = new List<BaseError>
                {
                    new BaseError()
                    {
                        ErrorMessage = "The Country you are looking for does not exists."
                    }
                },
            };
        }
        mapper.Map(data, country);
        command.Country.Update(country);
        await command.Save();
        return new CountryResponse<Country>
        {
            Results = country,
            StatusCode = StatusCodes.Status200OK,
            Success = true,
        };
    }
    public static async Task<CountryResponse<Country>> UpdateCountryPartial(int id, JsonPatchDocument data, IQuery query, ICommands command,IMapper mapper)
    {
        Country country = await query.Country.Get(c => c.Id == id);
        if (country == null)
        {
            return new CountryResponse<Country>
            {
                StatusCode = StatusCodes.Status404NotFound,
                Success = false,
                Errors = new List<BaseError>
                {
                    new BaseError()
                    {
                        ErrorMessage = "The Country you are looking for does not exists."
                    }
                },
            };
        }
        command.Country.UpdatePartial(country,data);
        await command.Save();
        return new CountryResponse<Country>
        {
            Results = country,
            StatusCode = StatusCodes.Status200OK,
            Success = true,
        };
    }
    #endregion
}