using System;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using CodeTutor.Native.Models;
using CodeTutor.Native.Services;

namespace CodeTutor.Native.ViewModels.Pages;

/// <summary>
/// View model for the lesson page (main learning interface)
/// </summary>
public class LessonPageViewModel : ViewModelBase, INavigableViewModel
{
    private readonly ICourseService _courseService;
    private readonly INavigationService _navigationService;
    private readonly IProgressService _progressService;

    private string _courseId = string.Empty;
    private string _moduleId = string.Empty;
    private string _lessonId = string.Empty;

    private Lesson? _lesson;
    private bool _isLoading;
    private string _errorMessage = string.Empty;
    private string _lessonContent = string.Empty;

    public LessonPageViewModel(
        ICourseService courseService,
        INavigationService navigationService,
        IProgressService progressService)
    {
        _courseService = courseService;
        _navigationService = navigationService;
        _progressService = progressService;

        // Commands
        GoBackCommand = ReactiveCommand.Create(GoBack);
        RetryLoadCommand = ReactiveCommand.CreateFromTask(LoadLessonAsync);
        MarkCompleteCommand = ReactiveCommand.CreateFromTask(MarkLessonCompleteAsync);
    }

    public Lesson? Lesson
    {
        get => _lesson;
        set => this.RaiseAndSetIfChanged(ref _lesson, value);
    }

    public string LessonContent
    {
        get => _lessonContent;
        set => this.RaiseAndSetIfChanged(ref _lessonContent, value);
    }

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

    public string Breadcrumb => $"{_courseId} / {_moduleId} / {_lessonId}";

    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }
    public ReactiveCommand<Unit, Unit> RetryLoadCommand { get; }
    public ReactiveCommand<Unit, Unit> MarkCompleteCommand { get; }

    public void OnNavigatedTo(object parameter)
    {
        if (parameter is LessonNavigationParameter navParam)
        {
            _courseId = navParam.CourseId;
            _moduleId = navParam.ModuleId;
            _lessonId = navParam.LessonId;

            this.RaisePropertyChanged(nameof(Breadcrumb));
            // Fire-and-forget is intentional - errors will be shown in UI via ErrorMessage
            _ = LoadLessonAsync();
        }
    }

    public void OnNavigatedBack()
    {
        // Refresh lesson when navigating back
        // Fire-and-forget is intentional - errors will be shown in UI via ErrorMessage
        _ = LoadLessonAsync();
    }

    private async Task LoadLessonAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var lesson = await _courseService.GetLessonAsync(_courseId, _moduleId, _lessonId);
            if (lesson == null)
            {
                ErrorMessage = "Lesson not found.";
                return;
            }

            Lesson = lesson;
            LessonContent = lesson.Content.Body;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            ErrorMessage = $"Failed to load lesson: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task MarkLessonCompleteAsync()
    {
        try
        {
            await _progressService.SaveProgressAsync(_courseId, _moduleId, _lessonId, 100, true);
            GoBack();
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            ErrorMessage = $"Failed to save progress: {ex.Message}";
        }
    }

    private void GoBack()
    {
        _navigationService.GoBack();
    }
}
