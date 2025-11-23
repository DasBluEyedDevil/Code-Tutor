using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CodeTutor.Native.ViewModels;
using CodeTutor.Native.ViewModels.Pages;

namespace CodeTutor.Native.Services;

/// <summary>
/// Navigation service implementation using stack-based navigation
/// </summary>
public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IErrorHandlerService? _errorHandler;
    private readonly Stack<(ViewModelBase ViewModel, IServiceScope Scope)> _navigationStack = new();
    private ViewModelBase? _currentViewModel;
    private IServiceScope? _currentScope;

    public NavigationService(IServiceProvider serviceProvider, IErrorHandlerService? errorHandler = null)
    {
        _serviceProvider = serviceProvider;
        _errorHandler = errorHandler;
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

        ViewModelBase viewModel;

        try
        {
            // Get view model from the new scope
            viewModel = _currentScope.ServiceProvider.GetRequiredService<TViewModel>();

            // If view model supports parameters, set it
            if (parameter != null && viewModel is INavigableViewModel navigable)
            {
                navigable.OnNavigatedTo(parameter);
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            // Log the error
            _errorHandler?.LogError(ex, $"Failed to resolve ViewModel: {typeof(TViewModel).Name}");

            // Fallback to NotFoundPageViewModel if the requested ViewModel fails to resolve
            // Avoid infinite recursion by checking if we're already trying to show NotFound
            if (typeof(TViewModel) != typeof(NotFoundPageViewModel))
            {
                try
                {
                    viewModel = _currentScope.ServiceProvider.GetRequiredService<NotFoundPageViewModel>();
                    _errorHandler?.LogWarning($"Navigated to NotFoundPage due to ViewModel resolution failure", "Navigation");
                }
                catch
                {
                    // If even NotFoundPageViewModel fails, rethrow original exception
                    throw;
                }
            }
            else
            {
                throw; // Can't recover if NotFoundPageViewModel itself fails
            }
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
