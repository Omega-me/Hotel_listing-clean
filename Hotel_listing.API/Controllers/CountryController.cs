using System.Dynamic;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using Hotel_listing.API.Common;
using Hotel_listing.API.Managers;
using Hotel_listing.Application.Common.Features;
using Hotel_listing.Application.Common.RepositoryOptions;
using Hotel_listing.Application.Common.Response;
using Hotel_listing.Application.Contracts.DataShaper;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Application.Exceptions;
using Hotel_listing.Application.Exceptions.ValidationResponseFilters;
using Hotel_listing.Domain.Entitites;

namespace Hotel_listing.API.Controllers;

[CountryModelStateFilter]
public class CountryController:BaseController<Country>
{
    public CountryController(IQuery query, ICommands command, IMapper mapper, IHttpContextAccessor context,IDataShaper<Country> dataShaper) 
        : base(query,command,mapper,context,dataShaper)
    { }

    /// <summary>
    /// GET ALL
    /// </summary>p
    [HttpGet]
    [HttpHead]
    [Produces(API_Const.PRODUCES_JSON,new []{API_Const.PRODUCES_XML,"text/xml"})]
    [SwaggerOperation(null, null, Summary = API_Const.GET_ALL, Description = API_Const.SWAGGER_OP_DESCR_GETALL)]
    [SwaggerResponse(StatusCodes.Status200OK,API_Const.SWAGGER_RES_DESCR_200, typeof(CountryResponse<List<Country>>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, API_Const.SWAGGER_RES_DESCR_400, typeof(CountryResponse<object>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500, typeof(AppException))]
    public async Task<ActionResult<CountryResponse<List<ExpandoObject>>>> GetAll(
        [FromQuery(Name = API_Const.FILTER)][SwaggerParameter(API_Const.QUERY_DESCR, Required = false)]string? @filter,
        [FromQuery(Name = API_Const.SORT)][SwaggerParameter(API_Const.SORT_DESCR, Required = false)] string? @sort,
        [FromQuery(Name = API_Const.FIELDS)][SwaggerParameter(API_Const.FIELDS_DESCR, Required = false)] string? @fields,
        [FromQuery(Name = API_Const.INCLUDE)][SwaggerParameter(API_Const.INCLUDE_DESCR, Required = false)] string? @include,
        [FromQuery(Name = API_Const.SIZE)][SwaggerParameter(API_Const.SIZE_DESCR, Required = false)] int @size=10,
        [FromQuery(Name = API_Const.PAGE)][SwaggerParameter(API_Const.PAGE_DESCR, Required = false)] int @page=1,
        [FromQuery(Name = API_Const.MAX)][SwaggerParameter(API_Const.MAX_DESCR, Required = false)] int @max=50
        ) {
        return HandleResponse(await CountryManager.GetAll(Query,Mapper,Context,DataShaper,new Features<Country>()
        {
            Sort = @sort,
            Includes = @include,
            Filter = @filter,
            Fields = @fields,
            Pagination = new PaginationParams()
            {
                PageNumber = @page,
                PageSize = @size,
                MaxPageSize = @max
            },
            OrderBy = x=>x.OrderBy(y=>y.Id)
        }));
    }

    [HttpPost("filter")]
    public ActionResult<CountryResponse<List<Country>>> GetWithFilters()
    {
        return HandleResponse(CountryManager.GetWithFilters());
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
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500,typeof(AppException))]
    public async Task<ActionResult<CountryResponse<ExpandoObject>>> Get(
        [SwaggerParameter(Required = true)]int id,
        [FromQuery(Name = API_Const.INCLUDE)][SwaggerParameter(API_Const.INCLUDE_DESCR, Required = false)] string? @include,
        [FromQuery(Name = API_Const.FIELDS)][SwaggerParameter(API_Const.FIELDS_DESCR, Required = false)] string? @fields)
    {
        return HandleResponse(await CountryManager.Get(Query,DataShaper,id,@include,@fields));
    }
    
    /// <summary>
    /// CREATE
    /// </summary>
    [HttpPost]
    [Produces(API_Const.PRODUCES_JSON)]
    [SwaggerOperation(null, null, Summary =API_Const.CREATE, Description = API_Const.SWAGGER_OP_DESCR_CREATE)]
    [SwaggerResponse(StatusCodes.Status201Created,API_Const.SWAGGER_RES_DESCR_201, typeof(CountryResponse<CountryDto>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest,API_Const.SWAGGER_RES_DESCR_400, typeof(CountryResponse<object>))]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity,API_Const.SWAGGER_RES_DESCR_422)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500,typeof(AppException))]
    public async Task<ActionResult<CountryResponse<Country>>> Create([FromBody][SwaggerRequestBody(Required = true)] Country @data)
    {
        return HandleResponse(await CountryManager.Create(@data, Command, Mapper));
    }
    
    /// <summary>
    /// DELETE
    /// </summary>
    [HttpDelete("{id:int}")]
    [Produces(API_Const.PRODUCES_JSON)]
    [SwaggerOperation(null, null, Summary = API_Const.DELETE, Description = API_Const.SWAGGER_OP_DESCR_DELETE)]
    [SwaggerResponse(StatusCodes.Status204NoContent,API_Const.SWAGGER_RES_DESCR_204)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, API_Const.SWAGGER_RES_DESCR_400, typeof(CountryResponse<object>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, API_Const.SWAGGER_RES_DESCR_404)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity,API_Const.SWAGGER_RES_DESCR_422)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500, typeof(AppException))]
    public async Task<IActionResult> Delete([SwaggerParameter(Required = true)]int id)
    {
        return HandleResponse(await CountryManager.Delete(id,Query,Command));
    }

    /// <summary>
    /// UPDATE
    /// </summary>
    [HttpPut("{id:int}")]
    [Produces(API_Const.PRODUCES_JSON)]
    [SwaggerOperation(null, null, Summary = API_Const.UPDATE, Description = API_Const.SWAGGER_OP_DESCR_UPDATE)]
    [SwaggerResponse(StatusCodes.Status200OK,API_Const.SWAGGER_RES_DESCR_200 , typeof(CountryResponse<Country>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, API_Const.SWAGGER_RES_DESCR_400, typeof(CountryResponse<object>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, API_Const.SWAGGER_RES_DESCR_404)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity,API_Const.SWAGGER_RES_DESCR_422)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500, typeof(AppException))]
    public async Task<ActionResult<CountryResponse<Country>>> Update(
        [SwaggerParameter(Required = true)]int id,
        [FromBody][SwaggerRequestBody(Required = true)] Country @data)
    {
        return HandleResponse(await CountryManager.Update(id,@data,Query,Command,Mapper));
    }

    /// <summary>
    /// UPDATE PARTIAL
    /// </summary>
    [HttpPatch("{id:int}")]
    [Produces(API_Const.PRODUCES_JSON)]
    [SwaggerOperation(null, null, Summary = API_Const.UPDATE_PARTIAL, Description = API_Const.SWAGGER_OP_DESCR_UPDATE_PARTIAL)]
    [SwaggerResponse(StatusCodes.Status200OK,API_Const.SWAGGER_RES_DESCR_200 , typeof(CountryResponse<Country>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, API_Const.SWAGGER_RES_DESCR_400, typeof(CountryResponse<object>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, API_Const.SWAGGER_RES_DESCR_404)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity,API_Const.SWAGGER_RES_DESCR_422)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, API_Const.SWAGGER_RES_DESCR_500, typeof(AppException))]
    public async Task<ActionResult<CountryResponse<Country>>> UpdatePartial(
        [SwaggerParameter(Required = true)]int id,
        [FromBody][SwaggerRequestBody(Required = true)] JsonPatchDocument @data)
    {
        return HandleResponse(await CountryManager.UpdatePartial(id,@data,Query,Command,Mapper));
    }

    /// <summary>
    /// OPTIONS
    /// </summary>
    [HttpOptions]
    [SwaggerOperation(null, null, Summary = API_Const.OPTIONS, Description = API_Const.SWAGGER_OP_DESCR_OPTIONS)]
    public IActionResult Options()
    {
        CountryManager.Options(Context);
        return Ok();
    }
}