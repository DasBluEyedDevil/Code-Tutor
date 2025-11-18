using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for managing code editor configuration
/// </summary>
public interface IEditorConfigurationService
{
    /// <summary>
    /// Get the current editor configuration
    /// </summary>
    EditorConfiguration GetConfiguration();

    /// <summary>
    /// Update the editor configuration
    /// </summary>
    void UpdateConfiguration(EditorConfiguration configuration);

    /// <summary>
    /// Reset to default configuration
    /// </summary>
    void ResetToDefaults();
}
