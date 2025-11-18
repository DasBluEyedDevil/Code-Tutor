using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CodeTutor.Native.Data;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for database initialization and management
/// </summary>
public class DatabaseService : IDatabaseService
{
    private readonly CodeTutorDbContext _dbContext;
    private readonly ILogger<DatabaseService>? _logger;

    public DatabaseService(CodeTutorDbContext dbContext, ILogger<DatabaseService>? logger = null)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        try
        {
            _logger?.LogInformation("Initializing database...");

            // Ensure database is created
            var created = await _dbContext.Database.EnsureCreatedAsync();

            if (created)
            {
                _logger?.LogInformation("Database created successfully at: {Path}", GetDatabasePath());
            }
            else
            {
                _logger?.LogInformation("Database already exists at: {Path}", GetDatabasePath());
            }

            // Apply any pending migrations
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                _logger?.LogInformation("Applying {Count} pending migrations...", pendingMigrations.Count());
                await _dbContext.Database.MigrateAsync();
                _logger?.LogInformation("Migrations applied successfully");
            }

            _logger?.LogInformation("Database initialization complete");
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to initialize database");
            throw;
        }
    }

    public async Task<bool> DatabaseExistsAsync()
    {
        try
        {
            return await _dbContext.Database.CanConnectAsync();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to check database connectivity");
            return false;
        }
    }

    public string GetDatabasePath()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var codeTutorPath = Path.Combine(appDataPath, "CodeTutor");
        return Path.Combine(codeTutorPath, "CodeTutor.db");
    }
}
