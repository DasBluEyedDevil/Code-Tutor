# Database Schema & State Management

## Overview

This document defines the complete data persistence strategy for the native application, including SQLite database schema and settings file structure.

---

## Persistence Strategy

### SQLite Database (`CodeTutor.db`)
**Location:** `AppData/CodeTutor/CodeTutor.db`

**Purpose:** Store user progress, achievements, streaks, and code history

**Why SQLite:**
- Serverless (no network required)
- ACID compliant (data integrity)
- Fast queries with indexes
- Cross-platform compatible
- No dependencies

---

### Settings File (`settings.json`)
**Location:** `AppData/CodeTutor/settings.json`

**Purpose:** Store user preferences and configuration

**Why JSON:**
- Human-readable
- Easy to edit manually
- Lightweight
- Version control friendly

---

## Database Schema

### Complete SQL Schema

```sql
-- Enable foreign keys
PRAGMA foreign_keys = ON;

-- ===================================================================
-- USERS TABLE
-- ===================================================================
-- Desktop app is single-user, but structured for potential cloud sync
CREATE TABLE Users (
    Id TEXT PRIMARY KEY,                    -- GUID
    Name TEXT NOT NULL,
    Email TEXT,
    CreatedAt TEXT NOT NULL,                -- ISO 8601 format
    LastLoginAt TEXT NOT NULL,
    CONSTRAINT chk_created_at CHECK (CreatedAt IS datetime(CreatedAt)),
    CONSTRAINT chk_last_login CHECK (LastLoginAt IS datetime(LastLoginAt))
);

-- Default user for desktop app
INSERT INTO Users (Id, Name, Email, CreatedAt, LastLoginAt)
VALUES ('00000000-0000-0000-0000-000000000001', 'Desktop User', NULL, datetime('now'), datetime('now'));

-- ===================================================================
-- PROGRESS TABLE
-- ===================================================================
-- Tracks completion and scores for lessons and challenges
CREATE TABLE Progress (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    CourseId TEXT NOT NULL,                 -- e.g., "python"
    ModuleId TEXT NOT NULL,                 -- e.g., "1-basics"
    LessonId TEXT NOT NULL,                 -- e.g., "1-1"
    ChallengeId TEXT,                       -- NULL if lesson-level progress
    Score INTEGER NOT NULL DEFAULT 0,       -- 0-100
    MaxScore INTEGER NOT NULL DEFAULT 100,
    HintsUsed INTEGER NOT NULL DEFAULT 0,
    Attempts INTEGER NOT NULL DEFAULT 0,
    Completed BOOLEAN NOT NULL DEFAULT FALSE,
    CompletedAt TEXT,                       -- ISO 8601, NULL if not completed
    FirstAttemptAt TEXT NOT NULL,
    LastAttemptAt TEXT NOT NULL,

    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    CONSTRAINT chk_score_range CHECK (Score >= 0 AND Score <= MaxScore),
    CONSTRAINT chk_hints_positive CHECK (HintsUsed >= 0),
    CONSTRAINT chk_attempts_positive CHECK (Attempts > 0),
    CONSTRAINT chk_completed_at CHECK (CompletedAt IS NULL OR CompletedAt IS datetime(CompletedAt)),
    CONSTRAINT chk_first_attempt CHECK (FirstAttemptAt IS datetime(FirstAttemptAt)),
    CONSTRAINT chk_last_attempt CHECK (LastAttemptAt IS datetime(LastAttemptAt)),

    -- Unique constraint: one progress record per user/course/module/lesson/challenge combo
    UNIQUE(UserId, CourseId, ModuleId, LessonId, ChallengeId)
);

-- Indexes for fast queries
CREATE INDEX idx_progress_user_course ON Progress(UserId, CourseId);
CREATE INDEX idx_progress_completed ON Progress(Completed, CompletedAt);
CREATE INDEX idx_progress_last_attempt ON Progress(LastAttemptAt DESC);

-- ===================================================================
-- ACHIEVEMENTS TABLE
-- ===================================================================
-- Tracks unlocked achievements
CREATE TABLE Achievements (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    AchievementType TEXT NOT NULL,          -- e.g., "FIRST_STEPS", "POLYGLOT"
    UnlockedAt TEXT NOT NULL,               -- ISO 8601
    Progress INTEGER NOT NULL DEFAULT 0,    -- For progressive achievements
    MaxProgress INTEGER NOT NULL DEFAULT 1,
    Notified BOOLEAN NOT NULL DEFAULT FALSE, -- Has user been notified?

    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    CONSTRAINT chk_unlocked_at CHECK (UnlockedAt IS datetime(UnlockedAt)),
    CONSTRAINT chk_progress_range CHECK (Progress >= 0 AND Progress <= MaxProgress),

    UNIQUE(UserId, AchievementType)
);

CREATE INDEX idx_achievements_user ON Achievements(UserId);
CREATE INDEX idx_achievements_type ON Achievements(AchievementType);
CREATE INDEX idx_achievements_notified ON Achievements(Notified) WHERE Notified = FALSE;

-- ===================================================================
-- STREAKS TABLE
-- ===================================================================
-- Tracks daily learning activity
CREATE TABLE Streaks (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    Date TEXT NOT NULL,                     -- YYYY-MM-DD format
    LessonsCompleted INTEGER NOT NULL DEFAULT 0,
    ChallengesCompleted INTEGER NOT NULL DEFAULT 0,
    MinutesSpent INTEGER NOT NULL DEFAULT 0,

    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    CONSTRAINT chk_date_format CHECK (Date IS date(Date)),
    CONSTRAINT chk_lessons_positive CHECK (LessonsCompleted >= 0),
    CONSTRAINT chk_challenges_positive CHECK (ChallengesCompleted >= 0),
    CONSTRAINT chk_minutes_positive CHECK (MinutesSpent >= 0),

    UNIQUE(UserId, Date)
);

CREATE INDEX idx_streaks_user_date ON Streaks(UserId, Date DESC);

-- ===================================================================
-- CODE HISTORY TABLE
-- ===================================================================
-- Auto-save and recovery
CREATE TABLE CodeHistory (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    CourseId TEXT NOT NULL,
    ModuleId TEXT NOT NULL,
    LessonId TEXT NOT NULL,
    ChallengeId TEXT NOT NULL,
    Code TEXT NOT NULL,
    SavedAt TEXT NOT NULL,                  -- ISO 8601

    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    CONSTRAINT chk_saved_at CHECK (SavedAt IS datetime(SavedAt))
);

-- Index for retrieving latest code
CREATE INDEX idx_code_history_lookup ON CodeHistory(UserId, CourseId, ModuleId, LessonId, ChallengeId, SavedAt DESC);

-- Cleanup old history (keep last 10 saves per challenge)
CREATE TRIGGER trg_cleanup_code_history
AFTER INSERT ON CodeHistory
BEGIN
    DELETE FROM CodeHistory
    WHERE Id IN (
        SELECT Id FROM CodeHistory
        WHERE UserId = NEW.UserId
          AND CourseId = NEW.CourseId
          AND ModuleId = NEW.ModuleId
          AND LessonId = NEW.LessonId
          AND ChallengeId = NEW.ChallengeId
        ORDER BY SavedAt DESC
        LIMIT -1 OFFSET 10
    );
END;

-- ===================================================================
-- KEYBOARD SHORTCUTS TABLE
-- ===================================================================
-- Custom keyboard shortcuts
CREATE TABLE KeyboardShortcuts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    ActionName TEXT NOT NULL,               -- e.g., "RunCode", "OpenSettings"
    KeyCombination TEXT NOT NULL,           -- e.g., "Ctrl+Enter", "Ctrl+K"
    IsEnabled BOOLEAN NOT NULL DEFAULT TRUE,

    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    UNIQUE(UserId, ActionName),
    UNIQUE(UserId, KeyCombination)
);

CREATE INDEX idx_shortcuts_user ON KeyboardShortcuts(UserId);

-- Default shortcuts
INSERT INTO KeyboardShortcuts (UserId, ActionName, KeyCombination, IsEnabled)
VALUES
    ('00000000-0000-0000-0000-000000000001', 'CommandPalette', 'Ctrl+K', TRUE),
    ('00000000-0000-0000-0000-000000000001', 'RunCode', 'Ctrl+Enter', TRUE),
    ('00000000-0000-0000-0000-000000000001', 'SaveProgress', 'Ctrl+S', TRUE),
    ('00000000-0000-0000-0000-000000000001', 'ToggleHints', 'Ctrl+/', TRUE),
    ('00000000-0000-0000-0000-000000000001', 'ResetChallenge', 'Ctrl+R', TRUE),
    ('00000000-0000-0000-0000-000000000001', 'OpenSettings', 'Ctrl+.', TRUE),
    ('00000000-0000-0000-0000-000000000001', 'Help', 'F1', TRUE);

-- ===================================================================
-- STATISTICS TABLE
-- ===================================================================
-- Aggregate statistics for display
CREATE TABLE Statistics (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    StatName TEXT NOT NULL,                 -- e.g., "TotalLessonsCompleted"
    StatValue INTEGER NOT NULL DEFAULT 0,
    LastUpdated TEXT NOT NULL,

    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
    CONSTRAINT chk_last_updated CHECK (LastUpdated IS datetime(LastUpdated)),

    UNIQUE(UserId, StatName)
);

CREATE INDEX idx_statistics_user ON Statistics(UserId);

-- Initialize statistics
INSERT INTO Statistics (UserId, StatName, StatValue, LastUpdated)
VALUES
    ('00000000-0000-0000-0000-000000000001', 'TotalLessonsCompleted', 0, datetime('now')),
    ('00000000-0000-0000-0000-000000000001', 'TotalChallengesCompleted', 0, datetime('now')),
    ('00000000-0000-0000-0000-000000000001', 'TotalTestsPassed', 0, datetime('now')),
    ('00000000-0000-0000-0000-000000000001', 'TotalCodeExecutions', 0, datetime('now')),
    ('00000000-0000-0000-0000-000000000001', 'CurrentStreak', 0, datetime('now')),
    ('00000000-0000-0000-0000-000000000001', 'LongestStreak', 0, datetime('now'));
```

---

## Entity Framework Core Models

### DbContext
```csharp
using Microsoft.EntityFrameworkCore;

namespace CodeTutor.Native.Data;

public class CodeTutorDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserProgress> Progress { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Streak> Streaks { get; set; }
    public DbSet<CodeHistory> CodeHistory { get; set; }
    public DbSet<KeyboardShortcut> KeyboardShortcuts { get; set; }
    public DbSet<Statistic> Statistics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = GetDatabasePath();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    private string GetDatabasePath()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var codeTutorPath = Path.Combine(appDataPath, "CodeTutor");
        Directory.CreateDirectory(codeTutorPath);
        return Path.Combine(codeTutorPath, "CodeTutor.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure entities
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<UserProgress>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.CourseId });
            entity.HasIndex(e => new { e.Completed, e.CompletedAt });
        });

        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.AchievementType);
        });

        // Seed default user
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = "00000000-0000-0000-0000-000000000001",
            Name = "Desktop User",
            CreatedAt = DateTime.UtcNow,
            LastLoginAt = DateTime.UtcNow
        });
    }
}
```

### Entity Models
```csharp
public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastLoginAt { get; set; } = DateTime.UtcNow;
}

public class UserProgress
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CourseId { get; set; } = string.Empty;
    public string ModuleId { get; set; } = string.Empty;
    public string LessonId { get; set; } = string.Empty;
    public string? ChallengeId { get; set; }
    public int Score { get; set; }
    public int MaxScore { get; set; } = 100;
    public int HintsUsed { get; set; }
    public int Attempts { get; set; }
    public bool Completed { get; set; }
    public DateTime? CompletedAt { get; set; }
    public DateTime FirstAttemptAt { get; set; } = DateTime.UtcNow;
    public DateTime LastAttemptAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User User { get; set; } = null!;
}

public class Achievement
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string AchievementType { get; set; } = string.Empty;
    public DateTime UnlockedAt { get; set; } = DateTime.UtcNow;
    public int Progress { get; set; }
    public int MaxProgress { get; set; } = 1;
    public bool Notified { get; set; }

    // Navigation
    public User User { get; set; } = null!;
}

public class Streak
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int LessonsCompleted { get; set; }
    public int ChallengesCompleted { get; set; }
    public int MinutesSpent { get; set; }

    // Navigation
    public User User { get; set; } = null!;
}

public class CodeHistory
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CourseId { get; set; } = string.Empty;
    public string ModuleId { get; set; } = string.Empty;
    public string LessonId { get; set; } = string.Empty;
    public string ChallengeId { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User User { get; set; } = null!;
}

public class KeyboardShortcut
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string ActionName { get; set; } = string.Empty;
    public string KeyCombination { get; set; } = string.Empty;
    public bool IsEnabled { get; set; } = true;

    // Navigation
    public User User { get; set; } = null!;
}

public class Statistic
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string StatName { get; set; } = string.Empty;
    public int StatValue { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    // Navigation
    public User User { get; set; } = null!;
}
```

---

## Settings File Schema

### Settings.json Structure
```json
{
  "version": "1.0.0",
  "theme": {
    "current": "dark",
    "customColors": {
      "primary": "#0E639C",
      "secondary": "#4EC9B0",
      "error": "#F48771",
      "success": "#89D185"
    }
  },
  "editor": {
    "fontFamily": "Consolas",
    "fontSize": 14,
    "tabSize": 4,
    "wordWrap": true,
    "showLineNumbers": true,
    "showMinimap": false,
    "enableLigatures": true,
    "autoIndent": true,
    "formatOnSave": false
  },
  "autoSave": {
    "enabled": true,
    "delayMs": 2000
  },
  "hints": {
    "showAutomatically": false,
    "penaltyPercentage": 5
  },
  "notifications": {
    "showAchievements": true,
    "showProgressUpdates": true,
    "playSound": false,
    "duration": 5000
  },
  "privacy": {
    "collectUsageStats": false,
    "shareProgress": false
  },
  "advanced": {
    "maxConcurrentExecutions": 1,
    "executionTimeout": 10000,
    "debugMode": false,
    "logLevel": "Info"
  },
  "window": {
    "rememberSize": true,
    "rememberPosition": true,
    "lastWidth": 1400,
    "lastHeight": 900,
    "lastX": 100,
    "lastY": 100,
    "startMaximized": false
  }
}
```

### Settings Model
```csharp
public class Settings
{
    public string Version { get; set; } = "1.0.0";
    public ThemeSettings Theme { get; set; } = new();
    public EditorSettings Editor { get; set; } = new();
    public AutoSaveSettings AutoSave { get; set; } = new();
    public HintsSettings Hints { get; set; } = new();
    public NotificationSettings Notifications { get; set; } = new();
    public PrivacySettings Privacy { get; set; } = new();
    public AdvancedSettings Advanced { get; set; } = new();
    public WindowSettings Window { get; set; } = new();
}

public class ThemeSettings
{
    public string Current { get; set; } = "dark";
    public Dictionary<string, string> CustomColors { get; set; } = new()
    {
        ["primary"] = "#0E639C",
        ["secondary"] = "#4EC9B0",
        ["error"] = "#F48771",
        ["success"] = "#89D185"
    };
}

public class EditorSettings
{
    public string FontFamily { get; set; } = "Consolas";
    public int FontSize { get; set; } = 14;
    public int TabSize { get; set; } = 4;
    public bool WordWrap { get; set; } = true;
    public bool ShowLineNumbers { get; set; } = true;
    public bool ShowMinimap { get; set; } = false;
    public bool EnableLigatures { get; set; } = true;
    public bool AutoIndent { get; set; } = true;
    public bool FormatOnSave { get; set; } = false;
}

public class AutoSaveSettings
{
    public bool Enabled { get; set; } = true;
    public int DelayMs { get; set; } = 2000;
}

public class HintsSettings
{
    public bool ShowAutomatically { get; set; } = false;
    public int PenaltyPercentage { get; set; } = 5;
}

public class NotificationSettings
{
    public bool ShowAchievements { get; set; } = true;
    public bool ShowProgressUpdates { get; set; } = true;
    public bool PlaySound { get; set; } = false;
    public int Duration { get; set; } = 5000;
}

public class PrivacySettings
{
    public bool CollectUsageStats { get; set; } = false;
    public bool ShareProgress { get; set; } = false;
}

public class AdvancedSettings
{
    public int MaxConcurrentExecutions { get; set; } = 1;
    public int ExecutionTimeout { get; set; } = 10000;
    public bool DebugMode { get; set; } = false;
    public string LogLevel { get; set; } = "Info";
}

public class WindowSettings
{
    public bool RememberSize { get; set; } = true;
    public bool RememberPosition { get; set; } = true;
    public int LastWidth { get; set; } = 1400;
    public int LastHeight { get; set; } = 900;
    public int LastX { get; set; } = 100;
    public int LastY { get; set; } = 100;
    public bool StartMaximized { get; set; } = false;
}
```

---

## Data Migration from Electron

### localStorage → SQLite Migration

```csharp
public class DataMigrationService
{
    private readonly CodeTutorDbContext _db;
    private readonly string _userId;

    public async Task MigrateFromElectronAsync(string electronDataPath)
    {
        // Read progress.json from Electron app
        var progressJson = await File.ReadAllTextAsync(
            Path.Combine(electronDataPath, "progress.json"));

        var electronProgress = JsonSerializer.Deserialize<Dictionary<string, object>>(progressJson);

        // Transform and insert into SQLite
        foreach (var (key, value) in electronProgress)
        {
            var parts = key.Split('/');
            if (parts.Length >= 3)
            {
                var progress = new UserProgress
                {
                    UserId = _userId,
                    CourseId = parts[0],
                    ModuleId = parts[1],
                    LessonId = parts[2],
                    // ... map remaining fields
                };

                _db.Progress.Add(progress);
            }
        }

        await _db.SaveChangesAsync();
    }
}
```

---

## Common Queries

### Get Course Progress
```csharp
public async Task<CourseProgressDto> GetCourseProgressAsync(string courseId)
{
    var progress = await _db.Progress
        .Where(p => p.UserId == _userId && p.CourseId == courseId)
        .ToListAsync();

    var totalLessons = progress.Count(p => p.ChallengeId == null);
    var completedLessons = progress.Count(p => p.ChallengeId == null && p.Completed);

    return new CourseProgressDto
    {
        TotalLessons = totalLessons,
        CompletedLessons = completedLessons,
        PercentageComplete = totalLessons > 0 ? (completedLessons * 100) / totalLessons : 0
    };
}
```

### Get Current Streak
```csharp
public async Task<int> GetCurrentStreakAsync()
{
    var today = DateTime.UtcNow.Date;
    var streak = 0;

    for (var date = today; date >= DateTime.UtcNow.AddDays(-365); date = date.AddDays(-1))
    {
        var hasActivity = await _db.Streaks
            .AnyAsync(s => s.UserId == _userId &&
                          s.Date.Date == date &&
                          s.LessonsCompleted > 0);

        if (hasActivity)
        {
            streak++;
        }
        else
        {
            break;
        }
    }

    return streak;
}
```

### Save Code Auto-Save
```csharp
public async Task SaveCodeAsync(string courseId, string moduleId, string lessonId, string challengeId, string code)
{
    var history = new CodeHistory
    {
        UserId = _userId,
        CourseId = courseId,
        ModuleId = moduleId,
        LessonId = lessonId,
        ChallengeId = challengeId,
        Code = code,
        SavedAt = DateTime.UtcNow
    };

    _db.CodeHistory.Add(history);
    await _db.SaveChangesAsync();

    // Trigger automatically cleans up old history
}
```

### Get Latest Code
```csharp
public async Task<string?> GetLatestCodeAsync(string courseId, string moduleId, string lessonId, string challengeId)
{
    var history = await _db.CodeHistory
        .Where(h => h.UserId == _userId &&
                   h.CourseId == courseId &&
                   h.ModuleId == moduleId &&
                   h.LessonId == lessonId &&
                   h.ChallengeId == challengeId)
        .OrderByDescending(h => h.SavedAt)
        .FirstOrDefaultAsync();

    return history?.Code;
}
```

---

**Document Version:** 1.0
**Last Updated:** 2025-11-18
