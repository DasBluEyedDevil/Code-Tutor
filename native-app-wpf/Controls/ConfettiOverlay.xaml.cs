using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CodeTutor.Wpf.Controls;

public partial class ConfettiOverlay : UserControl
{
    private readonly Random _random = new();
    private Storyboard? _activeStoryboard;

    public ConfettiOverlay()
    {
        InitializeComponent();
    }

    public void Play()
    {
        Visibility = Visibility.Visible;
        Opacity = 1;

        if (ActualWidth <= 0 || ActualHeight <= 0)
        {
            Dispatcher.BeginInvoke(new Action(Play));
            return;
        }
        ParticleCanvas.Children.Clear();

        _activeStoryboard?.Stop();
        _activeStoryboard = new Storyboard();

        var palette = BuildPalette();
        var origin = new Point(ActualWidth / 2, ActualHeight / 4);
        var particleCount = 36;

        for (int i = 0; i < particleCount; i++)
        {
            var size = _random.Next(5, 10);
            var rect = new Rectangle
            {
                Width = size,
                Height = size * 0.6,
                Fill = palette[_random.Next(palette.Count)],
                Opacity = 0.95,
                RadiusX = 1,
                RadiusY = 1,
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            var rotate = new RotateTransform();
            var translate = new TranslateTransform();
            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(rotate);
            transformGroup.Children.Add(translate);
            rect.RenderTransform = transformGroup;

            Canvas.SetLeft(rect, origin.X);
            Canvas.SetTop(rect, origin.Y);
            ParticleCanvas.Children.Add(rect);

            var angle = (_random.NextDouble() * 140) - 70;
            var angleRad = angle * (Math.PI / 180);
            var distance = _random.Next(120, 220);
            var xTarget = Math.Cos(angleRad) * distance;
            var yTarget = Math.Sin(angleRad) * distance + _random.Next(180, 260);

            var duration = TimeSpan.FromMilliseconds(_random.Next(2000, 2600));

            var xAnim = new DoubleAnimation(0, xTarget, duration)
            {
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };
            var yAnim = new DoubleAnimation(0, yTarget, duration)
            {
                AccelerationRatio = 0.7,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            var rotateAnim = new DoubleAnimation(0, _random.Next(180, 720), duration);
            var fadeAnim = new DoubleAnimation(0.95, 0, duration)
            {
                BeginTime = TimeSpan.FromMilliseconds(300)
            };

            Storyboard.SetTarget(xAnim, translate);
            Storyboard.SetTargetProperty(xAnim, new PropertyPath(TranslateTransform.XProperty));
            Storyboard.SetTarget(yAnim, translate);
            Storyboard.SetTargetProperty(yAnim, new PropertyPath(TranslateTransform.YProperty));
            Storyboard.SetTarget(rotateAnim, rotate);
            Storyboard.SetTargetProperty(rotateAnim, new PropertyPath(RotateTransform.AngleProperty));
            Storyboard.SetTarget(fadeAnim, rect);
            Storyboard.SetTargetProperty(fadeAnim, new PropertyPath(UIElement.OpacityProperty));

            _activeStoryboard.Children.Add(xAnim);
            _activeStoryboard.Children.Add(yAnim);
            _activeStoryboard.Children.Add(rotateAnim);
            _activeStoryboard.Children.Add(fadeAnim);
        }

        var overlayFade = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(400))
        {
            BeginTime = TimeSpan.FromMilliseconds(2100)
        };
        Storyboard.SetTarget(overlayFade, this);
        Storyboard.SetTargetProperty(overlayFade, new PropertyPath(OpacityProperty));
        _activeStoryboard.Children.Add(overlayFade);

        _activeStoryboard.Completed += (_, _) =>
        {
            ParticleCanvas.Children.Clear();
            Visibility = Visibility.Collapsed;
            Opacity = 0;
        };

        _activeStoryboard.Begin();
    }

    private List<Brush> BuildPalette()
    {
        var palette = new List<Brush>();

        AddBrushIfFound(palette, "AccentBlueBrush", Color.FromRgb(0x58, 0xA6, 0xFF));
        AddBrushIfFound(palette, "AccentPurpleBrush", Color.FromRgb(0xA3, 0x71, 0xF7));
        AddBrushIfFound(palette, "AccentGreenBrush", Color.FromRgb(0x3F, 0xB9, 0x50));
        AddBrushIfFound(palette, "AccentOrangeBrush", Color.FromRgb(0xD2, 0x99, 0x22));
        AddBrushIfFound(palette, "AccentRedBrush", Color.FromRgb(0xF8, 0x51, 0x49));

        return palette;
    }

    private void AddBrushIfFound(ICollection<Brush> palette, string key, Color fallback)
    {
        if (TryFindResource(key) is SolidColorBrush brush)
        {
            palette.Add(brush.Clone());
        }
        else
        {
            palette.Add(new SolidColorBrush(fallback));
        }
    }
}
