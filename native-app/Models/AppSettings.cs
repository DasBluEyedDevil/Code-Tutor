using System.Text.Json.Serialization;

namespace CodeTutor.Native.Models;

/// <summary>
/// Application settings model
/// Persisted to settings.json in AppData
/// </summary>
public class AppSettings
{
    // Appearance
    [JsonPropertyName("theme")]
    public string Theme { get; set; } = "Dark";

    [JsonPropertyName("accentColor")]
    public string AccentColor { get; set; } = "#6C5CE7";

    // Editor (references EditorConfiguration for most settings)
    [JsonPropertyName("autoSaveEnabled")]
    public bool AutoSaveEnabled { get; set; } = true;

    [JsonPropertyName("autoSaveDelay")]
    public int AutoSaveDelayMs { get; set; } = 2000;

    // Hints
    [JsonPropertyName("hintsEnabled")]
    public bool HintsEnabled { get; set; } = true;

    [JsonPropertyName("hintPenaltyEnabled")]
    public bool HintPenaltyEnabled { get; set; } = true;

    [JsonPropertyName("hintPenaltyPercent")]
    public int HintPenaltyPercent { get; set; } = 10;

    // Notifications
    [JsonPropertyName("notificationsEnabled")]
    public bool NotificationsEnabled { get; set; } = true;

    [JsonPropertyName("achievementNotifications")]
    public bool AchievementNotifications { get; set; } = true;

    [JsonPropertyName("soundEnabled")]
    public bool SoundEnabled { get; set; } = false;

    // Code Execution
    [JsonPropertyName("executionTimeout")]
    public int ExecutionTimeoutSeconds { get; set; } = 30;

    [JsonPropertyName("showExecutionTime")]
    public bool ShowExecutionTime { get; set; } = true;

    // Data
    [JsonPropertyName("collectAnonymousStats")]
    public bool CollectAnonymousStats { get; set; } = false;

    [JsonPropertyName("autoBackup")]
    public bool AutoBackup { get; set; } = true;

    [JsonPropertyName("backupRetentionDays")]
    public int BackupRetentionDays { get; set; } = 30;
}
