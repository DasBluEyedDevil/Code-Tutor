using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CodeTutor.Native.Models.Challenges;

/// <summary>
/// Base class for all challenge types
/// </summary>
public abstract class Challenge
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public abstract string Type { get; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("hints")]
    public List<string> Hints { get; set; } = new();

    [JsonPropertyName("commonMistakes")]
    public List<CommonMistake>? CommonMistakes { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; } = 100;
}

/// <summary>
/// Common mistake pattern with explanation and fix
/// </summary>
public class CommonMistake
{
    [JsonPropertyName("pattern")]
    public string Pattern { get; set; } = string.Empty;

    [JsonPropertyName("explanation")]
    public string Explanation { get; set; } = string.Empty;

    [JsonPropertyName("fix")]
    public string Fix { get; set; } = string.Empty;
}
