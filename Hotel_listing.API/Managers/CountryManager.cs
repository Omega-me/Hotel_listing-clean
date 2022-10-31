using System.Dynamic;
using AutoMapper;
using Hotel_listing.Application.Common.Features;
using Hotel_listing.Application.Common.Response;
using Hotel_listing.Application.Contracts.DataShaper;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Infrastructure.RepositoryManager.Query;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace Hotel_listing.API.Managers;
public static class CountryManager
{
    #region Managers and response builders
    public static async Task<CountryResponse<List<ExpandoObject>>> GetAll(
        IQuery query,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        IDataShaper<Country> dataShaper,
        Features<Country>? features)
    {
        var data =await query.Country.GetAll(features);
        var result = mapper.Map<List<Country>>(data.Results);
        features.Pagination.ResultsCount = data.ResultsCount;
        httpContextAccessor.HttpContext.Response.Headers.Add("X-Pagination",JsonConvert.SerializeObject(features.Pagination));

        return new CountryResponse<List<ExpandoObject>>
        {
            StatusCode = StatusCodes.Status200OK,
            Results = dataShaper.ShapeData(result,features.Fields) as List<ExpandoObject>,
            PageSize = features.Pagination.PageSize,
            Success = true,
            Count = data.Results.Count,
            PageNumber = features.Pagination.PageNumber,
        };
    }
    
    public static async Task<CountryResponse<ExpandoObject>> Get(
        IQuery query,
        IDataShaper<Country> dataShaper,
        int id,
        string? includes,
        string fields)
    {
        Country country = await query.Country.Get(c => c.Id == id, includes);

        if (country == null)
        {
            return new CountryResponse<ExpandoObject>
            {
                Success = false,
                StatusCode = StatusCodes.Status404NotFound,
                Errors = new List<BaseError>
                {   
                    new BaseError()
                    {
                        ErrorMessage = $"The Country with Id {id} does not exists."
                    }
                },
            };
        }

        return new CountryResponse<ExpandoObject>
        {
            Results =dataShaper.ShapeData(country,fields),
            Success = true,
            StatusCode = StatusCodes.Status200OK,
        };
    }
    public static async Task<CountryResponse<List<Country>>> GetWithFilters(IDataAccessor db,IMapper mapper)
    {
        var data = await db.Query<Country, dynamic>(new DataAccessorOptions<dynamic>
        {   
            Prams = {},
            Sql = @"Select * from ""Country"" ",
            ConnectionId = "sqlConnectionPsql",
            SqlType = Sqltype.Sql
        });
        List<Country> countries = mapper.Map<List<Country>>(data);
        
        return new CountryResponse<List<Country>>()
        {
            Count = countries.Count,
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Results = countries
        };
    } 
    public static async Task<CountryResponse<Country>> Create(Country data,ICommands command,IMapper mapper)
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
    public static async Task<CountryResponse<Country>> Delete(int id,IQuery query,ICommands command)
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
                        ErrorMessage = $"The Country with Id {id} does not exists."
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
    public static async Task<CountryResponse<Country>> Update(int id, Country data, IQuery query, ICommands command, IMapper mapper)
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
                        ErrorMessage = $"The Country with Id {id} does not exists."
                    }
                }
            };
        }
        Country country = await query.Country.Get(c => c.Id == id,"Hotels");
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
                        ErrorMessage = $"The Country with Id {id} does not exists."
                    }
                },
            };
        }
        var updatedData=mapper.Map(data, country);
        command.Country.Update(updatedData);
        await command.Save();
        return new CountryResponse<Country>
        {
            Results = data,
            StatusCode = StatusCodes.Status200OK,
            Success = true,
        };
    }
    public static async Task<CountryResponse<Country>> UpdatePartial(int id, JsonPatchDocument data, IQuery query, ICommands command,IMapper mapper)
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
                        ErrorMessage = $"The Country with Id {id} does not exists."
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
    public static void Options(IHttpContextAccessor context)
    {
        context.HttpContext.Response.Headers.Add("Allow","GET,POST,PUT,PATCH,OPTIONS,HEAD");
    }
    #endregion
}