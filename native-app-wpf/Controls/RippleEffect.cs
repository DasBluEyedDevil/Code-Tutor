using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CodeTutor.Wpf.Controls;

public static class RippleEffect
{
    public static readonly DependencyProperty EnableRippleProperty =
        DependencyProperty.RegisterAttached(
            "EnableRipple",
            typeof(bool),
            typeof(RippleEffect),
            new PropertyMetadata(false, OnEnableRippleChanged));

    public static readonly DependencyProperty RippleBrushProperty =
        DependencyProperty.RegisterAttached(
            "RippleBrush",
            typeof(Brush),
            typeof(RippleEffect),
            new PropertyMetadata(null));

    public static bool GetEnableRipple(DependencyObject obj)
        => (bool)obj.GetValue(EnableRippleProperty);

    public static void SetEnableRipple(DependencyObject obj, bool value)
        => obj.SetValue(EnableRippleProperty, value);

    public static Brush? GetRippleBrush(DependencyObject obj)
        => (Brush?)obj.GetValue(RippleBrushProperty);

    public static void SetRippleBrush(DependencyObject obj, Brush? value)
        => obj.SetValue(RippleBrushProperty, value);

    private static void OnEnableRippleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is UIElement element)
        {
            if ((bool)e.NewValue)
            {
                element.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            }
            else
            {
                element.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;
            }
        }
    }

    private static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is not FrameworkElement element || !element.IsEnabled)
            return;

        var layer = AdornerLayer.GetAdornerLayer(element);
        if (layer == null)
            return;

        var position = e.GetPosition(element);
        var brush = ResolveRippleBrush(element);

        var adorner = new RippleAdorner(element, position, brush);
        layer.Add(adorner);
        adorner.StartAnimation(() => layer.Remove(adorner));
    }

    private static Brush ResolveRippleBrush(FrameworkElement element)
    {
        if (GetRippleBrush(element) is Brush brush)
            return brush;

        if (element.TryFindResource("AccentBlueBrush") is SolidColorBrush accentBrush)
        {
            var clone = accentBrush.Clone();
            clone.Opacity = 0.3;
            return clone;
        }

        return new SolidColorBrush(Color.FromArgb(80, 255, 255, 255));
    }

    private sealed class RippleAdorner : Adorner
    {
        private readonly Canvas _canvas = new();
        private readonly Ellipse _ripple;
        private readonly ScaleTransform _scaleTransform;

        public RippleAdorner(UIElement adornedElement, Point origin, Brush brush) : base(adornedElement)
        {
            IsHitTestVisible = false;

            var size = adornedElement.RenderSize;
            var radius = Math.Sqrt(Math.Pow(Math.Max(origin.X, size.Width - origin.X), 2) +
                                   Math.Pow(Math.Max(origin.Y, size.Height - origin.Y), 2));

            _scaleTransform = new ScaleTransform(0, 0);
            _ripple = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Fill = brush,
                Opacity = 0.35,
                RenderTransform = _scaleTransform,
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            Canvas.SetLeft(_ripple, origin.X - radius);
            Canvas.SetTop(_ripple, origin.Y - radius);
            _canvas.Children.Add(_ripple);

            AddVisualChild(_canvas);
        }

        public void StartAnimation(Action onComplete)
        {
            var duration = TimeSpan.FromMilliseconds(600);

            var scaleAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = duration,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            var opacityAnimation = new DoubleAnimation
            {
                From = 0.35,
                To = 0,
                Duration = duration,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
            };

            scaleAnimation.Completed += (_, _) => onComplete();

            _scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
            _scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);
            _ripple.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            return _canvas;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            _canvas.Measure(constraint);
            return _canvas.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _canvas.Arrange(new Rect(finalSize));
            return finalSize;
        }
    }
}
