using System.Windows.Controls;
using System.Windows.Media;

namespace CodeTutor.Wpf.Controls;

public partial class EditorStatusBar : UserControl
{
    public EditorStatusBar()
    {
        InitializeComponent();
    }

    public void UpdatePosition(int line, int column)
    {
        PositionText.Text = $"Ln {line}, Col {column}";
    }

    public void SetLanguage(string language)
    {
        LanguageText.Text = language;
    }

    public void SetStatus(string status, bool isReady)
    {
        StatusText.Text = status;
        StatusIndicator.Fill = isReady
            ? (Brush)FindResource("AccentGreenBrush")
            : (Brush)FindResource("AccentOrangeBrush");
    }

    public void SetRuntimeStatus(bool isAvailable, string runtimeInfo)
    {
        if (isAvailable)
        {
            RuntimeStatusIcon.Text = "\u2713"; // checkmark
            RuntimeStatusIcon.Foreground = (Brush)FindResource("AccentGreenBrush");
            RuntimeStatusText.Text = runtimeInfo;
        }
        else
        {
            RuntimeStatusIcon.Text = "\u26A0"; // warning sign
            RuntimeStatusIcon.Foreground = (Brush)FindResource("AccentOrangeBrush");
            RuntimeStatusText.Text = "Not installed";
        }
    }
}
