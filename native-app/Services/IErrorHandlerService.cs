using System;
using System.Threading.Tasks;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for centralized error handling and logging
/// </summary>
public interface IErrorHandlerService
{
    /// <summary>
    /// Handle an exception with optional user message
    /// </summary>
    Task HandleErrorAsync(Exception exception, string? userMessage = null, bool showToUser = true);

    /// <summary>
    /// Log an error without showing to user
    /// </summary>
    void LogError(Exception exception, string? context = null);

    /// <summary>
    /// Log a warning message
    /// </summary>
    void LogWarning(string message, string? context = null);

    /// <summary>
    /// Log an informational message
    /// </summary>
    void LogInfo(string message, string? context = null);

    /// <summary>
    /// Check if exception is fatal (should not be caught)
    /// </summary>
    bool IsFatalException(Exception exception);

    /// <summary>
    /// Get user-friendly error message from exception
    /// </summary>
    string GetUserFriendlyMessage(Exception exception);
}
