using AutoMapper;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Presantation.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_listing.Presantation.Controllers;

public class CountryController:BaseController<Country>
{
    public CountryController(IQuery query, ICommands command,IMapper mapper) 
        : base(query,command,mapper)
    { }

    [HttpGet]
    public async Task<ActionResult<List<Country>>> GetCountries()
    {
        return HandleResponse(await CountryManager.GetCountries(Query));
    }
        
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        return HandleResponse(await CountryManager.GetCountry(id, Query));
    }

    [HttpPost]
    public async Task<ActionResult<Country>> CreateCountry([FromBody] CreateCountryDTO data)
    {
        return HandleResponse(await CountryManager.CreateCountry(data,Command,Mapper));
    }
    
    [HttpPost("insertrange")]
    public async Task<ActionResult<List<Country>>> CreateCountries([FromBody] IList<CreateCountryDTO> data)
    {
        return HandleResponse(await CountryManager.CreateCountries(data,Command,Mapper));
    }
}