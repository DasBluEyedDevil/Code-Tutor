using System.Threading.Tasks;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for database initialization and management
/// </summary>
public interface IDatabaseService
{
    /// <summary>
    /// Initialize the database (create if not exists, apply migrations)
    /// </summary>
    Task InitializeAsync();

    /// <summary>
    /// Check if the database exists and is accessible
    /// </summary>
    Task<bool> DatabaseExistsAsync();

    /// <summary>
    /// Get the path to the database file
    /// </summary>
    string GetDatabasePath();
}
