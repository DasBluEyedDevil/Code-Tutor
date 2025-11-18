# Testing & Quality Assurance Plan

## Overview

Comprehensive testing strategy for the Code Tutor native desktop application.

---

## Testing Pyramid

```
           /\
          /  \  E2E Tests (10%)
         /____\
        /      \  Integration Tests (30%)
       /________\
      /          \  Unit Tests (60%)
     /__________  \
```

**Target Coverage:** 80%+ overall

---

## 1. Unit Testing

### Tools
- **Framework:** xUnit
- **Mocking:** Moq
- **Assertions:** FluentAssertions

### Test Structure

```csharp
namespace CodeTutor.Native.Tests.Unit;

public class CodeExecutorTests
{
    [Fact]
    public async Task ExecuteAsync_Python_ReturnsOutput()
    {
        // Arrange
        var executor = new CodeExecutor();
        var code = "print('Hello, World!')";

        // Act
        var result = await executor.ExecuteAsync("python", code);

        // Assert
        result.Success.Should().BeTrue();
        result.Output.Should().Contain("Hello, World!");
        result.ExecutionTimeMs.Should().BeLessThan(5000);
    }

    [Theory]
    [InlineData("python", "print('test')", "test")]
    [InlineData("javascript", "console.log('test')", "test")]
    public async Task ExecuteAsync_MultipleLanguages_ProducesExpectedOutput(
        string language, string code, string expectedOutput)
    {
        // Arrange
        var executor = new CodeExecutor();

        // Act
        var result = await executor.ExecuteAsync(language, code);

        // Assert
        result.Output.Trim().Should().Be(expectedOutput);
    }

    [Fact]
    public async Task ExecuteAsync_InvalidCode_ReturnsError()
    {
        // Arrange
        var executor = new CodeExecutor();
        var code = "invalid python syntax <<<";

        // Act
        var result = await executor.ExecuteAsync("python", code);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task ExecuteAsync_LongRunningCode_TimesOut()
    {
        // Arrange
        var executor = new CodeExecutor();
        var code = "import time\ntime.sleep(15)";  // 15 seconds

        // Act
        var result = await executor.ExecuteAsync("python", code);

        // Assert
        result.Success.Should().BeFalse();
        result.Error.Should().Contain("timeout", Because("execution should timeout after 10 seconds"));
    }
}
```

### Test Coverage Areas

#### Services
- [ ] **CourseService**
  - [ ] GetCoursesAsync returns all courses
  - [ ] GetCourseAsync returns specific course
  - [ ] GetCourseAsync caches results
  - [ ] GetCourseAsync handles missing files
  - [ ] GetLessonAsync returns lesson

- [ ] **CodeExecutor**
  - [ ] ExecuteAsync for Python
  - [ ] ExecuteAsync for JavaScript
  - [ ] ExecuteAsync for Java (compile + run)
  - [ ] ExecuteAsync for Rust (compile + run)
  - [ ] ExecuteAsync for C#
  - [ ] ExecuteAsync handles syntax errors
  - [ ] ExecuteAsync handles runtime errors
  - [ ] ExecuteAsync enforces timeout
  - [ ] ExecuteAsync handles missing runtime

- [ ] **ChallengeValidator**
  - [ ] ValidateAsync for MultipleChoice
  - [ ] ValidateAsync for TrueFalse
  - [ ] ValidateAsync for CodeOutput
  - [ ] ValidateAsync for FreeCoding
  - [ ] ValidateAsync for CodeCompletion
  - [ ] ValidateAsync for Conceptual
  - [ ] RunTestCaseAsync executes code
  - [ ] RunTestCaseAsync compares outputs
  - [ ] RunTestCaseAsync handles errors
  - [ ] CompareOutputs exact match
  - [ ] CompareOutputs whitespace normalized
  - [ ] CompareOutputs case insensitive

- [ ] **ProgressService**
  - [ ] SaveProgressAsync creates new record
  - [ ] SaveProgressAsync updates existing record
  - [ ] SaveProgressAsync tracks best score
  - [ ] GetProgressAsync retrieves progress
  - [ ] GetCourseProgressAsync calculates percentage

- [ ] **AchievementService**
  - [ ] CheckAchievementsAsync detects "First Steps"
  - [ ] CheckAchievementsAsync detects "Perfectionist"
  - [ ] CheckAchievementsAsync detects "Marathon Runner"
  - [ ] UnlockAchievementAsync saves to database
  - [ ] UnlockAchievementAsync publishes event

#### ViewModels
- [ ] **MainWindowViewModel**
  - [ ] LoadCoursesAsync populates Courses
  - [ ] LoadCoursesAsync handles errors
  - [ ] ExecuteCodeAsync runs code
  - [ ] ExecuteCodeAsync updates Output
  - [ ] ExecuteCodeAsync sets IsExecuting

- [ ] **LessonPageViewModel**
  - [ ] LoadLessonAsync loads content
  - [ ] RunCodeCommand executes code
  - [ ] SubmitChallengeCommand validates
  - [ ] NextLessonCommand navigates
  - [ ] SaveProgress persists data

- [ ] **ChallengeViewModels** (all 6 types)
  - [ ] MultipleChoiceViewModel validates answer
  - [ ] TrueFalseViewModel validates answer
  - [ ] CodeOutputViewModel runs code
  - [ ] FreeCodingViewModel runs tests
  - [ ] CodeCompletionViewModel highlights TODOs
  - [ ] ConceptualViewModel counts words

#### Models
- [ ] **Course** deserialization from JSON
- [ ] **Challenge** polymorphic deserialization
- [ ] **ExecutionResult** validates properties
- [ ] **ValidationResult** calculates score

---

## 2. Integration Testing

### Database Tests

```csharp
public class ProgressServiceIntegrationTests : IDisposable
{
    private readonly CodeTutorDbContext _db;
    private readonly ProgressService _service;

    public ProgressServiceIntegrationTests()
    {
        // Use in-memory database for tests
        var options = new DbContextOptionsBuilder<CodeTutorDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _db = new CodeTutorDbContext(options);
        _service = new ProgressService(_db, new Mock<IEventAggregator>().Object);
    }

    [Fact]
    public async Task SaveProgress_PersistsToDatabase()
    {
        // Arrange
        var userId = "test-user";
        var lessonId = "python/1-1";
        var score = 95;

        // Act
        await _service.SaveProgressAsync(userId, lessonId, score);

        // Assert
        var progress = await _db.Progress
            .FirstOrDefaultAsync(p => p.UserId == userId && p.LessonId == lessonId);

        progress.Should().NotBeNull();
        progress!.Score.Should().Be(score);
        progress.Completed.Should().BeTrue();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}
```

### File I/O Tests

```csharp
public class CourseServiceIntegrationTests
{
    [Fact]
    public async Task GetCoursesAsync_ReadsFromDisk()
    {
        // Arrange
        var service = new CourseService();

        // Act
        var courses = await service.GetCoursesAsync();

        // Assert
        courses.Should().NotBeEmpty();
        courses.Should().Contain(c => c.Id == "python");
        courses.Should().Contain(c => c.Id == "java");
    }

    [Fact]
    public async Task GetCourseAsync_LoadsFullCourse()
    {
        // Arrange
        var service = new CourseService();

        // Act
        var course = await service.GetCourseAsync("python");

        // Assert
        course.Should().NotBeNull();
        course!.Modules.Should().NotBeEmpty();
        course.Modules.First().Lessons.Should().NotBeEmpty();
    }
}
```

### End-to-End Workflow Tests

```csharp
public class LessonCompletionWorkflowTests
{
    [Fact]
    public async Task CompleteLesson_SavesProgressAndUnlocksAchievement()
    {
        // Arrange
        var db = CreateTestDatabase();
        var progressService = new ProgressService(db, eventAggregator);
        var achievementService = new AchievementService(db, eventAggregator);

        // Act
        // 1. Save progress
        await progressService.SaveProgressAsync("user", "python/1-1", 100);

        // 2. Check achievements
        await achievementService.CheckAchievementsAsync("user");

        // Assert
        // Progress saved
        var progress = await db.Progress.FirstAsync();
        progress.Completed.Should().BeTrue();

        // Achievement unlocked
        var achievement = await db.Achievements
            .FirstOrDefaultAsync(a => a.AchievementType == "FIRST_STEPS");

        achievement.Should().NotBeNull();
    }
}
```

---

## 3. UI Testing

### View-ViewModel Binding Tests

```csharp
public class LessonPageViewTests
{
    [Fact]
    public void View_BindsToViewModel()
    {
        // Arrange
        var viewModel = new LessonPageViewModel(
            Mock.Of<ICourseService>(),
            Mock.Of<ICodeExecutor>());

        var view = new LessonPage
        {
            DataContext = viewModel
        };

        // Act
        viewModel.LessonTitle = "Test Lesson";

        // Assert
        var titleBlock = view.FindControl<TextBlock>("LessonTitle");
        titleBlock.Text.Should().Be("Test Lesson");
    }
}
```

### Command Tests

```csharp
public class ButtonCommandTests
{
    [Fact]
    public async Task RunButton_ExecutesCommand()
    {
        // Arrange
        var executed = false;
        var viewModel = new Mock<LessonPageViewModel>();
        viewModel.Setup(x => x.RunCodeCommand.Execute(It.IsAny<object>()))
            .Callback(() => executed = true);

        var button = new Button
        {
            Command = viewModel.Object.RunCodeCommand
        };

        // Act
        button.Command.Execute(null);

        // Assert
        executed.Should().BeTrue();
    }
}
```

---

## 4. Performance Testing

### Benchmarks

```csharp
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class CodeExecutionBenchmarks
{
    private CodeExecutor _executor = null!;

    [GlobalSetup]
    public void Setup()
    {
        _executor = new CodeExecutor();
    }

    [Benchmark]
    public async Task<ExecutionResult> ExecutePython()
    {
        return await _executor.ExecuteAsync("python", "print('Hello')");
    }

    [Benchmark]
    public async Task<ExecutionResult> ExecuteJavaScript()
    {
        return await _executor.ExecuteAsync("javascript", "console.log('Hello')");
    }
}
```

### Performance Targets

| Operation | Target | Max |
|-----------|--------|-----|
| App Startup | < 1s | 2s |
| Page Navigation | < 100ms | 200ms |
| Course Load | < 500ms | 1s |
| Code Execution (Python) | < 2s | 5s |
| Code Execution (Java) | < 5s | 10s |
| Progress Save | < 50ms | 100ms |
| Achievement Check | < 100ms | 200ms |

---

## 5. Cross-Platform Testing

### Test Matrix

| Feature | Windows | macOS | Linux |
|---------|---------|-------|-------|
| **Application** |
| Launch | ✓ | ✓ | ✓ |
| Window rendering | ✓ | ✓ | ✓ |
| Keyboard shortcuts | ✓ | ✓ | ✓ |
| **Code Execution** |
| Python | ✓ | ✓ | ✓ |
| JavaScript | ✓ | ✓ | ✓ |
| Java | ✓ | ✓ | ✓ |
| Rust | ✓ | ✓ | ✓ |
| C# | ✓ | ✓ | ✓ |
| **Data** |
| SQLite persistence | ✓ | ✓ | ✓ |
| Settings file | ✓ | ✓ | ✓ |
| **UI** |
| Dark theme | ✓ | ✓ | ✓ |
| Light theme | ✓ | ✓ | ✓ |
| High DPI | ✓ | ✓ | ✓ |

### Platform-Specific Tests

```csharp
[Fact]
[PlatformSpecific(TestPlatforms.Windows)]
public async Task ExecuteAsync_Windows_UsesCmdExe()
{
    // Windows-specific test
}

[Fact]
[PlatformSpecific(TestPlatforms.Linux | TestPlatforms.OSX)]
public async Task ExecuteAsync_Unix_UsesBash()
{
    // Unix-specific test
}
```

---

## 6. Manual Testing Checklist

### Functional Testing

#### Course Navigation
- [ ] Landing page displays all courses
- [ ] Clicking course navigates to course page
- [ ] Course page shows modules
- [ ] Clicking module expands lessons
- [ ] Clicking lesson loads lesson page
- [ ] Breadcrumb navigation works
- [ ] Back button works
- [ ] Progress indicators display correctly

#### Code Editor
- [ ] Editor loads with starter code
- [ ] Syntax highlighting works for all languages
- [ ] Line numbers display
- [ ] Code folding works
- [ ] Find/Replace works
- [ ] Undo/Redo works
- [ ] Tab indentation works
- [ ] Auto-save triggers after 2 seconds

#### Code Execution
- [ ] Run button executes code
- [ ] Ctrl+Enter executes code
- [ ] Output displays in panel
- [ ] Errors display in red
- [ ] Execution timeout works (10s)
- [ ] Loading indicator shows during execution
- [ ] Multiple executions work

#### Challenges (All 6 Types)
- [ ] Multiple Choice: Select option, submit, see result
- [ ] True/False: Click button, see result
- [ ] Code Output: Enter answer, submit, see result
- [ ] Free Coding: Write code, run tests, see results
- [ ] Code Completion: Fill blanks, run tests
- [ ] Conceptual: Write answer, word count updates, submit

#### Progress Tracking
- [ ] Completing lesson saves progress
- [ ] Progress persists after app restart
- [ ] Course progress percentage updates
- [ ] Checkmarks appear on completed lessons
- [ ] Best score is retained

#### Achievements
- [ ] "First Steps" unlocks after first lesson
- [ ] "Perfectionist" unlocks after 100% score
- [ ] "Marathon Runner" unlocks after 7-day streak
- [ ] Achievement notification displays
- [ ] Achievement gallery shows unlocked achievements

#### Settings
- [ ] Theme toggle works
- [ ] Font size slider works
- [ ] Auto-save toggle works
- [ ] Keyboard shortcuts editor works
- [ ] Settings persist after restart

#### Keyboard Shortcuts
- [ ] Ctrl+K opens command palette
- [ ] Ctrl+Enter runs code
- [ ] Ctrl+S saves progress
- [ ] Ctrl+/ toggles hints
- [ ] Ctrl+R resets challenge
- [ ] Ctrl+. opens settings
- [ ] F1 opens help

### Non-Functional Testing

#### Performance
- [ ] App starts in < 1 second
- [ ] Page navigation is instant
- [ ] Code execution completes in < 5 seconds
- [ ] No UI lag when typing in editor
- [ ] Memory usage < 100MB idle

#### Usability
- [ ] UI is intuitive
- [ ] Error messages are clear
- [ ] Loading states are visible
- [ ] Buttons are clearly labeled
- [ ] Text is readable

#### Accessibility
- [ ] Tab navigation works
- [ ] Focus indicators visible
- [ ] Screen reader compatible (basic)
- [ ] High contrast mode supported
- [ ] Keyboard-only navigation possible

#### Reliability
- [ ] App doesn't crash
- [ ] Data doesn't corrupt
- [ ] Errors are handled gracefully
- [ ] App recovers from errors
- [ ] No memory leaks

---

## 7. Regression Testing

### Test Suite Execution

```bash
# Run all tests
dotnet test

# Run specific category
dotnet test --filter Category=Integration

# Run with coverage
dotnet test /p:CollectCoverage=true
```

### Continuous Integration

```yaml
# .github/workflows/test.yml
name: Test

on: [push, pull_request]

jobs:
  test:
    runs-on: ${{ matrix.os }}
    strategy:
      matrix:
        os: [ubuntu-latest, windows-latest, macos-latest]

    steps:
      - uses: actions/checkout@v2

      - name: Setup .NET
        uses: actions/setup-dotnet@v1
        with:
          dotnet-version: 8.0.x

      - name: Restore dependencies
        run: dotnet restore

      - name: Build
        run: dotnet build --no-restore

      - name: Test
        run: dotnet test --no-build --verbosity normal /p:CollectCoverage=true

      - name: Upload coverage
        uses: codecov/codecov-action@v2
```

---

## 8. Acceptance Testing

### User Acceptance Criteria

#### Complete Python Course
- [ ] User can complete all Python lessons
- [ ] All challenges validate correctly
- [ ] Progress is saved
- [ ] Achievements unlock
- [ ] Final score is calculated

#### Multi-Language Support
- [ ] User can switch between courses
- [ ] Code executes for Python, Java, Rust, C#, JS
- [ ] Syntax highlighting works for all languages

#### Offline Operation
- [ ] App works without internet
- [ ] All content loads from local files
- [ ] Progress saves locally

---

## 9. Test Data

### Sample Test Data

```csharp
public static class TestData
{
    public static Course SampleCourse => new()
    {
        Id = "test-python",
        Language = "Python",
        Title = "Test Python Course",
        Modules = new List<Module>
        {
            new()
            {
                Id = "test-module-1",
                Title = "Test Module",
                Lessons = new List<Lesson>
                {
                    new()
                    {
                        Id = "test-lesson-1",
                        Title = "Test Lesson",
                        Content = new LessonContent
                        {
                            Body = "# Test\n\nThis is a test lesson."
                        },
                        Exercises = new List<Challenge>
                        {
                            new MultipleChoiceChallenge
                            {
                                Id = "test-mc-1",
                                Question = "What is 2+2?",
                                Options = new List<string> { "3", "4", "5" },
                                CorrectAnswer = 1
                            }
                        }
                    }
                }
            }
        }
    };
}
```

---

## 10. Bug Tracking

### Issue Template

```markdown
**Bug Description:**
Brief description of the issue

**Steps to Reproduce:**
1. Open app
2. Navigate to lesson
3. Click run button
4. Observe error

**Expected Behavior:**
Code should execute and show output

**Actual Behavior:**
Error message appears

**Environment:**
- OS: Windows 11
- .NET Version: 8.0.1
- App Version: 1.0.0

**Screenshots:**
[Attach if applicable]

**Logs:**
```
[Attach relevant logs]
```
```

---

## Quality Gates

### Before Release

- [ ] ✅ All unit tests pass
- [ ] ✅ All integration tests pass
- [ ] ✅ 80%+ code coverage
- [ ] ✅ Manual testing checklist complete
- [ ] ✅ Cross-platform testing complete
- [ ] ✅ Performance benchmarks met
- [ ] ✅ No critical bugs
- [ ] ✅ No memory leaks
- [ ] ✅ Documentation updated

---

**Document Version:** 1.0
**Last Updated:** 2025-11-18
