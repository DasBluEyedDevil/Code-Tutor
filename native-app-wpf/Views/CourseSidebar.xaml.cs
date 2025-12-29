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
    private readonly IProgressService _progressService = new ProgressService();
    private readonly ITutorService _tutorService;
    private readonly IModelDownloadService _downloadService;
    private Lesson? _currentLesson;
    private readonly Dictionary<string, Button> _lessonButtons = new();

    public CourseSidebar(Course course, ICourseService courseService, INavigationService navigation, ITutorService tutorService, IModelDownloadService downloadService)
    {
        InitializeComponent();
        _course = course;
        _courseService = courseService;
        _navigation = navigation;
        _tutorService = tutorService;
        _downloadService = downloadService;

        CourseTitle.Text = course.Title;
        ModulesList.ItemsSource = course.Modules;

        foreach (var module in course.Modules)
        {
            _expandedModules[module.Id] = true;
        }
    }

    public void SetCurrentLesson(Lesson lesson)
    {
        _currentLesson = lesson;
        UpdateLessonStyles();
    }

    private async void UpdateLessonStyles()
    {
        var progress = await _progressService.LoadProgressAsync();

        foreach (var (lessonId, button) in _lessonButtons)
        {
            if (_currentLesson?.Id == lessonId)
            {
                button.Style = (Style)FindResource("ActiveSidebarItemButton");
            }
            else if (progress.CompletedLessons.Contains(lessonId))
            {
                button.Style = (Style)FindResource("CompletedSidebarItemButton");
            }
            else
            {
                button.Style = (Style)FindResource("SidebarItemButton");
            }
        }
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        _navigation.GoBack();
    }

    private void CourseOverview_Click(object sender, RoutedEventArgs e)
    {
        var coursePage = new CoursePage(_courseService, _navigation, _course, _tutorService, _downloadService);
        _navigation.NavigateTo(coursePage, _course);
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
            if (button.Content is Grid grid)
            {
                foreach (var child in grid.Children)
                {
                    if (child is TextBlock chevron && chevron.Name == "ExpandIcon")
                    {
                        chevron.Text = _expandedModules[module.Id] ? "\u25BC" : "\u25B6";
                    }
                }
            }
        }
    }

    private void LessonItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Lesson lesson)
        {
            // Track button in dictionary if not already tracked
            if (!_lessonButtons.ContainsKey(lesson.Id))
            {
                _lessonButtons[lesson.Id] = button;
            }

            _currentLesson = lesson;
            UpdateLessonStyles();

            var lessonPage = new LessonPage(_course, lesson, _courseService, _navigation, _tutorService, _downloadService);
            _navigation.NavigateTo(lessonPage, lesson);
        }
    }
}
