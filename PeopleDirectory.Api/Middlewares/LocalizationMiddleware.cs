using System.Globalization;

namespace PeopleDirectory.Api.Middlewares
{
    public class LocalizationMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            var acceptLanguage = context.Request.Headers["Accept-Language"].ToString();
            if (!string.IsNullOrEmpty(acceptLanguage))
            {
                var culture = new CultureInfo(acceptLanguage.Split(',').First());
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

            await next(context);
        }
    }
}
