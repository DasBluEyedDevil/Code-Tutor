using Microsoft.EntityFrameworkCore;
using CodeTutor.Native.Services;
using CodeTutor.Native.Tests.Fixtures;
using CodeTutor.Native.Tests.Helpers;

namespace CodeTutor.Native.Tests.Unit.Services;

/// <summary>
/// Unit tests for StreakService
/// </summary>
public class StreakServiceTests : IDisposable
{
    private readonly DatabaseFixture _fixture;

    public StreakServiceTests()
    {
        _fixture = new DatabaseFixture();
    }

    [Fact]
    public async Task RecordActivityAsync_CreatesNewStreak_WhenNoneExistsForToday()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        // Act
        await service.RecordActivityAsync(lessonCompleted: true, challengeCompleted: false, minutesSpent: 30);

        // Assert
        var streak = await context.Streaks
            .FirstOrDefaultAsync(s => s.Date.Date == DateTime.UtcNow.Date);

        streak.Should().NotBeNull();
        streak!.LessonsCompleted.Should().Be(1);
        streak.ChallengesCompleted.Should().Be(0);
        streak.MinutesSpent.Should().Be(30);
    }

    [Fact]
    public async Task RecordActivityAsync_UpdatesExistingStreak_WhenAlreadyExistsForToday()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        var existingStreak = TestDataGenerator.CreateStreak(
            lessonsCompleted: 1,
            challengesCompleted: 3,
            minutesSpent: 25);
        _fixture.SeedStreaks(context, existingStreak);

        // Act
        await service.RecordActivityAsync(lessonCompleted: true, challengeCompleted: true, minutesSpent: 15);

        // Assert
        var streak = await context.Streaks
            .FirstOrDefaultAsync(s => s.Date.Date == DateTime.UtcNow.Date);

        streak!.LessonsCompleted.Should().Be(2); // 1 + 1
        streak.ChallengesCompleted.Should().Be(4); // 3 + 1
        streak.MinutesSpent.Should().Be(40); // 25 + 15
    }

    [Fact]
    public async Task GetCurrentStreakAsync_ReturnsZero_WhenNoActivity()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        // Act
        var streak = await service.GetCurrentStreakAsync();

        // Assert
        streak.Should().Be(0);
    }

    [Fact]
    public async Task GetCurrentStreakAsync_ReturnsOne_ForSingleDayActivity()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        _fixture.SeedStreaks(context, TestDataGenerator.CreateStreak(date: DateTime.UtcNow));

        // Act
        var streak = await service.GetCurrentStreakAsync();

        // Assert
        streak.Should().Be(1);
    }

    [Fact]
    public async Task GetCurrentStreakAsync_CalculatesConsecutiveDays_Correctly()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        var streaks = TestDataGenerator.CreateConsecutiveStreaks(7); // 7 consecutive days
        _fixture.SeedStreaks(context, streaks.ToArray());

        // Act
        var streak = await service.GetCurrentStreakAsync();

        // Assert
        streak.Should().Be(7);
    }

    [Fact]
    public async Task GetCurrentStreakAsync_BreaksOnGap_ReturnsRecentStreak()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        // 3 days recent, 2 day gap, 5 days old = should return 3
        var streaks = TestDataGenerator.CreateStreaksWithGap(
            daysBeforeGap: 5,
            gapDays: 2,
            daysAfterGap: 3);
        _fixture.SeedStreaks(context, streaks.ToArray());

        // Act
        var streak = await service.GetCurrentStreakAsync();

        // Assert
        streak.Should().Be(3); // Only recent streak counts
    }

    [Fact]
    public async Task GetCurrentStreakAsync_ReturnsZero_WhenLastActivityWasMoreThanOneDayAgo()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        // Activity 3 days ago - streak is broken
        _fixture.SeedStreaks(context, TestDataGenerator.CreateStreak(date: DateTime.UtcNow.AddDays(-3)));

        // Act
        var streak = await service.GetCurrentStreakAsync();

        // Assert
        streak.Should().Be(0); // Streak broken
    }

    [Fact]
    public async Task GetCurrentStreakAsync_CountsYesterday_AsValidStreakDay()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        // Activity yesterday counts as streak of 1
        _fixture.SeedStreaks(context, TestDataGenerator.CreateStreak(date: DateTime.UtcNow.AddDays(-1)));

        // Act
        var streak = await service.GetCurrentStreakAsync();

        // Assert
        streak.Should().Be(1);
    }

    [Fact]
    public async Task GetLongestStreakAsync_ReturnsZero_WhenNoActivity()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        // Act
        var longest = await service.GetLongestStreakAsync();

        // Assert
        longest.Should().Be(0);
    }

    [Fact]
    public async Task GetLongestStreakAsync_FindsLongestSequence_WithMultipleGaps()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        // Create multiple streak sequences: 3 days, gap, 7 days, gap, 2 days
        var today = DateTime.UtcNow.Date;
        var streaks = new List<CodeTutor.Native.Models.Streak>();

        // Recent 2 days
        for (int i = 0; i < 2; i++)
        {
            streaks.Add(TestDataGenerator.CreateStreak(date: today.AddDays(-i)));
        }

        // Gap of 2 days

        // 7 days (longest)
        for (int i = 0; i < 7; i++)
        {
            streaks.Add(TestDataGenerator.CreateStreak(date: today.AddDays(-(4 + i))));
        }

        // Gap of 3 days

        // 3 days
        for (int i = 0; i < 3; i++)
        {
            streaks.Add(TestDataGenerator.CreateStreak(date: today.AddDays(-(14 + i))));
        }

        _fixture.SeedStreaks(context, streaks.ToArray());

        // Act
        var longest = await service.GetLongestStreakAsync();

        // Assert
        longest.Should().Be(7); // Longest sequence
    }

    [Fact]
    public async Task GetLongestStreakAsync_ReturnsSingle_ForNonConsecutiveActivity()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        // Scattered activity with no consecutive days
        _fixture.SeedStreaks(context,
            TestDataGenerator.CreateStreak(date: DateTime.UtcNow.AddDays(-1)),
            TestDataGenerator.CreateStreak(date: DateTime.UtcNow.AddDays(-5)),
            TestDataGenerator.CreateStreak(date: DateTime.UtcNow.AddDays(-10)));

        // Act
        var longest = await service.GetLongestStreakAsync();

        // Assert
        longest.Should().Be(1); // No consecutive days
    }

    [Fact]
    public async Task RecordActivityAsync_UpdatesStreakStatistics()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        // Create a 5-day streak
        var streaks = TestDataGenerator.CreateConsecutiveStreaks(5);
        _fixture.SeedStreaks(context, streaks.ToArray());

        // Act
        await service.RecordActivityAsync(lessonCompleted: true, challengeCompleted: false, minutesSpent: 20);

        // Assert
        var currentStat = await context.Statistics
            .FirstOrDefaultAsync(s => s.StatisticType == "CurrentStreak");

        var longestStat = await context.Statistics
            .FirstOrDefaultAsync(s => s.StatisticType == "LongestStreak");

        currentStat.Should().NotBeNull();
        currentStat!.Value.Should().Be(6); // 5 + today

        longestStat.Should().NotBeNull();
        longestStat!.Value.Should().Be(6);
    }

    [Fact]
    public async Task RecordActivityAsync_HandlesMultipleActivities_InSameDay()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new StreakService(context);

        // Act - Multiple activities in same day
        await service.RecordActivityAsync(lessonCompleted: true, challengeCompleted: false, minutesSpent: 15);
        await service.RecordActivityAsync(lessonCompleted: false, challengeCompleted: true, minutesSpent: 10);
        await service.RecordActivityAsync(lessonCompleted: true, challengeCompleted: true, minutesSpent: 20);

        // Assert
        var streak = await context.Streaks
            .FirstOrDefaultAsync(s => s.Date.Date == DateTime.UtcNow.Date);

        streak.Should().NotBeNull();
        streak!.LessonsCompleted.Should().Be(2);
        streak.ChallengesCompleted.Should().Be(2);
        streak.MinutesSpent.Should().Be(45); // 15 + 10 + 20
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }
}
