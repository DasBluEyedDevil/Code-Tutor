# Testing Patterns

**Analysis Date:** 2026-02-02

## Test Framework

**Runner:**
- xUnit 2.6.3
- Config: `native-app.Tests/native-app.Tests.csproj`
- Target Framework: net8.0

**Assertion Library:**
- FluentAssertions 6.12.0

**Run Commands:**
```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"

# Run specific test category
dotnet test --filter "FullyQualifiedName~ContentValidation"
dotnet test --filter "FullyQualifiedName~CodeExecution"
dotnet test --filter "FullyQualifiedName~UserJourneys"

# Run tests with coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

## Test File Organization

**Location:**
- Tests co-located in separate test project: `native-app.Tests/`
- Content validation tests: `native-app.Tests/E2E/ContentValidation/`
- Code execution tests: `native-app.Tests/E2E/CodeExecution/`
- User journey tests: `native-app.Tests/E2E/UserJourneys/`
- Shared test models: `native-app.Tests/Models/`

**Naming:**
- File names match test class names: `CourseContentValidationTests.cs` contains `CourseContentValidationTests` class
- Test method naming: `Context_Condition_ExpectedResult`
  - Example: `Course_ShouldLoadSuccessfully()`, `Course_AllModuleIdsInLessons_ShouldMatchParentModule()`
  - Example: `Execute_NewFeature_Works()`

**Structure:**
```
native-app.Tests/
├── E2E/
│   ├── ContentValidation/
│   │   ├── CourseContentValidationTests.cs (25+ tests)
│   │   └── ChallengeValidationTests.cs (20+ tests)
│   ├── CodeExecution/
│   │   ├── RoslynExecutorTests.cs (25+ tests)
│   │   └── RuntimeDetectionTests.cs (15+ tests)
│   └── UserJourneys/
│       ├── LearningJourneyTests.cs (20+ tests)
│       └── ProgressTrackingTests.cs (25+ tests)
├── Models/
│   ├── Course.cs (test models)
│   └── UserProgress.cs (test models)
└── native-app.Tests.csproj
```

## Test Structure

**Suite Organization:**
```csharp
namespace CodeTutor.Tests.E2E.ContentValidation;

public class CourseContentValidationTests
{
    private readonly string _contentPath;
    private readonly JsonSerializerOptions _jsonOptions;

    public CourseContentValidationTests()
    {
        _contentPath = FindContentPath();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip
        };
    }

    [Fact]
    public void ContentPath_ShouldExist()
    {
        // Assert
        Directory.Exists(_contentPath).Should().BeTrue(
            $"Content path should exist at {_contentPath}");
    }
}
```

**Patterns:**

1. **Constructor Setup:**
   - Initialize shared resources in constructor (runs before each test)
   - Find content path with fallback logic for different test environments
   - Configure JSON deserialization options once

2. **Fact Tests (no parameters):**
   ```csharp
   [Fact]
   public void ContentPath_ShouldExist()
   {
       Directory.Exists(_contentPath).Should().BeTrue();
   }
   ```

3. **Theory Tests (parameterized):**
   ```csharp
   [Theory]
   [InlineData("python")]
   [InlineData("javascript")]
   [InlineData("java")]
   public void Course_ShouldLoadSuccessfully(string courseId)
   {
       var courseFile = Path.Combine(_contentPath, courseId, "course.json");
       if (!File.Exists(courseFile)) return; // Skip gracefully

       var json = File.ReadAllText(courseFile);
       var course = JsonSerializer.Deserialize<Course>(json, _jsonOptions);
       course.Should().NotBeNull();
   }
   ```

## Test Structure: AAA Pattern

**Arrange-Act-Assert consistently applied:**

```csharp
[Fact]
public void Course_ShouldLoadSuccessfully()
{
    // Arrange - Load course file
    var courseFile = Path.Combine(_contentPath, "python", "course.json");
    var json = File.ReadAllText(courseFile);

    // Act - Deserialize JSON
    var course = JsonSerializer.Deserialize<Course>(json, _jsonOptions);

    // Assert - Verify properties
    course.Should().NotBeNull();
    course.Id.Should().NotBeNullOrEmpty();
}
```

**Patterns:**
- Arrange: Load test data, setup preconditions
- Act: Execute single operation being tested
- Assert: Verify expected outcomes using FluentAssertions

## Mocking

**Framework:** Moq 4.20.70 (referenced in project but not heavily used)

**Patterns:**
- Minimal mocking observed in codebase
- Real objects preferred for integration tests
- Mocks used for future expansion (indicated by Moq dependency)

**Not Observed Yet:**
- Test doubles not heavily used
- Real CourseService, RuntimeDetectionService instantiated in tests
- Tests load actual JSON files from Content directory

## Fixtures and Factories

**Test Data:**
- Real course JSON files loaded from disk: `Content/courses/python/course.json`
- Test models defined in `native-app.Tests/Models/` mirror production models
- Setup methods in constructor initialize shared paths and options

**Location:**
- Shared test fixtures: `native-app.Tests/Models/Course.cs`, `UserProgress.cs`
- Content fixtures: `native-app.Tests/Content/` (symlinked from `content/courses`)

**Example from `CourseContentValidationTests.cs` (lines 15-23):**
```csharp
public CourseContentValidationTests()
{
    _contentPath = FindContentPath();
    _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip
    };
}

private static string FindContentPath()
{
    var possiblePaths = new[]
    {
        Path.Combine(Directory.GetCurrentDirectory(), "Content", "courses"),
        Path.Combine(Directory.GetCurrentDirectory(), "..", "Content", "courses"),
        // ... additional fallback paths for different test environments
    };

    foreach (var path in possiblePaths)
    {
        if (Directory.Exists(path))
            return Path.GetFullPath(path);
    }

    return Path.Combine(Directory.GetCurrentDirectory(), "Content", "courses");
}
```

## Coverage

**Requirements:** Not enforced, but target is comprehensive E2E coverage

**View Coverage:**
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

**Tool:** coverlet.collector 6.0.0

**Coverage Summary (from README):**
| Category | Test Files | Tests | Coverage |
|----------|------------|-------|----------|
| Content Validation | 2 | 45+ | Course JSON |
| Code Execution | 2 | 40+ | Roslyn, Runtime |
| User Journeys | 2 | 45+ | Workflows |
| **Total** | **6** | **130+** | **Full E2E** |

## Test Types

**Unit Tests:**
- Scope: Individual method/component validation
- Approach: Load single course file, verify structure
- Example: `Course_ShouldLoadSuccessfully()` validates CourseService can deserialize JSON
- Example: `Course_ShouldHaveValidModules()` validates module structure

**Integration Tests:**
- Scope: Full workflow from file load to model structure
- Approach: Load course hierarchy, verify relationships maintained
- Example: `Course_AllModuleIdsInLessons_ShouldMatchParentModule()` validates lesson-module relationships
- Example: Full content validation with nested JSON structures

**E2E Tests:**
- Scope: Complete user workflows
- Approach: Simulate course navigation, challenge completion, progress tracking
- Test categories:
  - `LearningJourneyTests`: Course browsing, module navigation, lesson progression
  - `ProgressTrackingTests`: Completion tracking, achievement unlocking, streaks
  - `CodeExecutionTests`: Roslyn C# execution with real code samples
  - `RuntimeDetectionTests`: Language runtime detection and validation

## Common Patterns

**Async Testing:**
```csharp
[Fact]
public async Task Execute_SimpleExpression_ReturnsCorrectOutput()
{
    var code = "Console.WriteLine(\"Hello\");";
    var result = await _executor.ExecuteAsync(code);

    result.Success.Should().BeTrue();
    result.Output.Should().Contain("Hello");
}
```

**Error Testing:**
```csharp
[Fact]
public async Task Execute_SyntaxError_ReturnsFalseWithErrorMessage()
{
    var code = "var x = ;;"; // Invalid syntax
    var result = await _executor.ExecuteAsync(code);

    result.Success.Should().BeFalse();
    result.Error.Should().Contain("syntax");
}
```

**Collection Assertions:**
```csharp
[Theory]
[InlineData("python")]
[InlineData("javascript")]
public void Course_AllChallenges_ShouldHaveValidLanguage(string courseId)
{
    var course = LoadCourse(courseId);
    if (course == null) return;

    var challenges = course.Modules.SelectMany(m => m.Lessons.SelectMany(l => l.Challenges));
    challenges.Should().AllSatisfy(c =>
        c.Language.Should().NotBeNullOrEmpty());
}
```

**Graceful Skipping for Missing Content:**
```csharp
public void Course_ShouldLoadSuccessfully(string courseId)
{
    var courseFile = Path.Combine(_contentPath, courseId, "course.json");

    if (!File.Exists(courseFile))
    {
        // Skip if course doesn't exist in test environment
        return;
    }

    var json = File.ReadAllText(courseFile);
    var course = JsonSerializer.Deserialize<Course>(json, _jsonOptions);
    course.Should().NotBeNull();
}
```

## Test Assertions

**Assertion Patterns (FluentAssertions):**

```csharp
// Null/Empty checks
result.Should().NotBeNull();
course.Id.Should().NotBeNullOrEmpty();
content.Should().BeNullOrWhiteSpace();

// Equality
result.Success.Should().BeTrue();
course.Language.Should().Be("python");
lesson.ModuleId.Should().Be(module.Id);

// Collections
courses.Should().NotBeEmpty();
challenges.Should().HaveCount(5);
lessons.Should().AllSatisfy(l => l.Id.Should().NotBeNullOrEmpty());

// String assertions
title.Should().Contain("expected text");
error.Should().StartWith("Syntax");
output.Should().EndWith("\n");

// Numeric assertions
percentage.Should().BeApproximately(50, 5);
count.Should().BeGreaterOrEqualTo(1);
```

## Global Usings

**Implicit using pattern configured in test project:**

From `native-app.Tests.csproj`:
```xml
<ItemGroup>
    <Using Include="Xunit" />
    <Using Include="FluentAssertions" />
    <Using Include="Moq" />
</ItemGroup>
```

This allows tests to use `[Fact]`, `Should()`, `It.Is()` without explicit imports.

## Best Practices Observed

1. **Use Theory for multi-language tests** - `[Theory]` with `[InlineData]` reduces duplication
2. **Load courses gracefully** - Return early if content not found (enables tests to run in partial environments)
3. **Test behavior, not implementation** - Assertions focus on expected outcomes, not internal state
4. **Use descriptive test names** - Format `Context_Condition_ExpectedResult` makes test purpose clear
5. **Keep tests independent** - No shared mutable state between tests
6. **Test edge cases** - Empty inputs, null values, boundaries tested
7. **Real content in integration tests** - Load actual JSON files rather than mocking

---

*Testing analysis: 2026-02-02*
