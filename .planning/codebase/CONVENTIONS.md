# Coding Conventions

**Analysis Date:** 2026-02-02

## Naming Patterns

**Files:**
- PascalCase for all C# files: `CourseService.cs`, `UserProgress.cs`, `TypewriterBehavior.cs`
- Namespace-organized structure with file names matching primary class/interface names
- XAML paired with code-behind: `MainWindow.xaml` + `MainWindow.xaml.cs`

**Classes & Interfaces:**
- PascalCase for all class names: `CourseService`, `CodeExecutionService`, `RuntimeDetectionService`
- Interface names prefixed with `I`: `ICourseService`, `ICodeExecutionService`, `ITutorService`
- Generic interface naming: `IInteractiveSession`, `IValueConverter`

**Methods:**
- PascalCase for public methods: `GetAllCoursesAsync()`, `ExecuteAsync()`, `LoadModelAsync()`
- Async methods suffixed with `Async`: `GetCourseAsync()`, `CheckRuntimeAsync()`, `StartInteractiveSessionAsync()`
- Private methods PascalCase: `LoadModulesAsync()`, `ParseMarkdownToContentSection()`, `CheckCommandAsync()`

**Variables & Properties:**
- PascalCase for public properties: `Id`, `Title`, `Language`, `IsModelLoaded`, `IsPistonAvailable`
- camelCase for private fields with underscore prefix: `_contentPath`, `_courseCache`, `_model`, `_disposed`, `_modelPath`
- camelCase for local variables: `course`, `module`, `lesson`, `json`, `courseFile`
- Boolean properties/variables prefixed descriptively: `IsAvailable`, `IsModelLoaded`, `HasContent`, `_disposed`

**Types & Records:**
- PascalCase: `Course`, `Module`, `Lesson`, `ContentSection`, `Challenge`, `RuntimeInfo`, `ExecutionResult`
- Record types for data transfer: `public record ExecutionResult(bool Success, string Output, string Error);`
- Record types for configuration: `public record RuntimeInfo(string Language, bool IsAvailable, string Version, string InstallHint);`

## Code Style

**Formatting:**
- Target Framework: .NET 8.0 for WPF (net8.0-windows)
- Language Version: C# 13.0 (LangVersion set in .csproj)
- Implicit usings enabled (no need to fully qualify System types)
- Nullable reference types enabled for null-safety
- Four-space indentation (standard C#)
- Max line length: No hard limit observed, but practical limit around 120 characters for readability

**Linting:**
- No explicit ESLint or StyleCop configuration detected
- Conventions inferred from codebase patterns
- Nullable reference types enabled in project: `<Nullable>enable</Nullable>`
- Project suppresses NU1701 warning for compatibility

**Class Structure Order:**
1. Using statements
2. Namespace declaration (file-scoped namespace: `namespace CodeTutor.Wpf.Services;`)
3. Records/public types at top
4. Interface definition (if applicable)
5. Class declaration
6. Private fields
7. Public properties
8. Constructors
9. Public methods
10. Private methods
11. Event handlers or inner classes at end

Example from `CourseService.cs`:
```csharp
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
// ... more usings

namespace CodeTutor.Wpf.Services;

public interface ICourseService
{
    Task<List<Course>> GetAllCoursesAsync();
}

public class CourseService : ICourseService
{
    private readonly string _contentPath;
    private readonly ConcurrentDictionary<string, Course> _courseCache = new();

    public CourseService() { }
    public async Task<List<Course>> GetAllCoursesAsync() { }
    private async Task LoadModulesAsync() { }
}
```

## Import Organization

**Order:**
1. System namespaces: `using System;`, `using System.Collections.Generic;`, etc.
2. System.* and Microsoft namespaces: `using System.IO;`, `using System.Text.Json;`
3. Application namespaces: `using CodeTutor.Wpf.Models;`, `using CodeTutor.Wpf.Services;`

**Observed Pattern from `CourseService.cs`:**
```csharp
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Services;
```

**Path Aliases:**
- No explicit path aliases detected
- Direct namespace references used throughout
- Root namespace: `CodeTutor.Wpf` for main application

## Error Handling

**Patterns:**
- Try-catch blocks for I/O and file operations: `try { ... } catch (Exception ex) { ... }`
- Specific exception handling for known failures: `catch (OperationCanceledException)`, `catch (TaskCanceledException)`
- Silent fallbacks with logging for non-critical failures (see `Log()` method in `CourseService.cs`)
- Graceful null-coalescing: `return course?.Id ?? string.Empty;`

**Example from `CodeExecutionService.cs` (lines 105-108):**
```csharp
catch (Exception ex)
{
    return new ExecutionResult(false, "", ex.Message);
}
```

**Example from `CourseService.cs` (lines 44-58):**
```csharp
try
{
    var json = await File.ReadAllTextAsync(courseFile);
    var course = JsonSerializer.Deserialize<Course>(json);
    if (course != null)
    {
        await LoadModulesAsync(course, courseDir);
        _courseCache.TryAdd(course.Id, course);
        courses.Add(course);
    }
}
catch (Exception ex)
{
    LogError(courseDir, ex);
}
```

**Logging Approach:**
- Debug.WriteLine for diagnostic output: `Debug.WriteLine($"Failed to load course...")`
- Custom logging to file: `Log($"ERROR: Failed to load {dir}: {ex.Message}")`
- Log location: `%LOCALAPPDATA%\CodeTutor\course-service.log`

**Example from `CourseService.cs` (lines 343-353):**
```csharp
private static void Log(string message)
{
    try
    {
        var logDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CodeTutor");
        Directory.CreateDirectory(logDir);
        var logPath = Path.Combine(logDir, "course-service.log");
        File.AppendAllText(logPath, $"{DateTime.Now:HH:mm:ss.fff}: {message}\n");
    }
    catch { /* Ignore logging errors */ }
}
```

## Logging

**Framework:** Custom file-based logging (no external framework)

**Patterns:**
- Logging via static `Log()` method that writes to local application data
- Timestamps included in log format: `DateTime.Now:HH:mm:ss.fff`
- Silent exception handling for logging failures (prevents logging from breaking application)
- Log levels prefixed in message: `"ERROR:"`, `"WARNING:"`

**Examples:**
- Success logging: `Log($"Found lesson: {lesson.Title}");`
- Error logging: `Log($"ERROR: Failed to load content from {mdFile}: {ex.Message}");`
- Debug logging: `Log($"GetLessonAsync: course={courseId}, module={moduleId}, lesson={lessonId}");`

## Comments

**When to Comment:**
- Public API documentation using XML doc comments: `/// <summary>` blocks on public classes, methods, and properties
- Complex algorithm explanation for non-obvious logic
- Warnings for important caveats or known limitations
- Section dividers using `#region` blocks for large files

**JSDoc/TSDoc:**
- Not applicable (C# codebase)
- Uses XML documentation comments: `/// <summary>`, `/// <remarks>`, `/// <exception>`

**Example from `TypewriterBehavior.cs` (lines 9-12):**
```csharp
/// <summary>
/// Provides a typewriter effect for TextBlock elements, making text appear character by character
/// like a retro terminal. This creates a satisfying "juice" effect for output display.
/// </summary>
public static class TypewriterBehavior
```

**Example from `TypewriterBehavior.cs` (lines 181-183):**
```csharp
/// <summary>
/// Cancels any ongoing typewriter animation and shows the full text immediately.
/// </summary>
public static void CancelAndShowFull(TextBlock textBlock)
```

## Function Design

**Size:** Functions generally 15-50 lines, with smaller private helpers for complex logic
- Larger methods (~100+ lines) break down into private helper methods
- Example: `CourseService.cs` `GetLessonAsync()` calls `LoadLessonContentAsync()`, `ParseMarkdownToContentSection()`, etc.

**Parameters:**
- Limited parameters (1-4 typical), additional data passed via objects
- Async methods use `CancellationToken` as final optional parameter: `async Task<...>(string param, CancellationToken cancellationToken = default)`
- Common pattern: `public async IAsyncEnumerable<T> MethodAsync(..., [EnumeratorCancellation] CancellationToken ct = default)`

**Return Values:**
- Task-based return types for async methods: `Task<T>`, `Task`
- IAsyncEnumerable for streaming results: `public async IAsyncEnumerable<string> SendMessageAsync(...)`
- Nullable returns when optional: `public async Task<Course?> GetCourseAsync(string courseId);`
- Records for multi-value returns: `public record ExecutionResult(bool Success, string Output, string Error);`

**Example from `CodeExecutionService.cs` (lines 53-109):**
```csharp
public async Task<ExecutionResult> ExecuteAsync(string code, string language)
{
    if (string.IsNullOrWhiteSpace(language))
        return new ExecutionResult(false, "", "Language cannot be empty");
    // ... implementation
}
```

## Module Design

**Exports:**
- Public interfaces define contracts: `public interface ICourseService { ... }`
- Classes implement interfaces: `public class CourseService : ICourseService { ... }`
- Services registered as dependency-injected singletons (pattern inferred from constructor usage)

**Barrel Files:**
- Not used; each file contains single primary type
- Namespaces organize related functionality: `CodeTutor.Wpf.Services`, `CodeTutor.Wpf.Models`, `CodeTutor.Wpf.Controls`

**Dependency Injection Pattern:**
- Constructor injection assumed for service dependencies
- Direct instantiation for simple utilities: `new RoslynCSharpExecutor()`, `new RuntimeDetectionService()`
- Example from `CodeExecutionService.cs` (lines 31-39):
```csharp
public CodeExecutionService()
{
    _roslynExecutor = new RoslynCSharpExecutor();
    _pistonExecutor = new PistonExecutor();
    _runtimeDetection = new RuntimeDetectionService();
    _pistonCheckTask = CheckPistonAsync();
}
```

**Resource Cleanup:**
- IDisposable pattern for resource management: `public void Dispose()`, `public class Phi4TutorService : IDisposable`
- Using statements for cleanup: `using var process = Process.Start(psi);`
- Null checks before disposal: `_tokenizer?.Dispose();`

---

*Convention analysis: 2026-02-02*
