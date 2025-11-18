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
/// Service for managing achievements and gamification
/// </summary>
public class AchievementService : IAchievementService
{
    private readonly CodeTutorDbContext _dbContext;
    private readonly ILogger<AchievementService>? _logger;
    private const string DefaultUserId = "00000000-0000-0000-0000-000000000001";

    public AchievementService(CodeTutorDbContext dbContext, ILogger<AchievementService>? logger = null)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task CheckAchievementsAsync()
    {
        try
        {
            await CheckFirstStepsAsync();
            await CheckQuickLearnerAsync();
            await CheckPerfectionistAsync();
            await CheckPolyglotAsync();
            await CheckMarathonRunnerAsync();
            await CheckSpeedDemonAsync();
            await CheckDebuggerAsync();
            await CheckCourseCompleteAsync();
            await CheckTestMasterAsync();
            await CheckNightOwlAsync();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to check achievements");
        }
    }

    public async Task<List<Achievement>> GetUnlockedAchievementsAsync()
    {
        try
        {
            return await _dbContext.Achievements
                .Where(a => a.UserId == DefaultUserId && a.Progress >= a.MaxProgress)
                .OrderByDescending(a => a.UnlockedAt)
                .ToListAsync();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get unlocked achievements");
            return new List<Achievement>();
        }
    }

    public async Task<Achievement?> GetAchievementProgressAsync(AchievementType type)
    {
        try
        {
            return await _dbContext.Achievements
                .FirstOrDefaultAsync(a => a.UserId == DefaultUserId && a.AchievementType == type.ToString());
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get achievement progress for {Type}", type);
            return null;
        }
    }

    public async Task IncrementProgressAsync(AchievementType type, int amount = 1)
    {
        try
        {
            var achievement = await GetAchievementProgressAsync(type);
            var definition = AchievementDefinition.All[type];

            if (achievement == null)
            {
                // Create new achievement progress
                achievement = new Achievement
                {
                    UserId = DefaultUserId,
                    AchievementType = type.ToString(),
                    Progress = amount,
                    MaxProgress = definition.MaxProgress,
                    UnlockedAt = DateTime.UtcNow,
                    Notified = false
                };

                _dbContext.Achievements.Add(achievement);
            }
            else if (achievement.Progress < achievement.MaxProgress)
            {
                achievement.Progress = Math.Min(achievement.Progress + amount, achievement.MaxProgress);
                _dbContext.Achievements.Update(achievement);
            }

            await _dbContext.SaveChangesAsync();

            _logger?.LogInformation("Incremented {Type} progress to {Progress}/{Max}",
                type, achievement.Progress, achievement.MaxProgress);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to increment achievement progress for {Type}", type);
        }
    }

    public async Task<List<Achievement>> GetUnnotifiedAchievementsAsync()
    {
        try
        {
            return await _dbContext.Achievements
                .Where(a => a.UserId == DefaultUserId && !a.Notified && a.Progress >= a.MaxProgress)
                .ToListAsync();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get unnotified achievements");
            return new List<Achievement>();
        }
    }

    public async Task MarkAsNotifiedAsync(int achievementId)
    {
        try
        {
            var achievement = await _dbContext.Achievements.FindAsync(achievementId);
            if (achievement != null)
            {
                achievement.Notified = true;
                _dbContext.Achievements.Update(achievement);
                await _dbContext.SaveChangesAsync();

                _logger?.LogInformation("Marked achievement {Id} as notified", achievementId);
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to mark achievement as notified");
        }
    }

    public async Task<int> GetTotalPointsAsync()
    {
        try
        {
            var unlockedAchievements = await GetUnlockedAchievementsAsync();
            var totalPoints = 0;

            foreach (var achievement in unlockedAchievements)
            {
                if (Enum.TryParse<AchievementType>(achievement.AchievementType, out var type))
                {
                    if (AchievementDefinition.All.TryGetValue(type, out var definition))
                    {
                        totalPoints += definition.Points;
                    }
                }
            }

            return totalPoints;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get total points");
            return 0;
        }
    }

    public async Task<int> GetCompletionPercentageAsync()
    {
        try
        {
            var totalAchievements = AchievementDefinition.All.Count;
            var unlockedAchievements = await GetUnlockedAchievementsAsync();

            if (totalAchievements == 0)
                return 0;

            return (unlockedAchievements.Count * 100) / totalAchievements;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get completion percentage");
            return 0;
        }
    }

    // Private achievement checking methods

    private async Task CheckFirstStepsAsync()
    {
        var completedLessons = await _dbContext.Progress
            .CountAsync(p => p.UserId == DefaultUserId && p.Completed && p.ChallengeId == null);

        if (completedLessons >= 1)
        {
            await UnlockAchievementAsync(AchievementType.FirstSteps);
        }
    }

    private async Task CheckQuickLearnerAsync()
    {
        var recentLessons = await _dbContext.Progress
            .Where(p => p.UserId == DefaultUserId && p.Completed && p.ChallengeId == null)
            .ToListAsync();

        foreach (var lesson in recentLessons)
        {
            var duration = lesson.CompletedAt - lesson.FirstAttemptAt;
            if (duration.HasValue && duration.Value.TotalMinutes <= 30)
            {
                await UnlockAchievementAsync(AchievementType.QuickLearner);
                break;
            }
        }
    }

    private async Task CheckPerfectionistAsync()
    {
        // Check if any lesson has all challenges with 100% score
        var lessonProgress = await _dbContext.Progress
            .Where(p => p.UserId == DefaultUserId && p.Completed)
            .GroupBy(p => new { p.CourseId, p.ModuleId, p.LessonId })
            .ToListAsync();

        foreach (var lessonGroup in lessonProgress)
        {
            var challenges = lessonGroup.Where(p => p.ChallengeId != null).ToList();
            if (challenges.Count > 0 && challenges.All(c => c.Score >= c.MaxScore))
            {
                await UnlockAchievementAsync(AchievementType.Perfectionist);
                break;
            }
        }
    }

    private async Task CheckPolyglotAsync()
    {
        var languagesCompleted = await _dbContext.Progress
            .Where(p => p.UserId == DefaultUserId && p.Completed && p.ChallengeId == null)
            .Select(p => p.CourseId)
            .Distinct()
            .CountAsync();

        if (languagesCompleted >= 3)
        {
            await IncrementProgressAsync(AchievementType.Polyglot, languagesCompleted);
        }
    }

    private async Task CheckMarathonRunnerAsync()
    {
        // Check for 7 consecutive days with activity
        var streaks = await _dbContext.Streaks
            .Where(s => s.UserId == DefaultUserId && (s.LessonsCompleted > 0 || s.ChallengesCompleted > 0))
            .OrderByDescending(s => s.Date)
            .ToListAsync();

        int consecutiveDays = 0;
        DateTime? lastDate = null;

        foreach (var streak in streaks)
        {
            if (lastDate == null || (lastDate.Value - streak.Date).Days == 1)
            {
                consecutiveDays++;
                lastDate = streak.Date;

                if (consecutiveDays >= 7)
                {
                    await IncrementProgressAsync(AchievementType.MarathonRunner, 7);
                    break;
                }
            }
            else
            {
                break;
            }
        }
    }

    private async Task CheckSpeedDemonAsync()
    {
        var challengesWithoutHints = await _dbContext.Progress
            .CountAsync(p => p.UserId == DefaultUserId && p.Completed && p.ChallengeId != null && p.HintsUsed == 0);

        if (challengesWithoutHints >= 5)
        {
            await IncrementProgressAsync(AchievementType.SpeedDemon, challengesWithoutHints);
        }
    }

    private async Task CheckDebuggerAsync()
    {
        // Count test cases that were failed then passed
        var testCasesPassed = await _dbContext.Progress
            .Where(p => p.UserId == DefaultUserId && p.Completed && p.Attempts > 1)
            .SumAsync(p => p.Attempts - 1); // Attempts - 1 = number of fixes

        if (testCasesPassed >= 10)
        {
            await IncrementProgressAsync(AchievementType.Debugger, testCasesPassed);
        }
    }

    private async Task CheckCourseCompleteAsync()
    {
        // Check if any course is 100% complete
        var courses = await _dbContext.Progress
            .Where(p => p.UserId == DefaultUserId && p.ChallengeId == null)
            .GroupBy(p => p.CourseId)
            .ToListAsync();

        foreach (var course in courses)
        {
            if (course.All(l => l.Completed))
            {
                await UnlockAchievementAsync(AchievementType.CourseComplete);
                break;
            }
        }
    }

    private async Task CheckTestMasterAsync()
    {
        // Count total test cases passed (assuming each challenge has multiple test cases)
        var totalTestsPassed = await _dbContext.Statistics
            .Where(s => s.UserId == DefaultUserId && s.StatName == "TotalTestsPassed")
            .Select(s => s.StatValue)
            .FirstOrDefaultAsync();

        if (totalTestsPassed >= 100)
        {
            await IncrementProgressAsync(AchievementType.TestMaster, totalTestsPassed);
        }
    }

    private async Task CheckNightOwlAsync()
    {
        var nightLessons = await _dbContext.Progress
            .Where(p => p.UserId == DefaultUserId && p.Completed && p.ChallengeId == null)
            .ToListAsync();

        foreach (var lesson in nightLessons)
        {
            if (lesson.CompletedAt.HasValue && lesson.CompletedAt.Value.Hour >= 22)
            {
                await UnlockAchievementAsync(AchievementType.NightOwl);
                break;
            }
        }
    }

    private async Task UnlockAchievementAsync(AchievementType type)
    {
        var existing = await GetAchievementProgressAsync(type);
        if (existing == null || existing.Progress < existing.MaxProgress)
        {
            var definition = AchievementDefinition.All[type];
            await IncrementProgressAsync(type, definition.MaxProgress);
        }
    }
}
