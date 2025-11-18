using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CodeTutor.Native.Data;
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

    public override async void OnFrameworkInitializationCompleted()
    {
        // Configure dependency injection
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();

        // Initialize database
        await InitializeDatabaseAsync();

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

    private async System.Threading.Tasks.Task InitializeDatabaseAsync()
    {
        try
        {
            var databaseService = _serviceProvider?.GetRequiredService<IDatabaseService>();
            if (databaseService != null)
            {
                await databaseService.InitializeAsync();
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            // Log error but don't crash the app
            Console.WriteLine($"Database initialization failed: {ex.Message}");
        }
    }

    private void ConfigureServices(ServiceCollection services)
    {
        // Database
        services.AddDbContext<CodeTutorDbContext>(options =>
        {
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var dbPath = System.IO.Path.Combine(appDataPath, "CodeTutor", "CodeTutor.db");
            options.UseSqlite($"Data Source={dbPath}");
        });

        // Services
        services.AddSingleton<IDatabaseService, DatabaseService>();
        services.AddSingleton<ICourseService, CourseService>();
        services.AddSingleton<ICodeExecutor, CodeExecutor>();
        services.AddScoped<IProgressService, ProgressService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IChallengeValidationService, ChallengeValidationService>();
        services.AddSingleton<IChallengeFactory, ChallengeFactory>();
        services.AddSingleton<ITextMateRegistryService, TextMateRegistryService>();
        services.AddSingleton<IEditorConfigurationService, EditorConfigurationService>();
        services.AddScoped<IAutoSaveService, AutoSaveService>();
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddScoped<IAchievementService, AchievementService>();
        services.AddScoped<IStreakService, StreakService>();

        // Main Window ViewModel
        services.AddSingleton<MainWindowViewModel>();

        // Page ViewModels (Transient so each navigation creates a new instance)
        services.AddTransient<LandingPageViewModel>();
        services.AddTransient<CoursePageViewModel>();
        services.AddTransient<LessonPageViewModel>();
        services.AddTransient<NotFoundPageViewModel>();
    }

    /// <summary>
    /// Expose service provider for controls that need DI
    /// </summary>
    public IServiceProvider? Services => _serviceProvider;
}
