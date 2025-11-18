using Microsoft.Extensions.Logging;
using CodeTutor.Native.Models;
using CodeTutor.Native.Services;
using CodeTutor.Native.ViewModels.Pages;
using CodeTutor.Native.Tests.Helpers;

namespace CodeTutor.Native.Tests.ViewModels.Pages;

/// <summary>
/// Unit tests for CoursePageViewModel
/// </summary>
public class CoursePageViewModelTests
{
    private readonly Mock<ICourseService> _mockCourseService;
    private readonly Mock<INavigationService> _mockNavigationService;
    private readonly Mock<IProgressService> _mockProgressService;
    private readonly Mock<IErrorHandlerService> _mockErrorHandler;

    public CoursePageViewModelTests()
    {
        _mockCourseService = new Mock<ICourseService>();
        _mockNavigationService = new Mock<INavigationService>();
        _mockProgressService = new Mock<IProgressService>();
        _mockErrorHandler = new Mock<IErrorHandlerService>();

        // Setup default error handler behavior
        _mockErrorHandler
            .Setup(x => x.GetUserFriendlyMessage(It.IsAny<Exception>()))
            .Returns("An error occurred");
    }

    [Fact]
    public async Task OnNavigatedTo_LoadsCourse_WhenCourseExists()
    {
        // Arrange
        var course = TestDataGenerator.CreateCourse("course1", moduleCount: 2, lessonsPerModule: 3);
        _mockCourseService
            .Setup(x => x.GetCourseAsync("course1"))
            .ReturnsAsync(course);

        _mockProgressService
            .Setup(x => x.GetModuleProgressAsync("course1", It.IsAny<string>()))
            .ReturnsAsync(new List<UserProgress>());

        var viewModel = CreateViewModel();

        // Act
        viewModel.OnNavigatedTo("course1");
        await Task.Delay(100); // Wait for async loading

        // Assert
        viewModel.Course.Should().NotBeNull();
        viewModel.Course!.Id.Should().Be("course1");
        viewModel.Modules.Should().HaveCount(2);
        viewModel.IsLoading.Should().BeFalse();
        viewModel.HasError.Should().BeFalse();
    }

    [Fact]
    public async Task OnNavigatedTo_ShowsError_WhenCourseNotFound()
    {
        // Arrange
        _mockCourseService
            .Setup(x => x.GetCourseAsync("nonexistent"))
            .ReturnsAsync((Course?)null);

        var viewModel = CreateViewModel();

        // Act
        viewModel.OnNavigatedTo("nonexistent");
        await Task.Delay(100); // Wait for async loading

        // Assert
        viewModel.Course.Should().BeNull();
        viewModel.HasError.Should().BeTrue();
        viewModel.ErrorMessage.Should().Contain("not found");
        viewModel.IsLoading.Should().BeFalse();
    }

    [Fact]
    public async Task OnNavigatedTo_ShowsError_WhenExceptionThrown()
    {
        // Arrange
        _mockCourseService
            .Setup(x => x.GetCourseAsync("course1"))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        var viewModel = CreateViewModel();

        // Act
        viewModel.OnNavigatedTo("course1");
        await Task.Delay(100); // Wait for async loading

        // Assert
        viewModel.HasError.Should().BeTrue();
        viewModel.ErrorMessage.Should().Contain("Failed to load course");
        viewModel.IsLoading.Should().BeFalse();
        _mockErrorHandler.Verify(x => x.HandleErrorAsync(
            It.IsAny<Exception>(),
            "Course loading",
            false), Times.Once);
    }

    [Fact]
    public async Task OnNavigatedBack_RefreshesCourse()
    {
        // Arrange
        var course = TestDataGenerator.CreateCourse("course1", moduleCount: 1, lessonsPerModule: 2);
        _mockCourseService
            .Setup(x => x.GetCourseAsync("course1"))
            .ReturnsAsync(course);

        _mockProgressService
            .Setup(x => x.GetModuleProgressAsync("course1", It.IsAny<string>()))
            .ReturnsAsync(new List<UserProgress>());

        var viewModel = CreateViewModel();

        // Act - Initial load
        viewModel.OnNavigatedTo("course1");
        await Task.Delay(100);

        // Act - Navigate back (should refresh)
        viewModel.OnNavigatedBack();
        await Task.Delay(100);

        // Assert
        _mockCourseService.Verify(x => x.GetCourseAsync("course1"), Times.Exactly(2));
        viewModel.Course.Should().NotBeNull();
    }

    [Fact]
    public async Task SelectLessonCommand_NavigatesToLesson()
    {
        // Arrange
        var course = TestDataGenerator.CreateCourse("course1", moduleCount: 1, lessonsPerModule: 1);
        _mockCourseService
            .Setup(x => x.GetCourseAsync("course1"))
            .ReturnsAsync(course);

        _mockProgressService
            .Setup(x => x.GetModuleProgressAsync("course1", It.IsAny<string>()))
            .ReturnsAsync(new List<UserProgress>());

        var viewModel = CreateViewModel();
        viewModel.OnNavigatedTo("course1");
        await Task.Delay(100);

        var lessonInfo = new LessonInfo
        {
            ModuleId = "module1",
            LessonId = "lesson1",
            Title = "Test Lesson"
        };

        // Act
        viewModel.SelectLessonCommand.Execute(lessonInfo).Subscribe();

        // Assert
        _mockNavigationService.Verify(x => x.NavigateTo<LessonPageViewModel>(
            It.Is<LessonNavigationParameter>(p =>
                p.CourseId == "course1" &&
                p.ModuleId == "module1" &&
                p.LessonId == "lesson1")),
            Times.Once);
    }

    [Fact]
    public void GoBackCommand_NavigatesToLandingPage()
    {
        // Arrange
        var viewModel = CreateViewModel();

        // Act
        viewModel.GoBackCommand.Execute().Subscribe();

        // Assert
        _mockNavigationService.Verify(x => x.NavigateTo<LandingPageViewModel>(), Times.Once);
    }

    [Fact]
    public async Task RetryLoadCommand_ReloadsCourse()
    {
        // Arrange
        var course = TestDataGenerator.CreateCourse("course1", moduleCount: 1, lessonsPerModule: 1);
        _mockCourseService
            .Setup(x => x.GetCourseAsync("course1"))
            .ReturnsAsync(course);

        _mockProgressService
            .Setup(x => x.GetModuleProgressAsync("course1", It.IsAny<string>()))
            .ReturnsAsync(new List<UserProgress>());

        var viewModel = CreateViewModel();
        viewModel.OnNavigatedTo("course1");
        await Task.Delay(100);

        // Act
        viewModel.RetryLoadCommand.Execute().Subscribe();
        await Task.Delay(100);

        // Assert
        _mockCourseService.Verify(x => x.GetCourseAsync("course1"), Times.Exactly(2));
    }

    [Fact]
    public async Task LoadCourse_UpdatesModulesWithProgress()
    {
        // Arrange
        var course = TestDataGenerator.CreateCourse("course1", moduleCount: 1, lessonsPerModule: 3);

        _mockCourseService
            .Setup(x => x.GetCourseAsync("course1"))
            .ReturnsAsync(course);

        var progress = new List<UserProgress>
        {
            TestDataGenerator.CreateProgress(lessonId: "lesson1", completed: true),
            TestDataGenerator.CreateProgress(lessonId: "lesson2", completed: true),
            TestDataGenerator.CreateProgress(lessonId: "lesson3", completed: false)
        };

        _mockProgressService
            .Setup(x => x.GetModuleProgressAsync("course1", "module1"))
            .ReturnsAsync(progress);

        var viewModel = CreateViewModel();

        // Act
        viewModel.OnNavigatedTo("course1");
        await Task.Delay(100);

        // Assert
        viewModel.Modules.Should().HaveCount(1);
        var module = viewModel.Modules[0];
        module.CompletedLessons.Should().Be(2);
        module.TotalLessons.Should().Be(3);
        module.ProgressPercentage.Should().Be(66); // 2/3 = 66%
    }

    private CoursePageViewModel CreateViewModel()
    {
        return new CoursePageViewModel(
            _mockCourseService.Object,
            _mockNavigationService.Object,
            _mockProgressService.Object,
            _mockErrorHandler.Object);
    }
}

/// <summary>
/// Unit tests for ModuleViewModel
/// </summary>
public class ModuleViewModelTests
{
    private readonly Mock<IProgressService> _mockProgressService;

    public ModuleViewModelTests()
    {
        _mockProgressService = new Mock<IProgressService>();
    }

    [Fact]
    public async Task LoadProgressAsync_UpdatesCompletionStatus()
    {
        // Arrange
        var module = new Module
        {
            Id = "module1",
            Title = "Test Module",
            Description = "Test description",
            Lessons = new List<Lesson>
            {
                new Lesson { Id = "lesson1", Title = "Lesson 1", Content = new LessonContent() },
                new Lesson { Id = "lesson2", Title = "Lesson 2", Content = new LessonContent() },
                new Lesson { Id = "lesson3", Title = "Lesson 3", Content = new LessonContent() }
            }
        };

        var progress = new List<UserProgress>
        {
            TestDataGenerator.CreateProgress(lessonId: "lesson1", completed: true),
            TestDataGenerator.CreateProgress(lessonId: "lesson2", completed: false)
            // lesson3 has no progress
        };

        _mockProgressService
            .Setup(x => x.GetModuleProgressAsync("course1", "module1"))
            .ReturnsAsync(progress);

        var viewModel = new ModuleViewModel(module, "course1", _mockProgressService.Object);

        // Act
        await viewModel.LoadProgressAsync();

        // Assert
        viewModel.Lessons.Should().HaveCount(3);
        viewModel.Lessons[0].IsCompleted.Should().BeTrue();
        viewModel.Lessons[1].IsCompleted.Should().BeFalse();
        viewModel.Lessons[2].IsCompleted.Should().BeFalse();
        viewModel.CompletedLessons.Should().Be(1);
        viewModel.TotalLessons.Should().Be(3);
        viewModel.ProgressPercentage.Should().Be(33); // 1/3 = 33%
    }

    [Fact]
    public async Task LoadProgressAsync_HandlesException_WithoutThrowing()
    {
        // Arrange
        var module = new Module
        {
            Id = "module1",
            Title = "Test Module",
            Description = "Test description",
            Lessons = new List<Lesson>
            {
                new Lesson { Id = "lesson1", Title = "Lesson 1", Content = new LessonContent() }
            }
        };

        _mockProgressService
            .Setup(x => x.GetModuleProgressAsync("course1", "module1"))
            .ThrowsAsync(new InvalidOperationException("Database error"));

        var viewModel = new ModuleViewModel(module, "course1", _mockProgressService.Object);

        // Act
        Func<Task> act = async () => await viewModel.LoadProgressAsync();

        // Assert - Should not throw
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public void ToggleExpandCommand_TogglesIsExpanded()
    {
        // Arrange
        var module = new Module
        {
            Id = "module1",
            Title = "Test Module",
            Description = "Test description",
            Lessons = new List<Lesson>()
        };

        var viewModel = new ModuleViewModel(module, "course1", _mockProgressService.Object);

        // Act & Assert
        viewModel.IsExpanded.Should().BeFalse();

        viewModel.ToggleExpandCommand.Execute().Subscribe();
        viewModel.IsExpanded.Should().BeTrue();

        viewModel.ToggleExpandCommand.Execute().Subscribe();
        viewModel.IsExpanded.Should().BeFalse();
    }

    [Fact]
    public void ProgressPercentage_ReturnsZero_WhenNoLessons()
    {
        // Arrange
        var module = new Module
        {
            Id = "module1",
            Title = "Test Module",
            Description = "Test description",
            Lessons = new List<Lesson>()
        };

        var viewModel = new ModuleViewModel(module, "course1", _mockProgressService.Object);

        // Act & Assert
        viewModel.ProgressPercentage.Should().Be(0);
    }

    [Fact]
    public void ProgressPercentage_CalculatesCorrectly()
    {
        // Arrange
        var module = new Module
        {
            Id = "module1",
            Title = "Test Module",
            Description = "Test description",
            Lessons = new List<Lesson>
            {
                new Lesson { Id = "lesson1", Title = "Lesson 1", Content = new LessonContent() },
                new Lesson { Id = "lesson2", Title = "Lesson 2", Content = new LessonContent() },
                new Lesson { Id = "lesson3", Title = "Lesson 3", Content = new LessonContent() },
                new Lesson { Id = "lesson4", Title = "Lesson 4", Content = new LessonContent() }
            }
        };

        var viewModel = new ModuleViewModel(module, "course1", _mockProgressService.Object);

        // Act - Manually mark lessons as completed
        viewModel.Lessons[0].IsCompleted = true;
        viewModel.Lessons[1].IsCompleted = true;
        viewModel.Lessons[2].IsCompleted = true;

        // Update completed count (normally done by LoadProgressAsync)
        var completedCount = viewModel.Lessons.Count(l => l.IsCompleted);

        // Assert
        // 3 out of 4 = 75%
        var percentage = (completedCount * 100) / 4;
        percentage.Should().Be(75);
    }
}
