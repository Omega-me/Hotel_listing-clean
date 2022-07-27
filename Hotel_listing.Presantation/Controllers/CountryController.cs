using AutoMapper;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Domain.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_listing.Presantation.Controllers;

public class CountryController:BaseController<Country>
{
    public CountryController(IQuery query, ICommands command,IMapper mapper ,ILogger<Country> logger) : base(query, command,mapper,logger)
    { }

    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        string test = Test("");
        Logger.LogInformation(test);
        return Ok(await Query.Countries.GetAll());
    }
        
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCountry(int id)
    {
        return Ok(await Query.Countries.Get(country =>country.CountryId==id , new List<string>(){"Hotels"}));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromBody] Country country)
    {
        await Command.Countries.Insert(country);
        await Command.Save();
        return NoContent();
    }
}