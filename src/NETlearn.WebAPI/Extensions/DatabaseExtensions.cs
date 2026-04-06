using Microsoft.EntityFrameworkCore;
using NETlearn.Infrastructure.Persistence;

namespace NETlearn.WebAPI.Extensions;

public static class DatabaseExtensions
{
    /// <summary>
    /// Ensures that the database connection is available before the host starts.
    /// This follows the "fail fast" principle to prevent the app from running in an unstable state.
    /// </summary>
    public static async Task EnsureDatabaseConnectedAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();

        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            
            // Note: You could also run context.Database.MigrateAsync() here if needed.
            if (!await context.Database.CanConnectAsync())
            {
                throw new Exception("The database is unreachable. Please ensure your PostgreSQL instance is running and the connection string is correct.");
            }
            
            logger.LogInformation("Database connection verified successfully.");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "CRITICAL: Could not verify database connection on startup.");
            throw; 
        }
    }
}
