# Code Tutor UI Rebuild - WPF Implementation

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Rebuild the Code Tutor UI as a native WPF desktop application with clean design, proper lesson content display, and interactive coding challenges.

**Architecture:** Three-panel layout with sidebar navigation, main content area, and collapsible code editor panel. MVVM pattern with ReactiveUI. WPF's mature binding system and proven control library.

**Tech Stack:** .NET 8, WPF, ReactiveUI, ICSharpCode.AvalonEdit (code editor), Markdig + markdig.wpf (markdown), SQLite for progress.

---

## Phase 1: Create New WPF Project

### Task 1: Initialize WPF Project Structure

**Files:**
- Create: `native-app-wpf/CodeTutor.Wpf.csproj`
- Create: `native-app-wpf/App.xaml`
- Create: `native-app-wpf/App.xaml.cs`

**Step 1: Create new WPF project**

```bash
cd "C:\Users\dasbl\Downloads\Code-Tutor"
dotnet new wpf -n CodeTutor.Wpf -o native-app-wpf
```

**Step 2: Update csproj with dependencies**

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
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="ReactiveUI.WPF" Version="20.1.1" />
    <PackageReference Include="AvalonEdit" Version="6.3.0.90" />
    <PackageReference Include="Markdig" Version="0.34.0" />
    <PackageReference Include="Markdig.Wpf" Version="0.6.2" />
    <PackageReference Include="Microsoft.Data.Sqlite" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.0" />
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="8.0.0" />
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

**Step 3: Copy assets**

```bash
mkdir native-app-wpf/Assets
cp native-app/Assets/icon.ico native-app-wpf/Assets/
```

**Step 4: Commit project setup**

```bash
git add native-app-wpf/
git commit -m "feat: initialize WPF project with dependencies"
```

---

## Phase 2: Design System - Resources and Styles

### Task 2: Create Theme Resources

**Files:**
- Create: `native-app-wpf/Themes/Colors.xaml`
- Create: `native-app-wpf/Themes/Typography.xaml`
- Create: `native-app-wpf/Themes/Controls.xaml`

**Step 1: Create Colors.xaml**

```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <!-- Background Colors -->
    <Color x:Key="BackgroundDark">#0D1117</Color>
    <Color x:Key="BackgroundMedium">#161B22</Color>
    <Color x:Key="BackgroundLight">#21262D</Color>
    <Color x:Key="BackgroundHover">#30363D</Color>

    <!-- Text Colors -->
    <Color x:Key="TextPrimary">#E6EDF3</Color>
    <Color x:Key="TextSecondary">#8B949E</Color>
    <Color x:Key="TextMuted">#6E7681</Color>

    <!-- Accent Colors -->
    <Color x:Key="AccentBlue">#58A6FF</Color>
    <Color x:Key="AccentGreen">#3FB950</Color>
    <Color x:Key="AccentOrange">#D29922</Color>
    <Color x:Key="AccentRed">#F85149</Color>
    <Color x:Key="AccentPurple">#A371F7</Color>

    <!-- Border Colors -->
    <Color x:Key="BorderDefault">#30363D</Color>

    <!-- Brushes -->
    <SolidColorBrush x:Key="BackgroundDarkBrush" Color="{StaticResource BackgroundDark}" />
    <SolidColorBrush x:Key="BackgroundMediumBrush" Color="{StaticResource BackgroundMedium}" />
    <SolidColorBrush x:Key="BackgroundLightBrush" Color="{StaticResource BackgroundLight}" />
    <SolidColorBrush x:Key="BackgroundHoverBrush" Color="{StaticResource BackgroundHover}" />

    <SolidColorBrush x:Key="TextPrimaryBrush" Color="{StaticResource TextPrimary}" />
    <SolidColorBrush x:Key="TextSecondaryBrush" Color="{StaticResource TextSecondary}" />
    <SolidColorBrush x:Key="TextMutedBrush" Color="{StaticResource TextMuted}" />

    <SolidColorBrush x:Key="AccentBlueBrush" Color="{StaticResource AccentBlue}" />
    <SolidColorBrush x:Key="AccentGreenBrush" Color="{StaticResource AccentGreen}" />
    <SolidColorBrush x:Key="AccentOrangeBrush" Color="{StaticResource AccentOrange}" />
    <SolidColorBrush x:Key="AccentRedBrush" Color="{StaticResource AccentRed}" />

    <SolidColorBrush x:Key="BorderDefaultBrush" Color="{StaticResource BorderDefault}" />

</ResourceDictionary>
```

**Step 2: Create Typography.xaml**

```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <!-- Font Families -->
    <FontFamily x:Key="UIFont">Segoe UI</FontFamily>
    <FontFamily x:Key="MonoFont">Cascadia Code, Consolas</FontFamily>

    <!-- Text Styles -->
    <Style x:Key="TitleText" TargetType="TextBlock">
        <Setter Property="FontSize" Value="28" />
        <Setter Property="FontWeight" Value="SemiBold" />
        <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}" />
    </Style>

    <Style x:Key="HeadingText" TargetType="TextBlock">
        <Setter Property="FontSize" Value="20" />
        <Setter Property="FontWeight" Value="SemiBold" />
        <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}" />
    </Style>

    <Style x:Key="SubheadingText" TargetType="TextBlock">
        <Setter Property="FontSize" Value="16" />
        <Setter Property="FontWeight" Value="Medium" />
        <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}" />
    </Style>

    <Style x:Key="BodyText" TargetType="TextBlock">
        <Setter Property="FontSize" Value="14" />
        <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}" />
        <Setter Property="TextWrapping" Value="Wrap" />
    </Style>

    <Style x:Key="CaptionText" TargetType="TextBlock">
        <Setter Property="FontSize" Value="12" />
        <Setter Property="Foreground" Value="{StaticResource TextSecondaryBrush}" />
    </Style>

    <Style x:Key="CodeText" TargetType="TextBlock">
        <Setter Property="FontFamily" Value="{StaticResource MonoFont}" />
        <Setter Property="FontSize" Value="13" />
        <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}" />
    </Style>

</ResourceDictionary>
```

**Step 3: Create Controls.xaml with button styles**

```xml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <!-- Primary Button -->
    <Style x:Key="PrimaryButton" TargetType="Button">
        <Setter Property="Background" Value="{StaticResource AccentBlueBrush}" />
        <Setter Property="Foreground" Value="White" />
        <Setter Property="Padding" Value="16,10" />
        <Setter Property="BorderThickness" Value="0" />
        <Setter Property="Cursor" Value="Hand" />
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="Button">
                    <Border Background="{TemplateBinding Background}"
                            CornerRadius="6"
                            Padding="{TemplateBinding Padding}">
                        <ContentPresenter HorizontalAlignment="Center"
                                          VerticalAlignment="Center" />
                    </Border>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
        <Style.Triggers>
            <Trigger Property="IsMouseOver" Value="True">
                <Setter Property="Background" Value="#79B8FF" />
            </Trigger>
            <Trigger Property="IsPressed" Value="True">
                <Setter Property="Background" Value="#388BFD" />
            </Trigger>
        </Style.Triggers>
    </Style>

    <!-- Secondary Button -->
    <Style x:Key="SecondaryButton" TargetType="Button">
        <Setter Property="Background" Value="{StaticResource BackgroundLightBrush}" />
        <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}" />
        <Setter Property="BorderBrush" Value="{StaticResource BorderDefaultBrush}" />
        <Setter Property="BorderThickness" Value="1" />
        <Setter Property="Padding" Value="16,10" />
        <Setter Property="Cursor" Value="Hand" />
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="Button">
                    <Border Background="{TemplateBinding Background}"
                            BorderBrush="{TemplateBinding BorderBrush}"
                            BorderThickness="{TemplateBinding BorderThickness}"
                            CornerRadius="6"
                            Padding="{TemplateBinding Padding}">
                        <ContentPresenter HorizontalAlignment="Center"
                                          VerticalAlignment="Center" />
                    </Border>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
        <Style.Triggers>
            <Trigger Property="IsMouseOver" Value="True">
                <Setter Property="Background" Value="{StaticResource BackgroundHoverBrush}" />
            </Trigger>
        </Style.Triggers>
    </Style>

    <!-- Ghost Button -->
    <Style x:Key="GhostButton" TargetType="Button">
        <Setter Property="Background" Value="Transparent" />
        <Setter Property="Foreground" Value="{StaticResource TextSecondaryBrush}" />
        <Setter Property="BorderThickness" Value="0" />
        <Setter Property="Padding" Value="8,6" />
        <Setter Property="Cursor" Value="Hand" />
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="Button">
                    <Border Background="{TemplateBinding Background}"
                            CornerRadius="4"
                            Padding="{TemplateBinding Padding}">
                        <ContentPresenter HorizontalAlignment="Left"
                                          VerticalAlignment="Center" />
                    </Border>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
        <Style.Triggers>
            <Trigger Property="IsMouseOver" Value="True">
                <Setter Property="Background" Value="{StaticResource BackgroundHoverBrush}" />
                <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}" />
            </Trigger>
        </Style.Triggers>
    </Style>

    <!-- Sidebar Item Button -->
    <Style x:Key="SidebarItemButton" TargetType="Button">
        <Setter Property="Background" Value="Transparent" />
        <Setter Property="Foreground" Value="{StaticResource TextSecondaryBrush}" />
        <Setter Property="BorderThickness" Value="0" />
        <Setter Property="Padding" Value="12,8" />
        <Setter Property="HorizontalContentAlignment" Value="Left" />
        <Setter Property="Cursor" Value="Hand" />
        <Setter Property="Template">
            <Setter.Value>
                <ControlTemplate TargetType="Button">
                    <Border Background="{TemplateBinding Background}"
                            CornerRadius="6"
                            Padding="{TemplateBinding Padding}">
                        <ContentPresenter HorizontalAlignment="{TemplateBinding HorizontalContentAlignment}"
                                          VerticalAlignment="Center" />
                    </Border>
                </ControlTemplate>
            </Setter.Value>
        </Setter>
        <Style.Triggers>
            <Trigger Property="IsMouseOver" Value="True">
                <Setter Property="Background" Value="{StaticResource BackgroundLightBrush}" />
                <Setter Property="Foreground" Value="{StaticResource TextPrimaryBrush}" />
            </Trigger>
        </Style.Triggers>
    </Style>

    <!-- Card Border -->
    <Style x:Key="CardBorder" TargetType="Border">
        <Setter Property="Background" Value="{StaticResource BackgroundMediumBrush}" />
        <Setter Property="BorderBrush" Value="{StaticResource BorderDefaultBrush}" />
        <Setter Property="BorderThickness" Value="1" />
        <Setter Property="CornerRadius" Value="8" />
        <Setter Property="Padding" Value="16" />
    </Style>

    <!-- ScrollViewer Dark Style -->
    <Style x:Key="DarkScrollViewer" TargetType="ScrollViewer">
        <Setter Property="Background" Value="Transparent" />
        <Setter Property="HorizontalScrollBarVisibility" Value="Disabled" />
        <Setter Property="VerticalScrollBarVisibility" Value="Auto" />
    </Style>

</ResourceDictionary>
```

**Step 4: Register themes in App.xaml**

```xml
<Application x:Class="CodeTutor.Wpf.App"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             StartupUri="MainWindow.xaml">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Themes/Colors.xaml" />
                <ResourceDictionary Source="Themes/Typography.xaml" />
                <ResourceDictionary Source="Themes/Controls.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

**Step 5: Commit design system**

```bash
git add native-app-wpf/Themes/ native-app-wpf/App.xaml
git commit -m "feat: add WPF design system with dark theme"
```

---

## Phase 3: Core Infrastructure

### Task 3: Create Models and Services

**Files:**
- Create: `native-app-wpf/Models/Course.cs`
- Create: `native-app-wpf/Services/CourseService.cs`
- Create: `native-app-wpf/Services/NavigationService.cs`
- Create: `native-app-wpf/Services/CodeExecutionService.cs`

**Step 1: Create Course.cs (copy from native-app)**

```csharp
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CodeTutor.Wpf.Models;

public class Course
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("difficulty")]
    public string Difficulty { get; set; } = string.Empty;

    [JsonPropertyName("estimatedHours")]
    public int EstimatedHours { get; set; }

    [JsonPropertyName("modules")]
    public List<Module> Modules { get; set; } = new();

    // Computed properties
    public int ModuleCount => Modules.Count;
    public bool IsRuntimeAvailable { get; set; } = true;
}

public class Module
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("lessons")]
    public List<Lesson> Lessons { get; set; } = new();
}

public class Lesson
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("moduleId")]
    public string ModuleId { get; set; } = string.Empty;

    [JsonPropertyName("order")]
    public int Order { get; set; }

    [JsonPropertyName("estimatedMinutes")]
    public int EstimatedMinutes { get; set; }

    [JsonPropertyName("difficulty")]
    public string Difficulty { get; set; } = string.Empty;

    [JsonPropertyName("contentSections")]
    public List<ContentSection> ContentSections { get; set; } = new();

    [JsonPropertyName("challenges")]
    public List<Challenge> Challenges { get; set; } = new();
}

public class ContentSection
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("language")]
    public string? Language { get; set; }
}

public class Challenge
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("instructions")]
    public string Instructions { get; set; } = string.Empty;

    [JsonPropertyName("starterCode")]
    public string StarterCode { get; set; } = string.Empty;

    [JsonPropertyName("solution")]
    public string Solution { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = string.Empty;

    [JsonPropertyName("hints")]
    public List<Hint> Hints { get; set; } = new();

    [JsonPropertyName("testCases")]
    public List<TestCase> TestCases { get; set; } = new();
}

public class Hint
{
    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}

public class TestCase
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("expectedOutput")]
    public string ExpectedOutput { get; set; } = string.Empty;

    [JsonPropertyName("isVisible")]
    public bool IsVisible { get; set; } = true;
}
```

**Step 2: Create CourseService.cs**

```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Services;

public interface ICourseService
{
    Task<List<Course>> GetAllCoursesAsync();
    Task<Course?> GetCourseAsync(string courseId);
    Task<Lesson?> GetLessonAsync(string courseId, string moduleId, string lessonId);
}

public class CourseService : ICourseService
{
    private readonly string _contentPath;
    private readonly Dictionary<string, Course> _courseCache = new();

    public CourseService()
    {
        _contentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", "courses");
    }

    public async Task<List<Course>> GetAllCoursesAsync()
    {
        var courses = new List<Course>();

        if (!Directory.Exists(_contentPath))
            return courses;

        foreach (var dir in Directory.GetDirectories(_contentPath))
        {
            var courseFile = Path.Combine(dir, "course.json");
            if (File.Exists(courseFile))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(courseFile);
                    var course = JsonSerializer.Deserialize<Course>(json);
                    if (course != null)
                    {
                        _courseCache[course.Id] = course;
                        courses.Add(course);
                    }
                }
                catch { /* Skip invalid courses */ }
            }
        }

        return courses;
    }

    public async Task<Course?> GetCourseAsync(string courseId)
    {
        if (_courseCache.TryGetValue(courseId, out var cached))
            return cached;

        await GetAllCoursesAsync();
        return _courseCache.GetValueOrDefault(courseId);
    }

    public async Task<Lesson?> GetLessonAsync(string courseId, string moduleId, string lessonId)
    {
        var course = await GetCourseAsync(courseId);
        if (course == null) return null;

        foreach (var module in course.Modules)
        {
            if (module.Id == moduleId)
            {
                foreach (var lesson in module.Lessons)
                {
                    if (lesson.Id == lessonId)
                        return lesson;
                }
            }
        }

        return null;
    }
}
```

**Step 3: Create NavigationService.cs**

```csharp
using System;
using System.Windows.Controls;

namespace CodeTutor.Wpf.Services;

public interface INavigationService
{
    event EventHandler<object>? Navigated;
    void NavigateTo(UserControl view, object? parameter = null);
    void GoBack();
    bool CanGoBack { get; }
}

public class NavigationService : INavigationService
{
    private readonly Stack<(UserControl View, object? Parameter)> _history = new();

    public event EventHandler<object>? Navigated;

    public bool CanGoBack => _history.Count > 1;

    public void NavigateTo(UserControl view, object? parameter = null)
    {
        _history.Push((view, parameter));
        Navigated?.Invoke(this, view);
    }

    public void GoBack()
    {
        if (_history.Count > 1)
        {
            _history.Pop();
            var (view, _) = _history.Peek();
            Navigated?.Invoke(this, view);
        }
    }
}
```

**Step 4: Commit infrastructure**

```bash
git add native-app-wpf/Models/ native-app-wpf/Services/
git commit -m "feat: add models and services for WPF app"
```

---

## Phase 4: Main Window Shell

### Task 4: Create Main Window with Navigation

**Files:**
- Modify: `native-app-wpf/MainWindow.xaml`
- Modify: `native-app-wpf/MainWindow.xaml.cs`

**Step 1: Create MainWindow.xaml**

```xml
<Window x:Class="CodeTutor.Wpf.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="Code Tutor"
        Width="1400" Height="900"
        MinWidth="1000" MinHeight="700"
        Background="{StaticResource BackgroundDarkBrush}"
        WindowStartupLocation="CenterScreen">

    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="280" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>

        <!-- Sidebar -->
        <Border Grid.Column="0"
                Background="{StaticResource BackgroundMediumBrush}"
                BorderBrush="{StaticResource BorderDefaultBrush}"
                BorderThickness="0,0,1,0">
            <DockPanel>
                <!-- App Header -->
                <Border DockPanel.Dock="Top" Padding="20,16">
                    <StackPanel>
                        <TextBlock Text="Code Tutor" Style="{StaticResource HeadingText}" />
                        <TextBlock Text="Learn to code interactively"
                                   Style="{StaticResource CaptionText}"
                                   Margin="0,4,0,0" />
                    </StackPanel>
                </Border>

                <!-- Navigation Content -->
                <ContentControl x:Name="SidebarContent" />
            </DockPanel>
        </Border>

        <!-- Main Content -->
        <ContentControl x:Name="MainContent" Grid.Column="1" />
    </Grid>
</Window>
```

**Step 2: Create MainWindow.xaml.cs with DI**

```csharp
using System.Windows;
using CodeTutor.Wpf.Services;
using CodeTutor.Wpf.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CodeTutor.Wpf;

public partial class MainWindow : Window
{
    private readonly INavigationService _navigation;
    private readonly ICourseService _courseService;

    public MainWindow()
    {
        InitializeComponent();

        // Set up services
        var services = new ServiceCollection();
        services.AddSingleton<ICourseService, CourseService>();
        services.AddSingleton<INavigationService, NavigationService>();
        var provider = services.BuildServiceProvider();

        _navigation = provider.GetRequiredService<INavigationService>();
        _courseService = provider.GetRequiredService<ICourseService>();

        // Subscribe to navigation
        _navigation.Navigated += (_, view) => MainContent.Content = view;

        // Navigate to landing page
        var landingPage = new LandingPage(_courseService, _navigation);
        _navigation.NavigateTo(landingPage);
    }

    public void SetSidebarContent(object content)
    {
        SidebarContent.Content = content;
    }
}
```

**Step 3: Commit main window**

```bash
git add native-app-wpf/MainWindow.xaml native-app-wpf/MainWindow.xaml.cs
git commit -m "feat: create main window shell with sidebar"
```

---

## Phase 5: Landing Page - Course Selection

### Task 5: Create Course Selection View

**Files:**
- Create: `native-app-wpf/Views/LandingPage.xaml`
- Create: `native-app-wpf/Views/LandingPage.xaml.cs`

**Step 1: Create LandingPage.xaml**

```xml
<UserControl x:Class="CodeTutor.Wpf.Views.LandingPage"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <ScrollViewer Style="{StaticResource DarkScrollViewer}" Padding="40">
        <StackPanel MaxWidth="1200">
            <!-- Header -->
            <TextBlock Text="Choose Your Language"
                       Style="{StaticResource TitleText}"
                       Margin="0,0,0,8" />
            <TextBlock Text="Select a programming language to start learning"
                       Style="{StaticResource BodyText}"
                       Foreground="{StaticResource TextSecondaryBrush}"
                       Margin="0,0,0,32" />

            <!-- Course Grid -->
            <ItemsControl x:Name="CourseList">
                <ItemsControl.ItemsPanel>
                    <ItemsPanelTemplate>
                        <WrapPanel Orientation="Horizontal" />
                    </ItemsPanelTemplate>
                </ItemsControl.ItemsPanel>
                <ItemsControl.ItemTemplate>
                    <DataTemplate>
                        <Button Style="{StaticResource GhostButton}"
                                Click="CourseCard_Click"
                                Tag="{Binding}"
                                Margin="0,0,16,16"
                                IsEnabled="{Binding IsRuntimeAvailable}">
                            <Border Style="{StaticResource CardBorder}"
                                    Width="320" Height="200">
                                <Grid>
                                    <Grid.RowDefinitions>
                                        <RowDefinition Height="Auto" />
                                        <RowDefinition Height="*" />
                                        <RowDefinition Height="Auto" />
                                    </Grid.RowDefinitions>

                                    <!-- Language Badge -->
                                    <Border Grid.Row="0"
                                            Background="{StaticResource AccentBlueBrush}"
                                            CornerRadius="4"
                                            Padding="8,4"
                                            HorizontalAlignment="Left">
                                        <TextBlock Text="{Binding Language}"
                                                   FontWeight="Bold"
                                                   Foreground="White"
                                                   FontSize="12" />
                                    </Border>

                                    <!-- Course Info -->
                                    <StackPanel Grid.Row="1" Margin="0,12,0,0">
                                        <TextBlock Text="{Binding Title}"
                                                   Style="{StaticResource SubheadingText}"
                                                   TextWrapping="Wrap" />
                                        <TextBlock Text="{Binding Description}"
                                                   Style="{StaticResource CaptionText}"
                                                   TextWrapping="Wrap"
                                                   MaxHeight="40"
                                                   Margin="0,8,0,0" />
                                    </StackPanel>

                                    <!-- Stats -->
                                    <StackPanel Grid.Row="2"
                                                Orientation="Horizontal">
                                        <TextBlock Style="{StaticResource CaptionText}">
                                            <Run Text="{Binding ModuleCount}" />
                                            <Run Text=" modules" />
                                        </TextBlock>
                                        <TextBlock Style="{StaticResource CaptionText}"
                                                   Margin="16,0,0,0">
                                            <Run Text="{Binding EstimatedHours}" />
                                            <Run Text="h" />
                                        </TextBlock>
                                    </StackPanel>
                                </Grid>
                            </Border>
                        </Button>
                    </DataTemplate>
                </ItemsControl.ItemTemplate>
            </ItemsControl>

            <!-- Loading indicator -->
            <TextBlock x:Name="LoadingText"
                       Text="Loading courses..."
                       Style="{StaticResource BodyText}"
                       Visibility="Collapsed" />
        </StackPanel>
    </ScrollViewer>
</UserControl>
```

**Step 2: Create LandingPage.xaml.cs**

```csharp
using System.Windows;
using System.Windows.Controls;
using CodeTutor.Wpf.Models;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Views;

public partial class LandingPage : UserControl
{
    private readonly ICourseService _courseService;
    private readonly INavigationService _navigation;

    public LandingPage(ICourseService courseService, INavigationService navigation)
    {
        InitializeComponent();
        _courseService = courseService;
        _navigation = navigation;
        Loaded += LandingPage_Loaded;
    }

    private async void LandingPage_Loaded(object sender, RoutedEventArgs e)
    {
        LoadingText.Visibility = Visibility.Visible;
        var courses = await _courseService.GetAllCoursesAsync();
        CourseList.ItemsSource = courses;
        LoadingText.Visibility = Visibility.Collapsed;
    }

    private void CourseCard_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Course course)
        {
            var coursePage = new CoursePage(_courseService, _navigation, course);
            _navigation.NavigateTo(coursePage, course);
        }
    }
}
```

**Step 3: Commit landing page**

```bash
git add native-app-wpf/Views/LandingPage.xaml native-app-wpf/Views/LandingPage.xaml.cs
git commit -m "feat: create landing page with course cards"
```

---

## Phase 6: Course Page with Sidebar Navigation

### Task 6: Create Course View and Sidebar

**Files:**
- Create: `native-app-wpf/Views/CoursePage.xaml`
- Create: `native-app-wpf/Views/CourseSidebar.xaml`

**Step 1: Create CourseSidebar.xaml**

```xml
<UserControl x:Class="CodeTutor.Wpf.Views.CourseSidebar"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <ScrollViewer Style="{StaticResource DarkScrollViewer}">
        <StackPanel Margin="12">
            <!-- Back Button -->
            <Button Style="{StaticResource GhostButton}"
                    Click="BackButton_Click"
                    Margin="0,0,0,12">
                <StackPanel Orientation="Horizontal">
                    <TextBlock Text="← " />
                    <TextBlock Text="All Courses" />
                </StackPanel>
            </Button>

            <!-- Course Title -->
            <TextBlock x:Name="CourseTitle"
                       Style="{StaticResource SubheadingText}"
                       TextWrapping="Wrap"
                       Margin="8,0,0,16" />

            <!-- Modules List -->
            <ItemsControl x:Name="ModulesList">
                <ItemsControl.ItemTemplate>
                    <DataTemplate>
                        <StackPanel Margin="0,0,0,4">
                            <!-- Module Header -->
                            <Button Style="{StaticResource SidebarItemButton}"
                                    Click="ModuleHeader_Click"
                                    Tag="{Binding}"
                                    HorizontalAlignment="Stretch">
                                <Grid>
                                    <Grid.ColumnDefinitions>
                                        <ColumnDefinition Width="*" />
                                        <ColumnDefinition Width="Auto" />
                                    </Grid.ColumnDefinitions>
                                    <TextBlock Text="{Binding Title}"
                                               Style="{StaticResource CaptionText}"
                                               TextWrapping="Wrap" />
                                    <TextBlock x:Name="ExpandIcon"
                                               Grid.Column="1"
                                               Text="▼"
                                               Style="{StaticResource CaptionText}" />
                                </Grid>
                            </Button>

                            <!-- Lessons (Expandable) -->
                            <ItemsControl ItemsSource="{Binding Lessons}"
                                          x:Name="LessonsList"
                                          Margin="16,4,0,0">
                                <ItemsControl.ItemTemplate>
                                    <DataTemplate>
                                        <Button Style="{StaticResource SidebarItemButton}"
                                                Click="LessonItem_Click"
                                                Tag="{Binding}"
                                                HorizontalAlignment="Stretch">
                                            <TextBlock Text="{Binding Title}"
                                                       Style="{StaticResource CaptionText}"
                                                       TextWrapping="Wrap" />
                                        </Button>
                                    </DataTemplate>
                                </ItemsControl.ItemTemplate>
                            </ItemsControl>
                        </StackPanel>
                    </DataTemplate>
                </ItemsControl.ItemTemplate>
            </ItemsControl>
        </StackPanel>
    </ScrollViewer>
</UserControl>
```

**Step 2: Commit course navigation**

```bash
git add native-app-wpf/Views/CourseSidebar.xaml native-app-wpf/Views/CoursePage.xaml
git commit -m "feat: create course page with sidebar navigation"
```

---

## Phase 7: Lesson Page with Content Sections

### Task 7: Create Lesson Content View

**Files:**
- Create: `native-app-wpf/Views/LessonPage.xaml`
- Create: `native-app-wpf/Views/LessonPage.xaml.cs`

**Step 1: Create LessonPage.xaml**

```xml
<UserControl x:Class="CodeTutor.Wpf.Views.LessonPage"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:markdig="clr-namespace:Markdig.Wpf;assembly=Markdig.Wpf">

    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
            <RowDefinition Height="Auto" />
        </Grid.RowDefinitions>

        <!-- Lesson Header -->
        <Border Grid.Row="0"
                Background="{StaticResource BackgroundMediumBrush}"
                Padding="40,24">
            <StackPanel>
                <TextBlock x:Name="LessonTitle"
                           Style="{StaticResource HeadingText}" />
                <StackPanel Orientation="Horizontal" Margin="0,8,0,0">
                    <TextBlock x:Name="LessonTime"
                               Style="{StaticResource CaptionText}" />
                    <TextBlock x:Name="LessonDifficulty"
                               Style="{StaticResource CaptionText}"
                               Margin="16,0,0,0" />
                </StackPanel>
            </StackPanel>
        </Border>

        <!-- Lesson Content -->
        <ScrollViewer Grid.Row="1"
                      Style="{StaticResource DarkScrollViewer}"
                      Padding="40,32">
            <StackPanel x:Name="ContentPanel" MaxWidth="800">
                <!-- Content sections will be added dynamically -->
            </StackPanel>
        </ScrollViewer>

        <!-- Footer Navigation -->
        <Border Grid.Row="2"
                Background="{StaticResource BackgroundMediumBrush}"
                Padding="40,16">
            <Grid>
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="*" />
                    <ColumnDefinition Width="Auto" />
                    <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>

                <Button x:Name="PrevButton"
                        Grid.Column="0"
                        Style="{StaticResource SecondaryButton}"
                        Click="PrevButton_Click"
                        HorizontalAlignment="Left"
                        Visibility="Collapsed">
                    <StackPanel Orientation="Horizontal">
                        <TextBlock Text="← " />
                        <TextBlock Text="Previous" />
                    </StackPanel>
                </Button>

                <Button x:Name="CompleteButton"
                        Grid.Column="1"
                        Style="{StaticResource PrimaryButton}"
                        Click="CompleteButton_Click">
                    <TextBlock Text="Mark Complete" />
                </Button>

                <Button x:Name="NextButton"
                        Grid.Column="2"
                        Style="{StaticResource PrimaryButton}"
                        Click="NextButton_Click"
                        HorizontalAlignment="Right"
                        Visibility="Collapsed">
                    <StackPanel Orientation="Horizontal">
                        <TextBlock Text="Next" />
                        <TextBlock Text=" →" />
                    </StackPanel>
                </Button>
            </Grid>
        </Border>
    </Grid>
</UserControl>
```

**Step 2: Commit lesson page**

```bash
git add native-app-wpf/Views/LessonPage.xaml native-app-wpf/Views/LessonPage.xaml.cs
git commit -m "feat: create lesson page with content sections"
```

---

## Phase 8: Content Section Controls

### Task 8: Create Section Display Controls

**Files:**
- Create: `native-app-wpf/Controls/TheorySection.xaml`
- Create: `native-app-wpf/Controls/CodeExampleSection.xaml`
- Create: `native-app-wpf/Controls/KeyPointSection.xaml`

**Step 1: Create TheorySection.xaml**

```xml
<UserControl x:Class="CodeTutor.Wpf.Controls.TheorySection"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:markdig="clr-namespace:Markdig.Wpf;assembly=Markdig.Wpf">

    <StackPanel Margin="0,0,0,24">
        <TextBlock x:Name="SectionTitle"
                   Style="{StaticResource SubheadingText}"
                   Margin="0,0,0,12" />
        <markdig:MarkdownViewer x:Name="ContentViewer"
                                 Foreground="{StaticResource TextPrimaryBrush}" />
    </StackPanel>
</UserControl>
```

**Step 2: Create CodeExampleSection.xaml**

```xml
<UserControl x:Class="CodeTutor.Wpf.Controls.CodeExampleSection"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:avalonEdit="http://icsharpcode.net/sharpdevelop/avalonedit">

    <Border Style="{StaticResource CardBorder}"
            Background="#0D1117"
            Margin="0,0,0,24">
        <StackPanel>
            <TextBlock x:Name="ExampleTitle"
                       Style="{StaticResource SubheadingText}"
                       Margin="0,0,0,12" />

            <!-- Code Block -->
            <Border Background="{StaticResource BackgroundDarkBrush}"
                    CornerRadius="6"
                    Padding="4">
                <avalonEdit:TextEditor x:Name="CodeEditor"
                                        IsReadOnly="True"
                                        FontFamily="{StaticResource MonoFont}"
                                        FontSize="13"
                                        ShowLineNumbers="True"
                                        Background="Transparent"
                                        Foreground="{StaticResource TextPrimaryBrush}"
                                        LineNumbersForeground="{StaticResource TextMutedBrush}"
                                        HorizontalScrollBarVisibility="Auto"
                                        VerticalScrollBarVisibility="Auto" />
            </Border>

            <!-- Description -->
            <TextBlock x:Name="Description"
                       Style="{StaticResource CaptionText}"
                       TextWrapping="Wrap"
                       Margin="0,12,0,0" />
        </StackPanel>
    </Border>
</UserControl>
```

**Step 3: Create KeyPointSection.xaml**

```xml
<UserControl x:Class="CodeTutor.Wpf.Controls.KeyPointSection"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">

    <Border Style="{StaticResource CardBorder}"
            Background="#1A2F1A"
            BorderBrush="{StaticResource AccentGreenBrush}"
            Margin="0,0,0,24">
        <StackPanel>
            <StackPanel Orientation="Horizontal" Margin="0,0,0,12">
                <TextBlock Text="📝 " FontSize="18" />
                <TextBlock Text="Key Takeaways"
                           Style="{StaticResource SubheadingText}"
                           Foreground="{StaticResource AccentGreenBrush}" />
            </StackPanel>
            <TextBlock x:Name="Content"
                       Style="{StaticResource BodyText}"
                       TextWrapping="Wrap" />
        </StackPanel>
    </Border>
</UserControl>
```

**Step 4: Commit section controls**

```bash
git add native-app-wpf/Controls/
git commit -m "feat: create content section display controls"
```

---

## Phase 9: Coding Challenge Component

### Task 9: Create Interactive Challenge Control

**Files:**
- Create: `native-app-wpf/Controls/CodingChallenge.xaml`
- Create: `native-app-wpf/Controls/CodingChallenge.xaml.cs`

**Step 1: Create CodingChallenge.xaml**

```xml
<UserControl x:Class="CodeTutor.Wpf.Controls.CodingChallenge"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:avalonEdit="http://icsharpcode.net/sharpdevelop/avalonedit">

    <Border Style="{StaticResource CardBorder}" Margin="0,0,0,24">
        <StackPanel>
            <!-- Challenge Header -->
            <TextBlock x:Name="ChallengeTitle"
                       Style="{StaticResource SubheadingText}"
                       Margin="0,0,0,8" />
            <TextBlock x:Name="Description"
                       Style="{StaticResource BodyText}"
                       Foreground="{StaticResource TextSecondaryBrush}"
                       Margin="0,0,0,16" />

            <!-- Instructions -->
            <Border Background="{StaticResource BackgroundLightBrush}"
                    CornerRadius="6"
                    Padding="12"
                    Margin="0,0,0,16">
                <TextBlock x:Name="Instructions"
                           Style="{StaticResource CaptionText}"
                           TextWrapping="Wrap" />
            </Border>

            <!-- Code Editor -->
            <Border Background="#0D1117"
                    BorderBrush="{StaticResource BorderDefaultBrush}"
                    BorderThickness="1"
                    CornerRadius="6"
                    MinHeight="200"
                    Margin="0,0,0,16">
                <avalonEdit:TextEditor x:Name="CodeEditor"
                                        FontFamily="{StaticResource MonoFont}"
                                        FontSize="13"
                                        ShowLineNumbers="True"
                                        Background="Transparent"
                                        Foreground="{StaticResource TextPrimaryBrush}"
                                        LineNumbersForeground="{StaticResource TextMutedBrush}" />
            </Border>

            <!-- Action Buttons -->
            <StackPanel Orientation="Horizontal" Margin="0,0,0,16">
                <Button Style="{StaticResource PrimaryButton}"
                        Click="RunCode_Click">
                    <StackPanel Orientation="Horizontal">
                        <TextBlock Text="▶ " />
                        <TextBlock Text="Run Code" />
                    </StackPanel>
                </Button>

                <Button x:Name="HintButton"
                        Style="{StaticResource SecondaryButton}"
                        Click="ShowHint_Click"
                        Margin="12,0,0,0">
                    <TextBlock Text="Show Hint" />
                </Button>

                <Button Style="{StaticResource GhostButton}"
                        Click="ShowSolution_Click"
                        Margin="12,0,0,0">
                    <TextBlock Text="Show Solution" />
                </Button>

                <Button Style="{StaticResource GhostButton}"
                        Click="Reset_Click"
                        Margin="12,0,0,0">
                    <TextBlock Text="Reset" />
                </Button>
            </StackPanel>

            <!-- Output Panel -->
            <Border x:Name="OutputPanel"
                    Background="{StaticResource BackgroundDarkBrush}"
                    BorderBrush="{StaticResource BorderDefaultBrush}"
                    BorderThickness="1"
                    CornerRadius="6"
                    Padding="12"
                    MinHeight="80"
                    Visibility="Collapsed">
                <StackPanel>
                    <TextBlock Text="Output:"
                               Style="{StaticResource CaptionText}"
                               Margin="0,0,0,8" />
                    <TextBlock x:Name="OutputText"
                               Style="{StaticResource CodeText}"
                               TextWrapping="Wrap" />
                </StackPanel>
            </Border>

            <!-- Hint Panel -->
            <Border x:Name="HintPanel"
                    Background="#1A1F2A"
                    BorderBrush="{StaticResource AccentOrangeBrush}"
                    BorderThickness="1"
                    CornerRadius="6"
                    Padding="12"
                    Margin="0,8,0,0"
                    Visibility="Collapsed">
                <StackPanel>
                    <TextBlock Text="💡 Hint"
                               Style="{StaticResource CaptionText}"
                               Foreground="{StaticResource AccentOrangeBrush}"
                               Margin="0,0,0,4" />
                    <TextBlock x:Name="HintText"
                               Style="{StaticResource BodyText}"
                               TextWrapping="Wrap" />
                </StackPanel>
            </Border>
        </StackPanel>
    </Border>
</UserControl>
```

**Step 2: Commit challenge component**

```bash
git add native-app-wpf/Controls/CodingChallenge.xaml native-app-wpf/Controls/CodingChallenge.xaml.cs
git commit -m "feat: create interactive coding challenge control"
```

---

## Phase 10: Code Execution Service

### Task 10: Implement Code Runner

**Files:**
- Create: `native-app-wpf/Services/CodeExecutionService.cs`

**Step 1: Create CodeExecutionService.cs**

```csharp
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CodeTutor.Wpf.Services;

public interface ICodeExecutionService
{
    Task<ExecutionResult> ExecuteAsync(string code, string language);
}

public record ExecutionResult(bool Success, string Output, string Error);

public class CodeExecutionService : ICodeExecutionService
{
    public async Task<ExecutionResult> ExecuteAsync(string code, string language)
    {
        return language.ToLower() switch
        {
            "python" => await ExecutePythonAsync(code),
            "javascript" => await ExecuteJavaScriptAsync(code),
            "csharp" => await ExecuteCSharpAsync(code),
            _ => new ExecutionResult(false, "", $"Language '{language}' not supported")
        };
    }

    private async Task<ExecutionResult> ExecutePythonAsync(string code)
    {
        var tempFile = Path.GetTempFileName() + ".py";
        await File.WriteAllTextAsync(tempFile, code);

        try
        {
            var result = await RunProcessAsync("python", tempFile);
            return result;
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    private async Task<ExecutionResult> ExecuteJavaScriptAsync(string code)
    {
        var tempFile = Path.GetTempFileName() + ".js";
        await File.WriteAllTextAsync(tempFile, code);

        try
        {
            var result = await RunProcessAsync("node", tempFile);
            return result;
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    private async Task<ExecutionResult> ExecuteCSharpAsync(string code)
    {
        // Use dotnet-script or C# scripting
        // Simplified implementation
        return new ExecutionResult(false, "", "C# execution coming soon");
    }

    private async Task<ExecutionResult> RunProcessAsync(string command, string arguments)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            if (process == null)
                return new ExecutionResult(false, "", "Failed to start process");

            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            return new ExecutionResult(
                process.ExitCode == 0,
                output.Trim(),
                error.Trim()
            );
        }
        catch (Exception ex)
        {
            return new ExecutionResult(false, "", ex.Message);
        }
    }
}
```

**Step 2: Commit code execution**

```bash
git add native-app-wpf/Services/CodeExecutionService.cs
git commit -m "feat: add code execution service for Python and JavaScript"
```

---

## Phase 11: Final Integration and Testing

### Task 11: Wire Everything Together

**Step 1: Build the application**

```bash
cd native-app-wpf && dotnet build
```

Expected: Build succeeds with 0 errors

**Step 2: Run and test**

```bash
cd native-app-wpf && dotnet run
```

Test checklist:
- [ ] App launches with dark theme
- [ ] Course cards display correctly
- [ ] Clicking a course navigates to course view
- [ ] Sidebar shows modules and lessons
- [ ] Clicking a lesson shows content
- [ ] Content sections render properly (THEORY, EXAMPLE, KEY_POINT)
- [ ] Code editor is functional
- [ ] Run Code button executes Python code
- [ ] Hints display correctly
- [ ] Navigation (prev/next) works

**Step 3: Final commit**

```bash
git add -A && git commit -m "feat: complete WPF UI rebuild"
```

---

## Summary

This plan creates a clean WPF implementation with:

1. **Dark Theme Design System** - GitHub-inspired colors, consistent typography
2. **Three-Panel Layout** - Sidebar navigation + main content + code editor
3. **Native WPF Controls** - Proper templates, triggers, and styles
4. **Content Section Rendering** - THEORY, EXAMPLE, KEY_POINT displays
5. **Interactive Code Editor** - AvalonEdit with syntax highlighting
6. **Code Execution** - Python and JavaScript support
7. **Markdown Rendering** - Markdig.Wpf for lesson content

**New Project Structure:**
```
native-app-wpf/
├── Assets/
├── Controls/
│   ├── TheorySection.xaml
│   ├── CodeExampleSection.xaml
│   ├── KeyPointSection.xaml
│   └── CodingChallenge.xaml
├── Models/
│   └── Course.cs
├── Services/
│   ├── CourseService.cs
│   ├── NavigationService.cs
│   └── CodeExecutionService.cs
├── Themes/
│   ├── Colors.xaml
│   ├── Typography.xaml
│   └── Controls.xaml
├── Views/
│   ├── LandingPage.xaml
│   ├── CoursePage.xaml
│   ├── CourseSidebar.xaml
│   └── LessonPage.xaml
├── App.xaml
└── MainWindow.xaml
```

**Total Tasks:** 11
