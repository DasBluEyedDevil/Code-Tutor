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
    }
}
