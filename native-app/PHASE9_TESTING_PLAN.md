# Phase 9: Testing & Quality Assurance Plan

**Status**: In Progress
**Start Date**: 2025-11-18

---

## Overview

Comprehensive testing strategy for Code Tutor native application covering unit tests, integration tests, and ViewModel tests to ensure reliability and correctness.

---

## Testing Infrastructure

### Test Project Structure

```
native-app.Tests/
├── Unit/
│   ├── Services/
│   │   ├── ProgressServiceTests.cs
│   │   ├── AchievementServiceTests.cs
│   │   ├── StreakServiceTests.cs
│   │   ├── ErrorHandlerServiceTests.cs
│   │   ├── SettingsServiceTests.cs
│   │   └── CourseServiceTests.cs
│   └── Models/
│       └── AchievementDefinitionTests.cs
├── Integration/
│   ├── LessonCompletionWorkflowTests.cs
│   ├── AchievementUnlockWorkflowTests.cs
│   ├── StreakTrackingWorkflowTests.cs
│   └── DatabaseIntegrationTests.cs
├── ViewModels/
│   ├── Challenges/
│   │   ├── FreeCodingViewModelTests.cs
│   │   ├── CodeCompletionViewModelTests.cs
│   │   ├── MultipleChoiceViewModelTests.cs
│   │   └── ChallengeViewModelBaseTests.cs
│   └── Pages/
│       ├── LessonPageViewModelTests.cs
│       └── CoursePageViewModelTests.cs
├── Fixtures/
│   ├── DatabaseFixture.cs
│   ├── ServiceFixture.cs
│   └── TestDataGenerator.cs
└── Helpers/
    ├── MockFactory.cs
    └── AssertExtensions.cs
```

### Dependencies Required

```xml
<PackageReference Include="xunit" Version="2.6.3" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.5.5" />
<PackageReference Include="Moq" Version="4.20.70" />
<PackageReference Include="FluentAssertions" Version="6.12.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.0" />
<PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="8.0.0" />
```

---

## Test Categories

### 1. Unit Tests - Services (Priority: High)

#### ProgressService Tests
- **SaveProgressAsync**
  - ✅ Creates new progress record
  - ✅ Updates existing progress with better score
  - ✅ Increments attempt count
  - ✅ Sets completion timestamp
  - ✅ Handles hints tracking

- **SaveChallengeProgressAsync**
  - ✅ Creates challenge-specific progress
  - ✅ Keeps best score across attempts
  - ✅ Tracks hints per challenge

- **GetLessonProgressAsync**
  - ✅ Returns existing progress
  - ✅ Returns null for non-existent progress

- **IncrementHintUsageAsync**
  - ✅ Increments hint count for lesson
  - ✅ Increments hint count for challenge

#### AchievementService Tests
- **CheckAchievementsAsync**
  - ✅ FirstSteps: Unlocks after first lesson
  - ✅ QuickLearner: Unlocks for lesson <30 min
  - ✅ Perfectionist: Unlocks for 100% lesson score
  - ✅ Polyglot: Unlocks after 3 languages
  - ✅ MarathonRunner: Unlocks after 7-day streak
  - ✅ SpeedDemon: Unlocks after 5 challenges without hints
  - ✅ Debugger: Unlocks after 10 failed attempts
  - ✅ CourseComplete: Unlocks after completing course
  - ✅ TestMaster: Unlocks after 100 tests passed
  - ✅ NightOwl: Unlocks for lesson after 10 PM

- **UnlockAchievementAsync**
  - ✅ Creates achievement record
  - ✅ Does not duplicate achievements
  - ✅ Sets unlock timestamp

- **IncrementProgressAsync**
  - ✅ Updates progressive achievement progress
  - ✅ Caps at max progress

#### StreakService Tests
- **RecordActivityAsync**
  - ✅ Creates new streak record for today
  - ✅ Updates existing streak record
  - ✅ Increments lesson/challenge counts
  - ✅ Adds minutes spent

- **GetCurrentStreakAsync**
  - ✅ Returns 0 for no activity
  - ✅ Returns 1 for single day
  - ✅ Calculates consecutive days correctly
  - ✅ Breaks on missing days
  - ✅ Breaks if last activity >1 day ago

- **GetLongestStreakAsync**
  - ✅ Finds longest consecutive sequence
  - ✅ Returns 0 for no activity

#### ErrorHandlerService Tests
- **HandleErrorAsync**
  - ✅ Logs error to file
  - ✅ Creates log directory if missing
  - ✅ Formats log correctly

- **GetUserFriendlyMessage**
  - ✅ FileNotFoundException → "File not found..."
  - ✅ UnauthorizedAccessException → "Permission denied..."
  - ✅ IOException → "File access error..."
  - ✅ Unknown exception → "Unexpected error..."

- **IsFatalException**
  - ✅ OutOfMemoryException → true
  - ✅ StackOverflowException → true
  - ✅ AccessViolationException → true
  - ✅ Regular exceptions → false

---

### 2. Unit Tests - ViewModels (Priority: High)

#### ChallengeViewModelBase Tests
- **ShowHintCommand**
  - ✅ Increments CurrentHintIndex
  - ✅ Increments HintsUsed
  - ✅ Raises HintShown event
  - ✅ Updates HasMoreHints property
  - ✅ Doesn't exceed hint count

- **ResetCommand**
  - ✅ Resets HasSubmitted to false
  - ✅ Clears Result
  - ✅ Resets CurrentHintIndex to -1
  - ✅ Resets HintsUsed to 0

#### FreeCodingViewModel Tests
- **SubmitCommand**
  - ✅ Disabled when code is empty
  - ✅ Disabled while validating
  - ✅ Calls validation service
  - ✅ Sets Result property
  - ✅ Sets HasSubmitted to true
  - ✅ Handles validation errors gracefully

- **ShowSolutionCommand**
  - ✅ Sets ShowSolution to true
  - ✅ Exposes solution text

#### LessonPageViewModel Tests
- **LoadLessonAsync**
  - ✅ Loads lesson from course service
  - ✅ Creates challenge ViewModels
  - ✅ Wires up HintShown events
  - ✅ Wires up PropertyChanged events
  - ✅ Records initial streak activity
  - ✅ Sets lesson start time
  - ✅ Handles errors gracefully

- **MarkLessonCompleteAsync**
  - ✅ Calculates time spent
  - ✅ Sums hints used across challenges
  - ✅ Saves progress with hints
  - ✅ Records streak activity
  - ✅ Checks achievements
  - ✅ Navigates back

- **OnChallengeCompletedAsync**
  - ✅ Saves challenge progress
  - ✅ Records streak for correct answers
  - ✅ Checks achievements
  - ✅ Handles errors without crashing

- **OnChallengeHintShown**
  - ✅ Increments hint usage in progress

---

### 3. Integration Tests (Priority: Medium)

#### Lesson Completion Workflow
**Test**: Complete lesson with 3 challenges, 2 hints used
1. Load lesson → LessonPageViewModel
2. Complete Challenge 1 (100%) → Progress saved
3. Use hint on Challenge 2 → Hint tracked
4. Complete Challenge 2 (80%) → Progress saved
5. Complete Challenge 3 (100%) → Progress saved
6. Mark lesson complete → Achievement check

**Assertions**:
- Lesson progress: 100%, 2 hints
- Challenge 1 progress: 100%, 0 hints
- Challenge 2 progress: 80%, 1 hint
- Challenge 3 progress: 100%, 0 hints
- Streak recorded
- Achievements checked

#### Achievement Unlock Workflow
**Test**: Unlock FirstSteps and Perfectionist
1. Complete first lesson (all challenges 100%)
2. Check achievements

**Assertions**:
- FirstSteps achievement unlocked
- Perfectionist achievement unlocked
- Both have unlock timestamps

#### Streak Tracking Workflow
**Test**: 7-day learning streak
1. Record activity for 7 consecutive days
2. Check current streak
3. Verify MarathonRunner achievement

**Assertions**:
- Current streak = 7
- MarathonRunner unlocked
- Streak breaks on day gap

#### Database Persistence
**Test**: Data persists across DbContext instances
1. Save progress in one context
2. Dispose context
3. Create new context
4. Verify progress exists

---

### 4. Test Fixtures

#### DatabaseFixture
```csharp
public class DatabaseFixture : IDisposable
{
    public CodeTutorDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<CodeTutorDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new CodeTutorDbContext(options);
        context.Database.EnsureCreated();
        SeedData(context);
        return context;
    }

    private void SeedData(CodeTutorDbContext context)
    {
        // Seed default user
        // Seed sample progress
    }
}
```

#### ServiceFixture
```csharp
public class ServiceFixture
{
    public Mock<ICourseService> MockCourseService { get; }
    public Mock<INavigationService> MockNavigationService { get; }
    public Mock<IChallengeValidationService> MockValidationService { get; }

    public ServiceFixture()
    {
        MockCourseService = new Mock<ICourseService>();
        MockNavigationService = new Mock<INavigationService>();
        MockValidationService = new Mock<IChallengeValidationService>();

        SetupDefaultBehavior();
    }
}
```

---

## Testing Best Practices

### 1. AAA Pattern
```csharp
[Fact]
public async Task SaveProgressAsync_CreatesNewRecord_WhenNoExistingProgress()
{
    // Arrange
    using var context = _fixture.CreateContext();
    var service = new ProgressService(context);

    // Act
    await service.SaveProgressAsync("course1", "module1", "lesson1", 85, true);

    // Assert
    var progress = await context.Progress.FirstOrDefaultAsync();
    progress.Should().NotBeNull();
    progress.Score.Should().Be(85);
    progress.Completed.Should().BeTrue();
}
```

### 2. Test Naming Convention
- **Format**: `MethodName_ExpectedBehavior_WhenCondition`
- **Examples**:
  - `SaveProgressAsync_CreatesNewRecord_WhenNoExistingProgress`
  - `GetCurrentStreakAsync_ReturnsZero_WhenNoActivity`
  - `ShowHintCommand_RaisesEvent_WhenExecuted`

### 3. FluentAssertions
```csharp
// Use FluentAssertions for readable assertions
result.Score.Should().Be(100);
result.IsCorrect.Should().BeTrue();
achievements.Should().HaveCount(2);
achievements.Should().Contain(a => a.AchievementType == "FirstSteps");
```

### 4. Async Testing
```csharp
// Always test async methods properly
[Fact]
public async Task CheckAchievementsAsync_UnlocksFirstSteps_AfterFirstLesson()
{
    // Arrange
    await SeedCompletedLesson();

    // Act
    await _achievementService.CheckAchievementsAsync();

    // Assert
    var achievement = await GetAchievement(AchievementType.FirstSteps);
    achievement.Should().NotBeNull();
}
```

---

## Coverage Goals

| Component | Target Coverage | Priority |
|-----------|----------------|----------|
| Services | 90%+ | High |
| ViewModels | 80%+ | High |
| Models | 70%+ | Medium |
| Converters | 80%+ | Low |
| Overall | 80%+ | High |

---

## Test Execution Plan

### Phase 9.1: Service Unit Tests (Days 1-2)
- ProgressService (12 tests)
- AchievementService (15 tests)
- StreakService (8 tests)
- ErrorHandlerService (8 tests)
- **Total**: ~40 tests

### Phase 9.2: ViewModel Tests (Days 3-4)
- ChallengeViewModelBase (6 tests)
- FreeCodingViewModel (6 tests)
- CodeCompletionViewModel (6 tests)
- LessonPageViewModel (10 tests)
- CoursePageViewModel (5 tests)
- **Total**: ~35 tests

### Phase 9.3: Integration Tests (Day 5)
- Lesson completion workflow (3 tests)
- Achievement unlock workflow (3 tests)
- Streak tracking workflow (3 tests)
- Database persistence (3 tests)
- **Total**: ~12 tests

### Phase 9.4: Polish & Documentation (Day 6)
- Add missing tests for edge cases
- Document test patterns
- Create test data generators
- CI/CD integration (if applicable)

---

## Manual Testing Checklist

### End-to-End User Flows

#### Flow 1: New User First Lesson
- [ ] Launch app
- [ ] Navigate to course
- [ ] Open first lesson
- [ ] Complete multiple choice challenge
- [ ] Complete coding challenge
- [ ] Use hint system
- [ ] Submit answers
- [ ] Mark lesson complete
- [ ] Verify FirstSteps achievement unlocked
- [ ] Verify streak recorded

#### Flow 2: Multi-Day Streak
- [ ] Complete lesson on Day 1
- [ ] Complete lesson on Day 2
- [ ] Complete lesson on Day 3
- [ ] Verify 3-day streak
- [ ] Skip Day 4
- [ ] Complete lesson on Day 5
- [ ] Verify streak reset to 1

#### Flow 3: Achievement Hunting
- [ ] Complete lesson in <30 min → QuickLearner
- [ ] Complete all challenges at 100% → Perfectionist
- [ ] Complete 5 challenges without hints → SpeedDemon
- [ ] Verify achievements appear
- [ ] Verify timestamps correct

#### Flow 4: Error Handling
- [ ] Disconnect network during lesson load
- [ ] Verify user-friendly error message
- [ ] Verify error logged to file
- [ ] Retry lesson load
- [ ] Verify recovery works

---

## Known Test Challenges

### 1. Time-Dependent Tests
**Issue**: QuickLearner and NightOwl achievements depend on time
**Solution**:
- Use dependency injection for time provider
- Mock `DateTime.UtcNow` in tests
- Create `ITimeProvider` interface

### 2. Database State Management
**Issue**: Tests may interfere with each other
**Solution**:
- Use InMemory database with unique names per test
- Dispose contexts properly
- Use fixtures with IDisposable

### 3. ViewModel Event Testing
**Issue**: Testing PropertyChanged and custom events
**Solution**:
- Use `PropertyChangedEventArgs` assertions
- Subscribe to events in test setup
- Verify events raised with correct args

### 4. Async Void Event Handlers
**Issue**: Fire-and-forget async in ViewModels
**Solution**:
- Convert to Task-returning methods for testing
- Use `TaskCompletionSource` to wait for completion
- Test error handling separately

---

## Success Criteria

✅ **All service methods have unit tests**
✅ **All ViewModels have tests for commands and properties**
✅ **Integration tests cover main user workflows**
✅ **80%+ code coverage achieved**
✅ **All tests pass consistently**
✅ **No flaky tests**
✅ **Test execution time < 30 seconds**
✅ **Documentation complete**

---

## Next Steps After Phase 9

Once Phase 9 is complete:
1. **Phase 10: Packaging & Distribution**
   - Build configuration
   - Platform-specific installers
   - AOT compilation
   - Release notes

2. **Continuous Testing**
   - Set up CI/CD pipeline
   - Automated test runs on commits
   - Code coverage reporting
   - Test result notifications

---

## Resources

- **xUnit Documentation**: https://xunit.net/
- **Moq Quickstart**: https://github.com/moq/moq4
- **FluentAssertions**: https://fluentassertions.com/
- **EF Core Testing**: https://learn.microsoft.com/en-us/ef/core/testing/

---

**Status**: Planning Complete - Ready for Implementation
**Estimated Completion**: 6 days
**Test Count Target**: ~90 tests
**Coverage Target**: 80%+
