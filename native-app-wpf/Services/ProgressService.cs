using System;
using System.IO;
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
}

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
}
