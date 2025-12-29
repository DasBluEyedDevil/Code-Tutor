using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Services;

public interface IProgressService
{
    Task<UserProgress> LoadProgressAsync();
    Task SaveProgressAsync(UserProgress progress);
    Task MarkLessonCompleteAsync(string lessonId);
    Task<bool> IsLessonCompleteAsync(string lessonId);
    Task<CourseProgressStats> GetCourseProgressAsync(Course course);
    int GetCurrentStreak();
}

public record CourseProgressStats(
    int CompletedLessons,
    int TotalLessons,
    double PercentComplete,
    int CurrentStreak,
    TimeSpan TimeThisWeek
);

public class ProgressService : IProgressService
{
    private readonly string _progressFilePath;
    private UserProgress? _cachedProgress;

    public ProgressService()
    {
        var appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CodeTutor");
        Directory.CreateDirectory(appDataPath);
        _progressFilePath = Path.Combine(appDataPath, "progress.json");
    }

    public async Task<UserProgress> LoadProgressAsync()
    {
        if (_cachedProgress != null) return _cachedProgress;

        if (!File.Exists(_progressFilePath))
        {
            _cachedProgress = new UserProgress();
            return _cachedProgress;
        }

        try
        {
            var json = await File.ReadAllTextAsync(_progressFilePath);
            _cachedProgress = JsonSerializer.Deserialize<UserProgress>(json) ?? new UserProgress();
        }
        catch
        {
            _cachedProgress = new UserProgress();
        }

        return _cachedProgress;
    }

    public async Task SaveProgressAsync(UserProgress progress)
    {
        progress.LastUpdated = DateTime.UtcNow;
        _cachedProgress = progress;

        var json = JsonSerializer.Serialize(progress, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_progressFilePath, json);
    }

    public async Task MarkLessonCompleteAsync(string lessonId)
    {
        var progress = await LoadProgressAsync();
        progress.CompletedLessons.Add(lessonId);
        await SaveProgressAsync(progress);
    }

    public async Task<bool> IsLessonCompleteAsync(string lessonId)
    {
        var progress = await LoadProgressAsync();
        return progress.CompletedLessons.Contains(lessonId);
    }

    public async Task<CourseProgressStats> GetCourseProgressAsync(Course course)
    {
        var progress = await LoadProgressAsync();

        var allLessons = course.Modules.SelectMany(m => m.Lessons).ToList();
        int completed = allLessons.Count(l => progress.CompletedLessons.Contains(l.Id));
        int total = allLessons.Count;
        double percent = total > 0 ? (double)completed / total * 100 : 0;

        return new CourseProgressStats(
            CompletedLessons: completed,
            TotalLessons: total,
            PercentComplete: Math.Round(percent, 1),
            CurrentStreak: GetCurrentStreak(),
            TimeThisWeek: TimeSpan.Zero // Placeholder - would need session tracking
        );
    }

    public int GetCurrentStreak()
    {
        // Placeholder implementation - would need daily activity tracking
        return 0;
    }
}
