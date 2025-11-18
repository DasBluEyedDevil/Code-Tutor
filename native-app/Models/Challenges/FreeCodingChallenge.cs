using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CodeTutor.Native.Models.Challenges;

/// <summary>
/// Free coding challenge with test cases
/// </summary>
public class FreeCodingChallenge : Challenge
{
    public override string Type => "FREE_CODING";

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("starterCode")]
    public string StarterCode { get; set; } = string.Empty;

    [JsonPropertyName("testCases")]
    public List<TestCase> TestCases { get; set; } = new();

    [JsonPropertyName("solution")]
    public string? Solution { get; set; }

    [JsonPropertyName("bonusChallenges")]
    public List<string>? BonusChallenges { get; set; }
}
