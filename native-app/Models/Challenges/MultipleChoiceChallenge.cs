using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CodeTutor.Native.Models.Challenges;

/// <summary>
/// Multiple choice challenge with 4 options (A/B/C/D)
/// </summary>
public class MultipleChoiceChallenge : Challenge
{
    public override string Type => "MULTIPLE_CHOICE";

    [JsonPropertyName("question")]
    public string Question { get; set; } = string.Empty;

    [JsonPropertyName("options")]
    public List<string> Options { get; set; } = new();

    [JsonPropertyName("correctAnswer")]
    public int CorrectAnswer { get; set; }

    [JsonPropertyName("explanation")]
    public string Explanation { get; set; } = string.Empty;
}
