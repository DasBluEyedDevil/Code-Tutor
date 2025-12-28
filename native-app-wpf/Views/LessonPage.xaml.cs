using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CodeTutor.Wpf.Models;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Views;

public partial class LessonPage : UserControl
{
    private readonly Course _course;
    private readonly Lesson _lesson;
    private readonly ICourseService _courseService;
    private readonly INavigationService _navigation;

    public LessonPage(Course course, Lesson lesson, ICourseService courseService, INavigationService navigation)
    {
        InitializeComponent();
        _course = course;
        _lesson = lesson;
        _courseService = courseService;
        _navigation = navigation;

        LoadLesson();
        SetupNavigation();
    }

    private void LoadLesson()
    {
        LessonTitle.Text = _lesson.Title;
        LessonTime.Text = $"{_lesson.EstimatedMinutes} min";
        LessonDifficulty.Text = _lesson.Difficulty;

        // Add content sections dynamically
        ContentPanel.Children.Clear();

        foreach (var section in _lesson.ContentSections)
        {
            var sectionControl = CreateSectionControl(section);
            if (sectionControl != null)
            {
                ContentPanel.Children.Add(sectionControl);
            }
        }

        // Add challenges
        foreach (var challenge in _lesson.Challenges)
        {
            var challengeText = new TextBlock
            {
                Text = $"Challenge: {challenge.Title}",
                Style = (Style)FindResource("SubheadingText"),
                Margin = new Thickness(0, 24, 0, 8)
            };
            ContentPanel.Children.Add(challengeText);

            var descText = new TextBlock
            {
                Text = challenge.Description,
                Style = (Style)FindResource("BodyText"),
                Margin = new Thickness(0, 0, 0, 16)
            };
            ContentPanel.Children.Add(descText);
        }
    }

    private UIElement? CreateSectionControl(ContentSection section)
    {
        return section.Type?.ToUpperInvariant() switch
        {
            "THEORY" => new Controls.TheorySection(section),
            "EXAMPLE" => new Controls.CodeExampleSection(section),
            "KEY_POINT" => new Controls.KeyPointSection(section),
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
            PrevButton.Visibility = Visibility.Visible;
            PrevButton.Tag = allLessons[currentIndex - 1];
        }

        if (currentIndex < allLessons.Count - 1)
        {
            NextButton.Visibility = Visibility.Visible;
            NextButton.Tag = allLessons[currentIndex + 1];
        }
    }

    private void PrevButton_Click(object sender, RoutedEventArgs e)
    {
        if (PrevButton.Tag is Lesson prevLesson)
        {
            var lessonPage = new LessonPage(_course, prevLesson, _courseService, _navigation);
            _navigation.NavigateTo(lessonPage, prevLesson);
        }
    }

    private void NextButton_Click(object sender, RoutedEventArgs e)
    {
        if (NextButton.Tag is Lesson nextLesson)
        {
            var lessonPage = new LessonPage(_course, nextLesson, _courseService, _navigation);
            _navigation.NavigateTo(lessonPage, nextLesson);
        }
    }

    private void CompleteButton_Click(object sender, RoutedEventArgs e)
    {
        // Mark lesson as complete (could integrate with progress tracking later)
        CompleteButton.Content = new TextBlock { Text = "Completed" };
        CompleteButton.IsEnabled = false;
    }
}
