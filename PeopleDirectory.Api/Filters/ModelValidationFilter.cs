using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PeopleDirectory.Api.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values
                    .SelectMany(a => a.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var problemDetails = new ValidationProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Failed",
                    Detail = "One or more validation errors occurred.",
                    Instance = context.HttpContext.Request.Path,
                    Extensions = { ["errors"] = errors }
                };

                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
