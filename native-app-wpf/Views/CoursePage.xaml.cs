using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CodeTutor.Wpf.Models;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Views;

public partial class CoursePage : UserControl
{
    private readonly Course _course;
    private readonly ICourseService _courseService;
    private readonly INavigationService _navigation;

    public CoursePage(ICourseService courseService, INavigationService navigation, Course course)
    {
        InitializeComponent();
        _course = course;
        _courseService = courseService;
        _navigation = navigation;

        // Set up sidebar
        var mainWindow = Application.Current.MainWindow as MainWindow;
        var sidebar = new CourseSidebar(course, courseService, navigation);
        mainWindow?.SetSidebarContent(sidebar);

        // Populate course info
        LanguageBadge.Text = course.Language;
        CourseTitle.Text = course.Title;
        CourseDescription.Text = course.Description;
        ModuleCount.Text = course.Modules.Count.ToString();
        LessonCount.Text = course.Modules.Sum(m => m.Lessons.Count).ToString();
        EstimatedTime.Text = course.EstimatedHours.ToString();
    }

    private void StartButton_Click(object sender, RoutedEventArgs e)
    {
        var firstLesson = _course.Modules.FirstOrDefault()?.Lessons.FirstOrDefault();
        if (firstLesson != null)
        {
            var lessonPage = new LessonPage(_course, firstLesson, _courseService, _navigation);
            _navigation.NavigateTo(lessonPage, firstLesson);
        }
    }
}
