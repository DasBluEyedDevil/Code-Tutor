using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Controls;

public partial class ChatMessageBubble : UserControl
{
    public static readonly DependencyProperty MessageProperty =
        DependencyProperty.Register(nameof(Message), typeof(TutorMessage), typeof(ChatMessageBubble),
            new PropertyMetadata(null, OnMessageChanged));

    private TutorMessage? _lastMessage;

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
            bubble.ApplyMessageStyle(message);
        }
    }

    private void ApplyMessageStyle(TutorMessage message)
    {
        var isNewMessage = !ReferenceEquals(message, _lastMessage);
        _lastMessage = message;

        if (message.Role == MessageRole.User)
        {
            MessageBorder.Background = (Brush)FindResource("AccentBlueBrush");
            MessageBorder.CornerRadius = new CornerRadius(12, 12, 4, 12);
            MessageBorder.HorizontalAlignment = HorizontalAlignment.Right;
            MessageBorder.Margin = new Thickness(40, 4, 8, 4);
            UserMessageText.Text = message.Content;
            UserMessageText.Foreground = Brushes.White;
            UserMessageText.Visibility = Visibility.Visible;
            AssistantMessageText.Visibility = Visibility.Collapsed;
        }
        else
        {
            MessageBorder.Background = (Brush)FindResource("BackgroundLightBrush");
            MessageBorder.CornerRadius = new CornerRadius(12, 12, 12, 4);
            MessageBorder.HorizontalAlignment = HorizontalAlignment.Left;
            MessageBorder.Margin = new Thickness(8, 4, 40, 4);
            AssistantMessageText.TypewriterText = message.Content;
            AssistantMessageText.Visibility = Visibility.Visible;
            UserMessageText.Visibility = Visibility.Collapsed;
        }

        if (isNewMessage)
        {
            PlayEntranceAnimation(message.Role);
        }
    }

    private void PlayEntranceAnimation(MessageRole role)
    {
        MessageBorder.Opacity = 0;

        if (MessageBorder.RenderTransform is not TranslateTransform)
        {
            MessageBorder.RenderTransform = new TranslateTransform();
        }

        var translateTransform = (TranslateTransform)MessageBorder.RenderTransform;
        translateTransform.X = role == MessageRole.User ? 24 : -24;

        var duration = TimeSpan.FromMilliseconds(240);
        var easing = new CubicEase { EasingMode = EasingMode.EaseOut };

        var opacityAnim = new DoubleAnimation(0, 1, duration) { EasingFunction = easing };
        var slideAnim = new DoubleAnimation(translateTransform.X, 0, duration) { EasingFunction = easing };

        MessageBorder.BeginAnimation(OpacityProperty, opacityAnim);
        translateTransform.BeginAnimation(TranslateTransform.XProperty, slideAnim);
    }
}
