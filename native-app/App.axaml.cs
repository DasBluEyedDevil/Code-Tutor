using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using CodeTutor.Native.Services;
using CodeTutor.Native.ViewModels;
using CodeTutor.Native.ViewModels.Pages;
using CodeTutor.Native.Views;

namespace CodeTutor.Native;

public partial class App : Application
{
    private IServiceProvider? _serviceProvider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Configure dependency injection
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Get navigation service and main window view model from DI
            var navigationService = _serviceProvider.GetRequiredService<INavigationService>();
            var mainWindowViewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();

            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };

            // Navigate to landing page on startup
            navigationService.NavigateTo<LandingPageViewModel>();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        // Services
        services.AddSingleton<ICourseService, CourseService>();
        services.AddSingleton<ICodeExecutor, CodeExecutor>();
        services.AddSingleton<IProgressService, ProgressService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IChallengeValidationService, ChallengeValidationService>();
        services.AddSingleton<IChallengeFactory, ChallengeFactory>();

        // Main Window ViewModel
        services.AddSingleton<MainWindowViewModel>();

        // Page ViewModels (Transient so each navigation creates a new instance)
        services.AddTransient<LandingPageViewModel>();
        services.AddTransient<CoursePageViewModel>();
        services.AddTransient<LessonPageViewModel>();
        services.AddTransient<NotFoundPageViewModel>();
    }
}
