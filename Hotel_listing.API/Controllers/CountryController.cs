using AutoMapper;
using Hotel_listing.API.Common;
using Hotel_listing.API.Managers;
using Hotel_listing.Application.Common.RepositoryOptions;
using Hotel_listing.Application.Common.Response;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Application.Exceptions.ValidationResponseFilters;
using Hotel_listing.Domain.Entitites;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hotel_listing.API.Controllers;

[CountryModelStateFilter]
public class CountryController:BaseController<Country>
{
    public CountryController(IQuery query, ICommands command,IMapper mapper,IHttpContextAccessor context) 
        : base(query,command,mapper,context)
    { }

    /// <summary>
    /// GET ALL
    /// </summary>
    [HttpGet]
    [Produces(API_Const.PRODUCES_JSON)]
    [SwaggerOperation(null, null, Summary = API_Const.GET_ALL, Description = API_Const.SWAGGER_OP_DESCR_GETALL)]
    [SwaggerResponse(StatusCodes.Status200OK,API_Const.SWAGGER_RES_DESCR_200, typeof(CountryResponse<List<Country>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, API_Const.SWAGGER_RES_DESCR_400, typeof(CountryResponse<object>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, API_Const.SWAGGER_RES_DESCR_404)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500)]
    public async Task<ActionResult<CountryResponse<List<Country>>>> GetCountries(
        [FromQuery(Name = API_Const.FILTER)][SwaggerParameter(API_Const.QUERY_DESCR, Required = false)]string? _filter,
        [FromQuery(Name = API_Const.SORT)][SwaggerParameter(API_Const.SORT_DESCR, Required = false)] string? _sort,
        [FromQuery(Name = API_Const.INCLUDE)][SwaggerParameter(API_Const.ICLUDE_DESCR, Required = false)] string? _include,
        [FromQuery(Name = API_Const.SIZE)][SwaggerParameter(API_Const.SIZE_DESCR, Required = false)] int _size=10,
        [FromQuery(Name = API_Const.PAGE)][SwaggerParameter(API_Const.PAGE_DESCR, Required = false)] int _page=1,
        [FromQuery(Name = API_Const.MAX)][SwaggerParameter(API_Const.MAX_DESCR, Required = false)] int _max=50
        ) {
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
    
    /// <summary>
    /// GET ONE
    /// </summary>
    [HttpGet("{id:int}")]
    [Produces(API_Const.PRODUCES_JSON)]
    [SwaggerOperation(null, null, Summary = API_Const.GET, Description = API_Const.SWAGGER_OP_DESCR_GET)]
    [SwaggerResponse(StatusCodes.Status200OK,API_Const.SWAGGER_RES_DESCR_200, typeof(CountryResponse<Country>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, API_Const.SWAGGER_RES_DESCR_400, typeof(CountryResponse<object>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, API_Const.SWAGGER_RES_DESCR_404)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500)]
    public async Task<ActionResult<CountryResponse<Country>>> GetCountry(
        [SwaggerParameter(API_Const.ID_DESCR, Required = true)]int id,
        [FromQuery(Name = API_Const.INCLUDE)][SwaggerParameter(API_Const.ICLUDE_DESCR, Required = false)] string? _include)
    {
        return HandleResponse(await CountryManager.GetCountry(Query,id,_include));
    }
    
    /// <summary>
    /// CREATE
    /// </summary>
    [HttpPost]
    [Produces(API_Const.PRODUCES_JSON)]
    [SwaggerOperation(null, null, Summary =API_Const.CREATE, Description = API_Const.SWAGGER_OP_DESCR_CREATE)]
    [SwaggerResponse(StatusCodes.Status201Created,API_Const.SWAGGER_RES_DESCR_200, typeof(CountryResponse<Country>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest,API_Const.SWAGGER_RES_DESCR_400, typeof(CountryResponse<object>))]
    [SwaggerResponse(StatusCodes.Status409Conflict, API_Const.SWAGGER_RES_DESCR_400)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500)]
    public async Task<ActionResult<CountryResponse<Country>>> CreateCountry([FromBody][SwaggerRequestBody(API_Const.BODY_DESCR, Required = true)] CreateCountryDto data)
    {
        return HandleResponse(await CountryManager.CreateCountry(data,Command,Mapper));
    }
    
    /// <summary>
    /// DELETE
    /// </summary>
    [HttpDelete("{id:int}")]
    [Produces(API_Const.PRODUCES_JSON)]
    [SwaggerOperation(null, null, Summary = API_Const.GET, Description = API_Const.SWAGGER_OP_DESCR_GET)]
    [SwaggerResponse(StatusCodes.Status204NoContent,API_Const.SWAGGER_RES_DESCR_204)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, API_Const.SWAGGER_RES_DESCR_400, typeof(CountryResponse<object>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, API_Const.SWAGGER_RES_DESCR_404)]
    [SwaggerResponse(StatusCodes.Status409Conflict, API_Const.SWAGGER_RES_DESCR_409)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500, typeof(CountryResponse<object>))]
    public async Task<IActionResult> DeleteCountry([SwaggerParameter(API_Const.ID_DESCR, Required = true)]int id)
    {
        return HandleResponse(await CountryManager.DeleteCountry(id,Query,Command));
    }

    /// <summary>
    /// UPDATE
    /// </summary>
    [HttpPut("{id:int}")]
    [Produces(API_Const.PRODUCES_JSON)]
    [SwaggerOperation(null, null, Summary = API_Const.UPDATE, Description = API_Const.SWAGGER_OP_DESCR_GET)]
    [SwaggerResponse(StatusCodes.Status200OK,API_Const.SWAGGER_RES_DESCR_200 , typeof(CountryResponse<Country>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, API_Const.SWAGGER_RES_DESCR_400, typeof(CountryResponse<object>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, API_Const.SWAGGER_RES_DESCR_404)]
    [SwaggerResponse(StatusCodes.Status409Conflict, API_Const.SWAGGER_RES_DESCR_409)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500, typeof(CountryResponse<object>))]
    public async Task<ActionResult<CountryResponse<Country>>> UpdateCountry(
        [SwaggerParameter(API_Const.ID_DESCR, Required = true)]int id,
        [FromBody][SwaggerRequestBody(API_Const.BODY_DESCR, Required = true)] CountryDto data)
    {
        return HandleResponse(await CountryManager.UpdateCountry(id,data,Query,Command,Mapper));
    }

    /// <summary>
    /// UPDATE PARTIAL
    /// </summary>
    [HttpPatch("{id:int}")]
    [Produces(API_Const.PRODUCES_JSON)]
    [SwaggerOperation(null, null, Summary = API_Const.UPDATE_PARTIAL, Description = API_Const.SWAGGER_OP_DESCR_GET)]
    [SwaggerResponse(StatusCodes.Status200OK,API_Const.SWAGGER_RES_DESCR_200 , typeof(CountryResponse<Country>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, API_Const.SWAGGER_RES_DESCR_400, typeof(CountryResponse<object>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, API_Const.SWAGGER_RES_DESCR_404)]
    [SwaggerResponse(StatusCodes.Status409Conflict, API_Const.SWAGGER_RES_DESCR_409)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500, typeof(CountryResponse<object>))]
    public async Task<ActionResult<CountryResponse<Country>>> UpdateCountryPartial(
        [SwaggerParameter(API_Const.ID_DESCR, Required = true)]int id,
        [FromBody][SwaggerRequestBody(API_Const.BODY_DESCR, Required = true)] JsonPatchDocument data)
    {
        return HandleResponse(await CountryManager.UpdateCountryPartial(id,data,Query,Command,Mapper));
    }
}