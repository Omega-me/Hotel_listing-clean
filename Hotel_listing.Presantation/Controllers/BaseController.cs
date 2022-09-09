using AutoMapper;
using Hotel_listing.Application.Configurations.Response;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Domain.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_listing.Presantation.Controllers;

[Route("api/[controller]")]
[ApiController]
public partial class BaseController<T>:ControllerBase where T:class
{
    protected readonly ICommands Command;
    protected readonly IQuery Query;
    protected readonly IMapper Mapper;
    public BaseController(IQuery query,ICommands command,IMapper mapper)
    {
        Query = query;
        Command = command;
        Mapper = mapper;
    }

    // A set of virtual methods here which can be override on the children classes 
    #region HandleResponse overloaded methods
    protected virtual ActionResult ResponseBuilder(dynamic response)
    {
        switch (response.StatusCode)
        {
            case StatusCodes.Status200OK:
                return Ok(response); 
            case StatusCodes.Status201Created:
                return Created("",response.Results); 
            case StatusCodes.Status204NoContent:
                return NoContent(); 
            case StatusCodes.Status400BadRequest:
                return BadRequest(response);
            case StatusCodes.Status401Unauthorized:
                return Unauthorized(response);
            case StatusCodes.Status403Forbidden:
                return Forbid(response);
            case StatusCodes.Status404NotFound:
                return NotFound(response);
            case StatusCodes.Status409Conflict:
                return Conflict(response);
            default:
                return Ok(response);
        }
    }
    protected virtual ActionResult HandleResponse(BaseResponse<List<T>,BaseError> response)
    {
        return ResponseBuilder(response);
    }
    protected virtual ActionResult HandleResponse(BaseResponse<T,BaseError> response)
    {
        return ResponseBuilder(response);
    }
    protected virtual ActionResult HandleResponse(CountryResponse<List<Country>> response)
    {
        return ResponseBuilder(response);
    }
    protected virtual ActionResult HandleResponse(CountryResponse<Country> response)
    {
        return ResponseBuilder(response);
    }
    #endregion
}