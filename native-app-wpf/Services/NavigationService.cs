using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CodeTutor.Wpf.Services;

public interface INavigationService
{
    event EventHandler<object>? Navigated;
    void NavigateTo(UserControl view, object? parameter = null);
    void GoBack();
    bool CanGoBack { get; }
    bool IsBackNavigation { get; }
}

public class NavigationService : INavigationService
{
    private readonly Stack<(UserControl View, object? Parameter)> _history = new();
    private bool _isBackNavigation;

    public event EventHandler<object>? Navigated;

    public bool CanGoBack => _history.Count > 1;
    public bool IsBackNavigation => _isBackNavigation;

    public void NavigateTo(UserControl view, object? parameter = null)
    {
        _isBackNavigation = false;
        _history.Push((view, parameter));
        Navigated?.Invoke(this, view);
    }

    public void GoBack()
    {
        if (_history.Count > 1)
        {
            _isBackNavigation = true;
            _history.Pop();
            var (view, _) = _history.Peek();
            Navigated?.Invoke(this, view);
        }
    }
}
