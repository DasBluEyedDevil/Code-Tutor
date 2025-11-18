using System;

namespace CodeTutor.Native.Models;

/// <summary>
/// Represents user progress for a specific lesson
/// </summary>
public class UserProgress
{
    public string UserId { get; set; } = string.Empty;
    public string CourseId { get; set; } = string.Empty;
    public string ModuleId { get; set; } = string.Empty;
    public string LessonId { get; set; } = string.Empty;
    public bool Completed { get; set; }
    public int Score { get; set; }
    public DateTime LastAccessed { get; set; }
    public DateTime? CompletedAt { get; set; }
}
