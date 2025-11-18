using System.Text.Json.Serialization;

namespace CodeTutor.Native.Models;

/// <summary>
/// Test case for code challenges
/// </summary>
public class TestCase
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("input")]
    public string? Input { get; set; }

    [JsonPropertyName("expectedOutput")]
    public string ExpectedOutput { get; set; } = string.Empty;

    [JsonPropertyName("isVisible")]
    public bool IsVisible { get; set; } = true;
}
