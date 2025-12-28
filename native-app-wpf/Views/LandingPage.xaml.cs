using System.Windows.Controls;
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
    }
}
