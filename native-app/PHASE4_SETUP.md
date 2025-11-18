# Phase 4: State Management & Persistence - Setup Instructions

## Overview
Phase 4 replaces the JSON-based progress tracking with a proper SQLite database using Entity Framework Core.

## Required NuGet Packages

Before building the project, you need to add the following NuGet packages:

```bash
cd native-app
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 8.0.0
```

## What Was Implemented

### 1. Entity Models
Created EF Core entity models in `Models/`:
- `User.cs` - User entity with relationships
- `UserProgress.cs` - Updated for EF Core with additional fields
- `Achievement.cs` - Achievement tracking
- `Streak.cs` - Daily learning streaks
- `CodeHistory.cs` - Auto-save code history
- `KeyboardShortcut.cs` - Custom keyboard shortcuts
- `Statistic.cs` - Aggregate statistics

### 2. Database Context
- `Data/CodeTutorDbContext.cs` - EF Core DbContext with:
  - Complete entity configurations
  - Indexes for performance
  - Foreign key relationships
  - Data seeding for default user and shortcuts

### 3. Database Service
- `Services/IDatabaseService.cs` - Database service interface
- `Services/DatabaseService.cs` - Handles database initialization and migrations

### 4. Updated Services
- `Services/ProgressService.cs` - Rewritten to use SQLite instead of JSON
  - Now uses async EF Core queries
  - Proper error handling and logging
  - Tracks attempts, hints used, and timestamps

### 5. Application Integration
- `App.axaml.cs` - Updated with:
  - DbContext registration in DI
  - DatabaseService registration
  - Database initialization on startup
  - Changed ProgressService to scoped lifetime

## Database Location

The SQLite database will be created at:
- **Windows**: `%APPDATA%\CodeTutor\CodeTutor.db`
- **macOS**: `~/Library/Application Support/CodeTutor/CodeTutor.db`
- **Linux**: `~/.config/CodeTutor/CodeTutor.db`

## Database Schema

The database includes 7 tables:
1. `Users` - User accounts (default desktop user included)
2. `Progress` - Lesson and challenge progress
3. `Achievements` - Unlocked achievements
4. `Streaks` - Daily learning activity
5. `CodeHistory` - Auto-saved code (last 10 versions per challenge)
6. `KeyboardShortcuts` - Custom keyboard shortcuts
7. `Statistics` - Aggregate statistics

## Migration from JSON

The old JSON progress file (`progress.json`) will no longer be used. Progress data will need to be re-created in the new SQLite database. Consider writing a migration script if you have existing progress data you want to preserve.

## Testing

To verify the database setup:

1. Build and run the application:
   ```bash
   dotnet build
   dotnet run
   ```

2. Check that the database file is created
3. Complete a lesson and verify progress is saved
4. Close and reopen the app to verify data persistence

## Troubleshooting

### Build Errors
If you get errors about missing Entity Framework types:
- Ensure you've added both NuGet packages
- Run `dotnet restore`
- Clean and rebuild: `dotnet clean && dotnet build`

### Database Errors
If the database fails to initialize:
- Check the logs for specific errors
- Verify the AppData folder is writable
- Delete the database file and restart the app to recreate it

### Migration Errors
If you get migration-related errors:
- The app uses `EnsureCreated()` which creates the database from the model
- For production, you may want to switch to using migrations:
  ```bash
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```

## Next Steps

With Phase 4 complete, you now have:
- ✅ Persistent data storage with SQLite
- ✅ Proper progress tracking with attempt counts
- ✅ Foundation for achievements system (Phase 7)
- ✅ Auto-save capability (Phase 5)
- ✅ Performance optimized with indexes

Next: **Phase 5 - Interactive Features** (Hints, Auto-save, Settings)
