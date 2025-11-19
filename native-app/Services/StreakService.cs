using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CodeTutor.Native.Data;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for managing daily learning streaks
/// </summary>
public class StreakService : IStreakService
{
    private readonly CodeTutorDbContext _dbContext;
    private readonly ILogger<StreakService>? _logger;
    private const string DefaultUserId = "00000000-0000-0000-0000-000000000001";

    public StreakService(CodeTutorDbContext dbContext, ILogger<StreakService>? logger = null)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task RecordActivityAsync(bool lessonCompleted = false, bool challengeCompleted = false, int minutesSpent = 0)
    {
        try
        {
            var today = DateTime.UtcNow.Date;
            var existingStreak = await _dbContext.Streaks
                .FirstOrDefaultAsync(s => s.UserId == DefaultUserId && s.Date.Date == today);

            if (existingStreak == null)
            {
                // Create new streak entry for today
                existingStreak = new Streak
                {
                    UserId = DefaultUserId,
                    Date = today,
                    LessonsCompleted = lessonCompleted ? 1 : 0,
                    ChallengesCompleted = challengeCompleted ? 1 : 0,
                    MinutesSpent = minutesSpent
                };

                _dbContext.Streaks.Add(existingStreak);
            }
            else
            {
                // Update existing streak
                if (lessonCompleted)
                    existingStreak.LessonsCompleted++;

                if (challengeCompleted)
                    existingStreak.ChallengesCompleted++;

                existingStreak.MinutesSpent += minutesSpent;

                _dbContext.Streaks.Update(existingStreak);
            }

            await _dbContext.SaveChangesAsync();
            await UpdateStreakStatisticsAsync();

            _logger?.LogInformation("Recorded activity for {Date}: Lessons={Lessons}, Challenges={Challenges}, Minutes={Minutes}",
                today, existingStreak.LessonsCompleted, existingStreak.ChallengesCompleted, existingStreak.MinutesSpent);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to record activity");
        }
    }

    public async Task<int> GetCurrentStreakAsync()
    {
        try
        {
            var streaks = await _dbContext.Streaks
                .Where(s => s.UserId == DefaultUserId && (s.LessonsCompleted > 0 || s.ChallengesCompleted > 0))
                .OrderByDescending(s => s.Date)
                .ToListAsync();

            if (streaks.Count == 0)
                return 0;

            int currentStreak = 0;
            DateTime? lastDate = null;

            foreach (var streak in streaks)
            {
                if (lastDate == null)
                {
                    // First streak (most recent)
                    var daysSinceToday = (DateTime.UtcNow.Date - streak.Date.Date).Days;
                    if (daysSinceToday > 1)
                    {
                        // Streak is broken (no activity yesterday or today)
                        return 0;
                    }

                    currentStreak = 1;
                    lastDate = streak.Date.Date;
                }
                else
                {
                    var daysDifference = (lastDate.Value - streak.Date.Date).Days;

                    if (daysDifference == 1)
                    {
                        // Consecutive day
                        currentStreak++;
                        lastDate = streak.Date.Date;
                    }
                    else
                    {
                        // Gap in streak
                        break;
                    }
                }
            }

            return currentStreak;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get current streak");
            return 0;
        }
    }

    public async Task<int> GetLongestStreakAsync()
    {
        try
        {
            var streaks = await _dbContext.Streaks
                .Where(s => s.UserId == DefaultUserId && (s.LessonsCompleted > 0 || s.ChallengesCompleted > 0))
                .OrderBy(s => s.Date)
                .ToListAsync();

            if (streaks.Count == 0)
                return 0;

            int longestStreak = 1;
            int currentStreak = 1;
            DateTime? lastDate = streaks[0].Date.Date;

            for (int i = 1; i < streaks.Count; i++)
            {
                var currentDate = streaks[i].Date.Date;
                var daysDifference = (currentDate - lastDate!.Value).Days;

                if (daysDifference == 1)
                {
                    currentStreak++;
                    longestStreak = Math.Max(longestStreak, currentStreak);
                }
                else
                {
                    currentStreak = 1;
                }

                lastDate = currentDate;
            }

            return longestStreak;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get longest streak");
            return 0;
        }
    }

    public async Task<Streak?> GetTodayActivityAsync()
    {
        try
        {
            var today = DateTime.UtcNow.Date;
            return await _dbContext.Streaks
                .FirstOrDefaultAsync(s => s.UserId == DefaultUserId && s.Date.Date == today);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to get today's activity");
            return null;
        }
    }

    public async Task UpdateStreakStatisticsAsync()
    {
        try
        {
            var currentStreak = await GetCurrentStreakAsync();
            var longestStreak = await GetLongestStreakAsync();

            // Update or create current streak statistic
            var currentStreakStat = await _dbContext.Statistics
                .FirstOrDefaultAsync(s => s.UserId == DefaultUserId && s.StatName == "CurrentStreak");

            if (currentStreakStat != null)
            {
                currentStreakStat.StatValue = currentStreak;
                currentStreakStat.LastUpdated = DateTime.UtcNow;
                _dbContext.Statistics.Update(currentStreakStat);
            }
            else
            {
                // Initialize missing statistic record
                currentStreakStat = new Statistic
                {
                    UserId = DefaultUserId,
                    StatName = "CurrentStreak",
                    StatValue = currentStreak,
                    LastUpdated = DateTime.UtcNow
                };
                _dbContext.Statistics.Add(currentStreakStat);
                _logger?.LogWarning("CurrentStreak statistic was missing - initialized with value {Value}", currentStreak);
            }

            // Update or create longest streak statistic
            var longestStreakStat = await _dbContext.Statistics
                .FirstOrDefaultAsync(s => s.UserId == DefaultUserId && s.StatName == "LongestStreak");

            if (longestStreakStat != null)
            {
                longestStreakStat.StatValue = Math.Max(longestStreakStat.StatValue, longestStreak);
                longestStreakStat.LastUpdated = DateTime.UtcNow;
                _dbContext.Statistics.Update(longestStreakStat);
            }
            else
            {
                // Initialize missing statistic record
                longestStreakStat = new Statistic
                {
                    UserId = DefaultUserId,
                    StatName = "LongestStreak",
                    StatValue = longestStreak,
                    LastUpdated = DateTime.UtcNow
                };
                _dbContext.Statistics.Add(longestStreakStat);
                _logger?.LogWarning("LongestStreak statistic was missing - initialized with value {Value}", longestStreak);
            }

            await _dbContext.SaveChangesAsync();

            _logger?.LogInformation("Updated streak statistics: Current={Current}, Longest={Longest}",
                currentStreak, longestStreak);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to update streak statistics");
        }
    }
}
