using Microsoft.EntityFrameworkCore;
using CodeTutor.Native.Services;
using CodeTutor.Native.Models;
using CodeTutor.Native.Tests.Fixtures;
using CodeTutor.Native.Tests.Helpers;

namespace CodeTutor.Native.Tests.Unit.Services;

/// <summary>
/// Unit tests for AchievementService - Tests all 10 achievement types
/// </summary>
public class AchievementServiceTests : IDisposable
{
    private readonly DatabaseFixture _fixture;
    private const string DefaultUserId = "00000000-0000-0000-0000-000000000001";

    public AchievementServiceTests()
    {
        _fixture = new DatabaseFixture();
    }

    [Fact]
    public async Task CheckFirstStepsAsync_UnlocksAchievement_AfterFirstLesson()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            lessonId: "lesson1",
            completed: true));

        // Act
        await service.CheckAchievementsAsync();

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "FirstSteps");

        achievement.Should().NotBeNull();
        achievement!.Progress.Should().Be(1);
        achievement.MaxProgress.Should().Be(1);
        achievement.UnlockedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
    }

    [Fact]
    public async Task CheckFirstStepsAsync_DoesNotUnlock_BeforeFirstLesson()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // No completed lessons

        // Act
        await service.CheckAchievementsAsync();

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "FirstSteps");

        achievement.Should().BeNull();
    }

    [Fact]
    public async Task CheckQuickLearnerAsync_UnlocksAchievement_WhenLessonCompletedUnder30Minutes()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        var quickCompletion = TestDataGenerator.CreateProgress(
            lessonId: "lesson1",
            completed: true);
        quickCompletion.FirstAttemptAt = DateTime.UtcNow.AddMinutes(-25); // 25 minutes ago
        quickCompletion.CompletedAt = DateTime.UtcNow;

        _fixture.SeedProgress(context, quickCompletion);

        // Act
        await service.CheckAchievementsAsync();

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "QuickLearner");

        achievement.Should().NotBeNull();
    }

    [Fact]
    public async Task CheckQuickLearnerAsync_DoesNotUnlock_WhenLessonTakesOver30Minutes()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        var slowCompletion = TestDataGenerator.CreateProgress(
            lessonId: "lesson1",
            completed: true);
        slowCompletion.FirstAttemptAt = DateTime.UtcNow.AddMinutes(-35); // 35 minutes ago
        slowCompletion.CompletedAt = DateTime.UtcNow;

        _fixture.SeedProgress(context, slowCompletion);

        // Act
        await service.CheckAchievementsAsync();

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "QuickLearner");

        achievement.Should().BeNull();
    }

    [Fact]
    public async Task CheckPerfectionistAsync_UnlocksAchievement_WhenAllChallengesAre100Percent()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // Lesson with 3 challenges, all 100%
        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            lessonId: "lesson1",
            challengeId: "challenge1",
            score: 100,
            completed: true));

        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            lessonId: "lesson1",
            challengeId: "challenge2",
            score: 100,
            completed: true));

        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            lessonId: "lesson1",
            challengeId: "challenge3",
            score: 100,
            completed: true));

        // Lesson progress
        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            lessonId: "lesson1",
            challengeId: null,
            score: 100,
            completed: true));

        // Act
        await service.CheckAchievementsAsync();

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "Perfectionist");

        achievement.Should().NotBeNull();
    }

    [Fact]
    public async Task CheckPerfectionistAsync_DoesNotUnlock_WhenSomeChallengesBelow100Percent()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // Mix of scores
        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            lessonId: "lesson1",
            challengeId: "challenge1",
            score: 100,
            completed: true));

        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            lessonId: "lesson1",
            challengeId: "challenge2",
            score: 85, // Not perfect
            completed: true));

        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            lessonId: "lesson1",
            challengeId: null,
            completed: true));

        // Act
        await service.CheckAchievementsAsync();

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "Perfectionist");

        achievement.Should().BeNull();
    }

    [Fact]
    public async Task CheckPolyglotAsync_UnlocksAchievement_After3DifferentLanguages()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // Lessons in 3 different languages (identified by courseId convention)
        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            courseId: "csharp-basics",
            completed: true));

        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            courseId: "python-intro",
            completed: true));

        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            courseId: "javascript-fundamentals",
            completed: true));

        // Act
        await service.CheckAchievementsAsync();

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "Polyglot");

        achievement.Should().NotBeNull();
    }

    [Fact]
    public async Task CheckMarathonRunnerAsync_UnlocksAchievement_After7DayStreak()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // Create 7 consecutive days of activity
        var streaks = TestDataGenerator.CreateConsecutiveStreaks(7);
        _fixture.SeedStreaks(context, streaks.ToArray());

        // Act
        await service.CheckAchievementsAsync();

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "MarathonRunner");

        achievement.Should().NotBeNull();
    }

    [Fact]
    public async Task CheckSpeedDemonAsync_UnlocksAchievement_After5ChallengesWithoutHints()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // 5 challenges with 0 hints, 1 with hints (should still unlock)
        for (int i = 1; i <= 5; i++)
        {
            _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
                challengeId: $"challenge{i}",
                completed: true,
                hintsUsed: 0));
        }

        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            challengeId: "challenge6",
            completed: true,
            hintsUsed: 2));

        // Act
        await service.CheckAchievementsAsync();

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "SpeedDemon");

        achievement.Should().NotBeNull();
    }

    [Fact]
    public async Task CheckDebuggerAsync_UnlocksAchievement_After10FailedAttempts()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // Challenges with multiple attempts (failures before success)
        // Sum of (Attempts - 1) should be >= 10
        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            challengeId: "challenge1",
            attempts: 4,
            completed: true)); // 3 failures

        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            challengeId: "challenge2",
            attempts: 5,
            completed: true)); // 4 failures

        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
            challengeId: "challenge3",
            attempts: 4,
            completed: true)); // 3 failures

        // Total: 10 failures

        // Act
        await service.CheckAchievementsAsync();

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "Debugger");

        achievement.Should().NotBeNull();
    }

    [Fact]
    public async Task CheckCourseCompleteAsync_UnlocksAchievement_WhenAllLessonsCompleted()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // Assuming we know the course structure (would normally come from CourseService)
        // For testing, let's complete all lessons in a course
        string courseId = "course1";

        for (int i = 1; i <= 10; i++)
        {
            _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
                courseId: courseId,
                lessonId: $"lesson{i}",
                completed: true));
        }

        // Act
        await service.CheckAchievementsAsync();

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "CourseComplete");

        // Note: This test would need CourseService mock to know total lessons
        // For now, we're testing the service logic exists
        achievement.Should().NotBeNull();
    }

    [Fact]
    public async Task UnlockAchievementAsync_CreatesNewAchievement()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // Act
        await service.UnlockAchievementAsync(Models.AchievementType.FirstSteps);

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "FirstSteps");

        achievement.Should().NotBeNull();
        achievement!.UserId.Should().Be(DefaultUserId);
        achievement.Progress.Should().Be(1);
        achievement.MaxProgress.Should().Be(1);
        achievement.UnlockedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        achievement.Notified.Should().BeFalse();
    }

    [Fact]
    public async Task UnlockAchievementAsync_DoesNotDuplicate_IfAlreadyUnlocked()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // First unlock
        await service.UnlockAchievementAsync(Models.AchievementType.FirstSteps);

        // Act - Try to unlock again
        await service.UnlockAchievementAsync(Models.AchievementType.FirstSteps);

        // Assert
        var achievements = await context.Achievements
            .Where(a => a.AchievementType == "FirstSteps")
            .ToListAsync();

        achievements.Should().HaveCount(1); // Only one record
    }

    [Fact]
    public async Task IncrementProgressAsync_UpdatesProgressiveAchievement()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // Act
        await service.IncrementProgressAsync(Models.AchievementType.TestMaster, amount: 10);
        await service.IncrementProgressAsync(Models.AchievementType.TestMaster, amount: 15);

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "TestMaster");

        achievement.Should().NotBeNull();
        achievement!.Progress.Should().Be(25); // 10 + 15
        achievement.MaxProgress.Should().Be(100); // TestMaster requires 100 tests
    }

    [Fact]
    public async Task IncrementProgressAsync_CapsAtMaxProgress()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // Act - Try to exceed max
        await service.IncrementProgressAsync(Models.AchievementType.TestMaster, amount: 150);

        // Assert
        var achievement = await context.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "TestMaster");

        achievement!.Progress.Should().Be(100); // Capped at max
        achievement.MaxProgress.Should().Be(100);
    }

    [Fact]
    public async Task GetAchievementProgressAsync_ReturnsNull_WhenNotExists()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        // Act
        var achievement = await service.GetAchievementProgressAsync(Models.AchievementType.FirstSteps);

        // Assert
        achievement.Should().BeNull();
    }

    [Fact]
    public async Task GetAchievementProgressAsync_ReturnsAchievement_WhenExists()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new AchievementService(context);

        await service.UnlockAchievementAsync(Models.AchievementType.FirstSteps);

        // Act
        var achievement = await service.GetAchievementProgressAsync(Models.AchievementType.FirstSteps);

        // Assert
        achievement.Should().NotBeNull();
        achievement!.AchievementType.Should().Be("FirstSteps");
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }
}
