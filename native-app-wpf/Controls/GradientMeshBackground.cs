using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Controls;

public class GradientMeshBackground : FrameworkElement
{
    private readonly List<Orb> _orbs = new();
    private readonly Stopwatch _clock = new();
    private bool _isRendering;
    private Window? _window;
    private bool _isWindowMinimized;

    public GradientMeshBackground()
    {
        IsHitTestVisible = false;
        if (!PerformanceProfile.IsSoftwareRendering)
        {
            Effect = new BlurEffect { Radius = 35 };
        }

        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        IsVisibleChanged += OnIsVisibleChanged;

        InitializeOrbs();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        PerformanceProfile.UiThrottlingChanged += OnUiThrottlingChanged;
        _window = Window.GetWindow(this);
        if (_window != null)
        {
            _window.StateChanged += OnWindowStateChanged;
            _isWindowMinimized = _window.WindowState == WindowState.Minimized;
        }
        UpdateRenderingState();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        PerformanceProfile.UiThrottlingChanged -= OnUiThrottlingChanged;
        if (_window != null)
        {
            _window.StateChanged -= OnWindowStateChanged;
        }
        StopRendering();
    }

    private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        UpdateRenderingState();
    }

    private void OnWindowStateChanged(object? sender, EventArgs e)
    {
        if (_window == null)
            return;

        _isWindowMinimized = _window.WindowState == WindowState.Minimized;
        UpdateRenderingState();
    }

    private void OnUiThrottlingChanged(object? sender, bool isThrottled)
    {
        UpdateRenderingState();
    }

    private void UpdateRenderingState()
    {
        if (!IsLoaded || !IsVisible || _isWindowMinimized || PerformanceProfile.IsUiThrottled)
        {
            StopRendering();
            return;
        }

        StartRendering();
    }

    private void StartRendering()
    {
        if (_isRendering)
            return;

        _isRendering = true;
        _clock.Restart();
        CompositionTarget.Rendering += OnRendering;
    }

    private void StopRendering()
    {
        if (!_isRendering)
            return;

        _isRendering = false;
        CompositionTarget.Rendering -= OnRendering;
        _clock.Stop();
    }

    private void OnRendering(object? sender, EventArgs e)
    {
        if (PerformanceProfile.IsUiThrottled || !IsVisible || ActualWidth <= 0 || ActualHeight <= 0)
            return;

        InvalidateVisual();
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        if (ActualWidth <= 0 || ActualHeight <= 0)
            return;

        var time = _clock.Elapsed.TotalSeconds;
        var minDimension = Math.Min(ActualWidth, ActualHeight);

        foreach (var orb in _orbs)
        {
            var pulse = (Math.Sin(time * orb.PulseSpeed + orb.PulsePhase) + 1) * 0.5;
            var radius = minDimension * orb.BaseRadius * (0.85 + pulse * 0.2);

            var driftX = Math.Sin(time * orb.DriftSpeed + orb.DriftPhaseX) * ActualWidth * orb.DriftAmplitude;
            var driftY = Math.Cos(time * orb.DriftSpeed + orb.DriftPhaseY) * ActualHeight * orb.DriftAmplitude;

            var centerX = ActualWidth * orb.CenterX + driftX;
            var centerY = ActualHeight * orb.CenterY + driftY;

            var brush = CreateOrbBrush(orb.Color);
            drawingContext.DrawEllipse(brush, null, new Point(centerX, centerY), radius, radius);
        }
    }

    private void InitializeOrbs()
    {
        var accentBlue = GetResourceColor("AccentBlue", Color.FromRgb(0x58, 0xA6, 0xFF));
        var accentPurple = GetResourceColor("AccentPurple", Color.FromRgb(0xA3, 0x71, 0xF7));
        var accentBlueBright = GetResourceColor("AccentBlueBright", Color.FromRgb(0x79, 0xB8, 0xFF));

        _orbs.Add(new Orb
        {
            CenterX = 0.2,
            CenterY = 0.25,
            BaseRadius = 0.38,
            DriftAmplitude = 0.05,
            DriftSpeed = 0.18,
            PulseSpeed = 0.25,
            PulsePhase = 0.4,
            DriftPhaseX = 0.2,
            DriftPhaseY = 0.1,
            Color = accentBlue
        });

        _orbs.Add(new Orb
        {
            CenterX = 0.75,
            CenterY = 0.2,
            BaseRadius = 0.32,
            DriftAmplitude = 0.04,
            DriftSpeed = 0.14,
            PulseSpeed = 0.22,
            PulsePhase = 1.3,
            DriftPhaseX = 1.1,
            DriftPhaseY = 0.6,
            Color = accentPurple
        });

        _orbs.Add(new Orb
        {
            CenterX = 0.65,
            CenterY = 0.75,
            BaseRadius = 0.42,
            DriftAmplitude = 0.06,
            DriftSpeed = 0.12,
            PulseSpeed = 0.2,
            PulsePhase = 2.2,
            DriftPhaseX = 0.8,
            DriftPhaseY = 1.4,
            Color = accentBlueBright
        });
    }

    private static RadialGradientBrush CreateOrbBrush(Color color)
    {
        var centerColor = Color.FromArgb(32, color.R, color.G, color.B);
        var edgeColor = Color.FromArgb(0, color.R, color.G, color.B);

        return new RadialGradientBrush
        {
            GradientStops = new GradientStopCollection
            {
                new GradientStop(centerColor, 0),
                new GradientStop(edgeColor, 1)
            }
        };
    }

    private Color GetResourceColor(string key, Color fallback)
    {
        if (TryFindResource(key) is Color color)
            return color;

        if (TryFindResource($"{key}Brush") is SolidColorBrush brush)
            return brush.Color;

        return fallback;
    }

    private sealed class Orb
    {
        public double CenterX { get; init; }
        public double CenterY { get; init; }
        public double BaseRadius { get; init; }
        public double DriftAmplitude { get; init; }
        public double DriftSpeed { get; init; }
        public double DriftPhaseX { get; init; }
        public double DriftPhaseY { get; init; }
        public double PulseSpeed { get; init; }
        public double PulsePhase { get; init; }
        public Color Color { get; init; }
    }
}
