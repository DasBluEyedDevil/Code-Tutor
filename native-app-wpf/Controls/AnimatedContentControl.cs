using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CodeTutor.Wpf.Controls;

/// <summary>
/// A ContentControl that animates content transitions with smooth slide and fade effects.
/// Supports forward (slide from right) and backward (slide from left) navigation animations.
/// </summary>
public class AnimatedContentControl : ContentControl
{
    private ContentPresenter? _currentPresenter;
    private ContentPresenter? _previousPresenter;
    private Grid? _rootGrid;

    public static readonly DependencyProperty TransitionDurationProperty =
        DependencyProperty.Register(
            nameof(TransitionDuration),
            typeof(Duration),
            typeof(AnimatedContentControl),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(300))));

    public static readonly DependencyProperty SlideDistanceProperty =
        DependencyProperty.Register(
            nameof(SlideDistance),
            typeof(double),
            typeof(AnimatedContentControl),
            new PropertyMetadata(60.0));

    public static readonly DependencyProperty NavigationDirectionProperty =
        DependencyProperty.Register(
            nameof(NavigationDirection),
            typeof(NavigationDirection),
            typeof(AnimatedContentControl),
            new PropertyMetadata(NavigationDirection.Forward));

    public Duration TransitionDuration
    {
        get => (Duration)GetValue(TransitionDurationProperty);
        set => SetValue(TransitionDurationProperty, value);
    }

    public double SlideDistance
    {
        get => (double)GetValue(SlideDistanceProperty);
        set => SetValue(SlideDistanceProperty, value);
    }

    public NavigationDirection NavigationDirection
    {
        get => (NavigationDirection)GetValue(NavigationDirectionProperty);
        set => SetValue(NavigationDirectionProperty, value);
    }

    static AnimatedContentControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(AnimatedContentControl),
            new FrameworkPropertyMetadata(typeof(AnimatedContentControl)));
    }

    public AnimatedContentControl()
    {
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        InitializeVisualTree();
    }

    private void InitializeVisualTree()
    {
        _rootGrid = new Grid();

        _previousPresenter = new ContentPresenter
        {
            Opacity = 0,
            RenderTransform = new TranslateTransform(),
            RenderTransformOrigin = new Point(0.5, 0.5)
        };

        _currentPresenter = new ContentPresenter
        {
            Content = Content,
            RenderTransform = new TranslateTransform(),
            RenderTransformOrigin = new Point(0.5, 0.5)
        };

        _rootGrid.Children.Add(_previousPresenter);
        _rootGrid.Children.Add(_currentPresenter);

        AddVisualChild(_rootGrid);
        AddLogicalChild(_rootGrid);

        InvalidateVisual();
    }

    protected override int VisualChildrenCount => _rootGrid != null ? 1 : 0;

    protected override Visual GetVisualChild(int index)
    {
        if (index != 0 || _rootGrid == null)
            throw new ArgumentOutOfRangeException(nameof(index));
        return _rootGrid;
    }

    protected override Size MeasureOverride(Size constraint)
    {
        _rootGrid?.Measure(constraint);
        return _rootGrid?.DesiredSize ?? base.MeasureOverride(constraint);
    }

    protected override Size ArrangeOverride(Size arrangeBounds)
    {
        _rootGrid?.Arrange(new Rect(arrangeBounds));
        return arrangeBounds;
    }

    protected override void OnContentChanged(object oldContent, object newContent)
    {
        base.OnContentChanged(oldContent, newContent);

        if (_currentPresenter == null || _previousPresenter == null || _rootGrid == null)
            return;

        // Skip animation if this is the initial content load
        if (oldContent == null)
        {
            _currentPresenter.Content = newContent;
            return;
        }

        // Set up the transition
        _previousPresenter.Content = oldContent;
        _previousPresenter.Opacity = 1;
        ((TranslateTransform)_previousPresenter.RenderTransform).X = 0;

        _currentPresenter.Content = newContent;
        _currentPresenter.Opacity = 0;

        // Calculate slide direction
        double slideFrom = NavigationDirection == NavigationDirection.Forward
            ? SlideDistance
            : -SlideDistance;
        double slideTo = NavigationDirection == NavigationDirection.Forward
            ? -SlideDistance
            : SlideDistance;

        ((TranslateTransform)_currentPresenter.RenderTransform).X = slideFrom;

        // Create animations
        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };

        // Animate old content out
        var oldOpacity = new DoubleAnimation(1, 0, TransitionDuration) { EasingFunction = easing };
        var oldSlide = new DoubleAnimation(0, slideTo, TransitionDuration) { EasingFunction = easing };

        // Animate new content in
        var newOpacity = new DoubleAnimation(0, 1, TransitionDuration) { EasingFunction = easing };
        var newSlide = new DoubleAnimation(slideFrom, 0, TransitionDuration) { EasingFunction = easing };

        // Apply animations
        _previousPresenter.BeginAnimation(OpacityProperty, oldOpacity);
        ((TranslateTransform)_previousPresenter.RenderTransform).BeginAnimation(TranslateTransform.XProperty, oldSlide);

        _currentPresenter.BeginAnimation(OpacityProperty, newOpacity);
        ((TranslateTransform)_currentPresenter.RenderTransform).BeginAnimation(TranslateTransform.XProperty, newSlide);
    }

    /// <summary>
    /// Navigate to new content with forward animation (slide from right).
    /// </summary>
    public void NavigateForward(object newContent)
    {
        NavigationDirection = NavigationDirection.Forward;
        Content = newContent;
    }

    /// <summary>
    /// Navigate to new content with backward animation (slide from left).
    /// </summary>
    public void NavigateBack(object newContent)
    {
        NavigationDirection = NavigationDirection.Back;
        Content = newContent;
    }
}

public enum NavigationDirection
{
    Forward,
    Back
}
