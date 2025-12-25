using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using CodeTutor.Native.Models;
using CodeTutor.Native.Services;

namespace CodeTutor.Native.ViewModels.Pages;

/// <summary>
/// View model for the course page (module and lesson browser)
/// </summary>
public class CoursePageViewModel : ViewModelBase, INavigableViewModel
{
    private readonly ICourseService _courseService;
    private readonly INavigationService _navigationService;
    private readonly IProgressService _progressService;
    private readonly IErrorHandlerService _errorHandler;

    private string _courseId = string.Empty;
    private Course? _course;
    private bool _isLoading;
    private string _errorMessage = string.Empty;

    public CoursePageViewModel(
        ICourseService courseService,
        INavigationService navigationService,
        IProgressService progressService,
        IErrorHandlerService errorHandler)
    {
        _courseService = courseService;
        _navigationService = navigationService;
        _progressService = progressService;
        _errorHandler = errorHandler;

        // Commands
        SelectLessonCommand = ReactiveCommand.Create<LessonInfo>(SelectLesson);
        GoBackCommand = ReactiveCommand.Create(GoBack);
        RetryLoadCommand = ReactiveCommand.CreateFromTask(LoadCourseAsync);
    }

    public Course? Course
    {
        get => _course;
        set => this.RaiseAndSetIfChanged(ref _course, value);
    }

    public ObservableCollection<ModuleViewModel> Modules { get; } = new();

    public bool IsLoading
    {
        get => _isLoading;
        set => this.RaiseAndSetIfChanged(ref _isLoading, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

    public ReactiveCommand<LessonInfo, Unit> SelectLessonCommand { get; }
    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }
    public ReactiveCommand<Unit, Unit> RetryLoadCommand { get; }

    public void OnNavigatedTo(object parameter)
    {
        if (parameter is string courseId)
        {
            _courseId = courseId;
            // Fire-and-forget is intentional - errors will be shown in UI via ErrorMessage
            _ = LoadCourseAsync();
        }
    }

    public void OnNavigatedBack()
    {
        // Refresh course progress when navigating back
        // Fire-and-forget is intentional - errors will be shown in UI via ErrorMessage
        _ = LoadCourseAsync();
    }

    private async Task LoadCourseAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var course = await _courseService.GetCourseAsync(_courseId);
            if (course == null)
            {
                ErrorMessage = $"Course '{_courseId}' not found.";
                return;
            }

            Course = course;

            // Build module view models with progress
            Modules.Clear();
            foreach (var module in course.Modules)
            {
                var moduleVm = new ModuleViewModel(module, _courseId, _progressService);
                await moduleVm.LoadProgressAsync();
                Modules.Add(moduleVm);
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            await _errorHandler.HandleErrorAsync(ex, "Course loading", showToUser: false);
            ErrorMessage = $"Failed to load course: {_errorHandler.GetUserFriendlyMessage(ex)}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void SelectLesson(LessonInfo lessonInfo)
    {
        _navigationService.NavigateTo<LessonPageViewModel>(new LessonNavigationParameter
        {
            CourseId = _courseId,
            ModuleId = lessonInfo.ModuleId,
            LessonId = lessonInfo.LessonId
        });
    }

    private void GoBack()
    {
        _navigationService.NavigateTo<LandingPageViewModel>();
    }
}

/// <summary>
/// View model for a module with its lessons
/// </summary>
public class ModuleViewModel : ViewModelBase
{
    private readonly Module _module;
    private readonly string _courseId;
    private readonly IProgressService _progressService;
    private bool _isExpanded;
    private int _completedLessons;
    private int _totalLessons;

    public ModuleViewModel(Module module, string courseId, IProgressService progressService)
    {
        _module = module;
        _courseId = courseId;
        _progressService = progressService;

        ToggleExpandCommand = ReactiveCommand.Create(() => { IsExpanded = !IsExpanded; });

        // Build lesson info list
        foreach (var lesson in module.Lessons)
        {
            Lessons.Add(new LessonInfo
            {
                ModuleId = module.Id,
                LessonId = lesson.Id,
                Title = lesson.Title,
                IsCompleted = false // Will be updated by LoadProgressAsync
            });
        }

        _totalLessons = Lessons.Count;
    }

    public string Id => _module.Id;
    public string Title => _module.Title;
    public string Description => _module.Description;

    public ObservableCollection<LessonInfo> Lessons { get; } = new();

    public bool IsExpanded
    {
        get => _isExpanded;
        set => this.RaiseAndSetIfChanged(ref _isExpanded, value);
    }

    public int CompletedLessons
    {
        get => _completedLessons;
        set => this.RaiseAndSetIfChanged(ref _completedLessons, value);
    }

    public int TotalLessons
    {
        get => _totalLessons;
        set => this.RaiseAndSetIfChanged(ref _totalLessons, value);
    }

    public int ProgressPercentage => TotalLessons > 0 ? (CompletedLessons * 100) / TotalLessons : 0;

    public ReactiveCommand<Unit, Unit> ToggleExpandCommand { get; }

    public async Task LoadProgressAsync()
    {
        try
        {
            var progress = await _progressService.GetModuleProgressAsync(_courseId, Id);

            foreach (var lesson in Lessons)
            {
                var lessonProgress = progress.FirstOrDefault(p => p.LessonId == lesson.LessonId);
                lesson.IsCompleted = lessonProgress?.Completed ?? false;
            }

            CompletedLessons = Lessons.Count(l => l.IsCompleted);
            this.RaisePropertyChanged(nameof(ProgressPercentage));
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            // Ignore progress load errors - progress is optional
        }
    }
}

/// <summary>
/// Information about a lesson for display
/// </summary>
public class LessonInfo : ViewModelBase
{
    private bool _isCompleted;

    public string ModuleId { get; set; } = string.Empty;
    public string LessonId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;

    public bool IsCompleted
    {
        get => _isCompleted;
        set => this.RaiseAndSetIfChanged(ref _isCompleted, value);
    }
}

/// <summary>
/// Parameter for lesson navigation
/// </summary>
public class LessonNavigationParameter
{
    public string CourseId { get; set; } = string.Empty;
    public string ModuleId { get; set; } = string.Empty;
    public string LessonId { get; set; } = string.Empty;
}
