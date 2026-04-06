using Microsoft.EntityFrameworkCore;
using NETlearn.Application.Interfaces;
using NETlearn.Application.Services;
using NETlearn.Infrastructure.Persistence;
using NETlearn.Infrastructure.Repositories;
using NETlearn.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add EF Core and PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresqlConnection"));
});

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

var app = builder.Build();

// Verify database connection on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        if (!await context.Database.CanConnectAsync())
        {
            throw new Exception("Could not connect to the database.");
        }
        app.Logger.LogInformation("Successfully connected to the database.");
    }
    catch (Exception ex)
    {
        app.Logger.LogCritical(ex, "An error occurred while connecting to the database on startup.");
        throw; 
    }
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

app.UseUserInjection();

app.UseRequestCulture();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
