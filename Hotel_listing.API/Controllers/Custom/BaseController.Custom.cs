using Hotel_listing.Application.Common.Response;
using Hotel_listing.Application.DTO.Country;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_listing.API.Controllers;

public partial class BaseController<T> where T:class
{
    protected virtual ActionResult HandleResponse(CountryResponse<List<DapperCountryDTO>> response)
    {
        return ResponseBuilder(response);
    }
}