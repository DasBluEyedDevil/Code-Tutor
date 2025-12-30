using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Controls;

public partial class SpotlightOverlay : UserControl
{
    private Window? _window;

    public SpotlightOverlay()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _window = Window.GetWindow(this);
        if (_window != null)
        {
            _window.MouseMove += OnMouseMove;
            _window.MouseLeave += OnMouseLeave;
        }

        PerformanceProfile.UiThrottlingChanged += OnUiThrottlingChanged;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (_window != null)
        {
            _window.MouseMove -= OnMouseMove;
            _window.MouseLeave -= OnMouseLeave;
        }

        PerformanceProfile.UiThrottlingChanged -= OnUiThrottlingChanged;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (PerformanceProfile.IsUiThrottled)
            return;

        var position = e.GetPosition(this);
        UpdateSpotlight(position);
    }

    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        SpotlightBrush.Center = new Point(0.5, 0.5);
        SpotlightBrush.GradientOrigin = new Point(0.5, 0.5);
    }

    private void UpdateSpotlight(Point position)
    {
        if (ActualWidth <= 0 || ActualHeight <= 0)
            return;

        var x = Math.Clamp(position.X / ActualWidth, 0, 1);
        var y = Math.Clamp(position.Y / ActualHeight, 0, 1);
        SpotlightBrush.Center = new Point(x, y);
        SpotlightBrush.GradientOrigin = new Point(x, y);
    }

    private void OnUiThrottlingChanged(object? sender, bool isThrottled)
    {
        Opacity = isThrottled ? 0.2 : 0.45;
    }
}
