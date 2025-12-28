using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Services;

public interface ICourseService
{
    Task<List<Course>> GetAllCoursesAsync();
    Task<Course?> GetCourseAsync(string courseId);
    Task<Lesson?> GetLessonAsync(string courseId, string moduleId, string lessonId);
}

public class CourseService : ICourseService
{
    private readonly string _contentPath;
    private readonly ConcurrentDictionary<string, Course> _courseCache = new();

    public CourseService()
    {
        _contentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "courses");
    }

    public async Task<List<Course>> GetAllCoursesAsync()
    {
        var courses = new List<Course>();

        if (!Directory.Exists(_contentPath))
            return courses;

        foreach (var dir in Directory.GetDirectories(_contentPath))
        {
            var courseFile = Path.Combine(dir, "course.json");
            if (File.Exists(courseFile))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(courseFile);
                    var course = JsonSerializer.Deserialize<Course>(json);
                    if (course != null)
                    {
                        _courseCache.TryAdd(course.Id, course);
                        courses.Add(course);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to load course from {dir}: {ex.Message}");
                }
            }
        }

        return courses;
    }

    public async Task<Course?> GetCourseAsync(string courseId)
    {
        if (_courseCache.TryGetValue(courseId, out var cached))
            return cached;

        await GetAllCoursesAsync();
        return _courseCache.GetValueOrDefault(courseId);
    }

    public async Task<Lesson?> GetLessonAsync(string courseId, string moduleId, string lessonId)
    {
        var course = await GetCourseAsync(courseId);
        if (course == null) return null;

        foreach (var module in course.Modules)
        {
            if (module.Id == moduleId)
            {
                foreach (var lesson in module.Lessons)
                {
                    if (lesson.Id == lessonId)
                        return lesson;
                }
            }
        }

        return null;
    }
}
