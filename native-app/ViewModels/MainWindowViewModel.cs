using ReactiveUI;
using CodeTutor.Native.Services;

namespace CodeTutor.Native.ViewModels;

/// <summary>
/// Main window view model - Hosts the navigation service
/// </summary>
public class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private ViewModelBase? _currentPage;

    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        // Subscribe to navigation changes
        _navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;

        // Set initial page
        _currentPage = _navigationService.CurrentViewModel;
    }

    /// <summary>
    /// The currently displayed page view model
    /// </summary>
    public ViewModelBase? CurrentPage
    {
        get => _currentPage;
        private set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    private void OnCurrentViewModelChanged(object? sender, ViewModelBase viewModel)
    {
        CurrentPage = viewModel;
    }
}

/// <summary>
/// Base class for view models
/// </summary>
public class ViewModelBase : ReactiveObject
{
}
