using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Controls;

[ContentProperty(nameof(CardContent))]
public partial class AnimatedCard : UserControl
{
    public static readonly DependencyProperty CardContentProperty =
        DependencyProperty.Register(nameof(CardContent), typeof(object), typeof(AnimatedCard),
            new PropertyMetadata(null));

    private const double MaxTiltDegrees = 8;
    private bool _isHovering;

    public object? CardContent
    {
        get => GetValue(CardContentProperty);
        set => SetValue(CardContentProperty, value);
    }

    public AnimatedCard()
    {
        InitializeComponent();

        if (PerformanceProfile.IsSoftwareRendering)
        {
            DepthShadow.Opacity = 0;
            GlowEffect.Opacity = 0;
            GlowBorder.Opacity = 0;
        }

        SizeChanged += OnSizeChanged;
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        UpdateMeshAspectRatio();
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpdateMeshAspectRatio();
    }

    private void UpdateMeshAspectRatio()
    {
        if (ActualWidth <= 0 || ActualHeight <= 0)
            return;

        // Calculate aspect ratio and adjust mesh positions
        // The mesh is centered at origin, so we use half-width and half-height
        var aspectRatio = ActualWidth / ActualHeight;

        // Keep width at 1.0 (from -0.5 to 0.5), adjust height based on aspect ratio
        var halfWidth = 0.5;
        var halfHeight = 0.5 / aspectRatio;

        // Update mesh positions: bottom-left, bottom-right, top-right, top-left
        CardMesh.Positions = new Point3DCollection
        {
            new Point3D(-halfWidth, -halfHeight, 0),  // bottom-left
            new Point3D(halfWidth, -halfHeight, 0),   // bottom-right
            new Point3D(halfWidth, halfHeight, 0),    // top-right
            new Point3D(-halfWidth, halfHeight, 0)    // top-left
        };

        // The 2D visual inside Viewport2DVisual3D needs explicit dimensions
        CardBorder.Width = ActualWidth;
        CardBorder.Height = ActualHeight;

        // Calculate camera distance so mesh fills viewport
        // For perspective: visible_size = 2 * distance * tan(fov/2)
        // We want the mesh to fill the viewport, so calculate distance for both dimensions
        var fovRadians = CardCamera.FieldOfView * Math.PI / 180.0;
        var tanHalfFov = Math.Tan(fovRadians / 2.0);

        // Camera distance needed for width to fill viewport
        var distanceForWidth = halfWidth / tanHalfFov;

        // Camera distance needed for height to fill viewport (accounting for viewport aspect ratio)
        // The vertical FOV is effectively: 2 * atan(tan(fov/2) / aspectRatio)
        var distanceForHeight = halfHeight / (tanHalfFov / aspectRatio);

        // Use the larger distance so the entire mesh is visible
        var cameraDistance = Math.Max(distanceForWidth, distanceForHeight);

        CardCamera.Position = new Point3D(0, 0, cameraDistance);
    }

    private void OnMouseEnter(object sender, MouseEventArgs e)
    {
        _isHovering = true;
        AnimateShadow(true);
        FadeShine(0.35);
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (!_isHovering)
            return;

        var position = e.GetPosition(RootGrid);
        UpdateTilt(position);
        UpdateShine(position);
    }

    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        _isHovering = false;
        ResetTilt();
        AnimateShadow(false);
        FadeShine(0);
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Focus();
    }

    private void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        GlowBorder.Opacity = 0.2;
        if (Resources["FocusPulseStoryboard"] is Storyboard storyboard)
        {
            storyboard.Begin(this, true);
        }
    }

    private void OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (Resources["FocusPulseStoryboard"] is Storyboard storyboard)
        {
            storyboard.Remove(this);
        }

        GlowBorder.Opacity = 0;
    }

    private void UpdateTilt(Point position)
    {
        if (RootGrid.ActualWidth <= 0 || RootGrid.ActualHeight <= 0)
            return;

        var centerX = RootGrid.ActualWidth / 2;
        var centerY = RootGrid.ActualHeight / 2;

        var offsetX = (position.X - centerX) / centerX;
        var offsetY = (position.Y - centerY) / centerY;

        TiltRotationX.Angle = -offsetY * MaxTiltDegrees;
        TiltRotationY.Angle = offsetX * MaxTiltDegrees;
    }

    private void ResetTilt()
    {
        AnimateAxisAngle(TiltRotationX, 0);
        AnimateAxisAngle(TiltRotationY, 0);
    }

    private static void AnimateAxisAngle(AxisAngleRotation3D axis, double to)
    {
        var anim = new DoubleAnimation
        {
            To = to,
            Duration = TimeSpan.FromMilliseconds(200),
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };

        axis.BeginAnimation(AxisAngleRotation3D.AngleProperty, anim);
    }

    private void UpdateShine(Point position)
    {
        if (RootGrid.ActualWidth <= 0 || RootGrid.ActualHeight <= 0)
            return;

        var x = Math.Clamp(position.X / RootGrid.ActualWidth, 0, 1);
        var y = Math.Clamp(position.Y / RootGrid.ActualHeight, 0, 1);

        ShineBrush.Center = new Point(x, y);
        ShineBrush.GradientOrigin = new Point(x, y);
    }

    private void FadeShine(double targetOpacity)
    {
        var anim = new DoubleAnimation
        {
            To = targetOpacity,
            Duration = TimeSpan.FromMilliseconds(180),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        ShineOverlay.BeginAnimation(OpacityProperty, anim);
    }

    private void AnimateShadow(bool isHovering)
    {
        var blur = isHovering ? 22 : 12;
        var depth = isHovering ? 6 : 2;
        var opacity = isHovering ? 0.5 : 0.35;

        AnimateEffectProperty(DepthShadow, DropShadowEffect.BlurRadiusProperty, blur);
        AnimateEffectProperty(DepthShadow, DropShadowEffect.ShadowDepthProperty, depth);
        AnimateEffectProperty(DepthShadow, DropShadowEffect.OpacityProperty, opacity);
    }

    private static void AnimateEffectProperty(DropShadowEffect effect, DependencyProperty property, double value)
    {
        var anim = new DoubleAnimation
        {
            To = value,
            Duration = TimeSpan.FromMilliseconds(180),
            EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
        };

        effect.BeginAnimation(property, anim);
    }
}
