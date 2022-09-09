using Hotel_listing.Application.Configurations.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hotel_listing.Application.Exceptions.ValidationResponseFilters;

public class BaseModelStateFilter:ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .Select(v => new BaseError{ ErrorMessage = v.ErrorMessage})
                .ToList();

            var response = new BaseResponse<object, object>()
            {
                Results = null,
                Success = false,
                Errors = errors,
                StatusCode = StatusCodes.Status400BadRequest,
                PageNumber = null,
                PageSize = null,
                Count = null
            };
            
            context.Result = new JsonResult(response)
            {
                StatusCode = 400
            };
        }
        base.OnActionExecuting(context);
    }
}

