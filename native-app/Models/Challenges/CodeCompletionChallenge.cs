using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CodeTutor.Native.Models.Challenges;

/// <summary>
/// Challenge where user completes partial code with TODO markers
/// </summary>
public class CodeCompletionChallenge : Challenge
{
    public override string Type => "CODE_COMPLETION";

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("incompleteCode")]
    public string IncompleteCode { get; set; } = string.Empty;

    [JsonPropertyName("testCases")]
    public List<TestCase> TestCases { get; set; } = new();

    [JsonPropertyName("solution")]
    public string? Solution { get; set; }

    [JsonPropertyName("todoMarkers")]
    public List<TodoMarker>? TodoMarkers { get; set; }
}

/// <summary>
/// Marker for TODO sections in code completion challenges
/// </summary>
public class TodoMarker
{
    [JsonPropertyName("lineNumber")]
    public int LineNumber { get; set; }

    [JsonPropertyName("hint")]
    public string? Hint { get; set; }
}
