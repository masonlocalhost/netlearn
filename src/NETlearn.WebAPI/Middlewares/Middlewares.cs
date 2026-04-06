namespace NETlearn.WebAPI.Middlewares;

public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestCultureMiddleware>();
    }

    public static IApplicationBuilder UseUserInjection(this IApplicationBuilder app)
    {
        return app.UseMiddleware<UserInjectionMiddleware>();
    }
}
