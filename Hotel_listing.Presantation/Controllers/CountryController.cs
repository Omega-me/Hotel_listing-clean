using AutoMapper;
using Hotel_listing.Application.Configurations.RepositoryOptions;
using Hotel_listing.Application.Configurations.Response;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Application.Exceptions.ValidationResponseFilters;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Presantation.Managers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Hotel_listing.Presantation.Controllers;

[CountryModelStateFilter]
public class CountryController:BaseController<Country>
{
    public CountryController(IQuery query, ICommands command,IMapper mapper,IHttpContextAccessor context) 
        : base(query,command,mapper,context)
    { }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryResponse<List<Country>>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(BaseResponse<object,List<BaseError>>))]
    public async Task<ActionResult<CountryResponse<List<Country>>>> GetCountries(
        [FromQuery(Name = "_filter")] string? _filter,
        [FromQuery(Name = "_sort")] string? _sort,
        [FromQuery(Name = "_include")] string? _include,
        [FromQuery(Name = "_size")] int _size=10,
        [FromQuery(Name = "_page")] int _page=1,
        [FromQuery(Name = "_max")] int _max=50
        ) {
        // Logger.LogInformation(TestMethode("Test"));
        //https://medium.com/c-sharp-progarmming/configure-annotations-in-swagger-documentation-for-asp-net-core-api-8215596907c7
        return HandleResponse(await CountryManager.GetCountries(Query,new Options<Country>()
        {
            Sort = _sort,
            Includes = _include,
            Filter = _filter,
            Context = Context.HttpContext,
            Pagination = new PaginationParams()
            {
                PageNumber = _page,
                PageSize = _size,
                MaxPageSize = _max
            }
        }));
    }
    
    [HttpGet("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryResponse<Country>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(BaseResponse<object,List<BaseError>>))]
    public async Task<ActionResult<CountryResponse<Country>>> GetCountry(int id)
    {
        return HandleResponse(await CountryManager.GetCountry(id, Query));
    }
    
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CountryResponse<Country>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(BaseResponse<object,List<BaseError>>))]
    public async Task<ActionResult<CountryResponse<Country>>> CreateCountry([FromBody] CreateCountryDto data)
    {
        return HandleResponse(await CountryManager.CreateCountry(data,Command,Mapper));
    }

    [HttpDelete("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(BaseResponse<object,List<BaseError>>))]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        return HandleResponse(await CountryManager.DeleteCountry(id,Query,Command));
    }

    [HttpDelete]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(BaseResponse<object,List<BaseError>>))]
    public async Task<IActionResult> DeleteCountries([FromBody]List<int> ids)
    {
        return HandleResponse(await CountryManager.DeleteCountries(ids,Query,Command));
    }

    [HttpPut("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryResponse<Country>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(BaseResponse<object,List<BaseError>>))]
    public async Task<ActionResult<CountryResponse<Country>>> UpdateCountry(int id,[FromBody] CountryDto data)
    {
        return HandleResponse(await CountryManager.UpdateCountry(id,data,Query,Command,Mapper));
    }

    [HttpPatch("{id:int}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CountryResponse<Country>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest , Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized,Type = typeof(BaseResponse<object,List<BaseError>>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(BaseResponse<object,List<BaseError>>))]
    public async Task<ActionResult<CountryResponse<Country>>> UpdateCountryPartial(int id, [FromBody] JsonPatchDocument data)
    {
        return HandleResponse(await CountryManager.UpdateCountryPartial(id,data,Query,Command,Mapper));
    }
}