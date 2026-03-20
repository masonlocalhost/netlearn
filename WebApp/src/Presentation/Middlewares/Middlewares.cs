namespace WebApp.Presentation.Middlewares;

public static class MyCustomMiddlewareExtensions
{
    public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            // Logic before
            Console.WriteLine($"[LOG] Request: {context.Request.Method} {context.Request.Path}");

            await next();

            // Logic after
            Console.WriteLine($"[LOG] Response Status: {context.Response.StatusCode}");
        });
    }
}
