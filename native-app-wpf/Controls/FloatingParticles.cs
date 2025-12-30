using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Controls;

public class FloatingParticles : Canvas
{
    private const int DefaultParticleCount = 24;
    private readonly List<Particle> _particles = new();
    private readonly Random _random = new();
    private readonly Stopwatch _clock = new();
    private Window? _window;
    private bool _isRendering;
    private bool _isWindowMinimized;
    private double _lastTime;

    public int ParticleCount { get; set; } = DefaultParticleCount;

    public FloatingParticles()
    {
        IsHitTestVisible = false;
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        SizeChanged += OnSizeChanged;
        IsVisibleChanged += OnIsVisibleChanged;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        _window = Window.GetWindow(this);
        if (_window != null)
        {
            _window.StateChanged += OnWindowStateChanged;
            _isWindowMinimized = _window.WindowState == WindowState.Minimized;
        }

        PerformanceProfile.UiThrottlingChanged += OnUiThrottlingChanged;

        InitializeParticles();
        UpdateRenderingState();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (_window != null)
        {
            _window.StateChanged -= OnWindowStateChanged;
        }

        PerformanceProfile.UiThrottlingChanged -= OnUiThrottlingChanged;
        StopRendering();
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        InitializeParticles();
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
        _lastTime = 0;
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
        if (PerformanceProfile.IsUiThrottled || ActualWidth <= 0 || ActualHeight <= 0)
            return;

        var time = _clock.Elapsed.TotalSeconds;
        var delta = time - _lastTime;
        _lastTime = time;

        foreach (var particle in _particles)
        {
            particle.Y -= particle.Speed * delta;
            if (particle.Y < -30)
            {
                particle.Y = ActualHeight + _random.Next(20, 80);
                particle.X = _random.NextDouble() * ActualWidth;
            }

            var drift = Math.Sin(time * particle.DriftSpeed + particle.Phase) * particle.DriftAmplitude;
            SetLeft(particle.Visual, particle.X + drift);
            SetTop(particle.Visual, particle.Y);
        }
    }

    private void InitializeParticles()
    {
        if (ActualWidth <= 0 || ActualHeight <= 0)
            return;

        Children.Clear();
        _particles.Clear();

        var symbols = new[]
        {
            "{}", "</>", "//", ";", "[]", "()", "=>", "#", "<>", "++", "::", "&&"
        };

        var palette = BuildPalette();

        var count = PerformanceProfile.IsSoftwareRendering ? Math.Min(ParticleCount, 12) : ParticleCount;

        for (int i = 0; i < count; i++)
        {
            var symbol = symbols[_random.Next(symbols.Length)];
            var textBlock = new TextBlock
            {
                Text = symbol,
                FontFamily = (FontFamily)FindResource("MonoFont"),
                FontSize = _random.Next(10, 14),
                Foreground = palette[_random.Next(palette.Count)],
                Opacity = _random.NextDouble() * 0.35 + 0.05,
                CacheMode = new BitmapCache()
            };

            var particle = new Particle
            {
                Visual = textBlock,
                X = _random.NextDouble() * ActualWidth,
                Y = _random.NextDouble() * ActualHeight,
                Speed = _random.NextDouble() * 12 + 6,
                DriftAmplitude = _random.NextDouble() * 18 + 8,
                DriftSpeed = _random.NextDouble() * 0.6 + 0.2,
                Phase = _random.NextDouble() * Math.PI * 2
            };

            _particles.Add(particle);
            Children.Add(textBlock);
            SetLeft(textBlock, particle.X);
            SetTop(textBlock, particle.Y);
        }
    }

    private List<SolidColorBrush> BuildPalette()
    {
        var palette = new List<SolidColorBrush>();

        AddBrushIfFound(palette, "AccentBlueBrush", Color.FromRgb(0x58, 0xA6, 0xFF));
        AddBrushIfFound(palette, "AccentPurpleBrush", Color.FromRgb(0xA3, 0x71, 0xF7));
        AddBrushIfFound(palette, "AccentGreenBrush", Color.FromRgb(0x3F, 0xB9, 0x50));
        AddBrushIfFound(palette, "TextSecondaryBrush", Color.FromRgb(0x8B, 0x94, 0x9E));

        return palette;
    }

    private void AddBrushIfFound(ICollection<SolidColorBrush> palette, string key, Color fallback)
    {
        if (TryFindResource(key) is SolidColorBrush brush)
        {
            var clone = brush.Clone();
            palette.Add(clone);
        }
        else
        {
            palette.Add(new SolidColorBrush(fallback));
        }
    }

    private sealed class Particle
    {
        public required TextBlock Visual { get; init; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Speed { get; init; }
        public double DriftAmplitude { get; init; }
        public double DriftSpeed { get; init; }
        public double Phase { get; init; }
    }
}
