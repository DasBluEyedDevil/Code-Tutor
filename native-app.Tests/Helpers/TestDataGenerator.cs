using CodeTutor.Native.Models;
using CodeTutor.Native.Models.Challenges;

namespace CodeTutor.Native.Tests.Helpers;

/// <summary>
/// Generates test data for unit and integration tests
/// </summary>
public static class TestDataGenerator
{
    private const string DefaultUserId = "00000000-0000-0000-0000-000000000001";

    /// <summary>
    /// Creates a sample UserProgress record
    /// </summary>
    public static UserProgress CreateProgress(
        string courseId = "course1",
        string moduleId = "module1",
        string lessonId = "lesson1",
        string? challengeId = null,
        int score = 85,
        bool completed = false,
        int hintsUsed = 0,
        int attempts = 1)
    {
        return new UserProgress
        {
            UserId = DefaultUserId,
            CourseId = courseId,
            ModuleId = moduleId,
            LessonId = lessonId,
            ChallengeId = challengeId,
            Score = score,
            MaxScore = 100,
            HintsUsed = hintsUsed,
            Attempts = attempts,
            Completed = completed,
            CompletedAt = completed ? DateTime.UtcNow : null,
            FirstAttemptAt = DateTime.UtcNow.AddMinutes(-30),
            LastAttemptAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Creates a sample Streak record
    /// </summary>
    public static Streak CreateStreak(
        DateTime? date = null,
        int lessonsCompleted = 1,
        int challengesCompleted = 5,
        int minutesSpent = 45)
    {
        return new Streak
        {
            UserId = DefaultUserId,
            Date = (date ?? DateTime.UtcNow).Date,
            LessonsCompleted = lessonsCompleted,
            ChallengesCompleted = challengesCompleted,
            MinutesSpent = minutesSpent
        };
    }

    /// <summary>
    /// Creates a sample Achievement record
    /// </summary>
    public static Achievement CreateAchievement(
        string achievementType,
        int progress = 1,
        int maxProgress = 1,
        bool unlocked = true)
    {
        return new Achievement
        {
            UserId = DefaultUserId,
            AchievementType = achievementType,
            Progress = progress,
            MaxProgress = maxProgress,
            UnlockedAt = unlocked ? DateTime.UtcNow : DateTime.MinValue,
            Notified = false
        };
    }

    /// <summary>
    /// Creates a sample FreeCodingChallenge
    /// </summary>
    public static FreeCodingChallenge CreateFreeCodingChallenge(
        string id = "challenge1",
        string language = "csharp")
    {
        return new FreeCodingChallenge
        {
            Id = id,
            Type = "FreeCoding",
            Title = "Test Challenge",
            Description = "Test description",
            Points = 100,
            Language = language,
            StarterCode = "// Start here",
            Solution = "// Solution",
            TestCases = new List<TestCase>
            {
                new TestCase
                {
                    Input = "5",
                    ExpectedOutput = "25",
                    IsHidden = false
                }
            },
            Hints = new List<string> { "Hint 1", "Hint 2" },
            TimeLimit = 30
        };
    }

    /// <summary>
    /// Creates a sample MultipleChoiceChallenge
    /// </summary>
    public static MultipleChoiceChallenge CreateMultipleChoiceChallenge(
        string id = "mc1",
        int correctAnswer = 0)
    {
        return new MultipleChoiceChallenge
        {
            Id = id,
            Type = "MultipleChoice",
            Title = "Test Question",
            Description = "Choose the correct answer",
            Points = 10,
            Question = "What is 2 + 2?",
            Options = new List<string> { "4", "5", "3", "6" },
            CorrectOption = correctAnswer,
            Explanation = "Basic math",
            Hints = new List<string>()
        };
    }

    /// <summary>
    /// Creates a sample Course with modules and lessons
    /// </summary>
    public static Course CreateCourse(string id = "course1", int moduleCount = 2, int lessonsPerModule = 3)
    {
        var course = new Course
        {
            Id = id,
            Title = "Test Course",
            Description = "Test course description",
            Language = "csharp",
            Difficulty = "beginner",
            EstimatedHours = 10,
            Modules = new List<Module>()
        };

        for (int i = 0; i < moduleCount; i++)
        {
            var module = new Module
            {
                Id = $"module{i + 1}",
                Title = $"Module {i + 1}",
                Description = $"Module {i + 1} description",
                Lessons = new List<Lesson>()
            };

            for (int j = 0; j < lessonsPerModule; j++)
            {
                var lesson = new Lesson
                {
                    Id = $"lesson{j + 1}",
                    Title = $"Lesson {j + 1}",
                    Content = new LessonContent
                    {
                        Body = $"# Lesson {j + 1}\n\nContent here",
                        Examples = new List<CodeExample>()
                    },
                    Exercises = new List<Challenge>()
                };

                module.Lessons.Add(lesson);
            }

            course.Modules.Add(module);
        }

        return course;
    }

    /// <summary>
    /// Creates a sample Lesson
    /// </summary>
    public static Lesson CreateLesson(string id = "lesson1", int challengeCount = 3)
    {
        var lesson = new Lesson
        {
            Id = id,
            Title = "Test Lesson",
            Content = new LessonContent
            {
                Body = "# Test Lesson\n\nLesson content",
                KeyConcepts = new List<string> { "Concept 1", "Concept 2" },
                Examples = new List<CodeExample>(),
                CommonMistakes = new List<CommonMistake>()
            },
            Exercises = new List<Challenge>()
        };

        for (int i = 0; i < challengeCount; i++)
        {
            lesson.Exercises.Add(CreateMultipleChoiceChallenge($"challenge{i + 1}", correctAnswer: 0));
        }

        return lesson;
    }

    /// <summary>
    /// Creates consecutive streak records for testing
    /// </summary>
    public static List<Streak> CreateConsecutiveStreaks(int days, DateTime? startDate = null)
    {
        var start = (startDate ?? DateTime.UtcNow).Date;
        var streaks = new List<Streak>();

        for (int i = 0; i < days; i++)
        {
            streaks.Add(CreateStreak(
                date: start.AddDays(-i),
                lessonsCompleted: 1,
                challengesCompleted: 3,
                minutesSpent: 30));
        }

        return streaks;
    }

    /// <summary>
    /// Creates streak records with a gap for testing streak breaks
    /// </summary>
    public static List<Streak> CreateStreaksWithGap(int daysBeforeGap, int gapDays, int daysAfterGap)
    {
        var today = DateTime.UtcNow.Date;
        var streaks = new List<Streak>();

        // Days after gap (most recent)
        for (int i = 0; i < daysAfterGap; i++)
        {
            streaks.Add(CreateStreak(date: today.AddDays(-i)));
        }

        // Days before gap
        int startDayBeforeGap = daysAfterGap + gapDays;
        for (int i = 0; i < daysBeforeGap; i++)
        {
            streaks.Add(CreateStreak(date: today.AddDays(-(startDayBeforeGap + i))));
        }

        return streaks;
    }
}
