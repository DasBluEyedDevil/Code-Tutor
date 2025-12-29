using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Controls;

public partial class ChatMessageBubble : UserControl
{
    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(nameof(Message), typeof(TutorMessage), typeof(ChatMessageBubble),
            new PropertyMetadata(null, OnMessageChanged));

    public TutorMessage? Message
    {
        get => (TutorMessage?)GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public ChatMessageBubble()
    {
        InitializeComponent();
    }

    private static void OnMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is ChatMessageBubble bubble && e.NewValue is TutorMessage message)
        {
            bubble.UpdateAppearance(message);
        }
    }

    private void UpdateAppearance(TutorMessage message)
    {
        MessageText.Text = message.Content;

        if (message.Role == MessageRole.User)
        {
            MessageBorder.Background = (Brush)FindResource("AccentBlueBrush");
            MessageBorder.CornerRadius = new CornerRadius(12, 12, 4, 12);
            MessageBorder.HorizontalAlignment = HorizontalAlignment.Right;
            MessageBorder.Margin = new Thickness(40, 4, 8, 4);
            MessageText.Foreground = Brushes.White;
        }
        else
        {
            MessageBorder.Background = (Brush)FindResource("BackgroundLightBrush");
            MessageBorder.CornerRadius = new CornerRadius(12, 12, 12, 4);
            MessageBorder.HorizontalAlignment = HorizontalAlignment.Left;
            MessageBorder.Margin = new Thickness(8, 4, 40, 4);
            MessageText.Foreground = (Brush)FindResource("TextPrimaryBrush");
        }
    }
}
