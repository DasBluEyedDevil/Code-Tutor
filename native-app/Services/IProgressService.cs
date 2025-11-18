using System.Collections.Generic;
using System.Threading.Tasks;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for managing user progress
/// </summary>
public interface IProgressService
{
    /// <summary>
    /// Save progress for a lesson
    /// </summary>
    Task SaveProgressAsync(string courseId, string moduleId, string lessonId, int score, bool completed);

    /// <summary>
    /// Get progress for a specific lesson
    /// </summary>
    Task<UserProgress?> GetLessonProgressAsync(string courseId, string moduleId, string lessonId);

    /// <summary>
    /// Get progress for all lessons in a module
    /// </summary>
    Task<List<UserProgress>> GetModuleProgressAsync(string courseId, string moduleId);

    /// <summary>
    /// Get overall course progress percentage
    /// </summary>
    Task<int> GetCourseProgressPercentageAsync(string courseId);
}
