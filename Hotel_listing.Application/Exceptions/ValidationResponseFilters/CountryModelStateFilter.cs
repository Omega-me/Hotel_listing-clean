using Hotel_listing.Application.Common.Response;
using Hotel_listing.Domain.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hotel_listing.Application.Exceptions.ValidationResponseFilters;
public class CountryModelStateFilter : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .Select(v => new BaseError{ ErrorMessage = v.ErrorMessage})
                .ToList();

            var response = new CountryResponse<Country>()
            {
                Token = null,
                Results = null,
                Success = false,
                Errors = errors,
                StatusCode = StatusCodes.Status422UnprocessableEntity,
                PageNumber = null,
                PageSize = null,
                Count = null
            };
            
            context.Result = new JsonResult(response)
            {
                StatusCode = StatusCodes.Status422UnprocessableEntity
            };
        }
        base.OnResultExecuting(context);
    }
}