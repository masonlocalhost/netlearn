namespace NETlearn.WebAPI.Middlewares;

public class UserInjectionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Items["UserId"] = "1234";

        await next(context);
    }
}
