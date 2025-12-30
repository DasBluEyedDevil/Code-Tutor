using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace CodeTutor.Wpf.Controls;

public class ShimmerEffect : ContentControl
{
    public static readonly DependencyProperty IsActiveProperty =
        DependencyProperty.Register(nameof(IsActive), typeof(bool), typeof(ShimmerEffect),
            new PropertyMetadata(true, OnIsActiveChanged));

    public static readonly DependencyProperty AnimationDurationProperty =
        DependencyProperty.Register(nameof(AnimationDuration), typeof(Duration), typeof(ShimmerEffect),
            new PropertyMetadata(new Duration(TimeSpan.FromSeconds(1.6)), OnIsActiveChanged));

    private TranslateTransform? _shimmerTransform;
    private FrameworkElement? _shimmerOverlay;

    static ShimmerEffect()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ShimmerEffect),
            new FrameworkPropertyMetadata(typeof(ShimmerEffect)));
    }

    public ShimmerEffect()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        SizeChanged += OnSizeChanged;
    }

    public bool IsActive
    {
        get => (bool)GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }

    public Duration AnimationDuration
    {
        get => (Duration)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _shimmerTransform = GetTemplateChild("PART_ShimmerTransform") as TranslateTransform;
        _shimmerOverlay = GetTemplateChild("PART_ShimmerOverlay") as FrameworkElement;

        UpdateAnimationState();
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdateAnimationState();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        StopAnimation();
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (IsActive)
        {
            StartAnimation();
        }
    }

    private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ShimmerEffect shimmer)
        {
            shimmer.UpdateAnimationState();
        }
    }

    private void UpdateAnimationState()
    {
        if (!IsLoaded || _shimmerTransform == null || _shimmerOverlay == null)
            return;

        if (IsActive)
        {
            _shimmerOverlay.Visibility = Visibility.Visible;
            StartAnimation();
        }
        else
        {
            _shimmerOverlay.Visibility = Visibility.Collapsed;
            StopAnimation();
        }
    }

    private void StartAnimation()
    {
        if (_shimmerTransform == null || ActualWidth <= 0)
            return;

        var animation = new DoubleAnimation
        {
            From = -ActualWidth,
            To = ActualWidth,
            Duration = AnimationDuration,
            RepeatBehavior = RepeatBehavior.Forever,
            EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
        };

        _shimmerTransform.BeginAnimation(TranslateTransform.XProperty, animation);
    }

    private void StopAnimation()
    {
        _shimmerTransform?.BeginAnimation(TranslateTransform.XProperty, null);
    }
}
