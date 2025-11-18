using Microsoft.EntityFrameworkCore;
using CodeTutor.Native.Services;
using CodeTutor.Native.Tests.Fixtures;
using CodeTutor.Native.Tests.Helpers;

namespace CodeTutor.Native.Tests.Unit.Services;

/// <summary>
/// Unit tests for ProgressService
/// </summary>
public class ProgressServiceTests : IDisposable
{
    private readonly DatabaseFixture _fixture;

    public ProgressServiceTests()
    {
        _fixture = new DatabaseFixture();
    }

    [Fact]
    public async Task SaveProgressAsync_CreatesNewRecord_WhenNoExistingProgress()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        // Act
        await service.SaveProgressAsync("course1", "module1", "lesson1", 85, completed: true, hintsUsed: 2);

        // Assert
        var progress = await context.Progress
            .FirstOrDefaultAsync(p => p.CourseId == "course1" && p.LessonId == "lesson1");

        progress.Should().NotBeNull();
        progress!.Score.Should().Be(85);
        progress.Completed.Should().BeTrue();
        progress.HintsUsed.Should().Be(2);
        progress.Attempts.Should().Be(1);
        progress.CompletedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task SaveProgressAsync_UpdatesExistingRecord_WhenProgressExists()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        var existingProgress = TestDataGenerator.CreateProgress(
            score: 70,
            completed: false,
            attempts: 1);
        _fixture.SeedProgress(context, existingProgress);

        // Act
        await service.SaveProgressAsync("course1", "module1", "lesson1", 90, completed: true, hintsUsed: 1);

        // Assert
        var progress = await context.Progress
            .FirstOrDefaultAsync(p => p.CourseId == "course1" && p.LessonId == "lesson1");

        progress.Should().NotBeNull();
        progress!.Score.Should().Be(90); // Updated to better score
        progress.Completed.Should().BeTrue(); // Updated
        progress.HintsUsed.Should().Be(1); // Updated (max)
        progress.Attempts.Should().Be(2); // Incremented
    }

    [Fact]
    public async Task SaveProgressAsync_KeepsBestScore_WhenNewScoreIsLower()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        var existingProgress = TestDataGenerator.CreateProgress(score: 95);
        _fixture.SeedProgress(context, existingProgress);

        // Act
        await service.SaveProgressAsync("course1", "module1", "lesson1", 80, completed: false);

        // Assert
        var progress = await context.Progress
            .FirstOrDefaultAsync(p => p.CourseId == "course1" && p.LessonId == "lesson1");

        progress!.Score.Should().Be(95); // Kept best score
        progress.Attempts.Should().Be(2); // Still incremented attempts
    }

    [Fact]
    public async Task SaveProgressAsync_SetsCompletedAt_OnlyOnFirstCompletion()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        // Act - First completion
        await service.SaveProgressAsync("course1", "module1", "lesson1", 85, completed: true);

        var firstCompletion = await context.Progress
            .FirstOrDefaultAsync(p => p.CourseId == "course1" && p.LessonId == "lesson1");
        var originalCompletedAt = firstCompletion!.CompletedAt;

        await Task.Delay(10); // Small delay to ensure different timestamp

        // Act - Second completion (retry)
        await service.SaveProgressAsync("course1", "module1", "lesson1", 100, completed: true);

        // Assert
        var progress = await context.Progress
            .FirstOrDefaultAsync(p => p.CourseId == "course1" && p.LessonId == "lesson1");

        progress!.CompletedAt.Should().Be(originalCompletedAt); // Unchanged
        progress.Score.Should().Be(100); // Score updated
    }

    [Fact]
    public async Task SaveChallengeProgressAsync_CreatesNewRecord_WhenNoExistingProgress()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        // Act
        await service.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge1", 100, completed: true, hintsUsed: 0);

        // Assert
        var progress = await context.Progress
            .FirstOrDefaultAsync(p => p.ChallengeId == "challenge1");

        progress.Should().NotBeNull();
        progress!.Score.Should().Be(100);
        progress.Completed.Should().BeTrue();
        progress.HintsUsed.Should().Be(0);
        progress.ChallengeId.Should().Be("challenge1");
    }

    [Fact]
    public async Task SaveChallengeProgressAsync_KeepsBestScore_AcrossAttempts()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        // Act - First attempt
        await service.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge1", 60, completed: false);

        // Act - Second attempt (better)
        await service.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge1", 90, completed: true);

        // Act - Third attempt (worse)
        await service.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge1", 70, completed: true);

        // Assert
        var progress = await context.Progress
            .FirstOrDefaultAsync(p => p.ChallengeId == "challenge1");

        progress!.Score.Should().Be(90); // Best score kept
        progress.Attempts.Should().Be(3);
        progress.Completed.Should().BeTrue(); // Stays completed
    }

    [Fact]
    public async Task GetLessonProgressAsync_ReturnsProgress_WhenExists()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        var existingProgress = TestDataGenerator.CreateProgress(score: 88, completed: true);
        _fixture.SeedProgress(context, existingProgress);

        // Act
        var progress = await service.GetLessonProgressAsync("course1", "module1", "lesson1");

        // Assert
        progress.Should().NotBeNull();
        progress!.Score.Should().Be(88);
        progress.Completed.Should().BeTrue();
    }

    [Fact]
    public async Task GetLessonProgressAsync_ReturnsNull_WhenNotExists()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        // Act
        var progress = await service.GetLessonProgressAsync("course1", "module1", "nonexistent");

        // Assert
        progress.Should().BeNull();
    }

    [Fact]
    public async Task GetModuleProgressAsync_ReturnsAllLessonsInModule()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(lessonId: "lesson1", score: 80));
        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(lessonId: "lesson2", score: 90));
        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(lessonId: "lesson3", score: 70));

        // Add progress for different module (should not be returned)
        _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(moduleId: "module2", lessonId: "lesson1"));

        // Act
        var progressList = await service.GetModuleProgressAsync("course1", "module1");

        // Assert
        progressList.Should().HaveCount(3);
        progressList.Should().Contain(p => p.LessonId == "lesson1" && p.Score == 80);
        progressList.Should().Contain(p => p.LessonId == "lesson2" && p.Score == 90);
        progressList.Should().Contain(p => p.LessonId == "lesson3" && p.Score == 70);
    }

    [Fact]
    public async Task IncrementHintUsageAsync_IncrementsHintCount_ForExistingProgress()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        var existingProgress = TestDataGenerator.CreateProgress(hintsUsed: 2);
        _fixture.SeedProgress(context, existingProgress);

        // Act
        await service.IncrementHintUsageAsync("course1", "module1", "lesson1");

        // Assert
        var progress = await context.Progress
            .FirstOrDefaultAsync(p => p.LessonId == "lesson1");

        progress!.HintsUsed.Should().Be(3);
    }

    [Fact]
    public async Task GetCourseProgressPercentageAsync_CalculatesCorrectly()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        // Create 10 lessons, 6 completed = 60%
        for (int i = 1; i <= 10; i++)
        {
            _fixture.SeedProgress(context, TestDataGenerator.CreateProgress(
                lessonId: $"lesson{i}",
                completed: i <= 6));
        }

        // Act
        var percentage = await service.GetCourseProgressPercentageAsync("course1");

        // Assert
        percentage.Should().Be(60);
    }

    [Fact]
    public async Task GetCourseProgressPercentageAsync_ReturnsZero_WhenNoProgress()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new ProgressService(context);

        // Act
        var percentage = await service.GetCourseProgressPercentageAsync("course1");

        // Assert
        percentage.Should().Be(0);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }
}
