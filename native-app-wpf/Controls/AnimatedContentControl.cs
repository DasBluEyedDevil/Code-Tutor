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

    public static readonly DependencyProperty TransitionTypeProperty =
        DependencyProperty.Register(
            nameof(TransitionType),
            typeof(ContentTransition),
            typeof(AnimatedContentControl),
            new PropertyMetadata(ContentTransition.SlideAndFade));

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

    public ContentTransition TransitionType
    {
        get => (ContentTransition)GetValue(TransitionTypeProperty);
        set => SetValue(TransitionTypeProperty, value);
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
            RenderTransform = CreatePresenterTransform(),
            RenderTransformOrigin = new Point(0.5, 0.5)
        };

        _currentPresenter = new ContentPresenter
        {
            Content = Content,
            RenderTransform = CreatePresenterTransform(),
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
        ResetTransforms(_previousPresenter);

        _currentPresenter.Content = newContent;
        _currentPresenter.Opacity = 0;

        ApplyTransitionAnimations();
    }

    private static TransformGroup CreatePresenterTransform()
    {
        var group = new TransformGroup();
        group.Children.Add(new ScaleTransform(1, 1));
        group.Children.Add(new TranslateTransform());
        return group;
    }

    private static ScaleTransform GetScaleTransform(ContentPresenter presenter)
    {
        return (ScaleTransform)((TransformGroup)presenter.RenderTransform).Children[0];
    }

    private static TranslateTransform GetTranslateTransform(ContentPresenter presenter)
    {
        return (TranslateTransform)((TransformGroup)presenter.RenderTransform).Children[1];
    }

    private static void ResetTransforms(ContentPresenter presenter)
    {
        GetScaleTransform(presenter).ScaleX = 1;
        GetScaleTransform(presenter).ScaleY = 1;
        GetTranslateTransform(presenter).X = 0;
        GetTranslateTransform(presenter).Y = 0;
    }

    private void ApplyTransitionAnimations()
    {
        if (_currentPresenter == null || _previousPresenter == null)
            return;

        var duration = TransitionDuration;
        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };
        var fadeEasing = new QuadraticEase { EasingMode = EasingMode.EaseOut };

        var oldOpacity = new DoubleAnimation(1, 0, duration) { EasingFunction = fadeEasing };
        var newOpacity = new DoubleAnimation(0, 1, duration) { EasingFunction = fadeEasing };

        ResetTransforms(_previousPresenter);
        ResetTransforms(_currentPresenter);

        _previousPresenter.BeginAnimation(OpacityProperty, oldOpacity);
        _currentPresenter.BeginAnimation(OpacityProperty, newOpacity);

        switch (TransitionType)
        {
            case ContentTransition.CrossFade:
                break;
            case ContentTransition.ZoomAndFade:
                ApplyZoomTransition(0.98, easing);
                break;
            case ContentTransition.Morph:
                ApplyMorphTransition(easing);
                break;
            default:
                ApplySlideTransition(easing);
                break;
        }
    }

    private void ApplySlideTransition(IEasingFunction easing)
    {
        if (_currentPresenter == null || _previousPresenter == null)
            return;

        double slideFrom = NavigationDirection == NavigationDirection.Forward
            ? SlideDistance
            : -SlideDistance;
        double slideTo = NavigationDirection == NavigationDirection.Forward
            ? -SlideDistance
            : SlideDistance;

        var previousTranslate = GetTranslateTransform(_previousPresenter);
        var currentTranslate = GetTranslateTransform(_currentPresenter);

        currentTranslate.X = slideFrom;

        var oldSlide = new DoubleAnimation(0, slideTo, TransitionDuration) { EasingFunction = easing };
        var newSlide = new DoubleAnimation(slideFrom, 0, TransitionDuration) { EasingFunction = easing };

        previousTranslate.BeginAnimation(TranslateTransform.XProperty, oldSlide);
        currentTranslate.BeginAnimation(TranslateTransform.XProperty, newSlide);
    }

    private void ApplyZoomTransition(double startScale, IEasingFunction easing)
    {
        if (_currentPresenter == null || _previousPresenter == null)
            return;

        var previousScale = GetScaleTransform(_previousPresenter);
        var currentScale = GetScaleTransform(_currentPresenter);

        currentScale.ScaleX = startScale;
        currentScale.ScaleY = startScale;

        var oldScale = new DoubleAnimation(1, startScale, TransitionDuration) { EasingFunction = easing };
        var newScale = new DoubleAnimation(startScale, 1, TransitionDuration) { EasingFunction = easing };

        previousScale.BeginAnimation(ScaleTransform.ScaleXProperty, oldScale);
        previousScale.BeginAnimation(ScaleTransform.ScaleYProperty, oldScale);
        currentScale.BeginAnimation(ScaleTransform.ScaleXProperty, newScale);
        currentScale.BeginAnimation(ScaleTransform.ScaleYProperty, newScale);
    }

    private void ApplyMorphTransition(IEasingFunction easing)
    {
        if (_currentPresenter == null || _previousPresenter == null)
            return;

        var previousScale = GetScaleTransform(_previousPresenter);
        var currentScale = GetScaleTransform(_currentPresenter);

        currentScale.ScaleX = 0.96;
        currentScale.ScaleY = 0.96;

        var oldScale = new DoubleAnimation(1, 1.04, TransitionDuration) { EasingFunction = easing };
        var newScale = new DoubleAnimation(0.96, 1, TransitionDuration) { EasingFunction = easing };

        previousScale.BeginAnimation(ScaleTransform.ScaleXProperty, oldScale);
        previousScale.BeginAnimation(ScaleTransform.ScaleYProperty, oldScale);
        currentScale.BeginAnimation(ScaleTransform.ScaleXProperty, newScale);
        currentScale.BeginAnimation(ScaleTransform.ScaleYProperty, newScale);
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

public enum ContentTransition
{
    SlideAndFade,
    CrossFade,
    ZoomAndFade,
    Morph
}
