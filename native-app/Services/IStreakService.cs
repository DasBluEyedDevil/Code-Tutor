using System;
using System.Threading.Tasks;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for managing daily learning streaks
/// </summary>
public interface IStreakService
{
    /// <summary>
    /// Record learning activity for today
    /// </summary>
    Task RecordActivityAsync(bool lessonCompleted = false, bool challengeCompleted = false, int minutesSpent = 0);

    /// <summary>
    /// Get current streak count
    /// </summary>
    Task<int> GetCurrentStreakAsync();

    /// <summary>
    /// Get longest streak ever
    /// </summary>
    Task<int> GetLongestStreakAsync();

    /// <summary>
    /// Get today's activity
    /// </summary>
    Task<Streak?> GetTodayActivityAsync();

    /// <summary>
    /// Update statistics based on streaks
    /// </summary>
    Task UpdateStreakStatisticsAsync();
}
