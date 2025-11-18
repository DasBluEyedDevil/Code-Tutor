using Microsoft.EntityFrameworkCore;
using CodeTutor.Native.Services;
using CodeTutor.Native.Models;
using CodeTutor.Native.Tests.Fixtures;
using CodeTutor.Native.Tests.Helpers;

namespace CodeTutor.Native.Tests.Integration;

/// <summary>
/// Integration tests for complete lesson workflow
/// Tests the interaction between multiple services
/// </summary>
public class LessonCompletionWorkflowTests : IDisposable
{
    private readonly DatabaseFixture _fixture;

    public LessonCompletionWorkflowTests()
    {
        _fixture = new DatabaseFixture();
    }

    [Fact]
    public async Task CompleteLessonWorkflow_SavesProgressAndUnlocksAchievements()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var progressService = new ProgressService(context);
        var streakService = new StreakService(context);
        var achievementService = new AchievementService(context);

        string courseId = "course1";
        string moduleId = "module1";
        string lessonId = "lesson1";

        // Act 1: Complete 3 challenges with different scores
        await progressService.SaveChallengeProgressAsync(courseId, moduleId, lessonId, "challenge1", 100, true, hintsUsed: 0);
        await progressService.SaveChallengeProgressAsync(courseId, moduleId, lessonId, "challenge2", 80, true, hintsUsed: 1);
        await progressService.SaveChallengeProgressAsync(courseId, moduleId, lessonId, "challenge3", 100, true, hintsUsed: 0);

        // Act 2: Mark lesson complete
        await progressService.SaveProgressAsync(courseId, moduleId, lessonId, 93, completed: true, hintsUsed: 1);

        // Act 3: Record streak
        await streakService.RecordActivityAsync(lessonCompleted: true, challengeCompleted: false, minutesSpent: 25);

        // Act 4: Check achievements
        await achievementService.CheckAchievementsAsync();

        // Assert - Verify progress saved
        var lessonProgress = await progressService.GetLessonProgressAsync(courseId, moduleId, lessonId);
        lessonProgress.Should().NotBeNull();
        lessonProgress!.Score.Should().Be(93);
        lessonProgress.Completed.Should().BeTrue();
        lessonProgress.HintsUsed.Should().Be(1);

        // Assert - Verify challenge progress
        var challengeProgress = await context.Progress
            .Where(p => p.ChallengeId != null && p.LessonId == lessonId)
            .ToListAsync();
        challengeProgress.Should().HaveCount(3);

        // Assert - Verify streak recorded
        var currentStreak = await streakService.GetCurrentStreakAsync();
        currentStreak.Should().Be(1);

        // Assert - Verify FirstSteps achievement unlocked
        var firstSteps = await achievementService.GetAchievementProgressAsync(AchievementType.FirstSteps);
        firstSteps.Should().NotBeNull();
        firstSteps!.Progress.Should().Be(1);
    }

    [Fact]
    public async Task MultipleLessonCompletion_BuildsStreak_AndUnlocksAchievements()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var progressService = new ProgressService(context);
        var streakService = new StreakService(context);
        var achievementService = new AchievementService(context);

        // Act - Complete lessons over 7 consecutive days
        for (int day = 0; day < 7; day++)
        {
            // Simulate completing a lesson each day
            var lessonId = $"lesson{day + 1}";

            // Complete challenges
            for (int c = 1; c <= 3; c++)
            {
                await progressService.SaveChallengeProgressAsync(
                    "course1",
                    "module1",
                    lessonId,
                    $"challenge{c}",
                    100,
                    completed: true,
                    hintsUsed: 0);
            }

            // Complete lesson
            await progressService.SaveProgressAsync("course1", "module1", lessonId, 100, completed: true, hintsUsed: 0);

            // Record streak for this day
            var streakDate = DateTime.UtcNow.Date.AddDays(-day);
            var streak = new Streak
            {
                UserId = "00000000-0000-0000-0000-000000000001",
                Date = streakDate,
                LessonsCompleted = 1,
                ChallengesCompleted = 3,
                MinutesSpent = 30
            };
            context.Streaks.Add(streak);
            await context.SaveChangesAsync();
        }

        // Check achievements after all lessons
        await achievementService.CheckAchievementsAsync();

        // Assert - Verify 7-day streak
        var currentStreak = await streakService.GetCurrentStreakAsync();
        currentStreak.Should().Be(7);

        // Assert - Verify MarathonRunner unlocked
        var marathonRunner = await achievementService.GetAchievementProgressAsync(AchievementType.MarathonRunner);
        marathonRunner.Should().NotBeNull();

        // Assert - Verify FirstSteps unlocked
        var firstSteps = await achievementService.GetAchievementProgressAsync(AchievementType.FirstSteps);
        firstSteps.Should().NotBeNull();
    }

    [Fact]
    public async Task PerfectLessonCompletion_UnlocksPerfectionist()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var progressService = new ProgressService(context);
        var achievementService = new AchievementService(context);

        string courseId = "course1";
        string moduleId = "module1";
        string lessonId = "lesson1";

        // Act - Complete all challenges at 100%
        for (int i = 1; i <= 5; i++)
        {
            await progressService.SaveChallengeProgressAsync(
                courseId,
                moduleId,
                lessonId,
                $"challenge{i}",
                score: 100,
                completed: true,
                hintsUsed: 0);
        }

        // Complete lesson
        await progressService.SaveProgressAsync(courseId, moduleId, lessonId, 100, completed: true, hintsUsed: 0);

        // Check achievements
        await achievementService.CheckAchievementsAsync();

        // Assert - Verify Perfectionist unlocked
        var perfectionist = await achievementService.GetAchievementProgressAsync(AchievementType.Perfectionist);
        perfectionist.Should().NotBeNull();

        // Assert - Verify SpeedDemon unlocked (5 challenges without hints)
        var speedDemon = await achievementService.GetAchievementProgressAsync(AchievementType.SpeedDemon);
        speedDemon.Should().NotBeNull();
    }

    [Fact]
    public async Task QuickLessonCompletion_UnlocksQuickLearner()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var progressService = new ProgressService(context);
        var achievementService = new AchievementService(context);

        string courseId = "course1";
        string moduleId = "module1";
        string lessonId = "lesson1";

        // Create progress record with completion time < 30 minutes
        var quickProgress = new UserProgress
        {
            UserId = "00000000-0000-0000-0000-000000000001",
            CourseId = courseId,
            ModuleId = moduleId,
            LessonId = lessonId,
            ChallengeId = null,
            Score = 90,
            MaxScore = 100,
            HintsUsed = 0,
            Attempts = 1,
            Completed = true,
            FirstAttemptAt = DateTime.UtcNow.AddMinutes(-25), // Started 25 minutes ago
            CompletedAt = DateTime.UtcNow, // Just completed
            LastAttemptAt = DateTime.UtcNow
        };

        context.Progress.Add(quickProgress);
        await context.SaveChangesAsync();

        // Act
        await achievementService.CheckAchievementsAsync();

        // Assert
        var quickLearner = await achievementService.GetAchievementProgressAsync(AchievementType.QuickLearner);
        quickLearner.Should().NotBeNull();
        quickLearner!.Progress.Should().Be(1);
    }

    [Fact]
    public async Task MultipleAttempts_TracksFailures_ForDebuggerAchievement()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var progressService = new ProgressService(context);
        var achievementService = new AchievementService(context);

        // Act - Create challenges with multiple failed attempts
        // Challenge 1: 5 attempts (4 failures)
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge1", 50, false);
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge1", 60, false);
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge1", 70, false);
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge1", 80, false);
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge1", 100, true); // Success

        // Challenge 2: 4 attempts (3 failures)
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge2", 55, false);
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge2", 65, false);
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge2", 75, false);
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge2", 100, true); // Success

        // Challenge 3: 4 attempts (3 failures)
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge3", 60, false);
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge3", 70, false);
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge3", 80, false);
        await progressService.SaveChallengeProgressAsync("course1", "module1", "lesson1", "challenge3", 100, true); // Success

        // Total: 10 failures

        // Check achievements
        await achievementService.CheckAchievementsAsync();

        // Assert
        var debugger = await achievementService.GetAchievementProgressAsync(AchievementType.Debugger);
        debugger.Should().NotBeNull();
        debugger!.Progress.Should().BeGreaterOrEqualTo(10);
    }

    [Fact]
    public async Task StreakBreak_ResetsCurrentStreak_ButKeepsLongest()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var streakService = new StreakService(context);

        // Create a 5-day streak, then a 2-day gap, then a new 2-day streak
        var streaks = new List<Streak>();
        var today = DateTime.UtcNow.Date;

        // Current 2-day streak
        streaks.Add(TestDataGenerator.CreateStreak(date: today));
        streaks.Add(TestDataGenerator.CreateStreak(date: today.AddDays(-1)));

        // 2-day gap: -2, -3

        // Previous 5-day streak (this should be the longest)
        for (int i = 4; i <= 8; i++)
        {
            streaks.Add(TestDataGenerator.CreateStreak(date: today.AddDays(-i)));
        }

        _fixture.SeedStreaks(context, streaks.ToArray());

        // Act
        var currentStreak = await streakService.GetCurrentStreakAsync();
        var longestStreak = await streakService.GetLongestStreakAsync();

        // Assert
        currentStreak.Should().Be(2); // Current streak after break
        longestStreak.Should().Be(5); // Longest historical streak
    }

    [Fact]
    public async Task HintUsage_TrackedCorrectly_AcrossChallengesAndLesson()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var progressService = new ProgressService(context);

        string courseId = "course1";
        string moduleId = "module1";
        string lessonId = "lesson1";

        // Act - Complete challenges with varying hint usage
        await progressService.SaveChallengeProgressAsync(courseId, moduleId, lessonId, "challenge1", 100, true, hintsUsed: 0);
        await progressService.SaveChallengeProgressAsync(courseId, moduleId, lessonId, "challenge2", 85, true, hintsUsed: 2);
        await progressService.SaveChallengeProgressAsync(courseId, moduleId, lessonId, "challenge3", 100, true, hintsUsed: 1);

        // Complete lesson with total hints (should be sum: 0 + 2 + 1 = 3)
        await progressService.SaveProgressAsync(courseId, moduleId, lessonId, 95, completed: true, hintsUsed: 3);

        // Assert - Verify individual challenge hint tracking
        var challenge1 = await context.Progress.FirstOrDefaultAsync(p => p.ChallengeId == "challenge1");
        var challenge2 = await context.Progress.FirstOrDefaultAsync(p => p.ChallengeId == "challenge2");
        var challenge3 = await context.Progress.FirstOrDefaultAsync(p => p.ChallengeId == "challenge3");

        challenge1!.HintsUsed.Should().Be(0);
        challenge2!.HintsUsed.Should().Be(2);
        challenge3!.HintsUsed.Should().Be(1);

        // Assert - Verify lesson total hints
        var lesson = await progressService.GetLessonProgressAsync(courseId, moduleId, lessonId);
        lesson!.HintsUsed.Should().Be(3);
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }
}
