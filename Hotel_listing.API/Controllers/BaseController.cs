﻿using AutoMapper;
using Hotel_listing.Application.Common.Response;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Infrastructure.RepositoryManager.DataAccessor;
using Hotel_listing.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Hotel_listing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public partial class BaseController<T>:ControllerBase where T:class
{
    protected readonly ICommands Command;
    protected readonly IQuery Query;
    protected readonly IMapper Mapper;
    protected readonly IHttpContextAccessor Context;
    protected readonly IDataAccessor Db;
    protected readonly DatabaseContext DataContext;

    public BaseController(IQuery query, ICommands command, IMapper mapper, IHttpContextAccessor context,DatabaseContext dataContext,IDataAccessor db)
    {
        Query = query;
        Command = command;
        Mapper = mapper;
        Context = context;
        DataContext = dataContext;
        Db = db;
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
                return Created("",response); 
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
    protected virtual ActionResult HandleResponse(BaseResponse<IPagedList<T>,BaseError> response)
    {
        return ResponseBuilder(response);
    }
    protected virtual ActionResult HandleResponse(BaseResponse<T,BaseError> response)
    {
        return ResponseBuilder(response);
    }
    protected virtual ActionResult HandleResponse(CountryResponse<IPagedList<Country>> response)
    {
        return ResponseBuilder(response);
    }
    protected virtual ActionResult HandleResponse(CountryResponse<Country> response)
    {
        return ResponseBuilder(response);
    }
    #endregion
}