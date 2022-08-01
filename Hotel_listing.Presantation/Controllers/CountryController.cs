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
    public async Task<ActionResult<List<CountryDTO>>> GetCountries()
    {
        return HandleResponse(CountryManager.Response(await Query.Countries.GetAll()));
    }
        
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CountryDTO>> GetCountry(int id)
    {
        return HandleResponse(CountryManager.Response(await Query.Countries.Get(country => country.CountryId == id, new List<string>() {"Hotels"})));
    }

    [HttpPost]
    public async Task<ActionResult<CountryDTO>> CreateCountry([FromBody] CreateCountryDTO data)
    {
        // return HandleResponse(CountryManager.Response(await CountryManager.CreateCountry(data,Command,Mapper)));
        return HandleResponse(await CountryManager.CreateCountry(data,Command,Mapper));
    }
    
    [HttpPost("insertrange")]
    public async Task<IActionResult> CreateCountries([FromBody] IList<CreateCountryDTO> data)
    {
        return HandleResponse(CountryManager.Response(await CountryManager.CreateCountries(data,Command,Mapper)));
    }
}