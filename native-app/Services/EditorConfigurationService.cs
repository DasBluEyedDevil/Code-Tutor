using System;
using System.IO;
using System.Text.Json;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for managing code editor configuration
/// </summary>
public class EditorConfigurationService : IEditorConfigurationService
{
    private readonly string _configFilePath;
    private EditorConfiguration _currentConfiguration;

    public EditorConfigurationService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var configDir = Path.Combine(appDataPath, "CodeTutor", "config");
        Directory.CreateDirectory(configDir);

        _configFilePath = Path.Combine(configDir, "editor-config.json");
        _currentConfiguration = LoadConfiguration();
    }

    public EditorConfiguration GetConfiguration()
    {
        return _currentConfiguration;
    }

    public void UpdateConfiguration(EditorConfiguration configuration)
    {
        _currentConfiguration = configuration;
        SaveConfiguration();
    }

    public void ResetToDefaults()
    {
        _currentConfiguration = new EditorConfiguration();
        SaveConfiguration();
    }

    private EditorConfiguration LoadConfiguration()
    {
        try
        {
            if (File.Exists(_configFilePath))
            {
                var json = File.ReadAllText(_configFilePath);
                var config = JsonSerializer.Deserialize<EditorConfiguration>(json);
                return config ?? new EditorConfiguration();
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to load editor configuration: {ex.Message}");
        }

        return new EditorConfiguration();
    }

    private void SaveConfiguration()
    {
        try
        {
            var json = JsonSerializer.Serialize(_currentConfiguration, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_configFilePath, json);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to save editor configuration: {ex.Message}");
        }
    }
}
