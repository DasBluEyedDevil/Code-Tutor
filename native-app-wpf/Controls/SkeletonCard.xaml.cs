using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace CodeTutor.Wpf.Controls;

public partial class SkeletonCard : UserControl
{
    public SkeletonCard()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (FindResource("ShimmerAnimation") is Storyboard storyboard)
        {
            storyboard.Begin();
        }
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (FindResource("ShimmerAnimation") is Storyboard storyboard)
        {
            storyboard.Stop();
        }
    }
}
