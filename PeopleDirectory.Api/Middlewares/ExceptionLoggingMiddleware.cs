using PeopleDirectory.BusinessLogic.Exceptions;
using PeopleDirectory.DataAccess.Exceptions;
using PeopleDirectory.DataContracts.Resources;

namespace PeopleDirectory.Api.Middlewares
{
    public class ExceptionLoggingMiddleware
        (RequestDelegate next, 
        ILogger<ExceptionLoggingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (BusinessException ex)
            {
                logger.LogWarning(ex, "Business exception occurred: {Message}", ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var response = new
                {
                    error = ex.Message,
                };

                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
            }
            catch (DataException ex)
            {
                logger.LogWarning(ex, "Data exception occurred: {Message}", ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var response = new
                {
                    error = ex.Message,
                };

                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = new
                {
                    error = ValidationMessages.GetMessage("UnexpectedErrorMessage")
                };

                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
            }
        }
    }
}
