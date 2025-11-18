using Microsoft.EntityFrameworkCore;
using CodeTutor.Native.Data;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Tests.Fixtures;

/// <summary>
/// Test fixture for creating in-memory database contexts
/// </summary>
public class DatabaseFixture : IDisposable
{
    private readonly List<CodeTutorDbContext> _contexts = new();
    private bool _disposed;

    /// <summary>
    /// Creates a new in-memory database context with a unique database name
    /// </summary>
    public CodeTutorDbContext CreateContext(bool seedData = true)
    {
        var options = new DbContextOptionsBuilder<CodeTutorDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        var context = new CodeTutorDbContext(options);
        context.Database.EnsureCreated();

        if (seedData)
        {
            SeedDefaultUser(context);
        }

        _contexts.Add(context);
        return context;
    }

    /// <summary>
    /// Seeds the default test user
    /// </summary>
    private void SeedDefaultUser(CodeTutorDbContext context)
    {
        // Check if user already exists (shouldn't in fresh in-memory DB, but be safe)
        if (!context.Users.Any(u => u.Id == "00000000-0000-0000-0000-000000000001"))
        {
            context.Users.Add(new User
            {
                Id = "00000000-0000-0000-0000-000000000001",
                Name = "Test User",
                Email = "test@example.com",
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = DateTime.UtcNow
            });

            context.SaveChanges();
        }
    }

    /// <summary>
    /// Seeds sample progress data for testing
    /// </summary>
    public void SeedProgress(CodeTutorDbContext context, UserProgress progress)
    {
        context.Progress.Add(progress);
        context.SaveChanges();
    }

    /// <summary>
    /// Seeds sample streak data for testing
    /// </summary>
    public void SeedStreaks(CodeTutorDbContext context, params Streak[] streaks)
    {
        context.Streaks.AddRange(streaks);
        context.SaveChanges();
    }

    /// <summary>
    /// Seeds sample achievement data for testing
    /// </summary>
    public void SeedAchievements(CodeTutorDbContext context, params Achievement[] achievements)
    {
        context.Achievements.AddRange(achievements);
        context.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                foreach (var context in _contexts)
                {
                    context.Database.EnsureDeleted();
                    context.Dispose();
                }
                _contexts.Clear();
            }
            _disposed = true;
        }
    }
}
