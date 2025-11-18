using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CodeTutor.Native.Data;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// SQLite-based progress service using Entity Framework Core
/// </summary>
public class ProgressService : IProgressService
{
    private readonly CodeTutorDbContext _dbContext;
    private readonly ILogger<ProgressService>? _logger;
    private const string DefaultUserId = "00000000-0000-0000-0000-000000000001";

    public ProgressService(CodeTutorDbContext dbContext, ILogger<ProgressService>? logger = null)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task SaveProgressAsync(string courseId, string moduleId, string lessonId, int score, bool completed, int hintsUsed = 0)
    {
        try
        {
            // Find existing progress record
            var progress = await _dbContext.Progress
                .FirstOrDefaultAsync(p =>
                    p.UserId == DefaultUserId &&
                    p.CourseId == courseId &&
                    p.ModuleId == moduleId &&
                    p.LessonId == lessonId &&
                    p.ChallengeId == null);

            if (progress == null)
            {
                // Create new progress record
                progress = new UserProgress
                {
                    UserId = DefaultUserId,
                    CourseId = courseId,
                    ModuleId = moduleId,
                    LessonId = lessonId,
                    ChallengeId = null,
                    Score = score,
                    MaxScore = 100,
                    HintsUsed = hintsUsed,
                    Attempts = 1,
                    Completed = completed,
                    CompletedAt = completed ? DateTime.UtcNow : null,
                    FirstAttemptAt = DateTime.UtcNow,
                    LastAttemptAt = DateTime.UtcNow
                };

                _dbContext.Progress.Add(progress);
            }
            else
            {
                // Update existing progress
                progress.Score = Math.Max(progress.Score, score);
                progress.HintsUsed = Math.Max(progress.HintsUsed, hintsUsed);
                progress.Attempts++;
                progress.Completed = completed;
                progress.CompletedAt = completed && progress.CompletedAt == null ? DateTime.UtcNow : progress.CompletedAt;
                progress.LastAttemptAt = DateTime.UtcNow;

                _dbContext.Progress.Update(progress);
            }

            await _dbContext.SaveChangesAsync();

            _logger?.LogInformation("Saved progress for {Course}/{Module}/{Lesson}: Score={Score}, Completed={Completed}, Hints={Hints}",
                courseId, moduleId, lessonId, score, completed, hintsUsed);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to save progress for {Course}/{Module}/{Lesson}",
                courseId, moduleId, lessonId);
            throw;
        }
    }

    public async Task<UserProgress?> GetLessonProgressAsync(string courseId, string moduleId, string lessonId)
    {
        try
        {
            return await _dbContext.Progress
                .FirstOrDefaultAsync(p =>
                    p.UserId == DefaultUserId &&
                    p.CourseId == courseId &&
                    p.ModuleId == moduleId &&
                    p.LessonId == lessonId &&
                    p.ChallengeId == null);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get progress for {Course}/{Module}/{Lesson}",
                courseId, moduleId, lessonId);
            return null;
        }
    }

    public async Task<List<UserProgress>> GetModuleProgressAsync(string courseId, string moduleId)
    {
        try
        {
            return await _dbContext.Progress
                .Where(p =>
                    p.UserId == DefaultUserId &&
                    p.CourseId == courseId &&
                    p.ModuleId == moduleId &&
                    p.ChallengeId == null)
                .OrderBy(p => p.LessonId)
                .ToListAsync();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get progress for {Course}/{Module}",
                courseId, moduleId);
            return new List<UserProgress>();
        }
    }

    public async Task<int> GetCourseProgressPercentageAsync(string courseId)
    {
        try
        {
            var courseProgress = await _dbContext.Progress
                .Where(p =>
                    p.UserId == DefaultUserId &&
                    p.CourseId == courseId &&
                    p.ChallengeId == null)
                .ToListAsync();

            if (courseProgress.Count == 0)
                return 0;

            var completed = courseProgress.Count(p => p.Completed);
            return (completed * 100) / courseProgress.Count;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get course progress percentage for {Course}", courseId);
            return 0;
        }
    }

    public async Task IncrementHintUsageAsync(string courseId, string moduleId, string lessonId, string? challengeId = null)
    {
        try
        {
            var progress = await _dbContext.Progress
                .FirstOrDefaultAsync(p =>
                    p.UserId == DefaultUserId &&
                    p.CourseId == courseId &&
                    p.ModuleId == moduleId &&
                    p.LessonId == lessonId &&
                    p.ChallengeId == challengeId);

            if (progress != null)
            {
                progress.HintsUsed++;
                _dbContext.Progress.Update(progress);
                await _dbContext.SaveChangesAsync();

                _logger?.LogInformation("Incremented hint usage for {Course}/{Module}/{Lesson}/{Challenge}",
                    courseId, moduleId, lessonId, challengeId ?? "N/A");
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to increment hint usage");
        }
    }
}
