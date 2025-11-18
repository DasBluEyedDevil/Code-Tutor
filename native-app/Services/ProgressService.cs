using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Simple file-based progress service (will be replaced with SQLite in Phase 4)
/// </summary>
public class ProgressService : IProgressService
{
    private readonly string _progressFilePath;
    private Dictionary<string, UserProgress> _progressCache = new();
    private const string DefaultUserId = "default-user";

    public ProgressService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var codeTutorPath = Path.Combine(appDataPath, "CodeTutor");
        Directory.CreateDirectory(codeTutorPath);
        _progressFilePath = Path.Combine(codeTutorPath, "progress.json");

        LoadProgressAsync().Wait();
    }

    public async Task SaveProgressAsync(string courseId, string moduleId, string lessonId, int score, bool completed)
    {
        var key = $"{courseId}/{moduleId}/{lessonId}";

        if (!_progressCache.ContainsKey(key))
        {
            _progressCache[key] = new UserProgress
            {
                UserId = DefaultUserId,
                CourseId = courseId,
                ModuleId = moduleId,
                LessonId = lessonId,
                Score = score,
                Completed = completed,
                FirstAttemptAt = DateTime.UtcNow,
                LastAttemptAt = DateTime.UtcNow
            };
        }
        else
        {
            var progress = _progressCache[key];
            progress.Score = Math.Max(progress.Score, score);
            progress.Completed = completed;
            progress.LastAttemptAt = DateTime.UtcNow;
        }

        await SaveToDiskAsync();
    }

    public Task<UserProgress?> GetLessonProgressAsync(string courseId, string moduleId, string lessonId)
    {
        var key = $"{courseId}/{moduleId}/{lessonId}";
        _progressCache.TryGetValue(key, out var progress);
        return Task.FromResult(progress);
    }

    public Task<List<UserProgress>> GetModuleProgressAsync(string courseId, string moduleId)
    {
        var progress = _progressCache.Values
            .Where(p => p.CourseId == courseId && p.ModuleId == moduleId)
            .ToList();

        return Task.FromResult(progress);
    }

    public async Task<int> GetCourseProgressPercentageAsync(string courseId)
    {
        var courseProgress = _progressCache.Values
            .Where(p => p.CourseId == courseId && p.ChallengeId == null)
            .ToList();

        if (courseProgress.Count == 0)
            return 0;

        var completed = courseProgress.Count(p => p.Completed);
        return (completed * 100) / courseProgress.Count;
    }

    private async Task LoadProgressAsync()
    {
        if (!File.Exists(_progressFilePath))
        {
            _progressCache = new Dictionary<string, UserProgress>();
            return;
        }

        try
        {
            var json = await File.ReadAllTextAsync(_progressFilePath);
            _progressCache = JsonSerializer.Deserialize<Dictionary<string, UserProgress>>(json)
                ?? new Dictionary<string, UserProgress>();
        }
        catch
        {
            _progressCache = new Dictionary<string, UserProgress>();
        }
    }

    private async Task SaveToDiskAsync()
    {
        try
        {
            var json = JsonSerializer.Serialize(_progressCache, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(_progressFilePath, json);
        }
        catch
        {
            // Ignore save errors for now
        }
    }
}
