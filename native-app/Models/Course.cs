using System.Collections.Generic;
using System.Text.Json.Serialization;
using CodeTutor.Native.Models.Challenges;

namespace CodeTutor.Native.Models;

/// <summary>
/// Represents a programming course with modules and lessons
/// </summary>
public class Course
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("difficulty")]
    public string Difficulty { get; set; } = string.Empty;

    [JsonPropertyName("estimatedHours")]
    public int EstimatedHours { get; set; }

    [JsonPropertyName("modules")]
    public List<Module> Modules { get; set; } = new();
}

public class Module
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("lessons")]
    public List<Lesson> Lessons { get; set; } = new();
}

public class Lesson
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public LessonContent Content { get; set; } = new();

    [JsonPropertyName("exercises")]
    public List<Challenge> Exercises { get; set; } = new();
}

public class LessonContent
{
    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;

    [JsonPropertyName("overview")]
    public string? Overview { get; set; }

    [JsonPropertyName("keyTakeaways")]
    public List<string>? KeyTakeaways { get; set; }

    [JsonPropertyName("codeExamples")]
    public List<CodeExample> CodeExamples { get; set; } = new();
}

public class CodeExample
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("expectedOutput")]
    public string? ExpectedOutput { get; set; }

    [JsonPropertyName("explanation")]
    public string? Explanation { get; set; }
}
