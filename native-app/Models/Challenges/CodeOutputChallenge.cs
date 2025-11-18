using System.Text.Json.Serialization;

namespace CodeTutor.Native.Models.Challenges;

/// <summary>
/// Challenge where user predicts code output
/// </summary>
public class CodeOutputChallenge : Challenge
{
    public override string Type => "CODE_OUTPUT";

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("expectedOutput")]
    public string ExpectedOutput { get; set; } = string.Empty;

    [JsonPropertyName("explanation")]
    public string Explanation { get; set; } = string.Empty;

    [JsonPropertyName("allowRun")]
    public bool AllowRun { get; set; } = true;
}
