using System.Text.Json.Serialization;

namespace CodeTutor.Native.Models.Challenges;

/// <summary>
/// True/False challenge
/// </summary>
public class TrueFalseChallenge : Challenge
{
    public override string Type => "TRUE_FALSE";

    [JsonPropertyName("statement")]
    public string Statement { get; set; } = string.Empty;

    [JsonPropertyName("correctAnswer")]
    public bool CorrectAnswer { get; set; }

    [JsonPropertyName("explanation")]
    public string Explanation { get; set; } = string.Empty;
}
