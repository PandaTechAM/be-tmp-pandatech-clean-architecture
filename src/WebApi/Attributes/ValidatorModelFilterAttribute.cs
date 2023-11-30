using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Attributes;

public class ValidatorModelFilterAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Action for checking model state
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errors = context.ModelState.Values.Where(v => v.Errors.Any())
            .SelectMany(v => v.Errors)
            .Select(v => v.ErrorMessage)
            .ToList();

        var errorResponses = errors
            .Select(errorMessage => new ProblemDetails() { Detail = errorMessage, Status = 400 }).ToList();

        context.Result = new JsonResult(errorResponses) { StatusCode = 400 };
    }
}