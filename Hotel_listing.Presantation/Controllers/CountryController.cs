using AutoMapper;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Options;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Application.DTO.Country;
using Hotel_listing.Application.Exceptions.ValidationResponseFilters;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Presantation.Managers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_listing.Presantation.Controllers;

[CountryModelStateFilter]
public class CountryController:BaseController<Country>
{
    public CountryController(IQuery query, ICommands command,IMapper mapper,ILogger<Country> logger) 
        : base(query,command,mapper,logger)
    { }

    [HttpGet]
    public async Task<ActionResult<List<Country>>> GetCountries([FromQuery] int _size=10, int _page=1, int _max=50)
    {
        // Logger.LogInformation(TestMethode("Test"));
        return HandleResponse(await CountryManager.GetCountries(Query,new QueryOptions<Country>()
        {
            Pagination = new PaginationOptions()
            {
                PageNumber = _page,
                PageSize = _size,
                MaxPageSize = _max
            }
        }));
    }
        
    [HttpGet("{id:int}")]
    public async Task<ActionResult<CountryDto>> GetCountry(int id)
    {
        return HandleResponse(await CountryManager.GetCountry(id, Query));
    }

    [HttpPost]
    public async Task<ActionResult<CountryDto>> CreateCountry([FromBody] CreateCountryDto data)
    {
        return HandleResponse(await CountryManager.CreateCountry(data,Command,Mapper));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        return HandleResponse(await CountryManager.DeleteCountry(id,Query,Command));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCountries([FromBody]List<int> ids)
    {
        return HandleResponse(await CountryManager.DeleteCountries(ids,Query,Command));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CountryDto>> UpdateCountry(int id,[FromBody] CountryDto data)
    {
        return HandleResponse(await CountryManager.UpdateCountry(id,data,Query,Command,Mapper));
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult<CountryDto>> UpdateCountryPartial(int id, [FromBody] JsonPatchDocument data)
    {
        return HandleResponse(await CountryManager.UpdateCountryPartial(id,data,Query,Command,Mapper));
    }
}