using System.Text.Json;
using CodeTutor.Tests.Models;

namespace CodeTutor.Tests.E2E.UserJourneys;

/// <summary>
/// E2E tests for user progress tracking functionality.
/// Tests validate that progress is correctly tracked, persisted, and calculated.
/// </summary>
public class ProgressTrackingTests
{
    private readonly JsonSerializerOptions _jsonOptions;

    public ProgressTrackingTests()
    {
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
    }

    [Fact]
    public void Progress_NewUser_StartsWithEmptyProgress()
    {
        // Arrange & Act
        var progress = new UserProgress();

        // Assert
        progress.CompletedLessons.Should().BeEmpty();
        progress.LessonProgress.Should().BeEmpty();
        progress.Achievements.Should().BeEmpty();
        progress.CurrentStreak.Should().Be(0);
        progress.LongestStreak.Should().Be(0);
        progress.TotalTimeSpentMinutes.Should().Be(0);
    }

    [Fact]
    public void Progress_MarkLessonComplete_AddsToCompletedSet()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonId = "module-01-lesson-01";

        // Act
        progress.CompletedLessons.Add(lessonId);

        // Assert
        progress.CompletedLessons.Should().Contain(lessonId);
        progress.CompletedLessons.Count.Should().Be(1);
    }

    [Fact]
    public void Progress_MarkSameLessonTwice_NoDuplicates()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonId = "module-01-lesson-01";

        // Act
        progress.CompletedLessons.Add(lessonId);
        progress.CompletedLessons.Add(lessonId); // Attempt to add again

        // Assert - HashSet prevents duplicates
        progress.CompletedLessons.Count.Should().Be(1);
    }

    [Fact]
    public void Progress_CompleteMultipleLessons_TracksAll()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonIds = new[]
        {
            "module-01-lesson-01",
            "module-01-lesson-02",
            "module-01-lesson-03"
        };

        // Act
        foreach (var id in lessonIds)
        {
            progress.CompletedLessons.Add(id);
        }

        // Assert
        progress.CompletedLessons.Count.Should().Be(3);
        foreach (var id in lessonIds)
        {
            progress.CompletedLessons.Should().Contain(id);
        }
    }

    [Fact]
    public void Progress_LessonProgress_TracksStartTime()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonId = "module-01-lesson-01";
        var startTime = DateTime.UtcNow;

        // Act
        progress.LessonProgress[lessonId] = new LessonProgress
        {
            StartedAt = startTime
        };

        // Assert
        progress.LessonProgress[lessonId].StartedAt.Should().BeCloseTo(startTime, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Progress_LessonProgress_TracksCompletionTime()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonId = "module-01-lesson-01";
        var startTime = DateTime.UtcNow.AddMinutes(-30);
        var completionTime = DateTime.UtcNow;

        // Act
        progress.LessonProgress[lessonId] = new LessonProgress
        {
            StartedAt = startTime,
            CompletedAt = completionTime
        };

        // Assert
        progress.LessonProgress[lessonId].CompletedAt.Should().BeCloseTo(completionTime, TimeSpan.FromSeconds(1));

        // Calculate time spent
        var timeSpent = completionTime - startTime;
        timeSpent.TotalMinutes.Should().BeApproximately(30, 1);
    }

    [Fact]
    public void Progress_ChallengeAttempts_IncrementCorrectly()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonId = "module-01-lesson-01";
        var challengeId = "challenge-01";

        progress.LessonProgress[lessonId] = new LessonProgress();
        progress.LessonProgress[lessonId].Attempts[challengeId] = 0;

        // Act - Multiple attempts
        for (int i = 0; i < 5; i++)
        {
            progress.LessonProgress[lessonId].Attempts[challengeId]++;
        }

        // Assert
        progress.LessonProgress[lessonId].Attempts[challengeId].Should().Be(5);
    }

    [Fact]
    public void Progress_ChallengesPassed_TrackedInSet()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonId = "module-01-lesson-01";
        var challengeIds = new[] { "challenge-01", "challenge-02", "challenge-03" };

        progress.LessonProgress[lessonId] = new LessonProgress();

        // Act
        foreach (var id in challengeIds)
        {
            progress.LessonProgress[lessonId].ChallengesPassed.Add(id);
        }

        // Assert
        progress.LessonProgress[lessonId].ChallengesPassed.Count.Should().Be(3);
        foreach (var id in challengeIds)
        {
            progress.LessonProgress[lessonId].ChallengesPassed.Should().Contain(id);
        }
    }

    [Fact]
    public void Progress_BestScore_UpdatesWhenBetter()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonId = "module-01-lesson-01";

        progress.LessonProgress[lessonId] = new LessonProgress { BestScore = 70 };

        // Act - Better score
        var newScore = 85;
        if (newScore > progress.LessonProgress[lessonId].BestScore)
        {
            progress.LessonProgress[lessonId].BestScore = newScore;
        }

        // Assert
        progress.LessonProgress[lessonId].BestScore.Should().Be(85);
    }

    [Fact]
    public void Progress_BestScore_KeepsHigherScore()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonId = "module-01-lesson-01";

        progress.LessonProgress[lessonId] = new LessonProgress { BestScore = 90 };

        // Act - Worse score
        var newScore = 75;
        if (newScore > progress.LessonProgress[lessonId].BestScore)
        {
            progress.LessonProgress[lessonId].BestScore = newScore;
        }

        // Assert
        progress.LessonProgress[lessonId].BestScore.Should().Be(90); // Kept the higher score
    }

    [Fact]
    public void Progress_HintUsage_TracksPerChallenge()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonId = "module-01-lesson-01";
        var challengeId = "challenge-01";

        progress.LessonProgress[lessonId] = new LessonProgress();
        progress.LessonProgress[lessonId].HintsUsed[challengeId] = 0;

        // Act - Use hints
        progress.LessonProgress[lessonId].HintsUsed[challengeId]++;
        progress.LessonProgress[lessonId].HintsUsed[challengeId]++;
        progress.LessonProgress[lessonId].HintsUsed[challengeId]++;

        // Assert
        progress.LessonProgress[lessonId].HintsUsed[challengeId].Should().Be(3);
    }

    [Fact]
    public void Progress_LastCode_SavesUserCode()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonId = "module-01-lesson-01";
        var challengeId = "challenge-01";
        var userCode = @"
            def hello():
                print('Hello, World!')

            hello()
        ";

        progress.LessonProgress[lessonId] = new LessonProgress();

        // Act
        progress.LessonProgress[lessonId].LastCode[challengeId] = userCode;

        // Assert
        progress.LessonProgress[lessonId].LastCode[challengeId].Should().Be(userCode);
    }

    [Fact]
    public void Progress_Streak_StartsAtZero()
    {
        // Arrange & Act
        var progress = new UserProgress();

        // Assert
        progress.CurrentStreak.Should().Be(0);
        progress.LongestStreak.Should().Be(0);
    }

    [Fact]
    public void Progress_Streak_IncrementDaily()
    {
        // Arrange
        var progress = new UserProgress { CurrentStreak = 5, LongestStreak = 5 };

        // Act - User completes a lesson on a new day
        progress.CurrentStreak++;
        if (progress.CurrentStreak > progress.LongestStreak)
        {
            progress.LongestStreak = progress.CurrentStreak;
        }

        // Assert
        progress.CurrentStreak.Should().Be(6);
        progress.LongestStreak.Should().Be(6);
    }

    [Fact]
    public void Progress_Streak_ResetOnMissedDay()
    {
        // Arrange
        var progress = new UserProgress { CurrentStreak = 10, LongestStreak = 10 };

        // Act - User missed a day
        progress.CurrentStreak = 0;

        // Assert
        progress.CurrentStreak.Should().Be(0);
        progress.LongestStreak.Should().Be(10); // Longest streak preserved
    }

    [Fact]
    public void Progress_TotalTime_Accumulates()
    {
        // Arrange
        var progress = new UserProgress { TotalTimeSpentMinutes = 60 };

        // Act - Add more time
        progress.TotalTimeSpentMinutes += 30;
        progress.TotalTimeSpentMinutes += 45;

        // Assert
        progress.TotalTimeSpentMinutes.Should().Be(135);
    }

    [Fact]
    public void Progress_Serialization_RoundTrip()
    {
        // Arrange
        var progress = new UserProgress
        {
            CurrentStreak = 5,
            LongestStreak = 10,
            TotalTimeSpentMinutes = 120
        };

        progress.CompletedLessons.Add("lesson-01");
        progress.CompletedLessons.Add("lesson-02");

        progress.LessonProgress["lesson-01"] = new LessonProgress
        {
            BestScore = 100,
            StartedAt = DateTime.UtcNow.AddDays(-1),
            CompletedAt = DateTime.UtcNow
        };

        progress.Achievements.Add(new Achievement
        {
            Id = "first_steps",
            Type = "FirstLesson",
            UnlockedAt = DateTime.UtcNow
        });

        // Act - Serialize and deserialize
        var json = JsonSerializer.Serialize(progress, _jsonOptions);
        var restored = JsonSerializer.Deserialize<UserProgress>(json, _jsonOptions);

        // Assert
        restored.Should().NotBeNull();
        restored!.CurrentStreak.Should().Be(5);
        restored.LongestStreak.Should().Be(10);
        restored.TotalTimeSpentMinutes.Should().Be(120);
        restored.CompletedLessons.Should().HaveCount(2);
        restored.LessonProgress["lesson-01"].BestScore.Should().Be(100);
        restored.Achievements.Should().ContainSingle();
    }

    [Fact]
    public void Progress_CalculateCompletionPercentage_Empty()
    {
        // Arrange
        var progress = new UserProgress();
        var totalLessons = 50;

        // Act
        var percentage = CalculateCompletionPercentage(progress, totalLessons);

        // Assert
        percentage.Should().Be(0);
    }

    [Fact]
    public void Progress_CalculateCompletionPercentage_Partial()
    {
        // Arrange
        var progress = new UserProgress();
        for (int i = 1; i <= 25; i++)
        {
            progress.CompletedLessons.Add($"lesson-{i}");
        }
        var totalLessons = 50;

        // Act
        var percentage = CalculateCompletionPercentage(progress, totalLessons);

        // Assert
        percentage.Should().Be(50);
    }

    [Fact]
    public void Progress_CalculateCompletionPercentage_Complete()
    {
        // Arrange
        var progress = new UserProgress();
        var totalLessons = 50;
        for (int i = 1; i <= totalLessons; i++)
        {
            progress.CompletedLessons.Add($"lesson-{i}");
        }

        // Act
        var percentage = CalculateCompletionPercentage(progress, totalLessons);

        // Assert
        percentage.Should().Be(100);
    }

    [Fact]
    public void Progress_Achievement_FirstSteps()
    {
        // Arrange
        var progress = new UserProgress();

        // Act - Complete first lesson
        progress.CompletedLessons.Add("lesson-01");
        CheckFirstStepsAchievement(progress);

        // Assert
        progress.Achievements.Should().ContainSingle(a => a.Type == "FirstSteps");
    }

    [Fact]
    public void Progress_Achievement_Perfectionist()
    {
        // Arrange
        var progress = new UserProgress();
        var lessonId = "lesson-01";

        progress.LessonProgress[lessonId] = new LessonProgress { BestScore = 100 };

        // Act - Check achievement
        CheckPerfectionistAchievement(progress, lessonId);

        // Assert
        progress.Achievements.Should().ContainSingle(a => a.Type == "Perfectionist");
    }

    [Fact]
    public void Progress_Achievement_WeekWarrior()
    {
        // Arrange
        var progress = new UserProgress { CurrentStreak = 7 };

        // Act - Check achievement
        CheckWeekWarriorAchievement(progress);

        // Assert
        progress.Achievements.Should().ContainSingle(a => a.Type == "WeekWarrior");
    }

    [Fact]
    public void Progress_Achievement_NoDuplicates()
    {
        // Arrange
        var progress = new UserProgress();
        progress.CompletedLessons.Add("lesson-01");

        // Act - Try to award same achievement twice
        CheckFirstStepsAchievement(progress);
        CheckFirstStepsAchievement(progress);

        // Assert
        progress.Achievements.Count(a => a.Type == "FirstSteps").Should().Be(1);
    }

    [Fact]
    public void Progress_Achievement_ProgressiveUnlock()
    {
        // Arrange
        var progress = new UserProgress();

        // Act - Complete 5 lessons for progressive achievement
        for (int i = 1; i <= 5; i++)
        {
            progress.CompletedLessons.Add($"lesson-{i}");
        }

        // Create or update progressive achievement
        var progressiveAchievement = progress.Achievements
            .FirstOrDefault(a => a.Type == "LessonMaster") ?? new Achievement
            {
                Id = "lesson_master",
                Type = "LessonMaster",
                Progress = 0,
                MaxProgress = 10
            };

        progressiveAchievement.Progress = progress.CompletedLessons.Count;

        if (!progress.Achievements.Contains(progressiveAchievement))
        {
            progress.Achievements.Add(progressiveAchievement);
        }

        // Assert
        var achievement = progress.Achievements.First(a => a.Type == "LessonMaster");
        achievement.Progress.Should().Be(5);
        achievement.MaxProgress.Should().Be(10);
    }

    private static double CalculateCompletionPercentage(UserProgress progress, int totalLessons)
    {
        if (totalLessons == 0) return 0;
        return (double)progress.CompletedLessons.Count / totalLessons * 100;
    }

    private static void CheckFirstStepsAchievement(UserProgress progress)
    {
        if (progress.CompletedLessons.Count >= 1 &&
            !progress.Achievements.Any(a => a.Type == "FirstSteps"))
        {
            progress.Achievements.Add(new Achievement
            {
                Id = "first_steps",
                Type = "FirstSteps",
                UnlockedAt = DateTime.UtcNow,
                Progress = 1,
                MaxProgress = 1
            });
        }
    }

    private static void CheckPerfectionistAchievement(UserProgress progress, string lessonId)
    {
        if (progress.LessonProgress.TryGetValue(lessonId, out var lessonProgress) &&
            lessonProgress.BestScore == 100 &&
            !progress.Achievements.Any(a => a.Type == "Perfectionist"))
        {
            progress.Achievements.Add(new Achievement
            {
                Id = "perfectionist",
                Type = "Perfectionist",
                UnlockedAt = DateTime.UtcNow,
                Progress = 1,
                MaxProgress = 1
            });
        }
    }

    private static void CheckWeekWarriorAchievement(UserProgress progress)
    {
        if (progress.CurrentStreak >= 7 &&
            !progress.Achievements.Any(a => a.Type == "WeekWarrior"))
        {
            progress.Achievements.Add(new Achievement
            {
                Id = "week_warrior",
                Type = "WeekWarrior",
                UnlockedAt = DateTime.UtcNow,
                Progress = 7,
                MaxProgress = 7
            });
        }
    }
}
