using System;
using Avalonia;
using Avalonia.Controls;
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
            // Log error using ErrorHandlerService with fallback to user-visible message box
            try
            {
                var errorHandler = _serviceProvider?.GetService<IErrorHandlerService>();
                if (errorHandler != null)
                {
                    await errorHandler.HandleErrorAsync(ex, "Failed to initialize database", showToUser: true);
                }
                else
                {
                    // Fallback: Show message box if ErrorHandlerService unavailable
                    await ShowCriticalErrorDialogAsync(
                        "Database Initialization Failed",
                        $"Code Tutor could not initialize its database.\n\n" +
                        $"Error: {ex.Message}\n\n" +
                        $"The application may not function correctly. Please check file permissions " +
                        $"in your AppData folder or contact support."
                    );
                }
            }
            catch (Exception innerEx)
            {
                // Last resort: Show basic error dialog
                await ShowCriticalErrorDialogAsync(
                    "Critical Startup Error",
                    $"Code Tutor encountered a critical error during startup.\n\n" +
                    $"Database Error: {ex.Message}\n" +
                    $"Handler Error: {innerEx.Message}\n\n" +
                    $"Please restart the application or contact support."
                );
            }
        }
    }

    /// <summary>
    /// Shows a critical error dialog using bare Avalonia controls (no dependencies)
    /// Used when ErrorHandlerService is unavailable during startup
    /// </summary>
    private async System.Threading.Tasks.Task ShowCriticalErrorDialogAsync(string title, string message)
    {
        try
        {
            if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
                return;

            var mainWindow = desktop.MainWindow;

            await Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(async () =>
            {
                var errorWindow = new Window
                {
                    Title = title,
                    Width = 500,
                    Height = 300,
                    CanResize = false,
                    WindowStartupLocation = mainWindow != null
                        ? WindowStartupLocation.CenterOwner
                        : WindowStartupLocation.CenterScreen,
                    Background = Avalonia.Media.Brushes.White
                };

                var okButton = new Button
                {
                    Content = "OK",
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    Padding = new Thickness(40, 10),
                    Margin = new Thickness(0, 20, 0, 0)
                };

                okButton.Click += (s, e) => errorWindow.Close();

                errorWindow.Content = new StackPanel
                {
                    Margin = new Thickness(20),
                    Spacing = 15,
                    Children =
                    {
                        new TextBlock
                        {
                            Text = "⚠️ Critical Error",
                            FontSize = 18,
                            FontWeight = Avalonia.Media.FontWeight.Bold,
                            Foreground = Avalonia.Media.Brushes.Red
                        },
                        new TextBlock
                        {
                            Text = message,
                            TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                            FontSize = 13,
                            Foreground = Avalonia.Media.Brushes.Black
                        },
                        okButton
                    }
                };

                if (mainWindow != null)
                {
                    await errorWindow.ShowDialog(mainWindow);
                }
                else
                {
                    errorWindow.Show();
                    // Give user time to read the error
                    await System.Threading.Tasks.Task.Delay(5000);
                }
            });
        }
        catch
        {
            // Absolute last resort - write to console
            Console.WriteLine($"[CRITICAL ERROR] {title}: {message}");
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

        // Services (Register ErrorHandlerService first so other services can use it)
        services.AddSingleton<IErrorHandlerService, ErrorHandlerService>();
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

        // Page ViewModels (Scoped to match service lifetime - prevents DbContext disposal issues)
        services.AddScoped<LandingPageViewModel>();
        services.AddScoped<CoursePageViewModel>();
        services.AddScoped<LessonPageViewModel>();
        services.AddScoped<NotFoundPageViewModel>();
    }

    /// <summary>
    /// Expose service provider for controls that need DI
    /// </summary>
    public IServiceProvider? Services => _serviceProvider;
}
