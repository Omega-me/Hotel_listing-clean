using AutoMapper;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Presantation.Manager;
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
        return HandleResponse(CountryManager.Response(await Query.Countries.GetAll()));
    }
        
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        return HandleResponse(CountryManager.Response(await Query.Countries.Get(country => country.CountryId == id, new List<string>() {"Hotels"})));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromBody] Country country)
    {
        await Command.Countries.Insert(country);
        await Command.Save();
        return HandleResponse(
            CountryManager.Response(await Query.Countries.Get(c => c.CountryId == country.CountryId)));
    }
}