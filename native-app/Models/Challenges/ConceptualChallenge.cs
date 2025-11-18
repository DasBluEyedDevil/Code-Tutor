using System.Text.Json.Serialization;

namespace CodeTutor.Native.Models.Challenges;

/// <summary>
/// Open-ended conceptual challenge requiring text explanation
/// </summary>
public class ConceptualChallenge : Challenge
{
    public override string Type => "CONCEPTUAL";

    [JsonPropertyName("question")]
    public string Question { get; set; } = string.Empty;

    [JsonPropertyName("sampleAnswer")]
    public string? SampleAnswer { get; set; }

    [JsonPropertyName("keyPoints")]
    public List<string>? KeyPoints { get; set; }

    [JsonPropertyName("minWords")]
    public int? MinWords { get; set; }
}
