using AutoMapper;
using Hotel_listing.Application.Configurations.Response;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Domain.Entitites;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_listing.Presantation.Controllers;

public class CountryController:BaseController<Country>
{
    public CountryController(IQuery query, ICommands command,IMapper mapper) 
        : base(query, command,mapper)
    { }

    [HttpGet]
    public async Task<ActionResult<List<Country>>> GetCountries()
    {
        IList<Country> country = await Query.Countries.GetAll(null,null,new List<string>{"Hotels"});
        return HandleResponse(new CountryResponse().BuildResult<CountryResponse>(result =>
        {
            result.StatusCode = StatusCodes.Status200OK;
            result.Results = country.Count;
            result.Success = true;
            result.Token = "6225DCE5-59C6-4657-A8C0-7AFC87E6B9D4";
            result.Errors = null;
            result.Data = country;
        }));
    }
        
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        var country = await Query.Countries.Get(country => country.CountryId == id, new List<string>() {"Hotels"});
        if (country is null)
        {
            return HandleResponse(new BaseResponse<object, object>()
                .BuildResult<BaseResponse<object,object>>(option=>
                {
                    option.Success = false;
                    option.StatusCode = StatusCodes.Status404NotFound;
                }));
        }
        return HandleResponse(new BaseResponse<object, object>()
            .BuildResult<BaseResponse<object,object>>(option=>
        {
            option.Data = country;
            option.Errors = null;
            option.Results = 1;
            option.Success = true;
            option.StatusCode = StatusCodes.Status200OK;
        }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromBody] Country country)
    {
        await Command.Countries.Insert(country);
        await Command.Save();
        return Created("",null);
    }
}