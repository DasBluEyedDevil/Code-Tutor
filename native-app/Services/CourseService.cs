using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for loading and managing courses from JSON files
/// NO HTTP, NO IPC - direct file I/O
/// </summary>
public class CourseService
{
    private readonly string _contentPath;
    private readonly Dictionary<string, Course> _cachedCourses = new();

    public CourseService()
    {
        // Get content directory - bundled with the app
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        _contentPath = Path.Combine(baseDir, "Content", "courses");

        if (!Directory.Exists(_contentPath))
        {
            throw new DirectoryNotFoundException($"Content directory not found: {_contentPath}");
        }
    }

    /// <summary>
    /// Get all available courses
    /// </summary>
    public async Task<List<CourseInfo>> GetCoursesAsync()
    {
        var courses = new List<CourseInfo>();
        var languages = Directory.GetDirectories(_contentPath);

        foreach (var languageDir in languages)
        {
            var courseJsonPath = Path.Combine(languageDir, "course.json");
            if (!File.Exists(courseJsonPath)) continue;

            try
            {
                var json = await File.ReadAllTextAsync(courseJsonPath);
                var course = JsonSerializer.Deserialize<Course>(json);

                if (course != null)
                {
                    courses.Add(new CourseInfo
                    {
                        Id = course.Id,
                        Language = course.Language,
                        Title = course.Title,
                        Description = course.Description,
                        Difficulty = course.Difficulty,
                        EstimatedHours = course.EstimatedHours,
                        ModuleCount = course.Modules.Count
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load course from {courseJsonPath}: {ex.Message}");
            }
        }

        return courses;
    }

    /// <summary>
    /// Get a specific course by language
    /// </summary>
    public async Task<Course?> GetCourseAsync(string language)
    {
        // Check cache first
        if (_cachedCourses.TryGetValue(language, out var cached))
        {
            return cached;
        }

        var courseJsonPath = Path.Combine(_contentPath, language, "course.json");
        if (!File.Exists(courseJsonPath))
        {
            return null;
        }

        try
        {
            var json = await File.ReadAllTextAsync(courseJsonPath);
            var course = JsonSerializer.Deserialize<Course>(json);

            if (course != null)
            {
                _cachedCourses[language] = course;
            }

            return course;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load course {language}: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Get a specific lesson
    /// </summary>
    public async Task<Lesson?> GetLessonAsync(string language, string moduleId, string lessonId)
    {
        var course = await GetCourseAsync(language);
        if (course == null) return null;

        foreach (var module in course.Modules)
        {
            if (module.Id == moduleId)
            {
                return module.Lessons.Find(l => l.Id == lessonId);
            }
        }

        return null;
    }
}

/// <summary>
/// Lightweight course info for listing
/// </summary>
public class CourseInfo
{
    public string Id { get; set; } = string.Empty;
    public string Language { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public int EstimatedHours { get; set; }
    public int ModuleCount { get; set; }
}
