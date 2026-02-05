using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using CodeTutor.Wpf.Controls;
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
    private Controls.TutorChat? _tutorChat;
    private bool _isTutorOpen = false;

    public MainWindow()
    {
        InitializeComponent();

        // Set up services
        var services = new ServiceCollection();
        services.AddSingleton<ICourseService, CourseService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<ITutorService, QwenTutorService>();
        services.AddSingleton<IModelDownloadService, ModelDownloadService>();
        var provider = services.BuildServiceProvider();

        _navigation = provider.GetRequiredService<INavigationService>();
        _courseService = provider.GetRequiredService<ICourseService>();
        _tutorService = provider.GetRequiredService<ITutorService>();
        _downloadService = provider.GetRequiredService<IModelDownloadService>();

        // Subscribe to navigation
        _navigation.Navigated += (_, view) =>
        {
            if (MainContent is AnimatedContentControl animated)
            {
                if (_navigation.IsBackNavigation)
                {
                    animated.NavigateBack(view);
                }
                else
                {
                    animated.NavigateForward(view);
                }
            }
            else
            {
                MainContent.Content = view;
            }
        };

        // Navigate to landing page
        var landingPage = new LandingPage(_courseService, _navigation, _tutorService, _downloadService);
        _navigation.NavigateTo(landingPage);
    }

    public void SetSidebarContent(object content)
    {
        SidebarContent.Content = content;
    }

    private void TutorToggleButton_Click(object sender, RoutedEventArgs e)
    {
        if (_isTutorOpen)
        {
            CloseTutorPanel();
        }
        else
        {
            OpenTutorPanel();
        }
    }

    private void OpenTutorPanel()
    {
        if (_tutorChat == null)
        {
            _tutorChat = new Controls.TutorChat(_tutorService, _downloadService);
            _tutorChat.CloseRequested += (s, e) => CloseTutorPanel();
            TutorContent.Content = _tutorChat;
        }

        AnimateTutorPanel(true);
        _isTutorOpen = true;
    }

    private void CloseTutorPanel()
    {
        AnimateTutorPanel(false);
        _isTutorOpen = false;
    }

    private void AnimateTutorPanel(bool open)
    {
        if (TutorPanel.RenderTransform is not TranslateTransform translate)
        {
            translate = new TranslateTransform();
            TutorPanel.RenderTransform = translate;
        }

        var duration = TimeSpan.FromMilliseconds(260);
        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };
        var targetWidth = open ? 380 : 0;
        var targetOpacity = open ? 1 : 0;
        var targetX = open ? 0 : 24;

        TutorPanel.BeginAnimation(WidthProperty, new DoubleAnimation
        {
            To = targetWidth,
            Duration = duration,
            EasingFunction = easing
        });

        TutorPanel.BeginAnimation(OpacityProperty, new DoubleAnimation
        {
            To = targetOpacity,
            Duration = duration,
            EasingFunction = easing
        });

        translate.BeginAnimation(TranslateTransform.XProperty, new DoubleAnimation
        {
            To = targetX,
            Duration = duration,
            EasingFunction = easing
        });
    }
}
