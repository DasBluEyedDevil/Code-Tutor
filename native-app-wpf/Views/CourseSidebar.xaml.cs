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

    public CourseSidebar(Course course, ICourseService courseService, INavigationService navigation)
    {
        InitializeComponent();
        _course = course;
        _courseService = courseService;
        _navigation = navigation;

        CourseTitle.Text = course.Title;
        ModulesList.ItemsSource = course.Modules;
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        _navigation.GoBack();
    }

    private void ModuleHeader_Click(object sender, RoutedEventArgs e)
    {
        // Toggle expand/collapse could be added here
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
