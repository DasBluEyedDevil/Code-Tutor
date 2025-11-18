using System.Collections.Generic;
using System.Threading.Tasks;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for managing achievements and gamification
/// </summary>
public interface IAchievementService
{
    /// <summary>
    /// Check and unlock achievements based on recent activity
    /// </summary>
    Task CheckAchievementsAsync();

    /// <summary>
    /// Get all unlocked achievements for the user
    /// </summary>
    Task<List<Achievement>> GetUnlockedAchievementsAsync();

    /// <summary>
    /// Get achievement progress
    /// </summary>
    Task<Achievement?> GetAchievementProgressAsync(AchievementType type);

    /// <summary>
    /// Increment progress for a progressive achievement
    /// </summary>
    Task IncrementProgressAsync(AchievementType type, int amount = 1);

    /// <summary>
    /// Get all unnotified achievements
    /// </summary>
    Task<List<Achievement>> GetUnnotifiedAchievementsAsync();

    /// <summary>
    /// Mark achievement as notified
    /// </summary>
    Task MarkAsNotifiedAsync(int achievementId);

    /// <summary>
    /// Get total points earned
    /// </summary>
    Task<int> GetTotalPointsAsync();

    /// <summary>
    /// Get achievement completion percentage
    /// </summary>
    Task<int> GetCompletionPercentageAsync();
}
