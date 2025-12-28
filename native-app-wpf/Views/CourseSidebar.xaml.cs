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
            var lessonPage = new LessonPage(_course, lesson, _courseService, _navigation);
            _navigation.NavigateTo(lessonPage, lesson);
        }
    }
}
