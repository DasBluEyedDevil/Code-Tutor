# Code Tutor Native Application Architecture

## Overview

This document defines the complete architecture for the Code Tutor native desktop application built with C# and Avalonia UI.

---

## Architecture Principles

### 1. **True Native Desktop**
- No web technologies (no HTML/CSS/JavaScript)
- Direct OS API access
- Native rendering (no browser engine)
- Compiled binary execution

### 2. **MVVM Pattern**
- Clear separation of concerns
- Testable business logic
- Reactive data binding
- Command-based interactions

### 3. **Service-Oriented**
- Dependency injection
- Single responsibility
- Interface-based contracts
- Mockable for testing

### 4. **Performance First**
- Lazy loading where appropriate
- Async/await throughout
- Minimal memory allocation
- Fast startup (<1 second)

---

## Project Structure

```
CodeTutor.Native/
├── App.axaml                          # Application definition
├── App.axaml.cs                       # Application code-behind
├── Program.cs                         # Entry point
├── CodeTutor.Native.csproj            # Project file
│
├── Assets/                            # Static resources
│   ├── Icons/                         # Application icons
│   ├── Images/                        # UI images
│   └── Fonts/                         # Custom fonts
│
├── Models/                            # Data models (POCOs)
│   ├── Course.cs                      # Course structure
│   ├── Lesson.cs                      # Lesson data
│   ├── Challenge.cs                   # Challenge definitions
│   ├── ExecutionResult.cs             # Code execution results
│   ├── Achievement.cs                 # Achievement data
│   ├── UserProgress.cs                # Progress tracking
│   └── Settings.cs                    # User preferences
│
├── ViewModels/                        # MVVM ViewModels
│   ├── Base/
│   │   ├── ViewModelBase.cs           # Base class for all VMs
│   │   └── NavigationViewModel.cs    # Navigation support
│   │
│   ├── Pages/
│   │   ├── LandingPageViewModel.cs    # Course selection
│   │   ├── CoursePageViewModel.cs     # Module/lesson browser
│   │   └── LessonPageViewModel.cs     # Main learning interface
│   │
│   ├── Components/
│   │   ├── CodeEditorViewModel.cs     # Code editor logic
│   │   ├── ChallengeViewModel.cs      # Challenge base
│   │   ├── HintsPanelViewModel.cs     # Hints system
│   │   └── SettingsViewModel.cs       # Settings panel
│   │
│   └── MainWindowViewModel.cs         # Main window coordination
│
├── Views/                             # XAML Views
│   ├── Pages/
│   │   ├── LandingPage.axaml
│   │   ├── CoursePage.axaml
│   │   └── LessonPage.axaml
│   │
│   ├── Components/
│   │   ├── CodeEditorView.axaml
│   │   ├── ChallengeView.axaml
│   │   ├── HintsPanel.axaml
│   │   └── SettingsPanel.axaml
│   │
│   ├── Challenges/                    # Challenge type views
│   │   ├── MultipleChoiceView.axaml
│   │   ├── TrueFalseView.axaml
│   │   ├── CodeOutputView.axaml
│   │   ├── FreeCodingView.axaml
│   │   ├── CodeCompletionView.axaml
│   │   └── ConceptualView.axaml
│   │
│   └── MainWindow.axaml               # Main window
│
├── Services/                          # Business logic services
│   ├── Interfaces/                    # Service contracts
│   │   ├── ICourseService.cs
│   │   ├── ICodeExecutor.cs
│   │   ├── IChallengeValidator.cs
│   │   ├── IProgressService.cs
│   │   ├── IAchievementService.cs
│   │   ├── IThemeService.cs
│   │   └── ISettingsService.cs
│   │
│   ├── CourseService.cs               # Course loading/caching
│   ├── CodeExecutor.cs                # Code execution engine
│   ├── ChallengeValidator.cs          # Challenge validation
│   ├── ProgressService.cs             # Progress tracking
│   ├── AchievementService.cs          # Achievement unlocking
│   ├── ThemeService.cs                # Theme management
│   ├── SettingsService.cs             # Preferences persistence
│   └── NavigationService.cs           # Page navigation
│
├── Utilities/                         # Helper classes
│   ├── ProcessRunner.cs               # External process management
│   ├── FileHelper.cs                  # File I/O utilities
│   ├── JsonHelper.cs                  # JSON serialization
│   ├── MarkdownRenderer.cs            # Markdown processing
│   └── SyntaxHighlighter.cs           # Code highlighting
│
├── Data/                              # Data access layer
│   ├── Database/
│   │   ├── CodeTutorDbContext.cs      # SQLite context
│   │   ├── Migrations/                # Database migrations
│   │   └── Repositories/              # Data repositories
│   │       ├── ProgressRepository.cs
│   │       └── AchievementRepository.cs
│   │
│   └── Settings/
│       └── SettingsManager.cs         # JSON settings file
│
├── Converters/                        # Value converters for bindings
│   ├── BoolToVisibilityConverter.cs
│   ├── EnumToStringConverter.cs
│   └── MarkdownToFlowDocumentConverter.cs
│
├── Behaviors/                         # Attached behaviors
│   ├── AutoSaveBehavior.cs            # Auto-save for text editors
│   └── KeyboardShortcutBehavior.cs    # Global shortcuts
│
├── Controls/                          # Custom controls
│   ├── CodeEditor.cs                  # Custom AvaloniaEdit wrapper
│   ├── MarkdownViewer.cs              # Markdown display control
│   └── ProgressRing.cs                # Loading indicator
│
└── Themes/                            # Theme resources
    ├── Dark.axaml                     # Dark theme
    ├── Light.axaml                    # Light theme
    └── Common.axaml                   # Shared resources
```

---

## Layer Responsibilities

### 1. Models (Data Layer)
**Purpose:** Plain C# objects representing data structures

**Responsibilities:**
- Define data structure
- Data validation (attributes)
- JSON serialization mapping
- No business logic

**Example:**
```csharp
public class Course
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("modules")]
    public List<Module> Modules { get; set; } = new();

    [Required]
    public string Title { get; set; } = string.Empty;
}
```

---

### 2. ViewModels (Presentation Logic)
**Purpose:** Bridge between View and Services, implements presentation logic

**Responsibilities:**
- Expose data to View (via properties)
- Handle user commands (via ICommand)
- Coordinate service calls
- Manage UI state
- Data transformation for display
- Input validation

**Base Class:**
```csharp
public abstract class ViewModelBase : ReactiveObject
{
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }
}
```

**Example:**
```csharp
public class LessonPageViewModel : ViewModelBase
{
    private readonly ICourseService _courseService;
    private readonly ICodeExecutor _codeExecutor;

    public ReactiveCommand<Unit, Unit> RunCodeCommand { get; }

    public LessonPageViewModel(ICourseService courseService, ICodeExecutor codeExecutor)
    {
        _courseService = courseService;
        _codeExecutor = codeExecutor;

        RunCodeCommand = ReactiveCommand.CreateFromTask(ExecuteCodeAsync);
    }

    private async Task ExecuteCodeAsync()
    {
        IsBusy = true;
        try
        {
            var result = await _codeExecutor.ExecuteAsync(Language, Code);
            Output = result.Output;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
```

---

### 3. Views (UI Layer)
**Purpose:** XAML-based user interface

**Responsibilities:**
- Define UI structure
- Data binding to ViewModel
- Visual styling
- User interaction triggers
- Animations and transitions

**Example:**
```xml
<UserControl xmlns="https://github.com/avaloniaui">
    <Grid>
        <Button Content="Run Code"
                Command="{Binding RunCodeCommand}"
                IsEnabled="{Binding !IsBusy}" />

        <TextBlock Text="{Binding Output}" />
    </Grid>
</UserControl>
```

---

### 4. Services (Business Logic)
**Purpose:** Implement core application functionality

**Responsibilities:**
- Business logic execution
- External system interaction (file I/O, processes)
- Data processing and transformation
- Caching and optimization
- Error handling

**Interface Example:**
```csharp
public interface ICodeExecutor
{
    Task<ExecutionResult> ExecuteAsync(string language, string code);
    Task<ValidationResult> ValidateAsync(Challenge challenge, string userCode);
    bool IsRuntimeAvailable(string language);
}
```

**Implementation Pattern:**
```csharp
public class CodeExecutor : ICodeExecutor
{
    private readonly Dictionary<string, string> _runtimePaths = new();

    public async Task<ExecutionResult> ExecuteAsync(string language, string code)
    {
        // Validate runtime available
        if (!IsRuntimeAvailable(language))
        {
            return new ExecutionResult
            {
                Success = false,
                Error = $"Runtime for {language} not found"
            };
        }

        // Execute code in isolated process
        return await RunInProcessAsync(language, code);
    }
}
```

---

## Data Flow

### User Interaction Flow
```
1. User clicks button in View
   ↓
2. Button.Command bound to ViewModel.Command
   ↓
3. ViewModel calls Service method
   ↓
4. Service executes business logic
   ↓
5. Service returns result
   ↓
6. ViewModel updates properties
   ↓
7. View updates via data binding
```

### Example: Running Code
```
LessonPage.axaml (View)
  ├─ Button clicked
  ├─ Command binding: RunCodeCommand
  └─ Triggers...
      ↓
LessonPageViewModel
  ├─ RunCodeCommand.Execute()
  ├─ Calls: _codeExecutor.ExecuteAsync()
  └─ Updates: Output property
      ↓
CodeExecutor (Service)
  ├─ Creates temp file
  ├─ Spawns process (python3/java/etc)
  ├─ Captures output
  └─ Returns: ExecutionResult
      ↓
LessonPage.axaml (View)
  └─ TextBlock updates: {Binding Output}
```

---

## Dependency Injection

### Service Registration
```csharp
// App.axaml.cs
public override void OnFrameworkInitializationCompleted()
{
    var services = new ServiceCollection();

    // Register services
    services.AddSingleton<ICourseService, CourseService>();
    services.AddSingleton<ICodeExecutor, CodeExecutor>();
    services.AddSingleton<IProgressService, ProgressService>();
    services.AddTransient<INavigationService, NavigationService>();

    // Register ViewModels
    services.AddTransient<MainWindowViewModel>();
    services.AddTransient<LessonPageViewModel>();

    var provider = services.BuildServiceProvider();

    // Create main window with DI
    var mainWindow = new MainWindow
    {
        DataContext = provider.GetRequiredService<MainWindowViewModel>()
    };

    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
        desktop.MainWindow = mainWindow;
    }
}
```

---

## Navigation Architecture

### Navigation Service
```csharp
public interface INavigationService
{
    void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
    void NavigateTo<TViewModel>(object parameter);
    void GoBack();
    bool CanGoBack { get; }
}
```

### Implementation
```csharp
public class NavigationService : INavigationService
{
    private readonly Stack<ViewModelBase> _navigationStack = new();
    private ViewModelBase? _currentViewModel;

    public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
    {
        var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

        if (_currentViewModel != null)
        {
            _navigationStack.Push(_currentViewModel);
        }

        _currentViewModel = viewModel;
        CurrentViewModelChanged?.Invoke(this, viewModel);
    }

    public event EventHandler<ViewModelBase>? CurrentViewModelChanged;
}
```

### View-ViewModel Mapping
```csharp
// ViewLocator.cs
public class ViewLocator : IDataTemplate
{
    public Control Build(object data)
    {
        var name = data.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }
}
```

---

## State Management

### Local State (Per-Session)
**Managed by:** ViewModels
**Lifetime:** Application session
**Example:** Current code in editor, selected course

```csharp
public class LessonPageViewModel : ViewModelBase
{
    private string _code = string.Empty;
    public string Code
    {
        get => _code;
        set => this.RaiseAndSetIfChanged(ref _code, value);
    }
}
```

---

### Persisted State (Cross-Session)
**Managed by:** Services + Database/Files
**Lifetime:** Permanent
**Example:** Progress, achievements, settings

#### SQLite (Progress & Achievements)
```csharp
public class ProgressService : IProgressService
{
    private readonly CodeTutorDbContext _db;

    public async Task SaveProgressAsync(string userId, string lessonId, int score)
    {
        var progress = await _db.Progress
            .FirstOrDefaultAsync(p => p.UserId == userId && p.LessonId == lessonId);

        if (progress == null)
        {
            progress = new UserProgress
            {
                UserId = userId,
                LessonId = lessonId,
                Score = score,
                CompletedAt = DateTime.UtcNow
            };
            _db.Progress.Add(progress);
        }
        else
        {
            progress.Score = Math.Max(progress.Score, score);
            progress.LastAttemptAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();
    }
}
```

#### JSON File (Settings)
```csharp
public class SettingsService : ISettingsService
{
    private readonly string _settingsPath;
    private Settings _settings = new();

    public SettingsService()
    {
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _settingsPath = Path.Combine(appData, "CodeTutor", "settings.json");
        LoadSettings();
    }

    private void LoadSettings()
    {
        if (File.Exists(_settingsPath))
        {
            var json = File.ReadAllText(_settingsPath);
            _settings = JsonSerializer.Deserialize<Settings>(json) ?? new Settings();
        }
    }

    public async Task SaveSettingsAsync()
    {
        var json = JsonSerializer.Serialize(_settings, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        var directory = Path.GetDirectoryName(_settingsPath)!;
        Directory.CreateDirectory(directory);

        await File.WriteAllTextAsync(_settingsPath, json);
    }
}
```

---

## Database Schema

### SQLite Schema
```sql
-- Users table (single user for desktop)
CREATE TABLE Users (
    Id TEXT PRIMARY KEY,
    Name TEXT NOT NULL,
    Email TEXT,
    CreatedAt TEXT NOT NULL
);

-- Progress table
CREATE TABLE Progress (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    CourseId TEXT NOT NULL,
    ModuleId TEXT NOT NULL,
    LessonId TEXT NOT NULL,
    ChallengeId TEXT,
    Score INTEGER DEFAULT 0,
    HintsUsed INTEGER DEFAULT 0,
    Completed BOOLEAN DEFAULT FALSE,
    CompletedAt TEXT,
    LastAttemptAt TEXT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    UNIQUE(UserId, CourseId, ModuleId, LessonId, ChallengeId)
);

-- Achievements table
CREATE TABLE Achievements (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    AchievementType TEXT NOT NULL,
    UnlockedAt TEXT NOT NULL,
    Progress INTEGER DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    UNIQUE(UserId, AchievementType)
);

-- Streaks table
CREATE TABLE Streaks (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    Date TEXT NOT NULL,
    LessonsCompleted INTEGER DEFAULT 0,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    UNIQUE(UserId, Date)
);

-- Code history (for recovery)
CREATE TABLE CodeHistory (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    LessonId TEXT NOT NULL,
    Code TEXT NOT NULL,
    SavedAt TEXT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- Indexes for performance
CREATE INDEX idx_progress_user ON Progress(UserId);
CREATE INDEX idx_progress_lesson ON Progress(LessonId);
CREATE INDEX idx_achievements_user ON Achievements(UserId);
```

---

## Command Pattern

### ICommand Implementation
```csharp
public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;

    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

    public void Execute(object? parameter) => _execute(parameter);

    public event EventHandler? CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
```

### ReactiveCommand (Preferred)
```csharp
// In ViewModel
public ReactiveCommand<Unit, Unit> SaveProgressCommand { get; }

public LessonPageViewModel(IProgressService progressService)
{
    SaveProgressCommand = ReactiveCommand.CreateFromTask(async () =>
    {
        await progressService.SaveProgressAsync(userId, lessonId, score);
    });
}
```

---

## Event Aggregation

### Message Bus Pattern
```csharp
public interface IEventAggregator
{
    void Publish<TEvent>(TEvent eventMessage);
    IDisposable Subscribe<TEvent>(Action<TEvent> handler);
}

// Usage: Achievement unlocked notification
public class AchievementUnlockedEvent
{
    public string AchievementId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}

// Publisher (AchievementService)
_eventAggregator.Publish(new AchievementUnlockedEvent
{
    AchievementId = "first-steps",
    Title = "First Steps",
    Description = "Complete your first lesson"
});

// Subscriber (NotificationViewModel)
_eventAggregator.Subscribe<AchievementUnlockedEvent>(evt =>
{
    ShowToast(evt.Title, evt.Description);
});
```

---

## Error Handling Strategy

### Service Layer
```csharp
public async Task<ExecutionResult> ExecuteAsync(string language, string code)
{
    try
    {
        // Attempt execution
        return await RunProcessAsync(language, code);
    }
    catch (FileNotFoundException ex)
    {
        return new ExecutionResult
        {
            Success = false,
            Error = $"Runtime not found: {ex.Message}"
        };
    }
    catch (TimeoutException)
    {
        return new ExecutionResult
        {
            Success = false,
            Error = "Code execution timed out (10 seconds)"
        };
    }
    catch (Exception ex)
    {
        // Log unexpected errors
        _logger.LogError(ex, "Unexpected error during code execution");

        return new ExecutionResult
        {
            Success = false,
            Error = "An unexpected error occurred"
        };
    }
}
```

### ViewModel Layer
```csharp
public async Task RunCodeAsync()
{
    IsBusy = true;
    ErrorMessage = string.Empty;

    try
    {
        var result = await _codeExecutor.ExecuteAsync(Language, Code);

        if (result.Success)
        {
            Output = result.Output;
        }
        else
        {
            ErrorMessage = result.Error;
        }
    }
    catch (Exception ex)
    {
        ErrorMessage = "Failed to execute code. Please try again.";
        _logger.LogError(ex, "Error in RunCodeAsync");
    }
    finally
    {
        IsBusy = false;
    }
}
```

---

## Performance Considerations

### Lazy Loading
```csharp
// Load course on demand
private Course? _currentCourse;
public async Task<Course> GetCurrentCourseAsync()
{
    if (_currentCourse == null)
    {
        _currentCourse = await _courseService.GetCourseAsync(_currentLanguage);
    }
    return _currentCourse;
}
```

### Caching
```csharp
public class CourseService : ICourseService
{
    private readonly Dictionary<string, Course> _cache = new();

    public async Task<Course?> GetCourseAsync(string language)
    {
        if (_cache.TryGetValue(language, out var cached))
        {
            return cached;
        }

        var course = await LoadCourseFromDiskAsync(language);
        if (course != null)
        {
            _cache[language] = course;
        }

        return course;
    }
}
```

### Virtual Scrolling
```xml
<ListBox ItemsSource="{Binding Lessons}"
         VirtualizationMode="Recycling">
    <!-- Only renders visible items -->
</ListBox>
```

---

## Testing Architecture

### Unit Tests (xUnit)
```csharp
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
        Assert.True(result.Success);
        Assert.Contains("Hello, World!", result.Output);
    }
}
```

### ViewModel Tests
```csharp
public class LessonPageViewModelTests
{
    [Fact]
    public async Task RunCodeCommand_ExecutesCode()
    {
        // Arrange
        var mockExecutor = new Mock<ICodeExecutor>();
        mockExecutor.Setup(x => x.ExecuteAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ExecutionResult { Success = true, Output = "Test" });

        var vm = new LessonPageViewModel(mockExecutor.Object);
        vm.Code = "print('test')";

        // Act
        await vm.RunCodeCommand.Execute();

        // Assert
        Assert.Equal("Test", vm.Output);
        mockExecutor.Verify(x => x.ExecuteAsync("python", "print('test')"), Times.Once);
    }
}
```

---

## Security Considerations

### Code Execution Sandboxing
```csharp
public async Task<ExecutionResult> ExecuteAsync(string language, string code)
{
    var startInfo = new ProcessStartInfo
    {
        FileName = GetRuntimePath(language),
        Arguments = GetArguments(tempFile),
        UseShellExecute = false,
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        CreateNoWindow = true,

        // Sandbox: Set working directory to temp
        WorkingDirectory = tempDirectory,

        // Sandbox: Limit environment variables
        Environment = { ["HOME"] = tempDirectory }
    };

    // Timeout enforcement
    using var process = Process.Start(startInfo);
    var completed = await Task.Run(() => process.WaitForExit(10000));

    if (!completed)
    {
        process.Kill();
        return new ExecutionResult { Success = false, Error = "Timeout" };
    }
}
```

---

## Deployment Architecture

### File Structure (Published App)
```
CodeTutor/
├── CodeTutor.exe (Windows) / CodeTutor (macOS/Linux)
├── CodeTutor.dll
├── Avalonia.*.dll
├── Content/
│   └── courses/
│       ├── python/course.json
│       ├── java/course.json
│       └── ...
└── Data/
    ├── CodeTutor.db (created on first run)
    └── settings.json (created on first run)
```

### Application Data Directory
```
Windows: C:\Users\{User}\AppData\Roaming\CodeTutor\
macOS: ~/Library/Application Support/CodeTutor/
Linux: ~/.config/CodeTutor/

Contents:
├── CodeTutor.db       # SQLite database
├── settings.json      # User preferences
└── logs/              # Application logs
```

---

**Document Version:** 1.0
**Last Updated:** 2025-11-18
