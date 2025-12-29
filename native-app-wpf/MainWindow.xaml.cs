using System.Windows;
using CodeTutor.Wpf.Services;
using CodeTutor.Wpf.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CodeTutor.Wpf;

public partial class MainWindow : Window
{
    private readonly INavigationService _navigation;
    private readonly ICourseService _courseService;
    private readonly ITutorService _tutorService;
    private readonly IModelDownloadService _downloadService;

    public MainWindow()
    {
        InitializeComponent();

        // Set up services
        var services = new ServiceCollection();
        services.AddSingleton<ICourseService, CourseService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<ITutorService, Phi4TutorService>();
        services.AddSingleton<IModelDownloadService, ModelDownloadService>();
        var provider = services.BuildServiceProvider();

        _navigation = provider.GetRequiredService<INavigationService>();
        _courseService = provider.GetRequiredService<ICourseService>();
        _tutorService = provider.GetRequiredService<ITutorService>();
        _downloadService = provider.GetRequiredService<IModelDownloadService>();

        // Subscribe to navigation
        _navigation.Navigated += (_, view) => MainContent.Content = view;

        // Navigate to landing page
        var landingPage = new LandingPage(_courseService, _navigation, _tutorService, _downloadService);
        _navigation.NavigateTo(landingPage);
    }

    public void SetSidebarContent(object content)
    {
        SidebarContent.Content = content;
    }
}
