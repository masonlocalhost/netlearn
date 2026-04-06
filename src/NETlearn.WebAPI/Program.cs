using Microsoft.EntityFrameworkCore;
using NETlearn.Application.Interfaces;
using NETlearn.Application.Services;
using NETlearn.Infrastructure.Persistence;
using NETlearn.Infrastructure.Repositories;
using NETlearn.WebAPI.Middlewares;
using NETlearn.WebAPI.Extensions;

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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Fail fast if database is unreachable
// await app.EnsureDatabaseConnectedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseUserInjection();

app.UseRequestCulture();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
