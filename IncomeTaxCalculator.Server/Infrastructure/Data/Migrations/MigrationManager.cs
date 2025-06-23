

using IncomeTaxCalculator.Server.Application.Common.Models;
using IncomeTaxCalculator.Server.Infrastructure.Data;
using IncomeTaxCalculator.Server.Models;
using Microsoft.EntityFrameworkCore;

public sealed class MigrationManager{
  
    private readonly IServiceProvider _serviceProvider;

    public MigrationManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

     
   

    public async Task<Result<bool>> ApplyMigrationsAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TaxCalculatorDbContext>();

        try
        {
            // Check if database exists and can be connected to
            bool canConnect = await dbContext.Database.CanConnectAsync();
            if (!canConnect)
            {
                // Database doesn't exist or can't connect
                return Result<bool>.Failure("Cannot connect to database");
            }

            // Check if there are pending migrations
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                // Apply migrations only if there are pending changes
                await dbContext.Database.MigrateAsync();
                return Result<bool>.Success(true);
            }
            
            return Result<bool>.Success(false);
        }
        catch (Exception ex)
        {
            // Log or handle the exception appropriately
            // Unable to check or apply migrations
            return Result<bool>.Failure($"Failed to apply migrations: {ex.Message}");
        }
    }
}