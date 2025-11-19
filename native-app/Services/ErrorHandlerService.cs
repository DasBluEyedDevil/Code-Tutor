using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.Logging;

namespace CodeTutor.Native.Services;

/// <summary>
/// Centralized error handling service for logging and user notifications
/// </summary>
public class ErrorHandlerService : IErrorHandlerService
{
    private readonly ILogger<ErrorHandlerService>? _logger;
    private readonly string _logFilePath;

    public ErrorHandlerService(ILogger<ErrorHandlerService>? logger = null)
    {
        _logger = logger;

        // Set up log file path
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var logDir = Path.Combine(appDataPath, "CodeTutor", "logs");
        Directory.CreateDirectory(logDir);
        _logFilePath = Path.Combine(logDir, $"errors_{DateTime.UtcNow:yyyy-MM-dd}.log");
    }

    public async Task HandleErrorAsync(Exception exception, string? userMessage = null, bool showToUser = true)
    {
        try
        {
            // Log the exception
            LogError(exception, userMessage);

            // Show error dialog to user if requested
            if (showToUser)
            {
                var friendlyMessage = GetUserFriendlyMessage(exception);
                _logger?.LogWarning("User-facing error: {Message}", friendlyMessage);
                await ShowErrorDialogAsync(friendlyMessage, userMessage);
            }
        }
        catch (Exception ex)
        {
            // Even error handling can fail - log to console as last resort
            Console.WriteLine($"Error handler failed: {ex.Message}");
        }

        await Task.CompletedTask;
    }

    public void LogError(Exception exception, string? context = null)
    {
        var message = FormatLogMessage("ERROR", context ?? exception.Source, exception.ToString());
        _logger?.LogError(exception, "{Context}", context);
        WriteToLogFile(message);
    }

    public void LogWarning(string message, string? context = null)
    {
        var logMessage = FormatLogMessage("WARN", context ?? "Application", message);
        _logger?.LogWarning("{Message}", message);
        WriteToLogFile(logMessage);
    }

    public void LogInfo(string message, string? context = null)
    {
        var logMessage = FormatLogMessage("INFO", context ?? "Application", message);
        _logger?.LogInformation("{Message}", message);
        WriteToLogFile(logMessage);
    }

    public bool IsFatalException(Exception exception)
    {
        return exception is OutOfMemoryException
            || exception is StackOverflowException
            || exception is AccessViolationException
            || exception is AppDomainUnloadedException;
    }

    public string GetUserFriendlyMessage(Exception exception)
    {
        return exception switch
        {
            FileNotFoundException => "A required file could not be found. Please check your installation.",
            DirectoryNotFoundException => "A required folder could not be found. Please check your installation.",
            UnauthorizedAccessException => "Permission denied. Please check file permissions or run as administrator.",
            IOException => "An error occurred while accessing a file or network resource.",
            TimeoutException => "The operation took too long to complete. Please try again.",
            InvalidOperationException => "The operation could not be completed. Please try again.",
            ArgumentException => "Invalid input provided. Please check your data and try again.",
            System.Net.Http.HttpRequestException => "Network error. Please check your internet connection.",
            System.Data.Common.DbException => "Database error. Your progress may not have been saved.",
            _ => "An unexpected error occurred. Please try again or restart the application."
        };
    }

    private string FormatLogMessage(string level, string context, string message)
    {
        var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
        return $"[{timestamp}] [{level}] [{context}] {message}";
    }

    private void WriteToLogFile(string message)
    {
        try
        {
            File.AppendAllText(_logFilePath, message + Environment.NewLine);
        }
        catch
        {
            // If we can't write to log file, fail silently
            // Last resort: write to console
            Console.WriteLine(message);
        }
    }

    private async Task ShowErrorDialogAsync(string friendlyMessage, string? context = null)
    {
        try
        {
            // Get the main window from the application
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
                return;

            var mainWindow = desktop.MainWindow;
            if (mainWindow == null)
                return;

            var title = context ?? "Error";

            // Show message box on UI thread
            await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(async () =>
            {
                var messageBox = new Window
                {
                    Title = title,
                    Width = 450,
                    Height = 200,
                    CanResize = false,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                var okButton = new Button
                {
                    Content = "OK",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    Padding = new Thickness(30, 8)
                };

                okButton.Click += (s, e) => messageBox.Close();

                messageBox.Content = new StackPanel
                {
                    Margin = new Thickness(20),
                    Spacing = 15,
                    Children =
                    {
                        new TextBlock
                        {
                            Text = friendlyMessage,
                            TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                            FontSize = 14
                        },
                        okButton
                    }
                };

                await messageBox.ShowDialog(mainWindow);
            });
        }
        catch (Exception ex)
        {
            // If dialog fails, just log it
            _logger?.LogError(ex, "Failed to show error dialog");
        }
    }
}
