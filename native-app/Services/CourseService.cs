using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for loading and managing courses from JSON files
/// NO HTTP, NO IPC - direct file I/O
/// </summary>
public class CourseService : ICourseService
{
    private readonly string _contentPath;
    private readonly Dictionary<string, Course> _cachedCourses = new();
    private readonly IErrorHandlerService? _errorHandler;
    private readonly ILogger<CourseService>? _logger;
    private bool _contentDirectoryValid;

    public CourseService(IErrorHandlerService? errorHandler = null, ILogger<CourseService>? logger = null)
    {
        _errorHandler = errorHandler;
        _logger = logger;

        // Get content directory - bundled with the app
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        _contentPath = Path.Combine(baseDir, "Content", "courses");

        _contentDirectoryValid = Directory.Exists(_contentPath);

        if (!_contentDirectoryValid)
        {
            var errorMessage = $"Content directory not found: {_contentPath}";
            _logger?.LogError(errorMessage);
            _errorHandler?.LogError(
                new DirectoryNotFoundException(errorMessage),
                "CourseService Initialization"
            );
            // Don't throw - allow app to start with degraded functionality
        }
    }

    /// <summary>
    /// Get all available courses
    /// </summary>
    public async Task<IEnumerable<CourseInfo>> GetCoursesAsync()
    {
        var courses = new List<CourseInfo>();

        if (!_contentDirectoryValid)
        {
            _logger?.LogWarning("Content directory is invalid - returning empty course list");
            return courses;
        }

        try
        {
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
                catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
                {
                    _logger?.LogError(ex, "Failed to load course from {Path}", courseJsonPath);
                    _errorHandler?.LogError(ex, $"Failed to load course from {Path.GetFileName(languageDir)}");
                    // Continue loading other courses
                }
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to enumerate course directories");
            _errorHandler?.LogError(ex, "Failed to load courses");
        }

        return courses;
    }

    /// <summary>
    /// Get a specific course by language
    /// </summary>
    public async Task<Course?> GetCourseAsync(string language)
    {
        if (!_contentDirectoryValid)
        {
            _logger?.LogWarning("Content directory is invalid - cannot load course {Language}", language);
            return null;
        }

        // Check cache first
        if (_cachedCourses.TryGetValue(language, out var cached))
        {
            return cached;
        }

        var courseJsonPath = Path.Combine(_contentPath, language, "course.json");
        if (!File.Exists(courseJsonPath))
        {
            _logger?.LogWarning("Course file not found: {Path}", courseJsonPath);
            return null;
        }

        try
        {
            var json = await File.ReadAllTextAsync(courseJsonPath);
            var course = JsonSerializer.Deserialize<Course>(json);

            if (course != null)
            {
                _cachedCourses[language] = course;
                _logger?.LogInformation("Loaded course {Language} with {ModuleCount} modules", language, course.Modules.Count);
            }

            return course;
        }
        catch (JsonException jsonEx)
        {
            _logger?.LogError(jsonEx, "Invalid JSON in course file {Path}", courseJsonPath);
            _errorHandler?.LogError(jsonEx, $"Failed to parse course '{language}' - file may be corrupt");
            return null;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to load course {Language}", language);
            _errorHandler?.LogError(ex, $"Failed to load course '{language}'");
            return null;
        }
    }

    /// <summary>
    /// Get a specific lesson
    /// </summary>
    public async Task<Lesson?> GetLessonAsync(string language, string moduleId, string lessonId)
    {
        try
        {
            var course = await GetCourseAsync(language);
            if (course == null)
            {
                _logger?.LogWarning("Cannot find lesson - course {Language} not loaded", language);
                return null;
            }

            foreach (var module in course.Modules)
            {
                if (module.Id == moduleId)
                {
                    var lesson = module.Lessons.Find(l => l.Id == lessonId);
                    if (lesson != null)
                    {
                        _logger?.LogDebug("Found lesson {LessonId} in module {ModuleId}", lessonId, moduleId);
                        return lesson;
                    }
                }
            }

            _logger?.LogWarning("Lesson not found: {Language}/{ModuleId}/{LessonId}", language, moduleId, lessonId);
            return null;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Error retrieving lesson {Language}/{ModuleId}/{LessonId}", language, moduleId, lessonId);
            _errorHandler?.LogError(ex, $"Failed to load lesson");
            return null;
        }
    }

    public async Task<LessonReference?> GetNextLessonAsync(string courseId, string moduleId, string lessonId)
    {
        try
        {
            var course = await GetCourseAsync(courseId);
            if (course == null) return null;

            var moduleIndex = course.Modules.FindIndex(m => m.Id == moduleId);
            if (moduleIndex == -1) return null;

            var module = course.Modules[moduleIndex];
            var lessonIndex = module.Lessons.FindIndex(l => l.Id == lessonId);
            if (lessonIndex == -1) return null;

            // Check if there is a next lesson in the same module
            if (lessonIndex + 1 < module.Lessons.Count)
            {
                var nextLesson = module.Lessons[lessonIndex + 1];
                return new LessonReference
                {
                    CourseId = courseId,
                    ModuleId = moduleId,
                    LessonId = nextLesson.Id,
                    Title = nextLesson.Title
                };
            }

            // Check if there is a next module
            if (moduleIndex + 1 < course.Modules.Count)
            {
                var nextModule = course.Modules[moduleIndex + 1];
                if (nextModule.Lessons.Count > 0)
                {
                    var nextLesson = nextModule.Lessons[0];
                    return new LessonReference
                    {
                        CourseId = courseId,
                        ModuleId = nextModule.Id,
                        LessonId = nextLesson.Id,
                        Title = nextLesson.Title
                    };
                }
            }

            return null;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Error retrieving next lesson for {CourseId}/{ModuleId}/{LessonId}", courseId, moduleId, lessonId);
            return null;
        }
    }

    public async Task<LessonReference?> GetPreviousLessonAsync(string courseId, string moduleId, string lessonId)
    {
        try
        {
            var course = await GetCourseAsync(courseId);
            if (course == null) return null;

            var moduleIndex = course.Modules.FindIndex(m => m.Id == moduleId);
            if (moduleIndex == -1) return null;

            var module = course.Modules[moduleIndex];
            var lessonIndex = module.Lessons.FindIndex(l => l.Id == lessonId);
            if (lessonIndex == -1) return null;

            // Check if there is a previous lesson in the same module
            if (lessonIndex - 1 >= 0)
            {
                var prevLesson = module.Lessons[lessonIndex - 1];
                return new LessonReference
                {
                    CourseId = courseId,
                    ModuleId = moduleId,
                    LessonId = prevLesson.Id,
                    Title = prevLesson.Title
                };
            }

            // Check if there is a previous module
            if (moduleIndex - 1 >= 0)
            {
                var prevModule = course.Modules[moduleIndex - 1];
                if (prevModule.Lessons.Count > 0)
                {
                    var prevLesson = prevModule.Lessons[prevModule.Lessons.Count - 1];
                    return new LessonReference
                    {
                        CourseId = courseId,
                        ModuleId = prevModule.Id,
                        LessonId = prevLesson.Id,
                        Title = prevLesson.Title
                    };
                }
            }

            return null;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Error retrieving previous lesson for {CourseId}/{ModuleId}/{LessonId}", courseId, moduleId, lessonId);
            return null;
        }
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
    public bool IsRuntimeAvailable { get; set; } = true;
}
