using System;
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
        try
        {
            LoadingText.Visibility = Visibility.Visible;
            var courses = await _courseService.GetAllCoursesAsync();
            CourseList.ItemsSource = courses;
            LoadingText.Visibility = Visibility.Collapsed;
        }
        catch (Exception ex)
        {
            LoadingText.Text = $"Failed to load courses: {ex.Message}";
        }
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
