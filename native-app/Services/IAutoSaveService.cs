using System.Threading.Tasks;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for auto-saving code with draft recovery
/// </summary>
public interface IAutoSaveService
{
    /// <summary>
    /// Save code for a specific challenge
    /// </summary>
    Task SaveCodeAsync(string courseId, string moduleId, string lessonId, string challengeId, string code);

    /// <summary>
    /// Restore the most recent saved code for a challenge
    /// </summary>
    Task<string?> RestoreCodeAsync(string courseId, string moduleId, string lessonId, string challengeId);

    /// <summary>
    /// Delete saved code history for a challenge
    /// </summary>
    Task ClearHistoryAsync(string courseId, string moduleId, string lessonId, string challengeId);

    /// <summary>
    /// Get the last save timestamp for a challenge
    /// </summary>
    Task<System.DateTime?> GetLastSaveTimeAsync(string courseId, string moduleId, string lessonId, string challengeId);
}
