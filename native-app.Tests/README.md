# Code Tutor - Test Suite

Comprehensive test suite for the Code Tutor native application using xUnit, Moq, and FluentAssertions.

---

## Project Structure

```
native-app.Tests/
├── Unit/
│   └── Services/          # Service layer unit tests
│       ├── ProgressServiceTests.cs (14 tests)
│       ├── AchievementServiceTests.cs (20 tests)
│       └── StreakServiceTests.cs (11 tests)
├── Integration/            # End-to-end workflow tests
│   └── LessonCompletionWorkflowTests.cs (8 tests)
├── ViewModels/
│   └── Pages/             # ViewModel tests with mocks
│       └── LessonPageViewModelTests.cs (11 tests)
├── Fixtures/              # Test infrastructure
│   └── DatabaseFixture.cs
└── Helpers/               # Test utilities
    └── TestDataGenerator.cs
```

---

## Running Tests

### Command Line

```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --logger "console;verbosity=detailed"

# Run specific test class
dotnet test --filter "FullyQualifiedName~ProgressServiceTests"

# Run tests with coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

### Visual Studio

1. Open Test Explorer: `Test` → `Test Explorer`
2. Click `Run All` to execute all tests
3. Click individual tests to run specific tests
4. Right-click test → `Debug` to debug a failing test

### VS Code

1. Install C# Dev Kit extension
2. Open Testing panel (beaker icon)
3. Click play button to run tests
4. View results inline in code

---

## Test Categories

### Unit Tests - Services

**ProgressService** (14 tests)
- ✅ SaveProgressAsync creates/updates records
- ✅ Keeps best scores across attempts
- ✅ Tracks hints per lesson and challenge
- ✅ Sets completion timestamps correctly
- ✅ Calculates course progress percentage

**AchievementService** (20 tests)
- ✅ All 10 achievement types unlock correctly
- ✅ Progressive achievements increment properly
- ✅ No duplicate achievement records
- ✅ Achievement progress capped at max
- ✅ Integration with ProgressService and StreakService

**StreakService** (11 tests)
- ✅ Records daily activity
- ✅ Calculates consecutive day streaks
- ✅ Breaks streaks on gaps
- ✅ Tracks current and longest streaks
- ✅ Handles multiple activities per day

### Integration Tests

**LessonCompletionWorkflow** (8 tests)
- ✅ Complete lesson flow with progress, streaks, achievements
- ✅ 7-day streak unlocks MarathonRunner
- ✅ Perfect lesson unlocks Perfectionist and SpeedDemon
- ✅ Quick completion unlocks QuickLearner
- ✅ Multiple attempts track Debugger progress
- ✅ Streak breaks reset current but keep longest
- ✅ Hints tracked correctly across challenges

### ViewModel Tests

**LessonPageViewModel** (11 tests)
- ✅ Loads lessons and creates challenge ViewModels
- ✅ Records streak on load
- ✅ Handles errors gracefully
- ✅ Saves progress on completion
- ✅ Navigates back after completion
- ✅ Tracks hints through events
- ✅ Checks achievements on completion

---

## Test Patterns

### AAA Pattern (Arrange-Act-Assert)

```csharp
[Fact]
public async Task SaveProgressAsync_CreatesNewRecord_WhenNoExistingProgress()
{
    // Arrange - Set up test data and dependencies
    using var context = _fixture.CreateContext();
    var service = new ProgressService(context);

    // Act - Execute the method being tested
    await service.SaveProgressAsync("course1", "module1", "lesson1", 85, true);

    // Assert - Verify the expected outcomes
    var progress = await context.Progress.FirstOrDefaultAsync();
    progress.Should().NotBeNull();
    progress.Score.Should().Be(85);
}
```

### Database Testing with Fixtures

```csharp
public class MyServiceTests : IDisposable
{
    private readonly DatabaseFixture _fixture;

    public MyServiceTests()
    {
        _fixture = new DatabaseFixture();
    }

    [Fact]
    public async Task MyTest()
    {
        // Each test gets a fresh in-memory database
        using var context = _fixture.CreateContext();
        var service = new MyService(context);

        // ... test code
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }
}
```

### Mocking Dependencies

```csharp
[Fact]
public async Task LoadLessonAsync_CallsCourseService()
{
    // Arrange
    var mockCourseService = new Mock<ICourseService>();
    mockCourseService
        .Setup(s => s.GetLessonAsync("course1", "module1", "lesson1"))
        .ReturnsAsync(new Lesson { Id = "lesson1" });

    var viewModel = new LessonPageViewModel(mockCourseService.Object, ...);

    // Act
    await viewModel.LoadLessonAsync();

    // Assert
    mockCourseService.Verify(
        s => s.GetLessonAsync("course1", "module1", "lesson1"),
        Times.Once);
}
```

### FluentAssertions

```csharp
// Readable assertions
result.Should().NotBeNull();
result.Score.Should().Be(100);
result.IsCorrect.Should().BeTrue();

// Collection assertions
achievements.Should().HaveCount(2);
achievements.Should().Contain(a => a.AchievementType == "FirstSteps");

// Time assertions
achievement.UnlockedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
```

---

## Test Data Generation

Use `TestDataGenerator` for consistent test data:

```csharp
// Create progress
var progress = TestDataGenerator.CreateProgress(
    courseId: "course1",
    score: 85,
    completed: true,
    hintsUsed: 2);

// Create streak
var streak = TestDataGenerator.CreateStreak(
    date: DateTime.UtcNow,
    lessonsCompleted: 1);

// Create consecutive streaks
var streaks = TestDataGenerator.CreateConsecutiveStreaks(days: 7);

// Create challenge
var challenge = TestDataGenerator.CreateFreeCodingChallenge(
    id: "challenge1",
    language: "csharp");

// Create lesson with challenges
var lesson = TestDataGenerator.CreateLesson(
    id: "lesson1",
    challengeCount: 3);
```

---

## Current Test Coverage

| Component | Tests | Coverage Target | Status |
|-----------|-------|----------------|--------|
| ProgressService | 14 | 90% | ✅ Complete |
| AchievementService | 20 | 90% | ✅ Complete |
| StreakService | 11 | 90% | ✅ Complete |
| LessonPageViewModel | 11 | 80% | ✅ Complete |
| Integration Workflows | 8 | N/A | ✅ Complete |
| **Total** | **64** | **80%** | ✅ Phase 9 Core Complete |

---

## Adding New Tests

### 1. Create Test Class

```csharp
public class MyServiceTests : IDisposable
{
    private readonly DatabaseFixture _fixture;

    public MyServiceTests()
    {
        _fixture = new DatabaseFixture();
    }

    [Fact]
    public async Task MyTest_DoesExpectedThing()
    {
        // Arrange
        using var context = _fixture.CreateContext();
        var service = new MyService(context);

        // Act
        var result = await service.DoSomethingAsync();

        // Assert
        result.Should().NotBeNull();
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }
}
```

### 2. Run Tests

```bash
dotnet test
```

### 3. Fix Failures

- Check assertion messages
- Verify test data setup
- Debug failing tests
- Update expectations

---

## Common Issues

### Issue: InMemory Database Conflicts

**Problem**: Tests interfere with each other

**Solution**: Use unique database names per test
```csharp
var options = new DbContextOptionsBuilder<CodeTutorDbContext>()
    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
    .Options;
```

### Issue: Async Tests Timing Out

**Problem**: Async operations not completing

**Solution**: Use `await` properly and increase timeout
```csharp
[Fact(Timeout = 5000)] // 5 second timeout
public async Task MyAsyncTest()
{
    await service.DoSomethingAsync();
}
```

### Issue: Mock Verification Failing

**Problem**: Mock not called as expected

**Solution**: Verify setup and timing
```csharp
// Ensure setup matches call exactly
mockService.Setup(s => s.Method(It.IsAny<string>()))
          .ReturnsAsync(result);

// Verify after the call
await systemUnderTest.DoWork();
mockService.Verify(s => s.Method(It.IsAny<string>()), Times.Once);
```

---

## Best Practices

1. **One Assert Per Test** (when possible)
   - Tests should verify one behavior
   - Makes failures easier to diagnose

2. **Descriptive Test Names**
   - Format: `MethodName_ExpectedBehavior_WhenCondition`
   - Example: `SaveProgressAsync_CreatesNewRecord_WhenNoExistingProgress`

3. **Arrange-Act-Assert**
   - Always use AAA pattern
   - Separate sections with comments

4. **Clean Up Resources**
   - Dispose contexts and fixtures
   - Implement IDisposable properly

5. **Test Both Success and Failure**
   - Happy path tests
   - Error handling tests
   - Edge cases

6. **Avoid Test Interdependence**
   - Each test should run independently
   - Use fresh database per test
   - Don't rely on test execution order

7. **Use TestDataGenerator**
   - Consistent test data
   - Reduces boilerplate
   - Easy to maintain

---

## Future Enhancements

### Additional Test Coverage

- [ ] CoursePageViewModel tests
- [ ] Challenge ViewModel tests (all 6 types)
- [ ] ErrorHandlerService unit tests
- [ ] SettingsService unit tests
- [ ] CourseService unit tests
- [ ] NavigationService tests

### Integration Tests

- [ ] Multi-course achievement workflow
- [ ] Settings persistence workflow
- [ ] Error recovery workflow
- [ ] Database migration tests

### Performance Tests

- [ ] Large dataset handling
- [ ] Query performance benchmarks
- [ ] Memory usage tests

### UI Tests (Future)

- [ ] Avalonia UI component tests
- [ ] Visual regression tests
- [ ] Accessibility tests

---

## Resources

- **xUnit Documentation**: https://xunit.net/docs/getting-started/netcore/cmdline
- **Moq Quickstart**: https://github.com/moq/moq4/wiki/Quickstart
- **FluentAssertions**: https://fluentassertions.com/introduction
- **EF Core Testing**: https://learn.microsoft.com/en-us/ef/core/testing/

---

**Test Suite Version**: 1.0.0
**Last Updated**: 2025-11-18
**Total Tests**: 64
**Overall Status**: ✅ Phase 9 Core Complete
