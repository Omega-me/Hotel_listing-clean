﻿using AutoMapper;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
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
    public async Task<ActionResult<List<Country>>> GetCountries()
    {
        Logger.LogInformation(TestMethode($"This is from test methode"));
        return HandleResponse(await CountryManager.GetCountries(Query));
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