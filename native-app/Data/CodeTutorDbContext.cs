using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Data;

/// <summary>
/// Entity Framework Core database context for Code Tutor
/// Manages all database operations and entity configurations
/// </summary>
public class CodeTutorDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserProgress> Progress { get; set; } = null!;
    public DbSet<Achievement> Achievements { get; set; } = null!;
    public DbSet<Streak> Streaks { get; set; } = null!;
    public DbSet<CodeHistory> CodeHistory { get; set; } = null!;
    public DbSet<KeyboardShortcut> KeyboardShortcuts { get; set; } = null!;
    public DbSet<Statistic> Statistics { get; set; } = null!;

    public CodeTutorDbContext(DbContextOptions<CodeTutorDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var dbPath = GetDatabasePath();
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }

    private static string GetDatabasePath()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var codeTutorPath = Path.Combine(appDataPath, "CodeTutor");
        Directory.CreateDirectory(codeTutorPath);
        return Path.Combine(codeTutorPath, "CodeTutor.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();

            // Configure relationships
            entity.HasMany(e => e.Progress)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Achievements)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Streaks)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.CodeHistory)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.KeyboardShortcuts)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Statistics)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure UserProgress entity
        modelBuilder.Entity<UserProgress>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.CourseId }).HasDatabaseName("idx_progress_user_course");
            entity.HasIndex(e => new { e.Completed, e.CompletedAt }).HasDatabaseName("idx_progress_completed");
            entity.HasIndex(e => e.LastAttemptAt).HasDatabaseName("idx_progress_last_attempt");

            // Unique constraint for progress record
            entity.HasIndex(e => new { e.UserId, e.CourseId, e.ModuleId, e.LessonId, e.ChallengeId })
                .IsUnique()
                .HasDatabaseName("idx_progress_unique");
        });

        // Configure Achievement entity
        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId).HasDatabaseName("idx_achievements_user");
            entity.HasIndex(e => e.AchievementType).HasDatabaseName("idx_achievements_type");
            entity.HasIndex(e => e.Notified)
                .HasDatabaseName("idx_achievements_notified")
                .HasFilter("[Notified] = 0");

            // Unique constraint
            entity.HasIndex(e => new { e.UserId, e.AchievementType })
                .IsUnique()
                .HasDatabaseName("idx_achievement_unique");
        });

        // Configure Streak entity
        modelBuilder.Entity<Streak>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.Date }).HasDatabaseName("idx_streaks_user_date");

            // Unique constraint
            entity.HasIndex(e => new { e.UserId, e.Date })
                .IsUnique()
                .HasDatabaseName("idx_streak_unique");
        });

        // Configure CodeHistory entity
        modelBuilder.Entity<CodeHistory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.CourseId, e.ModuleId, e.LessonId, e.ChallengeId, e.SavedAt })
                .HasDatabaseName("idx_code_history_lookup");
        });

        // Configure KeyboardShortcut entity
        modelBuilder.Entity<KeyboardShortcut>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId).HasDatabaseName("idx_shortcuts_user");

            // Unique constraints
            entity.HasIndex(e => new { e.UserId, e.ActionName })
                .IsUnique()
                .HasDatabaseName("idx_shortcut_action_unique");

            entity.HasIndex(e => new { e.UserId, e.KeyCombination })
                .IsUnique()
                .HasDatabaseName("idx_shortcut_key_unique");
        });

        // Configure Statistic entity
        modelBuilder.Entity<Statistic>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId).HasDatabaseName("idx_statistics_user");

            // Unique constraint
            entity.HasIndex(e => new { e.UserId, e.StatName })
                .IsUnique()
                .HasDatabaseName("idx_statistic_unique");
        });

        // Seed default user
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = "00000000-0000-0000-0000-000000000001",
            Name = "Desktop User",
            Email = null,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            LastLoginAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });

        // Seed default keyboard shortcuts
        modelBuilder.Entity<KeyboardShortcut>().HasData(
            new KeyboardShortcut { Id = 1, UserId = "00000000-0000-0000-0000-000000000001", ActionName = "CommandPalette", KeyCombination = "Ctrl+K", IsEnabled = true },
            new KeyboardShortcut { Id = 2, UserId = "00000000-0000-0000-0000-000000000001", ActionName = "RunCode", KeyCombination = "Ctrl+Enter", IsEnabled = true },
            new KeyboardShortcut { Id = 3, UserId = "00000000-0000-0000-0000-000000000001", ActionName = "SaveProgress", KeyCombination = "Ctrl+S", IsEnabled = true },
            new KeyboardShortcut { Id = 4, UserId = "00000000-0000-0000-0000-000000000001", ActionName = "ToggleHints", KeyCombination = "Ctrl+/", IsEnabled = true },
            new KeyboardShortcut { Id = 5, UserId = "00000000-0000-0000-0000-000000000001", ActionName = "ResetChallenge", KeyCombination = "Ctrl+R", IsEnabled = true },
            new KeyboardShortcut { Id = 6, UserId = "00000000-0000-0000-0000-000000000001", ActionName = "OpenSettings", KeyCombination = "Ctrl+.", IsEnabled = true },
            new KeyboardShortcut { Id = 7, UserId = "00000000-0000-0000-0000-000000000001", ActionName = "Help", KeyCombination = "F1", IsEnabled = true }
        );

        // Seed default statistics
        modelBuilder.Entity<Statistic>().HasData(
            new Statistic { Id = 1, UserId = "00000000-0000-0000-0000-000000000001", StatName = "TotalLessonsCompleted", StatValue = 0, LastUpdated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Statistic { Id = 2, UserId = "00000000-0000-0000-0000-000000000001", StatName = "TotalChallengesCompleted", StatValue = 0, LastUpdated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Statistic { Id = 3, UserId = "00000000-0000-0000-0000-000000000001", StatName = "TotalTestsPassed", StatValue = 0, LastUpdated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Statistic { Id = 4, UserId = "00000000-0000-0000-0000-000000000001", StatName = "TotalCodeExecutions", StatValue = 0, LastUpdated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Statistic { Id = 5, UserId = "00000000-0000-0000-0000-000000000001", StatName = "CurrentStreak", StatValue = 0, LastUpdated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
            new Statistic { Id = 6, UserId = "00000000-0000-0000-0000-000000000001", StatName = "LongestStreak", StatValue = 0, LastUpdated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
        );
    }
}
