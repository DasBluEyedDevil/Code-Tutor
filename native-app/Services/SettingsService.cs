using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for managing application settings
/// Settings are persisted to settings.json in AppData/CodeTutor
/// </summary>
public class SettingsService : ISettingsService
{
    private readonly string _settingsFilePath;
    private readonly ILogger<SettingsService>? _logger;
    private AppSettings _currentSettings;

    public SettingsService(ILogger<SettingsService>? logger = null)
    {
        _logger = logger;

        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var configDir = Path.Combine(appDataPath, "CodeTutor");
        Directory.CreateDirectory(configDir);

        _settingsFilePath = Path.Combine(configDir, "settings.json");
        _currentSettings = LoadSettings();
    }

    public AppSettings GetSettings()
    {
        return _currentSettings;
    }

    public async Task SaveSettingsAsync(AppSettings settings)
    {
        try
        {
            _currentSettings = settings;

            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(_settingsFilePath, json);

            _logger?.LogInformation("Settings saved successfully");
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to save settings");
            throw;
        }
    }

    public async Task ResetToDefaultsAsync()
    {
        try
        {
            _currentSettings = new AppSettings();
            await SaveSettingsAsync(_currentSettings);

            _logger?.LogInformation("Settings reset to defaults");
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to reset settings");
            throw;
        }
    }

    public T GetSetting<T>(string key, T defaultValue)
    {
        try
        {
            var property = typeof(AppSettings).GetProperty(key);
            if (property != null)
            {
                var value = property.GetValue(_currentSettings);
                if (value is T typedValue)
                {
                    return typedValue;
                }
            }

            return defaultValue;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogWarning(ex, "Failed to get setting {Key}, returning default", key);
            return defaultValue;
        }
    }

    public async Task SetSettingAsync<T>(string key, T value)
    {
        try
        {
            var property = typeof(AppSettings).GetProperty(key);
            if (property != null && property.CanWrite)
            {
                property.SetValue(_currentSettings, value);
                await SaveSettingsAsync(_currentSettings);
            }
            else
            {
                _logger?.LogWarning("Setting {Key} not found or not writable", key);
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to set setting {Key}", key);
            throw;
        }
    }

    private AppSettings LoadSettings()
    {
        // Used during construction - synchronous is acceptable here
        // For runtime reloads, use LoadSettingsAsync
        try
        {
            if (File.Exists(_settingsFilePath))
            {
                var json = File.ReadAllText(_settingsFilePath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json);

                if (settings != null)
                {
                    _logger?.LogInformation("Settings loaded from {Path}", _settingsFilePath);
                    return settings;
                }
            }

            _logger?.LogInformation("No settings file found, using defaults");
            return new AppSettings();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to load settings, using defaults");
            return new AppSettings();
        }
    }

    /// <summary>
    /// Asynchronously reload settings from disk
    /// Use this method for runtime reloads to avoid blocking the UI thread
    /// </summary>
    public async Task<AppSettings> LoadSettingsAsync()
    {
        try
        {
            if (File.Exists(_settingsFilePath))
            {
                var json = await File.ReadAllTextAsync(_settingsFilePath);
                var settings = JsonSerializer.Deserialize<AppSettings>(json);

                if (settings != null)
                {
                    _currentSettings = settings;
                    _logger?.LogInformation("Settings loaded asynchronously from {Path}", _settingsFilePath);
                    return settings;
                }
            }

            _logger?.LogInformation("No settings file found, using defaults");
            return new AppSettings();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _logger?.LogError(ex, "Failed to load settings asynchronously, using defaults");
            return new AppSettings();
        }
    }
}
