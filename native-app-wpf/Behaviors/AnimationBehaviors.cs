using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace CodeTutor.Wpf.Behaviors;

/// <summary>
/// Provides attached properties for UI animations and effects.
/// </summary>
public static class AnimationBehaviors
{
    #region Staggered Entrance Animation

    public static readonly DependencyProperty EnableStaggeredEntranceProperty =
        DependencyProperty.RegisterAttached(
            "EnableStaggeredEntrance",
            typeof(bool),
            typeof(AnimationBehaviors),
            new PropertyMetadata(false, OnEnableStaggeredEntranceChanged));

    public static readonly DependencyProperty StaggerDelayProperty =
        DependencyProperty.RegisterAttached(
            "StaggerDelay",
            typeof(int),
            typeof(AnimationBehaviors),
            new PropertyMetadata(50));

    public static readonly DependencyProperty EntranceAnimationDurationProperty =
        DependencyProperty.RegisterAttached(
            "EntranceAnimationDuration",
            typeof(int),
            typeof(AnimationBehaviors),
            new PropertyMetadata(350));

    public static bool GetEnableStaggeredEntrance(DependencyObject obj)
        => (bool)obj.GetValue(EnableStaggeredEntranceProperty);

    public static void SetEnableStaggeredEntrance(DependencyObject obj, bool value)
        => obj.SetValue(EnableStaggeredEntranceProperty, value);

    public static int GetStaggerDelay(DependencyObject obj)
        => (int)obj.GetValue(StaggerDelayProperty);

    public static void SetStaggerDelay(DependencyObject obj, int value)
        => obj.SetValue(StaggerDelayProperty, value);

    public static int GetEntranceAnimationDuration(DependencyObject obj)
        => (int)obj.GetValue(EntranceAnimationDurationProperty);

    public static void SetEntranceAnimationDuration(DependencyObject obj, int value)
        => obj.SetValue(EntranceAnimationDurationProperty, value);

    private static void OnEnableStaggeredEntranceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ItemsControl itemsControl && (bool)e.NewValue)
        {
            itemsControl.Loaded += async (s, args) =>
            {
                await AnimateItemsWithStagger(itemsControl);
            };

            // Re-animate when items change
            itemsControl.ItemContainerGenerator.StatusChanged += async (s, args) =>
            {
                if (itemsControl.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                {
                    await Task.Delay(50); // Small delay to ensure containers are ready
                    await AnimateItemsWithStagger(itemsControl);
                }
            };
        }
    }

    private static async Task AnimateItemsWithStagger(ItemsControl itemsControl)
    {
        var staggerDelay = GetStaggerDelay(itemsControl);
        var duration = GetEntranceAnimationDuration(itemsControl);
        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };

        for (int i = 0; i < itemsControl.Items.Count; i++)
        {
            var container = itemsControl.ItemContainerGenerator.ContainerFromIndex(i) as FrameworkElement;
            if (container == null) continue;

            // Set initial state
            container.Opacity = 0;
            container.RenderTransform = new TranslateTransform(0, 30);
            container.RenderTransformOrigin = new Point(0.5, 0.5);

            // Delay based on index
            await Task.Delay(staggerDelay);

            // Animate in
            var opacityAnim = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(duration))
            {
                EasingFunction = easing
            };

            var slideAnim = new DoubleAnimation(30, 0, TimeSpan.FromMilliseconds(duration))
            {
                EasingFunction = easing
            };

            container.BeginAnimation(UIElement.OpacityProperty, opacityAnim);
            ((TranslateTransform)container.RenderTransform).BeginAnimation(TranslateTransform.YProperty, slideAnim);
        }
    }

    #endregion

    #region Fade-In On Load Animation

    public static readonly DependencyProperty EnableFadeInOnLoadProperty =
        DependencyProperty.RegisterAttached(
            "EnableFadeInOnLoad",
            typeof(bool),
            typeof(AnimationBehaviors),
            new PropertyMetadata(false, OnEnableFadeInOnLoadChanged));

    public static bool GetEnableFadeInOnLoad(DependencyObject obj)
        => (bool)obj.GetValue(EnableFadeInOnLoadProperty);

    public static void SetEnableFadeInOnLoad(DependencyObject obj, bool value)
        => obj.SetValue(EnableFadeInOnLoadProperty, value);

    private static void OnEnableFadeInOnLoadChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FrameworkElement element && (bool)e.NewValue)
        {
            element.Opacity = 0;
            element.RenderTransform = new TranslateTransform(0, 20);
            element.RenderTransformOrigin = new Point(0.5, 0.5);

            element.Loaded += (s, args) =>
            {
                var easing = new CubicEase { EasingMode = EasingMode.EaseOut };

                var opacityAnim = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300))
                {
                    EasingFunction = easing
                };

                var slideAnim = new DoubleAnimation(20, 0, TimeSpan.FromMilliseconds(300))
                {
                    EasingFunction = easing
                };

                element.BeginAnimation(UIElement.OpacityProperty, opacityAnim);
                ((TranslateTransform)element.RenderTransform).BeginAnimation(TranslateTransform.YProperty, slideAnim);
            };
        }
    }

    #endregion

    #region Success Flash Animation

    public static readonly DependencyProperty TriggerSuccessFlashProperty =
        DependencyProperty.RegisterAttached(
            "TriggerSuccessFlash",
            typeof(bool),
            typeof(AnimationBehaviors),
            new PropertyMetadata(false, OnTriggerSuccessFlashChanged));

    public static bool GetTriggerSuccessFlash(DependencyObject obj)
        => (bool)obj.GetValue(TriggerSuccessFlashProperty);

    public static void SetTriggerSuccessFlash(DependencyObject obj, bool value)
        => obj.SetValue(TriggerSuccessFlashProperty, value);

    private static void OnTriggerSuccessFlashChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is Border border && (bool)e.NewValue)
        {
            PlaySuccessFlashAnimation(border);

            // Reset the property after animation
            border.Dispatcher.BeginInvoke(new Action(() =>
            {
                border.SetValue(TriggerSuccessFlashProperty, false);
            }), System.Windows.Threading.DispatcherPriority.Background);
        }
    }

    private static void PlaySuccessFlashAnimation(Border border)
    {
        var originalBrush = border.BorderBrush;
        var greenBrush = new SolidColorBrush(Color.FromRgb(0x3F, 0xB9, 0x50));

        // Ensure we have a ScaleTransform
        if (border.RenderTransform is not ScaleTransform)
        {
            border.RenderTransform = new ScaleTransform(1, 1);
            border.RenderTransformOrigin = new Point(0.5, 0.5);
        }

        var scaleTransform = (ScaleTransform)border.RenderTransform;
        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };

        // Create storyboard
        var storyboard = new Storyboard();

        // Border color flash animation
        var colorAnim = new ColorAnimation
        {
            To = Color.FromRgb(0x3F, 0xB9, 0x50),
            Duration = TimeSpan.FromMilliseconds(150),
            AutoReverse = true,
            EasingFunction = easing
        };
        Storyboard.SetTarget(colorAnim, border);
        Storyboard.SetTargetProperty(colorAnim, new PropertyPath("(Border.BorderBrush).(SolidColorBrush.Color)"));

        // Scale X animation
        var scaleXAnim = new DoubleAnimationUsingKeyFrames();
        scaleXAnim.KeyFrames.Add(new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.Zero)));
        scaleXAnim.KeyFrames.Add(new EasingDoubleKeyFrame(1.02, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150))) { EasingFunction = easing });
        scaleXAnim.KeyFrames.Add(new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300))) { EasingFunction = easing });
        Storyboard.SetTarget(scaleXAnim, border);
        Storyboard.SetTargetProperty(scaleXAnim, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleX)"));

        // Scale Y animation
        var scaleYAnim = new DoubleAnimationUsingKeyFrames();
        scaleYAnim.KeyFrames.Add(new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.Zero)));
        scaleYAnim.KeyFrames.Add(new EasingDoubleKeyFrame(1.02, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150))) { EasingFunction = easing });
        scaleYAnim.KeyFrames.Add(new EasingDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300))) { EasingFunction = easing });
        Storyboard.SetTarget(scaleYAnim, border);
        Storyboard.SetTargetProperty(scaleYAnim, new PropertyPath("(UIElement.RenderTransform).(ScaleTransform.ScaleY)"));

        storyboard.Children.Add(colorAnim);
        storyboard.Children.Add(scaleXAnim);
        storyboard.Children.Add(scaleYAnim);

        storyboard.Begin();
    }

    #endregion

    #region Error Shake Animation

    public static readonly DependencyProperty TriggerErrorShakeProperty =
        DependencyProperty.RegisterAttached(
            "TriggerErrorShake",
            typeof(bool),
            typeof(AnimationBehaviors),
            new PropertyMetadata(false, OnTriggerErrorShakeChanged));

    public static bool GetTriggerErrorShake(DependencyObject obj)
        => (bool)obj.GetValue(TriggerErrorShakeProperty);

    public static void SetTriggerErrorShake(DependencyObject obj, bool value)
        => obj.SetValue(TriggerErrorShakeProperty, value);

    private static void OnTriggerErrorShakeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is FrameworkElement element && (bool)e.NewValue)
        {
            PlayShakeAnimation(element);

            element.Dispatcher.BeginInvoke(new Action(() =>
            {
                element.SetValue(TriggerErrorShakeProperty, false);
            }), System.Windows.Threading.DispatcherPriority.Background);
        }
    }

    private static void PlayShakeAnimation(FrameworkElement element)
    {
        if (element.RenderTransform is not TranslateTransform)
        {
            element.RenderTransform = new TranslateTransform();
        }

        var shakeAnim = new DoubleAnimationUsingKeyFrames();
        shakeAnim.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.Zero)));
        shakeAnim.KeyFrames.Add(new LinearDoubleKeyFrame(-8, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(50))));
        shakeAnim.KeyFrames.Add(new LinearDoubleKeyFrame(8, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(100))));
        shakeAnim.KeyFrames.Add(new LinearDoubleKeyFrame(-6, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150))));
        shakeAnim.KeyFrames.Add(new LinearDoubleKeyFrame(6, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200))));
        shakeAnim.KeyFrames.Add(new LinearDoubleKeyFrame(-3, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(250))));
        shakeAnim.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300))));

        ((TranslateTransform)element.RenderTransform).BeginAnimation(TranslateTransform.XProperty, shakeAnim);
    }

    #endregion
}
