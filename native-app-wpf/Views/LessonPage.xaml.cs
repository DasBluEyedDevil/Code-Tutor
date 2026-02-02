using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using CodeTutor.Wpf.Controls;
using CodeTutor.Wpf.Models;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Views;

public partial class LessonPage : UserControl
{
    private readonly Course _course;
    private readonly Lesson _lesson;
    private readonly ICourseService _courseService;
    private readonly INavigationService _navigation;
    private readonly IProgressService _progressService = new ProgressService();
    private readonly ITutorService _tutorService;
    private readonly IModelDownloadService _downloadService;
    private readonly List<Controls.CodingChallenge> _challengeControls = new();
    private TutorChat? _tutorChat;
    private bool _isTutorOpen = false;

    public LessonPage(Course course, Lesson lesson, ICourseService courseService, INavigationService navigation, ITutorService tutorService, IModelDownloadService downloadService)
    {
        InitializeComponent();
        _course = course;
        _lesson = lesson;
        _courseService = courseService;
        _navigation = navigation;
        _tutorService = tutorService;
        _downloadService = downloadService;

        // Refresh sidebar and highlight active lesson
        var mainWindow = Application.Current.MainWindow as MainWindow;
        var sidebar = new CourseSidebar(course, courseService, navigation, tutorService, downloadService);
        sidebar.SetCurrentLesson(lesson);
        mainWindow?.SetSidebarContent(sidebar);

        LoadLesson();
        SetupNavigation();
        CheckCompletionStatus();
    }

    private async void CheckCompletionStatus()
    {
        if (await _progressService.IsLessonCompleteAsync(_lesson.Id))
        {
            CompleteButton.Content = new TextBlock { Text = "Completed" };
            CompleteButton.IsEnabled = false;
        }
    }

    private async void LoadLesson()
    {
        LessonTitle.Text = _lesson.Title;
        LessonTime.Text = $"{_lesson.EstimatedMinutes} min";
        LessonDifficulty.Text = _lesson.Difficulty;

        // Set lesson position
        var allLessons = _course.Modules.SelectMany(m => m.Lessons).ToList();
        var currentIndex = allLessons.FindIndex(l => l.Id == _lesson.Id);
        var currentModule = _course.Modules.FirstOrDefault(m => m.Lessons.Any(l => l.Id == _lesson.Id));
        LessonPosition.Text = $"Module: {currentModule?.Title} • Lesson {currentIndex + 1} of {allLessons.Count}";

        // Set breadcrumb navigation
        BreadcrumbCourseText.Text = _course.Title;
        BreadcrumbModuleText.Text = currentModule?.Title ?? "";

        // Trigger lazy loading of content sections and challenges
        var moduleId = currentModule?.Id ?? _lesson.ModuleId;
        var loadedLesson = await _courseService.GetLessonAsync(_course.Id, moduleId, _lesson.Id);

        // Use the loaded lesson's content (in case it's a different reference)
        var contentSections = loadedLesson?.ContentSections ?? _lesson.ContentSections;
        var challenges = loadedLesson?.Challenges ?? _lesson.Challenges;

        // Add content sections dynamically
        ContentPanel.Children.Clear();

        foreach (var section in contentSections)
        {
            var sectionControl = CreateSectionControl(section);
            if (sectionControl != null)
            {
                ContentPanel.Children.Add(sectionControl);
            }
        }

        // Add challenges
        foreach (var challenge in challenges)
        {
            var challengeControl = new Controls.CodingChallenge(challenge);
            challengeControl.ChallengeCompleted += OnChallengeCompleted;
            _challengeControls.Add(challengeControl);
            ContentPanel.Children.Add(challengeControl);
        }
    }

    private UIElement? CreateSectionControl(ContentSection section)
    {
        return section.Type?.ToUpperInvariant() switch
        {
            "THEORY" => new Controls.TheorySection(section),
            "EXAMPLE" => new Controls.CodeExampleSection(section),
            "KEY_POINT" => new Controls.KeyPointSection(section),
            "ANALOGY" => new Controls.AnalogySection(section),
            "WARNING" => new Controls.WarningSection(section),
            "LEGACY_COMPARISON" => new Controls.LegacyComparisonSection(section),
            _ => CreateDefaultSection(section)
        };
    }

    private UIElement CreateDefaultSection(ContentSection section)
    {
        var container = new StackPanel { Margin = new Thickness(0, 0, 0, 24) };

        if (!string.IsNullOrEmpty(section.Title))
        {
            var title = new TextBlock
            {
                Text = section.Title,
                Style = (Style)FindResource("SubheadingText"),
                Margin = new Thickness(0, 0, 0, 12)
            };
            container.Children.Add(title);
        }

        var content = new TextBlock
        {
            Text = section.Content,
            Style = (Style)FindResource("BodyText"),
            TextWrapping = TextWrapping.Wrap
        };
        container.Children.Add(content);

        // Add code block if present
        if (!string.IsNullOrEmpty(section.Code))
        {
            var codeBorder = new Border
            {
                Background = (System.Windows.Media.Brush)FindResource("BackgroundDarkBrush"),
                BorderBrush = (System.Windows.Media.Brush)FindResource("BorderDefaultBrush"),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(6),
                Padding = new Thickness(16),
                Margin = new Thickness(0, 12, 0, 0)
            };

            var codeText = new TextBlock
            {
                Text = section.Code,
                Style = (Style)FindResource("CodeText"),
                TextWrapping = TextWrapping.Wrap
            };
            codeBorder.Child = codeText;
            container.Children.Add(codeBorder);
        }

        return container;
    }

    private void SetupNavigation()
    {
        var allLessons = _course.Modules.SelectMany(m => m.Lessons).ToList();
        var currentIndex = allLessons.FindIndex(l => l.Id == _lesson.Id);

        if (currentIndex > 0)
        {
            PrevButton.IsEnabled = true;
            PrevButton.Tag = allLessons[currentIndex - 1];
        }

        if (currentIndex < allLessons.Count - 1)
        {
            NextButton.IsEnabled = true;
            NextButton.Tag = allLessons[currentIndex + 1];
        }
    }

    private void PrevButton_Click(object sender, RoutedEventArgs e)
    {
        if (PrevButton.Tag is Lesson prevLesson)
        {
            var lessonPage = new LessonPage(_course, prevLesson, _courseService, _navigation, _tutorService, _downloadService);
            _navigation.NavigateTo(lessonPage, prevLesson);
        }
    }

    private void NextButton_Click(object sender, RoutedEventArgs e)
    {
        if (NextButton.Tag is Lesson nextLesson)
        {
            var lessonPage = new LessonPage(_course, nextLesson, _courseService, _navigation, _tutorService, _downloadService);
            _navigation.NavigateTo(lessonPage, nextLesson);
        }
    }

    private async void CompleteButton_Click(object sender, RoutedEventArgs e)
    {
        await _progressService.MarkLessonCompleteAsync(_lesson.Id);
        CompleteButton.Content = new TextBlock { Text = "Completed" };
        CompleteButton.IsEnabled = false;
    }

    private void BreadcrumbCourse_Click(object sender, RoutedEventArgs e)
    {
        var coursePage = new CoursePage(_courseService, _navigation, _course, _tutorService, _downloadService);
        _navigation.NavigateTo(coursePage, _course);
    }

    private async void OnChallengeCompleted(object? sender, string challengeId)
    {
        // Check if all challenges are now complete
        if (_challengeControls.Count > 0 && _challengeControls.All(c => c.IsCompleted))
        {
            // Auto-mark lesson as complete
            if (!await _progressService.IsLessonCompleteAsync(_lesson.Id))
            {
                await _progressService.MarkLessonCompleteAsync(_lesson.Id);
                CompleteButton.Content = new TextBlock { Text = "Completed" };
                CompleteButton.IsEnabled = false;
            }
        }
    }

    private void TutorToggleButton_Click(object sender, RoutedEventArgs e)
    {
        if (_isTutorOpen)
        {
            CloseTutorPanel();
        }
        else
        {
            OpenTutorPanel();
        }
    }

    private void OpenTutorPanel()
    {
        if (_tutorChat == null)
        {
            _tutorChat = new TutorChat(_tutorService, _downloadService);        
            _tutorChat.CloseRequested += (s, e) => CloseTutorPanel();
            TutorContent.Content = _tutorChat;
        }

        // Update context with current lesson info
        UpdateTutorContext();

        AnimateTutorPanel(true);
        _isTutorOpen = true;

        TutorToggleButton.Foreground = (Brush)FindResource("AccentGreenBrush"); 
    }

    private void CloseTutorPanel()
    {
        AnimateTutorPanel(false);
        _isTutorOpen = false;
        TutorToggleButton.Foreground = (Brush)FindResource("AccentPurpleBrush");
    }

    private void AnimateTutorPanel(bool open)
    {
        if (TutorPanel.RenderTransform is not TranslateTransform translate)
        {
            translate = new TranslateTransform();
            TutorPanel.RenderTransform = translate;
        }

        var duration = TimeSpan.FromMilliseconds(260);
        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };
        var targetWidth = open ? 380 : 0;
        var targetOpacity = open ? 1 : 0;
        var targetX = open ? 0 : 24;

        TutorPanel.BeginAnimation(WidthProperty, new DoubleAnimation
        {
            To = targetWidth,
            Duration = duration,
            EasingFunction = easing
        });

        TutorPanel.BeginAnimation(OpacityProperty, new DoubleAnimation
        {
            To = targetOpacity,
            Duration = duration,
            EasingFunction = easing
        });

        translate.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation
        {
            To = targetX,
            Duration = duration,
            EasingFunction = easing
        });
    }

    private void UpdateTutorContext()
    {
        if (_tutorChat == null || _lesson == null) return;

        var context = new TutorContext
        {
            CurrentLanguage = _course?.Language,
            LessonTitle = _lesson.Title,
            LessonContent = string.Join("\n", _lesson.ContentSections
                .Where(s => s.Type == "THEORY")
                .Select(s => s.Content))
        };

        _tutorChat.UpdateContext(context);
    }

}
