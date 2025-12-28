using System.Windows.Controls;
using CodeTutor.Wpf.Models;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Views;

public partial class CoursePage : UserControl
{
    private readonly ICourseService _courseService;
    private readonly INavigationService _navigation;
    private readonly Course _course;

    public CoursePage(ICourseService courseService, INavigationService navigation, Course course)
    {
        InitializeComponent();
        _courseService = courseService;
        _navigation = navigation;
        _course = course;
    }
}
