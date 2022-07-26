using AutoMapper;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_listing.Presantation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController<T>:ControllerBase where T:class
{
    protected readonly ICommands Command;
    protected readonly IMapper Mapper;
    protected readonly IQuery Query;
    protected readonly ILogger<T> Logger;
    
    public BaseController(IQuery query,ICommands command,IMapper mapper) : this(query, command,mapper, null!)
    { }
    public BaseController(IQuery query,ICommands command,IMapper mapper, ILogger<T> logger)
    {
        Query = query;
        Command = command;
        Mapper = mapper;
        Logger = logger;
    }
}