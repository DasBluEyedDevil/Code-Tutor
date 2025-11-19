using System.Collections.Generic;
using System.Threading.Tasks;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for loading and managing course content from bundled JSON files
/// </summary>
public interface ICourseService
{
    /// <summary>
    /// Get all available courses
    /// </summary>
    Task<IEnumerable<CourseInfo>> GetCoursesAsync();

    /// <summary>
    /// Get a specific course by language ID
    /// </summary>
    Task<Course?> GetCourseAsync(string language);

    /// <summary>
    /// Get a specific lesson from a course
    /// </summary>
    Task<Lesson?> GetLessonAsync(string courseId, string moduleId, string lessonId);

    /// <summary>
    /// Get the next lesson in the course
    /// </summary>
    Task<LessonReference?> GetNextLessonAsync(string courseId, string moduleId, string lessonId);

    /// <summary>
    /// Get the previous lesson in the course
    /// </summary>
    Task<LessonReference?> GetPreviousLessonAsync(string courseId, string moduleId, string lessonId);
}
