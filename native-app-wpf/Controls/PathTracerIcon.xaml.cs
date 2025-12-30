using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CodeTutor.Wpf.Controls;

public partial class PathTracerIcon : UserControl
{
    public static readonly DependencyProperty DataProperty =
        DependencyProperty.Register(nameof(Data), typeof(Geometry), typeof(PathTracerIcon),
            new PropertyMetadata(CreateDefaultGeometry(), OnGeometryChanged));

    public static readonly DependencyProperty StrokeProperty =
        DependencyProperty.Register(nameof(Stroke), typeof(Brush), typeof(PathTracerIcon),
            new PropertyMetadata(Brushes.White));

    public static readonly DependencyProperty StrokeThicknessProperty =
        DependencyProperty.Register(nameof(StrokeThickness), typeof(double), typeof(PathTracerIcon),
            new PropertyMetadata(2.0));

    public static readonly DependencyProperty AutoPlayProperty =
        DependencyProperty.Register(nameof(AutoPlay), typeof(bool), typeof(PathTracerIcon),
            new PropertyMetadata(true));

    public static readonly DependencyProperty AnimationDurationProperty =
        DependencyProperty.Register(nameof(AnimationDuration), typeof(Duration), typeof(PathTracerIcon),
            new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(650))));

    public Geometry Data
    {
        get => (Geometry)GetValue(DataProperty);
        set => SetValue(DataProperty, value);
    }

    public Brush Stroke
    {
        get => (Brush)GetValue(StrokeProperty);
        set => SetValue(StrokeProperty, value);
    }

    public double StrokeThickness
    {
        get => (double)GetValue(StrokeThicknessProperty);
        set => SetValue(StrokeThicknessProperty, value);
    }

    public bool AutoPlay
    {
        get => (bool)GetValue(AutoPlayProperty);
        set => SetValue(AutoPlayProperty, value);
    }

    public Duration AnimationDuration
    {
        get => (Duration)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    public PathTracerIcon()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (AutoPlay)
        {
            Play();
        }
    }

    public void Play()
    {
        var length = GetGeometryLength(TracePath.Data);
        if (length <= 0)
            return;

        TracePath.StrokeDashArray = new DoubleCollection { length, length };
        TracePath.StrokeDashOffset = length;

        var animation = new DoubleAnimation
        {
            From = length,
            To = 0,
            Duration = AnimationDuration,
            EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
        };

        TracePath.BeginAnimation(Shape.StrokeDashOffsetProperty, animation);
    }

    private static void OnGeometryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PathTracerIcon icon && icon.AutoPlay && icon.IsLoaded)
        {
            icon.Play();
        }
    }

    private static Geometry CreateDefaultGeometry()
    {
        return Geometry.Parse("M 2 10 L 8 16 L 18 4");
    }

    private static double GetGeometryLength(Geometry geometry)
    {
        var flattened = geometry.GetFlattenedPathGeometry();
        double length = 0;

        foreach (var figure in flattened.Figures)
        {
            var lastPoint = figure.StartPoint;
            foreach (var segment in figure.Segments)
            {
                switch (segment)
                {
                    case LineSegment line:
                        length += (line.Point - lastPoint).Length;
                        lastPoint = line.Point;
                        break;
                    case PolyLineSegment poly:
                        foreach (var point in poly.Points)
                        {
                            length += (point - lastPoint).Length;
                            lastPoint = point;
                        }
                        break;
                }
            }
        }

        return length;
    }
}
