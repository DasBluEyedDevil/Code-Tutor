# WPF Application Completion Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Complete the WPF Code-Tutor application by implementing missing features, fixing stubs, and cleaning up the repository.

**Architecture:** The WPF app uses a code-behind pattern with services for course loading and code execution. This plan adds test case validation, syntax highlighting, progress persistence, and additional language support.

**Tech Stack:** WPF (.NET 8), AvalonEdit (code editor), System.Text.Json, SQLite (for progress)

---

## Task 1: Repository Cleanup

**Files:**
- Delete: `content/courses/csharp/course.backup.json`
- Delete: `content/courses/flutter/course.backup.json`
- Delete: `content/courses/java/course.backup.json`
- Delete: `content/courses/javascript/course.backup.json`
- Delete: `content/courses/kotlin/course.backup.json`
- Delete: `content/courses/python/course.backup.json`
- Delete: `content/normalize-courses.js`
- Delete: `content/phase2-content-fill.js`
- Delete: `convert-icon.ps1`
- Modify: `.gitignore`

**Step 1: Delete backup and temporary files**

```bash
rm content/courses/*/course.backup.json
rm content/normalize-courses.js
rm content/phase2-content-fill.js
rm convert-icon.ps1
```

**Step 2: Update .gitignore to prevent future issues**

Add to `.gitignore`:
```
# Backup files
*.backup.json
*.backup

# Temporary scripts
normalize-courses.js
phase2-content-fill.js
```

**Step 3: Commit cleanup**

```bash
git add -A
git commit -m "chore: remove backup files and temp scripts"
```

---

## Task 2: Add Syntax Highlighting to Code Editor

**Files:**
- Modify: `native-app-wpf/Controls/CodeExampleSection.xaml.cs`
- Modify: `native-app-wpf/Controls/CodingChallenge.xaml.cs`

**Step 1: Update CodeExampleSection to load syntax highlighting**

Replace `native-app-wpf/Controls/CodeExampleSection.xaml.cs`:

```csharp
using System.Windows.Controls;
using System.Xml;
using CodeTutor.Wpf.Models;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace CodeTutor.Wpf.Controls;

public partial class CodeExampleSection : UserControl
{
    public CodeExampleSection(ContentSection section)
    {
        InitializeComponent();
        ExampleTitle.Text = section.Title;
        CodeEditor.Text = section.Code ?? string.Empty;
        Description.Text = section.Content;

        // Set syntax highlighting based on language
        if (!string.IsNullOrEmpty(section.Language))
        {
            var highlighting = GetHighlightingForLanguage(section.Language);
            if (highlighting != null)
            {
                CodeEditor.SyntaxHighlighting = highlighting;
            }
        }
    }

    private static IHighlightingDefinition? GetHighlightingForLanguage(string language)
    {
        var langLower = language.ToLower();
        return langLower switch
        {
            "python" => HighlightingManager.Instance.GetDefinition("Python"),
            "javascript" or "js" => HighlightingManager.Instance.GetDefinition("JavaScript"),
            "csharp" or "c#" => HighlightingManager.Instance.GetDefinition("C#"),
            "java" => HighlightingManager.Instance.GetDefinition("Java"),
            "kotlin" => HighlightingManager.Instance.GetDefinition("Java"), // Close enough
            "dart" or "flutter" => HighlightingManager.Instance.GetDefinition("C#"), // Close enough
            _ => null
        };
    }
}
```

**Step 2: Add syntax highlighting to CodingChallenge**

Add the same helper method to `CodingChallenge.xaml.cs` and call it in constructor after line 26:

```csharp
// After line 26: CodeEditor.Text = challenge.StarterCode;
var highlighting = GetHighlightingForLanguage(challenge.Language);
if (highlighting != null)
{
    CodeEditor.SyntaxHighlighting = highlighting;
}
```

Add the helper method:

```csharp
private static IHighlightingDefinition? GetHighlightingForLanguage(string language)
{
    var langLower = language.ToLower();
    return langLower switch
    {
        "python" => HighlightingManager.Instance.GetDefinition("Python"),
        "javascript" or "js" => HighlightingManager.Instance.GetDefinition("JavaScript"),
        "csharp" or "c#" => HighlightingManager.Instance.GetDefinition("C#"),
        "java" => HighlightingManager.Instance.GetDefinition("Java"),
        "kotlin" => HighlightingManager.Instance.GetDefinition("Java"),
        "dart" or "flutter" => HighlightingManager.Instance.GetDefinition("C#"),
        _ => null
    };
}
```

Add using statement:
```csharp
using ICSharpCode.AvalonEdit.Highlighting;
```

**Step 3: Build and verify**

Run: `cd native-app-wpf && dotnet build`
Expected: Build succeeded. 0 Errors.

**Step 4: Commit**

```bash
git add native-app-wpf/Controls/
git commit -m "feat: add syntax highlighting to code editors"
```

---

## Task 3: Implement Test Case Validation

**Files:**
- Modify: `native-app-wpf/Controls/CodingChallenge.xaml`
- Modify: `native-app-wpf/Controls/CodingChallenge.xaml.cs`

**Step 1: Add test results panel to XAML**

In `CodingChallenge.xaml`, after the OutputPanel (line 89), add:

```xml
<!-- Test Results Panel -->
<Border x:Name="TestResultsPanel"
        Background="{StaticResource BackgroundDarkBrush}"
        BorderBrush="{StaticResource BorderDefaultBrush}"
        BorderThickness="1"
        CornerRadius="6"
        Padding="12"
        Margin="0,8,0,0"
        Visibility="Collapsed">
    <StackPanel>
        <TextBlock Text="Test Results:"
                   Style="{StaticResource CaptionText}"
                   Margin="0,0,0,8" />
        <ItemsControl x:Name="TestResultsList">
            <ItemsControl.ItemTemplate>
                <DataTemplate>
                    <StackPanel Orientation="Horizontal" Margin="0,2">
                        <TextBlock x:Name="StatusIcon"
                                   FontFamily="Segoe UI Symbol"
                                   Margin="0,0,8,0" />
                        <TextBlock Text="{Binding Description}"
                                   Style="{StaticResource CaptionText}" />
                    </StackPanel>
                </DataTemplate>
            </ItemsControl.ItemTemplate>
        </ItemsControl>
        <TextBlock x:Name="TestSummary"
                   Style="{StaticResource BodyText}"
                   Margin="0,8,0,0" />
    </StackPanel>
</Border>
```

**Step 2: Add TestResult model and validation logic**

Add to `CodingChallenge.xaml.cs`:

```csharp
public record TestResult(string Description, bool Passed, string? ActualOutput = null);

private async void RunCode_Click(object sender, RoutedEventArgs e)
{
    try
    {
        OutputPanel.Visibility = Visibility.Visible;
        TestResultsPanel.Visibility = Visibility.Collapsed;
        OutputText.Text = "Running...";
        OutputText.Foreground = (System.Windows.Media.Brush)FindResource("TextPrimaryBrush");

        var result = await _executionService.ExecuteAsync(CodeEditor.Text, _challenge.Language);

        if (result.Success)
        {
            OutputText.Text = string.IsNullOrEmpty(result.Output) ? "(No output)" : result.Output;
            OutputText.Foreground = (System.Windows.Media.Brush)FindResource("TextPrimaryBrush");

            // Run test case validation if test cases exist
            if (_challenge.TestCases != null && _challenge.TestCases.Count > 0)
            {
                ValidateTestCases(result.Output);
            }
        }
        else
        {
            OutputText.Text = string.IsNullOrEmpty(result.Error) ? result.Output : result.Error;
            OutputText.Foreground = (System.Windows.Media.Brush)FindResource("AccentRedBrush");
        }
    }
    catch (Exception ex)
    {
        OutputText.Text = $"Execution failed: {ex.Message}";
        OutputText.Foreground = (System.Windows.Media.Brush)FindResource("AccentRedBrush");
    }
}

private void ValidateTestCases(string actualOutput)
{
    var results = new List<TestResult>();
    int passed = 0;

    foreach (var testCase in _challenge.TestCases)
    {
        if (!testCase.IsVisible) continue;

        bool testPassed = string.IsNullOrEmpty(testCase.ExpectedOutput) ||
                          actualOutput.Contains(testCase.ExpectedOutput.Trim());

        if (testPassed) passed++;
        results.Add(new TestResult(testCase.Description, testPassed, actualOutput));
    }

    TestResultsPanel.Visibility = Visibility.Visible;
    TestResultsList.ItemsSource = results.Select(r => new
    {
        r.Description,
        Status = r.Passed ? "✓" : "✗",
        Color = r.Passed ? FindResource("AccentGreenBrush") : FindResource("AccentRedBrush")
    });

    int total = results.Count;
    TestSummary.Text = $"{passed}/{total} tests passed";
    TestSummary.Foreground = passed == total
        ? (System.Windows.Media.Brush)FindResource("AccentGreenBrush")
        : (System.Windows.Media.Brush)FindResource("AccentRedBrush");
}
```

Add using:
```csharp
using System.Linq;
```

**Step 3: Build and verify**

Run: `cd native-app-wpf && dotnet build`
Expected: Build succeeded.

**Step 4: Commit**

```bash
git add native-app-wpf/Controls/CodingChallenge.*
git commit -m "feat: implement test case validation for coding challenges"
```

---

## Task 4: Implement C# Code Execution

**Files:**
- Modify: `native-app-wpf/Services/CodeExecutionService.cs`

**Step 1: Implement C# execution using dotnet-script**

Replace `ExecuteCSharpAsync` method:

```csharp
private async Task<ExecutionResult> ExecuteCSharpAsync(string code)
{
    // Check if dotnet-script is available
    var checkResult = await RunProcessAsync("dotnet", "tool list -g");
    if (!checkResult.Output.Contains("dotnet-script"))
    {
        return new ExecutionResult(false, "",
            "C# execution requires dotnet-script. Install with: dotnet tool install -g dotnet-script");
    }

    var tempFile = Path.GetTempFileName();
    var csharpFile = Path.ChangeExtension(tempFile, ".csx");
    File.Move(tempFile, csharpFile);

    await File.WriteAllTextAsync(csharpFile, code);

    try
    {
        var result = await RunProcessAsync("dotnet-script", csharpFile);
        return result;
    }
    finally
    {
        File.Delete(csharpFile);
    }
}
```

**Step 2: Build and verify**

Run: `cd native-app-wpf && dotnet build`
Expected: Build succeeded.

**Step 3: Commit**

```bash
git add native-app-wpf/Services/CodeExecutionService.cs
git commit -m "feat: implement C# code execution using dotnet-script"
```

---

## Task 5: Add Java and Kotlin Execution Support

**Files:**
- Modify: `native-app-wpf/Services/CodeExecutionService.cs`

**Step 1: Add Java and Kotlin to the switch statement**

Update the switch in `ExecuteAsync`:

```csharp
public async Task<ExecutionResult> ExecuteAsync(string code, string language)
{
    return language.ToLower() switch
    {
        "python" => await ExecutePythonAsync(code),
        "javascript" or "js" => await ExecuteJavaScriptAsync(code),
        "csharp" or "c#" => await ExecuteCSharpAsync(code),
        "java" => await ExecuteJavaAsync(code),
        "kotlin" => await ExecuteKotlinAsync(code),
        _ => new ExecutionResult(false, "", $"Language '{language}' not supported")
    };
}
```

**Step 2: Add Java execution method**

```csharp
private async Task<ExecutionResult> ExecuteJavaAsync(string code)
{
    // Extract class name from code (assumes public class Main or similar)
    var className = "Main";
    var classMatch = System.Text.RegularExpressions.Regex.Match(code, @"public\s+class\s+(\w+)");
    if (classMatch.Success)
    {
        className = classMatch.Groups[1].Value;
    }

    var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
    Directory.CreateDirectory(tempDir);
    var javaFile = Path.Combine(tempDir, $"{className}.java");

    await File.WriteAllTextAsync(javaFile, code);

    try
    {
        // Compile
        var compileResult = await RunProcessAsync("javac", javaFile);
        if (!compileResult.Success)
        {
            return new ExecutionResult(false, "", compileResult.Error);
        }

        // Run
        var result = await RunProcessAsync("java", $"-cp \"{tempDir}\" {className}");
        return result;
    }
    finally
    {
        Directory.Delete(tempDir, true);
    }
}
```

**Step 3: Add Kotlin execution method**

```csharp
private async Task<ExecutionResult> ExecuteKotlinAsync(string code)
{
    var tempFile = Path.GetTempFileName();
    var kotlinFile = Path.ChangeExtension(tempFile, ".kts");
    File.Move(tempFile, kotlinFile);

    await File.WriteAllTextAsync(kotlinFile, code);

    try
    {
        // Kotlin script mode (.kts files run directly)
        var result = await RunProcessAsync("kotlinc", $"-script \"{kotlinFile}\"");
        return result;
    }
    finally
    {
        File.Delete(kotlinFile);
    }
}
```

**Step 4: Build and verify**

Run: `cd native-app-wpf && dotnet build`
Expected: Build succeeded.

**Step 5: Commit**

```bash
git add native-app-wpf/Services/CodeExecutionService.cs
git commit -m "feat: add Java and Kotlin code execution support"
```

---

## Task 6: Implement Module Expand/Collapse in Sidebar

**Files:**
- Modify: `native-app-wpf/Views/CourseSidebar.xaml`
- Modify: `native-app-wpf/Views/CourseSidebar.xaml.cs`

**Step 1: Add expansion tracking to code-behind**

Add field and modify `CourseSidebar.xaml.cs`:

```csharp
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CodeTutor.Wpf.Models;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Views;

public partial class CourseSidebar : UserControl
{
    private readonly Course _course;
    private readonly ICourseService _courseService;
    private readonly INavigationService _navigation;
    private readonly Dictionary<string, bool> _expandedModules = new();

    public CourseSidebar(Course course, ICourseService courseService, INavigationService navigation)
    {
        InitializeComponent();
        _course = course;
        _courseService = courseService;
        _navigation = navigation;

        CourseTitle.Text = course.Title;
        ModulesList.ItemsSource = course.Modules;

        // Initialize all modules as expanded
        foreach (var module in course.Modules)
        {
            _expandedModules[module.Id] = true;
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        _navigation.GoBack();
    }

    private void ModuleHeader_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Module module)
        {
            _expandedModules[module.Id] = !_expandedModules[module.Id];

            // Find the lessons panel within the parent and toggle visibility
            var parent = button.Parent as StackPanel;
            if (parent != null)
            {
                foreach (var child in parent.Children)
                {
                    if (child is ItemsControl lessonsPanel)
                    {
                        lessonsPanel.Visibility = _expandedModules[module.Id]
                            ? Visibility.Visible
                            : Visibility.Collapsed;
                    }
                }
            }

            // Update chevron
            if (button.Content is StackPanel sp)
            {
                foreach (var child in sp.Children)
                {
                    if (child is TextBlock chevron && chevron.Name == "ChevronIcon")
                    {
                        chevron.Text = _expandedModules[module.Id] ? "▼" : "▶";
                    }
                }
            }
        }
    }

    private void LessonItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Lesson lesson)
        {
            var lessonPage = new LessonPage(_course, lesson, _courseService, _navigation);
            _navigation.NavigateTo(lessonPage, lesson);
        }
    }
}
```

**Step 2: Update XAML to add chevron and proper binding**

Update the module header template in `CourseSidebar.xaml` to include:

```xml
<Button Style="{StaticResource GhostButton}"
        Click="ModuleHeader_Click"
        Tag="{Binding}"
        HorizontalContentAlignment="Left"
        Padding="8,6">
    <StackPanel Orientation="Horizontal">
        <TextBlock x:Name="ChevronIcon"
                   Text="▼"
                   Margin="0,0,8,0"
                   Foreground="{StaticResource TextMutedBrush}" />
        <TextBlock Text="{Binding Title}"
                   Style="{StaticResource BodyText}"
                   FontWeight="SemiBold" />
    </StackPanel>
</Button>
```

**Step 3: Build and verify**

Run: `cd native-app-wpf && dotnet build`
Expected: Build succeeded.

**Step 4: Commit**

```bash
git add native-app-wpf/Views/CourseSidebar.*
git commit -m "feat: implement module expand/collapse in course sidebar"
```

---

## Task 7: Add Simple JSON-Based Progress Persistence

**Files:**
- Create: `native-app-wpf/Services/ProgressService.cs`
- Create: `native-app-wpf/Models/UserProgress.cs`
- Modify: `native-app-wpf/Views/LessonPage.xaml.cs`

**Step 1: Create UserProgress model**

Create `native-app-wpf/Models/UserProgress.cs`:

```csharp
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CodeTutor.Wpf.Models;

public class UserProgress
{
    [JsonPropertyName("completedLessons")]
    public HashSet<string> CompletedLessons { get; set; } = new();

    [JsonPropertyName("lessonProgress")]
    public Dictionary<string, LessonProgress> LessonProgress { get; set; } = new();

    [JsonPropertyName("lastUpdated")]
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}

public class LessonProgress
{
    [JsonPropertyName("challengesPassed")]
    public HashSet<string> ChallengesPassed { get; set; } = new();

    [JsonPropertyName("lastCode")]
    public Dictionary<string, string> LastCode { get; set; } = new();
}
```

**Step 2: Create ProgressService**

Create `native-app-wpf/Services/ProgressService.cs`:

```csharp
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Services;

public interface IProgressService
{
    Task<UserProgress> LoadProgressAsync();
    Task SaveProgressAsync(UserProgress progress);
    Task MarkLessonCompleteAsync(string lessonId);
    Task<bool> IsLessonCompleteAsync(string lessonId);
}

public class ProgressService : IProgressService
{
    private readonly string _progressFilePath;
    private UserProgress? _cachedProgress;

    public ProgressService()
    {
        var appDataPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CodeTutor");
        Directory.CreateDirectory(appDataPath);
        _progressFilePath = Path.Combine(appDataPath, "progress.json");
    }

    public async Task<UserProgress> LoadProgressAsync()
    {
        if (_cachedProgress != null) return _cachedProgress;

        if (!File.Exists(_progressFilePath))
        {
            _cachedProgress = new UserProgress();
            return _cachedProgress;
        }

        try
        {
            var json = await File.ReadAllTextAsync(_progressFilePath);
            _cachedProgress = JsonSerializer.Deserialize<UserProgress>(json) ?? new UserProgress();
        }
        catch
        {
            _cachedProgress = new UserProgress();
        }

        return _cachedProgress;
    }

    public async Task SaveProgressAsync(UserProgress progress)
    {
        progress.LastUpdated = DateTime.UtcNow;
        _cachedProgress = progress;

        var json = JsonSerializer.Serialize(progress, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_progressFilePath, json);
    }

    public async Task MarkLessonCompleteAsync(string lessonId)
    {
        var progress = await LoadProgressAsync();
        progress.CompletedLessons.Add(lessonId);
        await SaveProgressAsync(progress);
    }

    public async Task<bool> IsLessonCompleteAsync(string lessonId)
    {
        var progress = await LoadProgressAsync();
        return progress.CompletedLessons.Contains(lessonId);
    }
}
```

**Step 3: Update LessonPage to use ProgressService**

Modify `LessonPage.xaml.cs` to persist completion:

Add field:
```csharp
private readonly IProgressService _progressService = new ProgressService();
```

Update `CompleteButton_Click`:
```csharp
private async void CompleteButton_Click(object sender, RoutedEventArgs e)
{
    await _progressService.MarkLessonCompleteAsync(_lesson.Id);
    CompleteButton.Content = new TextBlock { Text = "✓ Completed" };
    CompleteButton.IsEnabled = false;
}
```

Add check on load (in constructor or Loaded event):
```csharp
private async void CheckCompletionStatus()
{
    if (await _progressService.IsLessonCompleteAsync(_lesson.Id))
    {
        CompleteButton.Content = new TextBlock { Text = "✓ Completed" };
        CompleteButton.IsEnabled = false;
    }
}
```

**Step 4: Build and verify**

Run: `cd native-app-wpf && dotnet build`
Expected: Build succeeded.

**Step 5: Commit**

```bash
git add native-app-wpf/Models/UserProgress.cs
git add native-app-wpf/Services/ProgressService.cs
git add native-app-wpf/Views/LessonPage.xaml.cs
git commit -m "feat: add JSON-based progress persistence"
```

---

## Task 8: Remove Unused Dependencies

**Files:**
- Modify: `native-app-wpf/CodeTutor.Wpf.csproj`

**Step 1: Remove unused packages**

Update project file to remove unused packages:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <UseWPF>true</UseWPF>
    <ApplicationIcon>Assets\icon.ico</ApplicationIcon>
    <AssemblyName>CodeTutor</AssemblyName>
    <NoWarn>NU1701</NoWarn>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="AvalonEdit" Version="6.3.0.90" />
    <PackageReference Include="System.Text.Json" Version="8.0.5" />
  </ItemGroup>

  <ItemGroup>
    <None Include="..\content\**\*">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
      <Link>Content\%(RecursiveDir)%(Filename)%(Extension)</Link>
    </None>
    <Resource Include="Assets\icon.ico" />
  </ItemGroup>
</Project>
```

Removed:
- `ReactiveUI.WPF` (not used)
- `Markdig` and `Markdig.Wpf` (not used)
- `Microsoft.Data.Sqlite` (not used)
- `Microsoft.EntityFrameworkCore.Sqlite` (not used)
- `Microsoft.Extensions.DependencyInjection` (not used)

**Step 2: Build and verify**

Run: `cd native-app-wpf && dotnet restore && dotnet build`
Expected: Build succeeded.

**Step 3: Commit**

```bash
git add native-app-wpf/CodeTutor.Wpf.csproj
git commit -m "chore: remove unused NuGet dependencies"
```

---

## Task 9: Final Integration Test

**Step 1: Build the full application**

```bash
cd native-app-wpf && dotnet build --configuration Release
```

Expected: Build succeeded.

**Step 2: Run and manually verify**

```bash
cd native-app-wpf && dotnet run
```

Verify:
- [ ] App launches with dark theme
- [ ] Course cards display on landing page
- [ ] Clicking course shows course page with sidebar
- [ ] Module headers expand/collapse
- [ ] Lessons display with syntax highlighting
- [ ] Code challenges run and validate test cases
- [ ] Python code executes correctly
- [ ] JavaScript code executes correctly
- [ ] Progress persists after marking lesson complete
- [ ] Progress survives app restart

**Step 3: Final commit**

```bash
git add -A
git commit -m "feat: complete WPF application MVP"
```

---

## Summary

| Task | Description | Est. Time |
|------|-------------|-----------|
| 1 | Repository Cleanup | 2 min |
| 2 | Syntax Highlighting | 5 min |
| 3 | Test Case Validation | 10 min |
| 4 | C# Code Execution | 5 min |
| 5 | Java/Kotlin Execution | 10 min |
| 6 | Module Expand/Collapse | 10 min |
| 7 | Progress Persistence | 15 min |
| 8 | Remove Unused Dependencies | 3 min |
| 9 | Final Integration Test | 10 min |

**Total: ~70 minutes**
