using CodeTutor.Native.ViewModels.Pages;
using CodeTutor.Native.Services;
using CodeTutor.Native.Models;
using CodeTutor.Native.ViewModels.Challenges;
using CodeTutor.Native.Tests.Helpers;

namespace CodeTutor.Native.Tests.ViewModels.Pages;

/// <summary>
/// Unit tests for LessonPageViewModel
/// </summary>
public class LessonPageViewModelTests
{
    private readonly Mock<ICourseService> _mockCourseService;
    private readonly Mock<INavigationService> _mockNavigationService;
    private readonly Mock<IProgressService> _mockProgressService;
    private readonly Mock<IChallengeFactory> _mockChallengeFactory;
    private readonly Mock<IAchievementService> _mockAchievementService;
    private readonly Mock<IStreakService> _mockStreakService;
    private readonly Mock<IErrorHandlerService> _mockErrorHandler;

    public LessonPageViewModelTests()
    {
        _mockCourseService = new Mock<ICourseService>();
        _mockNavigationService = new Mock<INavigationService>();
        _mockProgressService = new Mock<IProgressService>();
        _mockChallengeFactory = new Mock<IChallengeFactory>();
        _mockAchievementService = new Mock<IAchievementService>();
        _mockStreakService = new Mock<IStreakService>();
        _mockErrorHandler = new Mock<IErrorHandlerService>();
    }

    [Fact]
    public async Task LoadLessonAsync_LoadsLesson_AndCreatesChallengeViewModels()
    {
        // Arrange
        var lesson = TestDataGenerator.CreateLesson(challengeCount: 3);
        _mockCourseService
            .Setup(s => s.GetLessonAsync("course1", "module1", "lesson1"))
            .ReturnsAsync(lesson);

        _mockChallengeFactory
            .Setup(f => f.CreateViewModel(It.IsAny<Challenge>()))
            .Returns((Challenge c) => new Mock<ChallengeViewModelBase>(c).Object);

        var viewModel = CreateViewModel();

        // Act
        viewModel.OnNavigatedTo(new LessonNavigationParameter
        {
            CourseId = "course1",
            ModuleId = "module1",
            LessonId = "lesson1"
        });

        await Task.Delay(100); // Allow async load to complete

        // Assert
        viewModel.Lesson.Should().NotBeNull();
        viewModel.Lesson!.Id.Should().Be("lesson1");
        viewModel.Challenges.Should().HaveCount(3);
        viewModel.HasChallenges.Should().BeTrue();
    }

    [Fact]
    public async Task LoadLessonAsync_RecordsStreakActivity_OnLoad()
    {
        // Arrange
        var lesson = TestDataGenerator.CreateLesson();
        _mockCourseService
            .Setup(s => s.GetLessonAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(lesson);

        var viewModel = CreateViewModel();

        // Act
        viewModel.OnNavigatedTo(new LessonNavigationParameter
        {
            CourseId = "course1",
            ModuleId = "module1",
            LessonId = "lesson1"
        });

        await Task.Delay(100);

        // Assert
        _mockStreakService.Verify(
            s => s.RecordActivityAsync(
                false, // lessonCompleted
                false, // challengeCompleted
                0),    // minutesSpent
            Times.Once);
    }

    [Fact]
    public async Task LoadLessonAsync_SetsError_WhenLessonNotFound()
    {
        // Arrange
        _mockCourseService
            .Setup(s => s.GetLessonAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((Lesson?)null);

        var viewModel = CreateViewModel();

        // Act
        viewModel.OnNavigatedTo(new LessonNavigationParameter
        {
            CourseId = "course1",
            ModuleId = "module1",
            LessonId = "nonexistent"
        });

        await Task.Delay(100);

        // Assert
        viewModel.ErrorMessage.Should().Be("Lesson not found.");
        viewModel.HasError.Should().BeTrue();
    }

    [Fact]
    public async Task LoadLessonAsync_HandlesException_Gracefully()
    {
        // Arrange
        _mockCourseService
            .Setup(s => s.GetLessonAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ThrowsAsync(new InvalidOperationException("Test exception"));

        _mockErrorHandler
            .Setup(e => e.GetUserFriendlyMessage(It.IsAny<Exception>()))
            .Returns("Something went wrong");

        var viewModel = CreateViewModel();

        // Act
        viewModel.OnNavigatedTo(new LessonNavigationParameter
        {
            CourseId = "course1",
            ModuleId = "module1",
            LessonId = "lesson1"
        });

        await Task.Delay(100);

        // Assert
        viewModel.HasError.Should().BeTrue();
        viewModel.ErrorMessage.Should().Contain("Something went wrong");
        _mockErrorHandler.Verify(
            e => e.HandleErrorAsync(It.IsAny<Exception>(), "Lesson loading", false),
            Times.Once);
    }

    [Fact]
    public async Task MarkLessonCompleteAsync_SavesProgress_WithCorrectData()
    {
        // Arrange
        var lesson = TestDataGenerator.CreateLesson();
        _mockCourseService
            .Setup(s => s.GetLessonAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(lesson);

        var viewModel = CreateViewModel();
        viewModel.OnNavigatedTo(new LessonNavigationParameter
        {
            CourseId = "course1",
            ModuleId = "module1",
            LessonId = "lesson1"
        });

        await Task.Delay(100);

        // Act
        await viewModel.MarkCompleteCommand.Execute();

        // Assert
        _mockProgressService.Verify(
            p => p.SaveProgressAsync(
                "course1",
                "module1",
                "lesson1",
                100,
                true,
                It.IsAny<int>()), // hintsUsed
            Times.Once);
    }

    [Fact]
    public async Task MarkLessonCompleteAsync_RecordsStreak_WithTimeSpent()
    {
        // Arrange
        var lesson = TestDataGenerator.CreateLesson();
        _mockCourseService
            .Setup(s => s.GetLessonAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(lesson);

        var viewModel = CreateViewModel();
        viewModel.OnNavigatedTo(new LessonNavigationParameter
        {
            CourseId = "course1",
            ModuleId = "module1",
            LessonId = "lesson1"
        });

        await Task.Delay(100);

        // Simulate time passing
        await Task.Delay(100);

        // Act
        await viewModel.MarkCompleteCommand.Execute();

        // Assert
        _mockStreakService.Verify(
            s => s.RecordActivityAsync(
                true, // lessonCompleted
                false, // challengeCompleted
                It.IsAny<int>()), // minutesSpent (calculated)
            Times.Once);
    }

    [Fact]
    public async Task MarkLessonCompleteAsync_ChecksAchievements()
    {
        // Arrange
        var lesson = TestDataGenerator.CreateLesson();
        _mockCourseService
            .Setup(s => s.GetLessonAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(lesson);

        var viewModel = CreateViewModel();
        viewModel.OnNavigatedTo(new LessonNavigationParameter
        {
            CourseId = "course1",
            ModuleId = "module1",
            LessonId = "lesson1"
        });

        await Task.Delay(100);

        // Act
        await viewModel.MarkCompleteCommand.Execute();

        // Assert
        _mockAchievementService.Verify(
            a => a.CheckAchievementsAsync(),
            Times.Once);
    }

    [Fact]
    public async Task MarkLessonCompleteAsync_NavigatesBack_AfterCompletion()
    {
        // Arrange
        var lesson = TestDataGenerator.CreateLesson();
        _mockCourseService
            .Setup(s => s.GetLessonAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(lesson);

        var viewModel = CreateViewModel();
        viewModel.OnNavigatedTo(new LessonNavigationParameter
        {
            CourseId = "course1",
            ModuleId = "module1",
            LessonId = "lesson1"
        });

        await Task.Delay(100);

        // Act
        await viewModel.MarkCompleteCommand.Execute();

        // Assert
        _mockNavigationService.Verify(n => n.GoBack(), Times.Once);
    }

    [Fact]
    public async Task OnChallengeHintShown_IncrementsHintUsage()
    {
        // Arrange
        var lesson = TestDataGenerator.CreateLesson();
        var challenge = TestDataGenerator.CreateMultipleChoiceChallenge("challenge1");

        _mockCourseService
            .Setup(s => s.GetLessonAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(lesson);

        var mockChallengeVM = new Mock<ChallengeViewModelBase>(challenge);
        _mockChallengeFactory
            .Setup(f => f.CreateViewModel(It.IsAny<Challenge>()))
            .Returns(mockChallengeVM.Object);

        var viewModel = CreateViewModel();
        viewModel.OnNavigatedTo(new LessonNavigationParameter
        {
            CourseId = "course1",
            ModuleId = "module1",
            LessonId = "lesson1"
        });

        await Task.Delay(100);

        // Act - Simulate hint shown event
        mockChallengeVM.Raise(vm => vm.HintShown += null, EventArgs.Empty);

        await Task.Delay(50);

        // Assert
        _mockProgressService.Verify(
            p => p.IncrementHintUsageAsync(
                "course1",
                "module1",
                "lesson1",
                "challenge1"),
            Times.Once);
    }

    [Fact]
    public void GoBackCommand_NavigatesBack()
    {
        // Arrange
        var viewModel = CreateViewModel();

        // Act
        viewModel.GoBackCommand.Execute();

        // Assert
        _mockNavigationService.Verify(n => n.GoBack(), Times.Once);
    }

    [Fact]
    public void Breadcrumb_FormatsCorrectly()
    {
        // Arrange
        var viewModel = CreateViewModel();

        // Act
        viewModel.OnNavigatedTo(new LessonNavigationParameter
        {
            CourseId = "csharp-basics",
            ModuleId = "variables",
            LessonId = "intro-to-variables"
        });

        // Assert
        viewModel.Breadcrumb.Should().Be("csharp-basics / variables / intro-to-variables");
    }

    private LessonPageViewModel CreateViewModel()
    {
        return new LessonPageViewModel(
            _mockCourseService.Object,
            _mockNavigationService.Object,
            _mockProgressService.Object,
            _mockChallengeFactory.Object,
            _mockAchievementService.Object,
            _mockStreakService.Object,
            _mockErrorHandler.Object);
    }
}
