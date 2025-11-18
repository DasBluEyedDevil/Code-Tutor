using System;
using CodeTutor.Native.ViewModels;

namespace CodeTutor.Native.Services;

/// <summary>
/// Navigation service for managing page navigation
/// </summary>
public interface INavigationService
{
    /// <summary>
    /// Current view model being displayed
    /// </summary>
    ViewModelBase? CurrentViewModel { get; }

    /// <summary>
    /// Navigate to a view model type
    /// </summary>
    void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;

    /// <summary>
    /// Navigate to a view model with parameter
    /// </summary>
    void NavigateTo<TViewModel>(object? parameter) where TViewModel : ViewModelBase;

    /// <summary>
    /// Navigate back to previous page
    /// </summary>
    void GoBack();

    /// <summary>
    /// Check if can navigate back
    /// </summary>
    bool CanGoBack { get; }

    /// <summary>
    /// Clear navigation history
    /// </summary>
    void ClearHistory();

    /// <summary>
    /// Event raised when current view model changes
    /// </summary>
    event EventHandler<ViewModelBase>? CurrentViewModelChanged;
}
