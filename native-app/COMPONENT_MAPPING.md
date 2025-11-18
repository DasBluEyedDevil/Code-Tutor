# React → Avalonia Component Mapping Guide

## Overview

This document maps all React components from the Electron app to their Avalonia equivalents in the native app.

---

## UI Framework Comparison

| Aspect | React (Electron) | Avalonia (Native) |
|--------|-----------------|-------------------|
| **Markup** | JSX (JavaScript) | XAML (XML) |
| **Styling** | Tailwind CSS classes | Avalonia Styles/Classes |
| **State** | useState, Zustand | ReactiveUI, ViewModels |
| **Events** | onClick, onChange | Command bindings |
| **Rendering** | Virtual DOM | Native UI tree |
| **Data Binding** | Props drilling | Two-way bindings |

---

## Core Component Mappings

### 1. Button

**React (Electron):**
```tsx
<Button
  className="bg-blue-500 text-white px-4 py-2 rounded"
  onClick={() => handleClick()}
  disabled={isLoading}
>
  Click Me
</Button>
```

**Avalonia (Native):**
```xml
<Button Classes="primary"
        Command="{Binding ClickCommand}"
        IsEnabled="{Binding !IsLoading}">
    Click Me
</Button>

<!-- Style definition in Themes/Dark.axaml -->
<Style Selector="Button.primary">
    <Setter Property="Background" Value="#0E639C"/>
    <Setter Property="Foreground" Value="White"/>
    <Setter Property="Padding" Value="16,8"/>
    <Setter Property="CornerRadius" Value="4"/>
</Style>
```

---

### 2. Card Component

**React (Electron):**
```tsx
<Card className="p-6 bg-card hover:bg-card-hover">
  <CardContent>
    <h2>{title}</h2>
    <p>{description}</p>
  </CardContent>
</Card>
```

**Avalonia (Native):**
```xml
<Border Classes="card hoverable" Padding="24">
    <StackPanel Spacing="8">
        <TextBlock Text="{Binding Title}"
                   Classes="h2" />
        <TextBlock Text="{Binding Description}"
                   TextWrapping="Wrap" />
    </StackPanel>
</Border>

<!-- Style -->
<Style Selector="Border.card">
    <Setter Property="Background" Value="#2D2D30"/>
    <Setter Property="CornerRadius" Value="8"/>
    <Setter Property="BorderBrush" Value="#3A3A3A"/>
    <Setter Property="BorderThickness" Value="1"/>
</Style>

<Style Selector="Border.card.hoverable:pointerover">
    <Setter Property="Background" Value="#353537"/>
</Style>
```

---

### 3. Progress Bar

**React (Electron):**
```tsx
<ProgressBar
  value={progress}
  max={100}
  className="w-full h-2 bg-gray-700"
/>
```

**Avalonia (Native):**
```xml
<ProgressBar Value="{Binding Progress}"
             Maximum="100"
             Height="8"
             Classes="course-progress" />

<!-- Style -->
<Style Selector="ProgressBar.course-progress">
    <Setter Property="Foreground" Value="#4EC9B0"/>
    <Setter Property="Background" Value="#3A3A3A"/>
</Style>
```

---

### 4. Loading Spinner

**React (Electron):**
```tsx
{isLoading && (
  <LoadingSpinner size="large" />
)}
```

**Avalonia (Native):**
```xml
<Border IsVisible="{Binding IsLoading}">
    <StackPanel HorizontalAlignment="Center"
                VerticalAlignment="Center"
                Spacing="16">
        <PathIcon Data="{StaticResource SpinnerIcon}"
                  Classes="spinning"
                  Width="48" Height="48" />
        <TextBlock Text="Loading..."
                   HorizontalAlignment="Center" />
    </StackPanel>
</Border>

<!-- Animation -->
<Style Selector="PathIcon.spinning">
    <Style.Animations>
        <Animation Duration="0:0:1" IterationCount="Infinite">
            <KeyFrame Cue="100%">
                <Setter Property="RotateTransform.Angle" Value="360"/>
            </KeyFrame>
        </Animation>
    </Style.Animations>
</Style>
```

---

### 5. Text Input

**React (Electron):**
```tsx
<input
  type="text"
  value={text}
  onChange={(e) => setText(e.target.value)}
  placeholder="Enter text..."
  className="border rounded px-3 py-2"
/>
```

**Avalonia (Native):**
```xml
<TextBox Text="{Binding Text}"
         Watermark="Enter text..."
         Classes="input" />

<!-- Style -->
<Style Selector="TextBox.input">
    <Setter Property="Padding" Value="12,8"/>
    <Setter Property="BorderBrush" Value="#3A3A3A"/>
    <Setter Property="BorderThickness" Value="1"/>
    <Setter Property="CornerRadius" Value="4"/>
    <Setter Property="Background" Value="#1E1E1E"/>
    <Setter Property="Foreground" Value="#D4D4D4"/>
</Style>
```

---

### 6. Modal/Dialog

**React (Electron):**
```tsx
{isOpen && (
  <Modal onClose={() => setIsOpen(false)}>
    <ModalHeader>
      <h2>Settings</h2>
    </ModalHeader>
    <ModalBody>
      {/* Content */}
    </ModalBody>
  </Modal>
)}
```

**Avalonia (Native):**
```csharp
// In ViewModel
public async Task ShowSettingsAsync()
{
    var dialog = new SettingsDialog
    {
        DataContext = new SettingsViewModel()
    };

    await dialog.ShowDialog(mainWindow);
}
```

```xml
<!-- SettingsDialog.axaml -->
<Window xmlns="https://github.com/avaloniaui"
        Title="Settings"
        Width="600" Height="500"
        WindowStartupLocation="CenterOwner">
    <Grid RowDefinitions="Auto,*,Auto">
        <!-- Header -->
        <TextBlock Grid.Row="0" Text="Settings"
                   Classes="h2" Padding="24,16" />

        <!-- Content -->
        <ScrollViewer Grid.Row="1">
            <!-- Settings content -->
        </ScrollViewer>

        <!-- Footer -->
        <StackPanel Grid.Row="2"
                   Orientation="Horizontal"
                   HorizontalAlignment="Right"
                   Spacing="8"
                   Padding="16">
            <Button Content="Cancel" Command="{Binding CancelCommand}" />
            <Button Content="Save" Command="{Binding SaveCommand}"
                    Classes="primary" />
        </StackPanel>
    </Grid>
</Window>
```

---

### 7. Toast Notification

**React (Electron):**
```tsx
toast.success("Challenge completed!", {
  duration: 3000
});
```

**Avalonia (Native):**
```csharp
// NotificationService
public class NotificationService : INotificationService
{
    public void ShowSuccess(string message, int duration = 3000)
    {
        var notification = new NotificationViewModel
        {
            Message = message,
            Type = NotificationType.Success,
            Duration = TimeSpan.FromMilliseconds(duration)
        };

        Notifications.Add(notification);

        // Auto-remove after duration
        Task.Delay(duration).ContinueWith(_ =>
        {
            Notifications.Remove(notification);
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    public ObservableCollection<NotificationViewModel> Notifications { get; } = new();
}
```

```xml
<!-- In MainWindow.axaml -->
<ItemsControl ItemsSource="{Binding NotificationService.Notifications}"
              VerticalAlignment="Top"
              HorizontalAlignment="Right"
              Margin="16">
    <ItemsControl.ItemTemplate>
        <DataTemplate>
            <Border Classes="notification success"
                    Padding="16"
                    Margin="0,8">
                <StackPanel Orientation="Horizontal" Spacing="12">
                    <PathIcon Data="{StaticResource CheckmarkIcon}"/>
                    <TextBlock Text="{Binding Message}" />
                </StackPanel>

                <!-- Fade in/out animation -->
                <Border.Transitions>
                    <Transitions>
                        <DoubleTransition Property="Opacity" Duration="0:0:0.3"/>
                    </Transitions>
                </Border.Transitions>
            </Border>
        </DataTemplate>
    </ItemsControl.ItemTemplate>
</ItemsControl>
```

---

### 8. Tab Control

**React (Electron):**
```tsx
<Tabs>
  <TabList>
    <Tab>Lessons</Tab>
    <Tab>Progress</Tab>
  </TabList>

  <TabPanel>
    <LessonsList />
  </TabPanel>
  <TabPanel>
    <ProgressView />
  </TabPanel>
</Tabs>
```

**Avalonia (Native):**
```xml
<TabControl>
    <TabItem Header="Lessons">
        <ContentControl Content="{Binding LessonsViewModel}" />
    </TabItem>

    <TabItem Header="Progress">
        <ContentControl Content="{Binding ProgressViewModel}" />
    </TabItem>
</TabControl>
```

---

### 9. Command Palette

**React (Electron):**
```tsx
<CommandPalette
  isOpen={isOpen}
  onClose={() => setIsOpen(false)}
  commands={commands}
  onExecute={(cmd) => handleCommand(cmd)}
/>
```

**Avalonia (Native):**
```csharp
// CommandPaletteViewModel
public class CommandPaletteViewModel : ViewModelBase
{
    private string _searchText = string.Empty;
    private ObservableCollection<CommandItem> _filteredCommands = new();

    public string SearchText
    {
        get => _searchText;
        set
        {
            this.RaiseAndSetIfChanged(ref _searchText, value);
            FilterCommands();
        }
    }

    private void FilterCommands()
    {
        var filtered = _allCommands
            .Where(c => c.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            .ToList();

        FilteredCommands = new ObservableCollection<CommandItem>(filtered);
    }

    public ReactiveCommand<CommandItem, Unit> ExecuteCommand { get; }
}
```

```xml
<!-- CommandPalette.axaml -->
<Window Width="600" Height="400"
        WindowStartupLocation="CenterOwner"
        Background="Transparent"
        TransparencyLevelHint="Transparent">
    <Border Classes="command-palette">
        <Grid RowDefinitions="Auto,*">
            <!-- Search Input -->
            <TextBox Grid.Row="0"
                     Text="{Binding SearchText}"
                     Watermark="Type a command..."
                     FontSize="16"
                     Padding="16"
                     BorderThickness="0,0,0,1" />

            <!-- Commands List -->
            <ListBox Grid.Row="1"
                     ItemsSource="{Binding FilteredCommands}"
                     SelectedItem="{Binding SelectedCommand}">
                <ListBox.ItemTemplate>
                    <DataTemplate>
                        <StackPanel Orientation="Horizontal"
                                   Spacing="12"
                                   Padding="12,8">
                            <PathIcon Data="{Binding Icon}"/>
                            <StackPanel>
                                <TextBlock Text="{Binding Title}"
                                          FontWeight="Bold" />
                                <TextBlock Text="{Binding Description}"
                                          FontSize="11"
                                          Opacity="0.7" />
                            </StackPanel>
                            <TextBlock Text="{Binding Shortcut}"
                                      HorizontalAlignment="Right"
                                      VerticalAlignment="Center"
                                      Opacity="0.5" />
                        </StackPanel>
                    </DataTemplate>
                </ListBox.ItemTemplate>
            </ListBox>
        </Grid>
    </Border>
</Window>
```

---

## Page-Level Component Mappings

### Landing Page

**React:**
```tsx
export default function LandingPage() {
  const [courses, setCourses] = useState<Course[]>([]);

  useEffect(() => {
    fetchCourses().then(setCourses);
  }, []);

  return (
    <div className="container">
      <h1>Choose a Course</h1>
      <div className="grid grid-cols-3 gap-4">
        {courses.map(course => (
          <Card key={course.id} onClick={() => navigate(`/course/${course.id}`)}>
            <h2>{course.title}</h2>
            <p>{course.description}</p>
          </Card>
        ))}
      </div>
    </div>
  );
}
```

**Avalonia:**
```csharp
public class LandingPageViewModel : ViewModelBase
{
    private readonly ICourseService _courseService;
    private readonly INavigationService _navigation;
    private ObservableCollection<CourseInfo> _courses = new();
    private bool _isLoading;

    public LandingPageViewModel(ICourseService courseService, INavigationService navigation)
    {
        _courseService = courseService;
        _navigation = navigation;

        LoadCoursesAsync();
    }

    public ObservableCollection<CourseInfo> Courses
    {
        get => _courses;
        set => this.RaiseAndSetIfChanged(ref _courses, value);
    }

    public bool IsLoading
    {
        get => _isLoading;
        set => this.RaiseAndSetIfChanged(ref _isLoading, value);
    }

    public ReactiveCommand<CourseInfo, Unit> SelectCourseCommand { get; }

    private async void LoadCoursesAsync()
    {
        IsLoading = true;
        try
        {
            var courses = await _courseService.GetCoursesAsync();
            Courses = new ObservableCollection<CourseInfo>(courses);
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void SelectCourse(CourseInfo course)
    {
        _navigation.NavigateTo<CoursePageViewModel>(course.Id);
    }
}
```

```xml
<UserControl xmlns="https://github.com/avaloniaui">
    <Grid RowDefinitions="Auto,*">
        <!-- Header -->
        <TextBlock Grid.Row="0"
                   Text="Choose a Course"
                   Classes="h1"
                   Margin="32,24" />

        <!-- Loading State -->
        <ContentControl Grid.Row="1"
                       IsVisible="{Binding IsLoading}">
            <StackPanel HorizontalAlignment="Center"
                       VerticalAlignment="Center">
                <PathIcon Data="{StaticResource SpinnerIcon}"
                         Classes="spinning" />
                <TextBlock Text="Loading courses..." />
            </StackPanel>
        </ContentControl>

        <!-- Courses Grid -->
        <ItemsControl Grid.Row="1"
                     ItemsSource="{Binding Courses}"
                     IsVisible="{Binding !IsLoading}">
            <ItemsControl.ItemsPanel>
                <ItemsPanelTemplate>
                    <UniformGrid Columns="3" />
                </ItemsPanelTemplate>
            </ItemsControl.ItemsPanel>

            <ItemsControl.ItemTemplate>
                <DataTemplate>
                    <Button Classes="card-button"
                            Command="{Binding $parent[ItemsControl].DataContext.SelectCourseCommand}"
                            CommandParameter="{Binding}"
                            Margin="16">
                        <StackPanel Spacing="12" Padding="24">
                            <TextBlock Text="{Binding Title}"
                                      Classes="h2" />
                            <TextBlock Text="{Binding Description}"
                                      TextWrapping="Wrap"
                                      Opacity="0.8" />
                            <StackPanel Orientation="Horizontal" Spacing="16">
                                <TextBlock Text="{Binding Difficulty}" />
                                <TextBlock Text="{Binding EstimatedHours, StringFormat='{}{0} hours'}" />
                            </StackPanel>
                        </StackPanel>
                    </Button>
                </DataTemplate>
            </ItemsControl.ItemTemplate>
        </ItemsControl>
    </Grid>
</UserControl>
```

---

## State Management Migration

### Zustand Store → ReactiveUI ViewModel

**React (Zustand):**
```typescript
const useProgressStore = create<ProgressStore>((set) => ({
  progress: {},
  updateProgress: (lessonId, score) => set((state) => ({
    progress: {
      ...state.progress,
      [lessonId]: { score, completed: score >= 70 }
    }
  }))
}));

// Usage in component
const { progress, updateProgress } = useProgressStore();
```

**Avalonia (ReactiveUI):**
```csharp
public class ProgressService : IProgressService
{
    private readonly CodeTutorDbContext _db;
    private readonly IEventAggregator _events;

    public async Task UpdateProgressAsync(string lessonId, int score)
    {
        var progress = await _db.Progress
            .FirstOrDefaultAsync(p => p.LessonId == lessonId);

        if (progress == null)
        {
            progress = new UserProgress
            {
                LessonId = lessonId,
                Score = score,
                Completed = score >= 70
            };
            _db.Progress.Add(progress);
        }
        else
        {
            progress.Score = Math.Max(progress.Score, score);
            progress.Completed = progress.Score >= 70;
        }

        await _db.SaveChangesAsync();

        // Notify other ViewModels
        _events.Publish(new ProgressUpdatedEvent
        {
            LessonId = lessonId,
            Score = score
        });
    }
}

// Usage in ViewModel
public class LessonPageViewModel : ViewModelBase
{
    private readonly IProgressService _progressService;

    public async Task OnChallengeCompletedAsync(int score)
    {
        await _progressService.UpdateProgressAsync(_lessonId, score);
    }
}
```

---

## Event Handling Migration

### React Event Handlers

**React:**
```tsx
// Click handler
<button onClick={() => handleClick(data)}>
  Click Me
</button>

// Change handler
<input
  value={text}
  onChange={(e) => setText(e.target.value)}
/>

// Submit handler
<form onSubmit={(e) => {
  e.preventDefault();
  handleSubmit();
}}>
```

**Avalonia:**
```xml
<!-- Click handler via Command -->
<Button Command="{Binding ClickCommand}"
        CommandParameter="{Binding Data}">
    Click Me
</Button>

<!-- Change handler via Two-Way Binding -->
<TextBox Text="{Binding Text, Mode=TwoWay}" />

<!-- Submit handler via Command -->
<Grid>
    <!-- Input fields -->
    <Button Content="Submit"
            Command="{Binding SubmitCommand}" />
</Grid>
```

```csharp
public class MyViewModel : ViewModelBase
{
    public ReactiveCommand<object, Unit> ClickCommand { get; }
    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }

    public MyViewModel()
    {
        ClickCommand = ReactiveCommand.Create<object>(HandleClick);
        SubmitCommand = ReactiveCommand.Create(HandleSubmit);
    }

    private void HandleClick(object data)
    {
        // Handle click with data parameter
    }

    private void HandleSubmit()
    {
        // Handle form submission
    }
}
```

---

## Styling Migration

### Tailwind → Avalonia Styles

**Tailwind Classes:**
```
bg-blue-500    →  Background="#0E639C"
text-white     →  Foreground="White"
px-4 py-2      →  Padding="16,8"
rounded        →  CornerRadius="4"
font-bold      →  FontWeight="Bold"
text-xl        →  FontSize="20"
opacity-50     →  Opacity="0.5"
```

**Create reusable styles:**
```xml
<!-- Themes/Common.axaml -->
<Styles xmlns="https://github.com/avaloniaui">
    <!-- Typography -->
    <Style Selector="TextBlock.h1">
        <Setter Property="FontSize" Value="32"/>
        <Setter Property="FontWeight" Value="Bold"/>
    </Style>

    <Style Selector="TextBlock.h2">
        <Setter Property="FontSize" Value="24"/>
        <Setter Property="FontWeight" Value="Bold"/>
    </Style>

    <!-- Buttons -->
    <Style Selector="Button.primary">
        <Setter Property="Background" Value="#0E639C"/>
        <Setter Property="Foreground" Value="White"/>
        <Setter Property="Padding" Value="16,8"/>
        <Setter Property="CornerRadius" Value="4"/>
    </Style>

    <!-- Spacing -->
    <Style Selector="StackPanel.spacing-sm">
        <Setter Property="Spacing" Value="8"/>
    </Style>

    <Style Selector="StackPanel.spacing-md">
        <Setter Property="Spacing" Value="16"/>
    </Style>
</Styles>
```

---

## Animation Migration

**React (Framer Motion):**
```tsx
<motion.div
  initial={{ opacity: 0, y: 20 }}
  animate={{ opacity: 1, y: 0 }}
  transition={{ duration: 0.3 }}
>
  Content
</motion.div>
```

**Avalonia (Transitions):**
```xml
<Border>
    <Border.Transitions>
        <Transitions>
            <DoubleTransition Property="Opacity" Duration="0:0:0.3"/>
            <TransformOperationsTransition Property="RenderTransform"
                                          Duration="0:0:0.3"/>
        </Transitions>
    </Border.Transitions>

    <!-- Content -->
</Border>
```

---

**Document Version:** 1.0
**Last Updated:** 2025-11-18
