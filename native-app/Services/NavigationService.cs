using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using CodeTutor.Native.ViewModels;

namespace CodeTutor.Native.Services;

/// <summary>
/// Navigation service implementation using stack-based navigation
/// </summary>
public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Stack<ViewModelBase> _navigationStack = new();
    private ViewModelBase? _currentViewModel;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ViewModelBase? CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            _currentViewModel = value;
            if (value != null)
            {
                CurrentViewModelChanged?.Invoke(this, value);
            }
        }
    }

    public bool CanGoBack => _navigationStack.Count > 0;

    public event EventHandler<ViewModelBase>? CurrentViewModelChanged;

    public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
    {
        NavigateTo<TViewModel>(null);
    }

    public void NavigateTo<TViewModel>(object? parameter) where TViewModel : ViewModelBase
    {
        // Push current view model to stack if it exists
        if (CurrentViewModel != null)
        {
            _navigationStack.Push(CurrentViewModel);
        }

        // Create new view model instance
        var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

        // If view model supports parameters, set it
        if (parameter != null && viewModel is INavigableViewModel navigable)
        {
            navigable.OnNavigatedTo(parameter);
        }

        CurrentViewModel = viewModel;
    }

    public void GoBack()
    {
        if (!CanGoBack)
        {
            return;
        }

        var previousViewModel = _navigationStack.Pop();
        CurrentViewModel = previousViewModel;

        // Notify view model it's being navigated back to
        if (previousViewModel is INavigableViewModel navigable)
        {
            navigable.OnNavigatedBack();
        }
    }

    public void ClearHistory()
    {
        _navigationStack.Clear();
    }
}

/// <summary>
/// Interface for view models that need navigation lifecycle events
/// </summary>
public interface INavigableViewModel
{
    /// <summary>
    /// Called when navigating to this view model with a parameter
    /// </summary>
    void OnNavigatedTo(object parameter);

    /// <summary>
    /// Called when navigating back to this view model
    /// </summary>
    void OnNavigatedBack();
}
