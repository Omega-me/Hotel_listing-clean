using Hotel_listing.Application.Configurations.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hotel_listing.Application.Validation;

public class ModelStateFilter:ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage)
                .ToList();

            var response = new BaseResponse<object, object>()
            {
                Data = null,
                Success = false,
                Errors = errors,
                StatusCode = StatusCodes.Status400BadRequest,
                Results = 0
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = 400
            };
        }
        base.OnActionExecuting(context);
    }
}