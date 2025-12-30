using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Controls;

public partial class AchievementBadge : UserControl
{
    private readonly DispatcherTimer _hideTimer;

    public AchievementBadge()
    {
        InitializeComponent();
        _hideTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2.6) };
        _hideTimer.Tick += (_, _) => HideBadge();
    }

    public void Show(string title, string subtitle)
    {
        TitleText.Text = title;
        SubtitleText.Text = subtitle;
        BadgeIcon.Play();

        if (PerformanceProfile.IsSoftwareRendering)
        {
            BadgeGlow.BlurRadius = 0;
            BadgeGlow.Opacity = 0;
        }

        Visibility = Visibility.Visible;
        BadgeRoot.Opacity = 0;
        BadgeScale.ScaleX = 0.9;
        BadgeScale.ScaleY = 0.9;
        BadgeTranslate.X = 40;

        if (Resources["BadgeShowStoryboard"] is Storyboard showStoryboard)
        {
            showStoryboard.Begin(this, true);
        }

        if (!PerformanceProfile.IsSoftwareRendering && Resources["BadgeGlowStoryboard"] is Storyboard glowStoryboard)
        {
            glowStoryboard.Begin(this, true);
        }

        _hideTimer.Stop();
        _hideTimer.Start();
    }

    private void HideBadge()
    {
        _hideTimer.Stop();

        if (Resources["BadgeHideStoryboard"] is Storyboard hideStoryboard)
        {
            var storyboard = hideStoryboard.Clone();
            storyboard.Completed += (_, _) =>
            {
                Visibility = Visibility.Collapsed;
                if (Resources["BadgeGlowStoryboard"] is Storyboard glowStoryboard)
                {
                    glowStoryboard.Remove(this);
                }
            };
            storyboard.Begin(this, true);
            return;
        }

        Visibility = Visibility.Collapsed;
    }
}
