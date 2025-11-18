using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ReactiveUI;
using CodeTutor.Native.Models;
using CodeTutor.Native.Services;

namespace CodeTutor.Native.ViewModels;

/// <summary>
/// Main window view model - NO HTTP, NO IPC, just C# methods
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private readonly CourseService _courseService;
    private readonly CodeExecutor _codeExecutor;

    private string _selectedLanguage = "python";
    private string _code = "print('Hello, World!')";
    private string _output = "";
    private bool _isExecuting;

    public MainWindowViewModel()
    {
        _courseService = new CourseService();
        _codeExecutor = new CodeExecutor();

        // Load courses on startup
        LoadCoursesAsync();
    }

    public ObservableCollection<CourseInfo> Courses { get; } = new();

    public string SelectedLanguage
    {
        get => _selectedLanguage;
        set => this.RaiseAndSetIfChanged(ref _selectedLanguage, value);
    }

    public string Code
    {
        get => _code;
        set => this.RaiseAndSetIfChanged(ref _code, value);
    }

    public string Output
    {
        get => _output;
        set => this.RaiseAndSetIfChanged(ref _output, value);
    }

    public bool IsExecuting
    {
        get => _isExecuting;
        set => this.RaiseAndSetIfChanged(ref _isExecuting, value);
    }

    /// <summary>
    /// Load all courses - direct file I/O, no HTTP
    /// </summary>
    private async void LoadCoursesAsync()
    {
        var courses = await _courseService.GetCoursesAsync();

        Courses.Clear();
        foreach (var course in courses)
        {
            Courses.Add(course);
        }
    }

    /// <summary>
    /// Execute code - direct process spawning, no HTTP, no IPC
    /// </summary>
    public async Task ExecuteCodeAsync()
    {
        IsExecuting = true;
        Output = "Executing...";

        try
        {
            var result = await _codeExecutor.ExecuteAsync(SelectedLanguage, Code);

            if (result.Success)
            {
                Output = $"✓ Success ({result.ExecutionTimeMs}ms)\n\n{result.Output}";
            }
            else
            {
                Output = $"✗ Error\n\n{result.Error}";
            }
        }
        catch (System.Exception ex)
        {
            Output = $"✗ Failed to execute: {ex.Message}";
        }
        finally
        {
            IsExecuting = false;
        }
    }
}

/// <summary>
/// Base class for view models
/// </summary>
public class ViewModelBase : ReactiveObject
{
}
