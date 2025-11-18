namespace CodeTutor.Native.Models;

/// <summary>
/// Result of code execution
/// </summary>
public class ExecutionResult
{
    public bool Success { get; set; }
    public string Output { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public int ExecutionTimeMs { get; set; }
    public int ExitCode { get; set; }
}

/// <summary>
/// Result of challenge validation
/// </summary>
public class ValidationResult
{
    public bool Passed { get; set; }
    public int Score { get; set; }
    public List<TestResult> TestResults { get; set; } = new();
    public string? CompilationError { get; set; }
    public string? RuntimeError { get; set; }
}

public class TestResult
{
    public string Description { get; set; } = string.Empty;
    public bool Passed { get; set; }
    public string? ActualOutput { get; set; }
    public string? ExpectedOutput { get; set; }
    public string? Error { get; set; }
}
