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
        const int maxAttempts = 2;
        int attempt = 0;

        while (attempt < maxAttempts)
        {
            attempt++;

            try
            {
                _logger?.LogInformation("Initializing database (attempt {Attempt}/{Max})...", attempt, maxAttempts);

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
                return; // Success - exit method
            }
            catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
            {
                _logger?.LogError(ex, "Failed to initialize database (attempt {Attempt}/{Max})", attempt, maxAttempts);

                // If this was the last attempt, rethrow
                if (attempt >= maxAttempts)
                {
                    _logger?.LogCritical("Database initialization failed after {Attempts} attempts", maxAttempts);
                    throw;
                }

                // Attempt recovery: backup corrupt database and delete it
                try
                {
                    var dbPath = GetDatabasePath();
                    if (File.Exists(dbPath))
                    {
                        // Create backup with timestamp
                        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        var backupPath = Path.Combine(
                            Path.GetDirectoryName(dbPath)!,
                            $"CodeTutor_CORRUPT_{timestamp}.db");

                        _logger?.LogWarning("Backing up corrupt database to: {BackupPath}", backupPath);
                        File.Copy(dbPath, backupPath, overwrite: true);

                        // Delete corrupt database and related files
                        _logger?.LogWarning("Deleting corrupt database files...");
                        DeleteDatabaseFiles(dbPath);

                        _logger?.LogInformation("Database recovery attempt complete. Retrying initialization...");
                    }
                }
                catch (Exception recoveryEx)
                {
                    _logger?.LogError(recoveryEx, "Failed to recover from corrupt database");
                    throw; // If recovery fails, rethrow original exception
                }
            }
        }
    }

    private void DeleteDatabaseFiles(string dbPath)
    {
        // Delete main database file
        if (File.Exists(dbPath))
        {
            File.Delete(dbPath);
            _logger?.LogInformation("Deleted: {Path}", dbPath);
        }

        // Delete SQLite journal files
        var journalPath = $"{dbPath}-journal";
        if (File.Exists(journalPath))
        {
            File.Delete(journalPath);
            _logger?.LogInformation("Deleted: {Path}", journalPath);
        }

        // Delete SQLite WAL (Write-Ahead Log) file
        var walPath = $"{dbPath}-wal";
        if (File.Exists(walPath))
        {
            File.Delete(walPath);
            _logger?.LogInformation("Deleted: {Path}", walPath);
        }

        // Delete SQLite SHM (Shared Memory) file
        var shmPath = $"{dbPath}-shm";
        if (File.Exists(shmPath))
        {
            File.Delete(shmPath);
            _logger?.LogInformation("Deleted: {Path}", shmPath);
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
