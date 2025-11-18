using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CodeTutor.Native.Data;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for auto-saving code with debouncing and draft recovery
/// Uses CodeHistory table to store last 10 versions per challenge
/// </summary>
public class AutoSaveService : IAutoSaveService
{
    private readonly CodeTutorDbContext _dbContext;
    private readonly ILogger<AutoSaveService>? _logger;
    private const string DefaultUserId = "00000000-0000-0000-0000-000000000001";
    private const int MaxHistoryVersions = 10;

    public AutoSaveService(CodeTutorDbContext dbContext, ILogger<AutoSaveService>? logger = null)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task SaveCodeAsync(string courseId, string moduleId, string lessonId, string challengeId, string code)
    {
        try
        {
            // Create new code history entry
            var history = new CodeHistory
            {
                UserId = DefaultUserId,
                CourseId = courseId,
                ModuleId = moduleId,
                LessonId = lessonId,
                ChallengeId = challengeId,
                Code = code,
                SavedAt = DateTime.UtcNow
            };

            _dbContext.CodeHistory.Add(history);
            await _dbContext.SaveChangesAsync();

            // Cleanup old history (keep only last 10)
            await CleanupOldHistoryAsync(courseId, moduleId, lessonId, challengeId);

            _logger?.LogInformation("Auto-saved code for {Course}/{Module}/{Lesson}/{Challenge}",
                courseId, moduleId, lessonId, challengeId);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to auto-save code for {Course}/{Module}/{Lesson}/{Challenge}",
                courseId, moduleId, lessonId, challengeId);
            // Don't throw - auto-save failures shouldn't crash the app
        }
    }

    public async Task<string?> RestoreCodeAsync(string courseId, string moduleId, string lessonId, string challengeId)
    {
        try
        {
            var latestCode = await _dbContext.CodeHistory
                .Where(h =>
                    h.UserId == DefaultUserId &&
                    h.CourseId == courseId &&
                    h.ModuleId == moduleId &&
                    h.LessonId == lessonId &&
                    h.ChallengeId == challengeId)
                .OrderByDescending(h => h.SavedAt)
                .Select(h => h.Code)
                .FirstOrDefaultAsync();

            if (latestCode != null)
            {
                _logger?.LogInformation("Restored code for {Course}/{Module}/{Lesson}/{Challenge}",
                    courseId, moduleId, lessonId, challengeId);
            }

            return latestCode;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to restore code for {Course}/{Module}/{Lesson}/{Challenge}",
                courseId, moduleId, lessonId, challengeId);
            return null;
        }
    }

    public async Task ClearHistoryAsync(string courseId, string moduleId, string lessonId, string challengeId)
    {
        try
        {
            var historyEntries = await _dbContext.CodeHistory
                .Where(h =>
                    h.UserId == DefaultUserId &&
                    h.CourseId == courseId &&
                    h.ModuleId == moduleId &&
                    h.LessonId == lessonId &&
                    h.ChallengeId == challengeId)
                .ToListAsync();

            if (historyEntries.Count > 0)
            {
                _dbContext.CodeHistory.RemoveRange(historyEntries);
                await _dbContext.SaveChangesAsync();

                _logger?.LogInformation("Cleared code history for {Course}/{Module}/{Lesson}/{Challenge}",
                    courseId, moduleId, lessonId, challengeId);
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to clear code history for {Course}/{Module}/{Lesson}/{Challenge}",
                courseId, moduleId, lessonId, challengeId);
        }
    }

    public async Task<DateTime?> GetLastSaveTimeAsync(string courseId, string moduleId, string lessonId, string challengeId)
    {
        try
        {
            return await _dbContext.CodeHistory
                .Where(h =>
                    h.UserId == DefaultUserId &&
                    h.CourseId == courseId &&
                    h.ModuleId == moduleId &&
                    h.LessonId == lessonId &&
                    h.ChallengeId == challengeId)
                .OrderByDescending(h => h.SavedAt)
                .Select(h => (DateTime?)h.SavedAt)
                .FirstOrDefaultAsync();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get last save time for {Course}/{Module}/{Lesson}/{Challenge}",
                courseId, moduleId, lessonId, challengeId);
            return null;
        }
    }

    private async Task CleanupOldHistoryAsync(string courseId, string moduleId, string lessonId, string challengeId)
    {
        try
        {
            var allHistory = await _dbContext.CodeHistory
                .Where(h =>
                    h.UserId == DefaultUserId &&
                    h.CourseId == courseId &&
                    h.ModuleId == moduleId &&
                    h.LessonId == lessonId &&
                    h.ChallengeId == challengeId)
                .OrderByDescending(h => h.SavedAt)
                .ToListAsync();

            if (allHistory.Count > MaxHistoryVersions)
            {
                var toDelete = allHistory.Skip(MaxHistoryVersions).ToList();
                _dbContext.CodeHistory.RemoveRange(toDelete);
                await _dbContext.SaveChangesAsync();

                _logger?.LogInformation("Cleaned up {Count} old history entries for {Course}/{Module}/{Lesson}/{Challenge}",
                    toDelete.Count, courseId, moduleId, lessonId, challengeId);
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to cleanup old history for {Course}/{Module}/{Lesson}/{Challenge}",
                courseId, moduleId, lessonId, challengeId);
        }
    }
}
