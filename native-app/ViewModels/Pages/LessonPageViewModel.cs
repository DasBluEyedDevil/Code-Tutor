using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using CodeTutor.Native.Models;
using CodeTutor.Native.Services;
using CodeTutor.Native.ViewModels.Challenges;

namespace CodeTutor.Native.ViewModels.Pages;

/// <summary>
/// View model for the lesson page (main learning interface)
/// </summary>
public class LessonPageViewModel : ViewModelBase, INavigableViewModel
{
    private readonly ICourseService _courseService;
    private readonly INavigationService _navigationService;
    private readonly IProgressService _progressService;
    private readonly IChallengeFactory _challengeFactory;
    private readonly IAchievementService _achievementService;
    private readonly IStreakService _streakService;
    private readonly IErrorHandlerService _errorHandler;

    private string _courseId = string.Empty;
    private string _moduleId = string.Empty;
    private string _lessonId = string.Empty;

    private Lesson? _lesson;
    private bool _isLoading;
    private string _errorMessage = string.Empty;
    private string _lessonContent = string.Empty;
    private DateTime _lessonStartTime;
    private bool _isLessonCompleted;

    private LessonReference? _nextLesson;
    private LessonReference? _previousLesson;

    public LessonPageViewModel(
        ICourseService courseService,
        INavigationService navigationService,
        IProgressService progressService,
        IChallengeFactory challengeFactory,
        IAchievementService achievementService,
        IStreakService streakService,
        IErrorHandlerService errorHandler)
    {
        _courseService = courseService;
        _navigationService = navigationService;
        _progressService = progressService;
        _challengeFactory = challengeFactory;
        _achievementService = achievementService;
        _streakService = streakService;
        _errorHandler = errorHandler;

        // Commands
        GoBackCommand = ReactiveCommand.Create(GoBack);
        RetryLoadCommand = ReactiveCommand.CreateFromTask(LoadLessonAsync);
        MarkCompleteCommand = ReactiveCommand.CreateFromTask(MarkLessonCompleteAsync);
        NextLessonCommand = ReactiveCommand.Create(NavigateToNextLesson);
        PreviousLessonCommand = ReactiveCommand.Create(NavigateToPreviousLesson);
        GoToCourseCommand = ReactiveCommand.Create(GoBack);
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

    public bool IsLessonCompleted
    {
        get => _isLessonCompleted;
        set => this.RaiseAndSetIfChanged(ref _isLessonCompleted, value);
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }

    public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

    public string Breadcrumb => $"{_courseId} / {_moduleId} / {_lessonId}";

    public ObservableCollection<ChallengeViewModelBase> Challenges { get; } = new();

    public bool HasChallenges => Challenges.Count > 0;

    public LessonReference? NextLesson
    {
        get => _nextLesson;
        set 
        {
            this.RaiseAndSetIfChanged(ref _nextLesson, value);
            this.RaisePropertyChanged(nameof(HasNextLesson));
        }
    }

    public LessonReference? PreviousLesson
    {
        get => _previousLesson;
        set 
        {
            this.RaiseAndSetIfChanged(ref _previousLesson, value);
            this.RaisePropertyChanged(nameof(HasPreviousLesson));
        }
    }

    public bool HasNextLesson => NextLesson != null;
    public bool HasPreviousLesson => PreviousLesson != null;

    public ReactiveCommand<Unit, Unit> GoBackCommand { get; }
    public ReactiveCommand<Unit, Unit> RetryLoadCommand { get; }
    public ReactiveCommand<Unit, Unit> MarkCompleteCommand { get; }
    public ReactiveCommand<Unit, Unit> NextLessonCommand { get; }
    public ReactiveCommand<Unit, Unit> PreviousLessonCommand { get; }
    public ReactiveCommand<Unit, Unit> GoToCourseCommand { get; }

    public void OnNavigatedTo(object parameter)
    {
        if (parameter is LessonNavigationParameter navParam)
        {
            _courseId = navParam.CourseId;
            _moduleId = navParam.ModuleId;
            _lessonId = navParam.LessonId;

            // Reset state
            IsLessonCompleted = false;
            Lesson = null;
            LessonContent = string.Empty;
            Challenges.Clear();

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
        IsLessonCompleted = false;

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
            _lessonStartTime = DateTime.UtcNow;

            // Load navigation info
            NextLesson = await _courseService.GetNextLessonAsync(_courseId, _moduleId, _lessonId);
            PreviousLesson = await _courseService.GetPreviousLessonAsync(_courseId, _moduleId, _lessonId);

            // Load challenges
            Challenges.Clear();
            foreach (var challenge in lesson.Exercises)
            {
                try
                {
                    var viewModel = _challengeFactory.CreateViewModel(challenge);

                    // Wire up hint event to track hints used
                    viewModel.HintShown += OnChallengeHintShown;

                    // Wire up result property changed to track challenge completion
                    viewModel.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(ChallengeViewModelBase.Result) && viewModel.Result != null)
                        {
                            _ = OnChallengeCompletedAsync(viewModel);
                        }
                    };

                    Challenges.Add(viewModel);
                }
                catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
                {
                    _errorHandler.LogError(ex, "Challenge ViewModel creation");
                }
            }

            this.RaisePropertyChanged(nameof(HasChallenges));

            // Record lesson activity
            await _streakService.RecordActivityAsync(lessonCompleted: false, challengeCompleted: false, minutesSpent: 0);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            await _errorHandler.HandleErrorAsync(ex, "Lesson loading", showToUser: false);
            ErrorMessage = $"Failed to load lesson: {_errorHandler.GetUserFriendlyMessage(ex)}";
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
            // Calculate time spent on lesson
            var timeSpent = (int)(DateTime.UtcNow - _lessonStartTime).TotalMinutes;

            // Calculate total hints used across all challenges
            var totalHints = Challenges.Sum(c => c.HintsUsed);

            // Save lesson progress
            await _progressService.SaveProgressAsync(_courseId, _moduleId, _lessonId, 100, true, totalHints);

            // Record streak activity
            await _streakService.RecordActivityAsync(
                lessonCompleted: true,
                challengeCompleted: false,
                minutesSpent: timeSpent);

            // Check for achievements
            await _achievementService.CheckAchievementsAsync();

            // Show completion overlay instead of going back immediately
            IsLessonCompleted = true;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            await _errorHandler.HandleErrorAsync(ex, "Lesson completion", showToUser: false);
            ErrorMessage = $"Failed to save progress: {_errorHandler.GetUserFriendlyMessage(ex)}";
        }
    }

    private void NavigateToNextLesson()
    {
        if (NextLesson != null)
        {
            _navigationService.NavigateTo<LessonPageViewModel>(new LessonNavigationParameter
            {
                CourseId = NextLesson.CourseId,
                ModuleId = NextLesson.ModuleId,
                LessonId = NextLesson.LessonId
            });
        }
    }

    private void NavigateToPreviousLesson()
    {
        if (PreviousLesson != null)
        {
            _navigationService.NavigateTo<LessonPageViewModel>(new LessonNavigationParameter
            {
                CourseId = PreviousLesson.CourseId,
                ModuleId = PreviousLesson.ModuleId,
                LessonId = PreviousLesson.LessonId
            });
        }
    }

    private void OnChallengeHintShown(object? sender, EventArgs e)
    {
        if (sender is not ChallengeViewModelBase challenge)
            return;

        // Track hint usage in progress
        _ = _progressService.IncrementHintUsageAsync(
            _courseId,
            _moduleId,
            _lessonId,
            challenge.Challenge.Id);
    }

    private async Task OnChallengeCompletedAsync(ChallengeViewModelBase challengeViewModel)
    {
        try
        {
            var result = challengeViewModel.Result;
            if (result == null) return;

            // Save challenge progress
            await _progressService.SaveChallengeProgressAsync(
                _courseId,
                _moduleId,
                _lessonId,
                challengeViewModel.Challenge.Id,
                result.Score,
                result.IsCorrect,
                challengeViewModel.HintsUsed);

            // Record streak activity for challenge completion
            if (result.IsCorrect)
            {
                await _streakService.RecordActivityAsync(
                    lessonCompleted: false,
                    challengeCompleted: true,
                    minutesSpent: 0);

                // Check for achievements after challenge completion
                await _achievementService.CheckAchievementsAsync();
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            _errorHandler.LogError(ex, "Challenge completion tracking");
        }
    }

    private void GoBack()
    {
        _navigationService.GoBack();
    }
}
