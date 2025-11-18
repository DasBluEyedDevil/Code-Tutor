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
/// View model for the landing page (course selection)
/// </summary>
public class LandingPageViewModel : ViewModelBase
{
    private readonly ICourseService _courseService;
    private readonly INavigationService _navigationService;
    private bool _isLoading;
    private string _errorMessage = string.Empty;

    public LandingPageViewModel(
        ICourseService courseService,
        INavigationService navigationService)
    {
        _courseService = courseService;
        _navigationService = navigationService;

        // Commands
        SelectCourseCommand = ReactiveCommand.Create<CourseInfo>(SelectCourse);
        RetryLoadCommand = ReactiveCommand.CreateFromTask(LoadCoursesAsync);

        // Load courses on initialization
        LoadCoursesAsync();
    }

    public ObservableCollection<CourseInfo> Courses { get; } = new();

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

    public ReactiveCommand<CourseInfo, Unit> SelectCourseCommand { get; }
    public ReactiveCommand<Unit, Unit> RetryLoadCommand { get; }

    private async Task LoadCoursesAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var courses = await _courseService.GetCoursesAsync();

            Courses.Clear();
            foreach (var course in courses.OrderBy(c => c.Language))
            {
                Courses.Add(course);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load courses: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void SelectCourse(CourseInfo course)
    {
        _navigationService.NavigateTo<CoursePageViewModel>(course.Id);
    }
}
