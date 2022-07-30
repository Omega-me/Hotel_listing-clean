using AutoMapper;
using Hotel_listing.Application.Configurations.Response;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace Hotel_listing.Presantation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController<T>:ControllerBase where T:class
{
    protected readonly ICommands Command;
    protected readonly IQuery Query;
    protected readonly IMapper Mapper;
    protected readonly ILogger<T> Logger;
    public BaseController(IQuery query,ICommands command,IMapper mapper) : this(query, command,mapper,null!)
    { }
    public BaseController(IQuery query,ICommands command,IMapper mapper,ILogger<T> logger)
    {
        Query = query;
        Command = command;
        Mapper = mapper;
        Logger = logger;
    }

    // A set of virtual methods here which can be override on the children classes 
    #region HandleResponse overloaded methods
    protected virtual ActionResult HandleResponse(BaseResponse<object,object> response)
    {
        switch (response.StatusCode)
        {
            case StatusCodes.Status200OK:
                return Ok(response); 
            case StatusCodes.Status201Created:
                return Created("",response.Data); 
            case StatusCodes.Status204NoContent:
                return NoContent(); 
            case StatusCodes.Status400BadRequest:
                return BadRequest(response.Errors);
            case StatusCodes.Status401Unauthorized:
                return Unauthorized(response.Errors);
            case StatusCodes.Status403Forbidden:
                return Forbid();
            case StatusCodes.Status404NotFound:
                return NotFound();
            case StatusCodes.Status409Conflict:
                return Conflict();
            default:
                return Ok(response);
        }
    }
    protected virtual ActionResult HandleResponse(CountryResponse response)
    {
        if (response.Token is null)
        {
            return BadRequest();
        }
        switch (response.StatusCode)
        {
            case StatusCodes.Status200OK:
                return Ok(response); 
            case StatusCodes.Status201Created:
                return Created("",response.Data); 
            case StatusCodes.Status204NoContent:
                return NoContent(); 
            case StatusCodes.Status400BadRequest:
                return BadRequest(response.Errors);
            case StatusCodes.Status401Unauthorized:
                return Unauthorized(response.Errors);
            case StatusCodes.Status403Forbidden:
                return Forbid();
            case StatusCodes.Status404NotFound:
                return NotFound();
            case StatusCodes.Status409Conflict:
                return Conflict();
            default:
                return Ok(response);
        }
    }
    #endregion
}