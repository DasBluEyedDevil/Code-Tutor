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
    private readonly Stack<(ViewModelBase ViewModel, IServiceScope Scope)> _navigationStack = new();
    private ViewModelBase? _currentViewModel;
    private IServiceScope? _currentScope;

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
        // Push current view model and scope to stack if they exist
        if (CurrentViewModel != null && _currentScope != null)
        {
            _navigationStack.Push((CurrentViewModel, _currentScope));
        }

        // Create new scope for the new ViewModel and its dependencies
        _currentScope = _serviceProvider.CreateScope();

        // Get view model from the new scope
        var viewModel = _currentScope.ServiceProvider.GetRequiredService<TViewModel>();

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

        // Dispose current scope before navigating back
        _currentScope?.Dispose();

        // Pop previous view model and scope
        var (previousViewModel, previousScope) = _navigationStack.Pop();
        CurrentViewModel = previousViewModel;
        _currentScope = previousScope;

        // Notify view model it's being navigated back to
        if (previousViewModel is INavigableViewModel navigable)
        {
            navigable.OnNavigatedBack();
        }
    }

    public void ClearHistory()
    {
        // Dispose all scopes in the navigation stack
        while (_navigationStack.Count > 0)
        {
            var (_, scope) = _navigationStack.Pop();
            scope?.Dispose();
        }
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
