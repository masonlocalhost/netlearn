using System.Globalization;

namespace WebApp.Presentation.Middlewares;

public class RequestCultureMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var cultureQuery = context.Request.Query["culture"];
        if (!string.IsNullOrWhiteSpace(cultureQuery))
        {
            Console.WriteLine($"culture is: {cultureQuery}");
            var culture = new CultureInfo(cultureQuery);

            CultureInfo.CurrentCulture = culture;
        }

        await next(context);
    }
}