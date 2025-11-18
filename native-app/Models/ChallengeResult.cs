using System.Collections.Generic;

namespace CodeTutor.Native.Models;

/// <summary>
/// Result of a challenge validation
/// </summary>
public class ChallengeResult
{
    public bool IsCorrect { get; set; }
    public int Score { get; set; }
    public int MaxScore { get; set; }
    public string? Feedback { get; set; }
    public List<TestCaseResult>? TestResults { get; set; }
}

/// <summary>
/// Result of an individual test case
/// </summary>
public class TestCaseResult
{
    public string Description { get; set; } = string.Empty;
    public bool Passed { get; set; }
    public string? ActualOutput { get; set; }
    public string? ExpectedOutput { get; set; }
    public string? Error { get; set; }
}
