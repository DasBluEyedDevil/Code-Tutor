namespace CodeTutor.Native.Models;

/// <summary>
/// Types of achievements that can be unlocked
/// </summary>
public enum AchievementType
{
    /// <summary>
    /// Complete first lesson
    /// </summary>
    FirstSteps,

    /// <summary>
    /// Complete lesson in under 30 minutes
    /// </summary>
    QuickLearner,

    /// <summary>
    /// 100% score on all challenges in lesson
    /// </summary>
    Perfectionist,

    /// <summary>
    /// Complete lessons in 3+ languages
    /// </summary>
    Polyglot,

    /// <summary>
    /// 7-day learning streak
    /// </summary>
    MarathonRunner,

    /// <summary>
    /// Complete 5 challenges without hints
    /// </summary>
    SpeedDemon,

    /// <summary>
    /// Fix 10 failing test cases
    /// </summary>
    Debugger,

    /// <summary>
    /// Finish entire course
    /// </summary>
    CourseComplete,

    /// <summary>
    /// Pass 100 test cases
    /// </summary>
    TestMaster,

    /// <summary>
    /// Complete lesson after 10 PM
    /// </summary>
    NightOwl
}

/// <summary>
/// Definition of an achievement with metadata
/// </summary>
public class AchievementDefinition
{
    public AchievementType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int MaxProgress { get; set; } = 1;
    public int Points { get; set; } = 100;

    /// <summary>
    /// Get all achievement definitions
    /// </summary>
    public static Dictionary<AchievementType, AchievementDefinition> All => new()
    {
        {
            AchievementType.FirstSteps,
            new AchievementDefinition
            {
                Type = AchievementType.FirstSteps,
                Title = "First Steps",
                Description = "Complete your first lesson",
                Icon = "🎯",
                MaxProgress = 1,
                Points = 100
            }
        },
        {
            AchievementType.QuickLearner,
            new AchievementDefinition
            {
                Type = AchievementType.QuickLearner,
                Title = "Quick Learner",
                Description = "Complete a lesson in under 30 minutes",
                Icon = "⚡",
                MaxProgress = 1,
                Points = 150
            }
        },
        {
            AchievementType.Perfectionist,
            new AchievementDefinition
            {
                Type = AchievementType.Perfectionist,
                Title = "Perfectionist",
                Description = "Score 100% on all challenges in a lesson",
                Icon = "💯",
                MaxProgress = 1,
                Points = 200
            }
        },
        {
            AchievementType.Polyglot,
            new AchievementDefinition
            {
                Type = AchievementType.Polyglot,
                Title = "Polyglot",
                Description = "Complete lessons in 3 or more languages",
                Icon = "🌍",
                MaxProgress = 3,
                Points = 250
            }
        },
        {
            AchievementType.MarathonRunner,
            new AchievementDefinition
            {
                Type = AchievementType.MarathonRunner,
                Title = "Marathon Runner",
                Description = "Maintain a 7-day learning streak",
                Icon = "🏃",
                MaxProgress = 7,
                Points = 300
            }
        },
        {
            AchievementType.SpeedDemon,
            new AchievementDefinition
            {
                Type = AchievementType.SpeedDemon,
                Title = "Speed Demon",
                Description = "Complete 5 challenges without using hints",
                Icon = "🚀",
                MaxProgress = 5,
                Points = 175
            }
        },
        {
            AchievementType.Debugger,
            new AchievementDefinition
            {
                Type = AchievementType.Debugger,
                Title = "Debugger",
                Description = "Fix 10 failing test cases",
                Icon = "🐛",
                MaxProgress = 10,
                Points = 150
            }
        },
        {
            AchievementType.CourseComplete,
            new AchievementDefinition
            {
                Type = AchievementType.CourseComplete,
                Title = "Course Complete",
                Description = "Finish an entire course",
                Icon = "🎓",
                MaxProgress = 1,
                Points = 500
            }
        },
        {
            AchievementType.TestMaster,
            new AchievementDefinition
            {
                Type = AchievementType.TestMaster,
                Title = "Test Master",
                Description = "Pass 100 test cases",
                Icon = "✅",
                MaxProgress = 100,
                Points = 400
            }
        },
        {
            AchievementType.NightOwl,
            new AchievementDefinition
            {
                Type = AchievementType.NightOwl,
                Title = "Night Owl",
                Description = "Complete a lesson after 10 PM",
                Icon = "🦉",
                MaxProgress = 1,
                Points = 125
            }
        }
    };
}
