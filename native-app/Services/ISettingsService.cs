using System.Threading.Tasks;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for managing application settings
/// </summary>
public interface ISettingsService
{
    /// <summary>
    /// Get current application settings
    /// </summary>
    AppSettings GetSettings();

    /// <summary>
    /// Update application settings
    /// </summary>
    Task SaveSettingsAsync(AppSettings settings);

    /// <summary>
    /// Reset settings to defaults
    /// </summary>
    Task ResetToDefaultsAsync();

    /// <summary>
    /// Get a specific setting value
    /// </summary>
    T GetSetting<T>(string key, T defaultValue);

    /// <summary>
    /// Set a specific setting value
    /// </summary>
    Task SetSettingAsync<T>(string key, T value);
}
