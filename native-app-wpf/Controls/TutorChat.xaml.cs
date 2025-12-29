using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodeTutor.Wpf.Controls;

public partial class TutorChat : UserControl
{
    public TutorChat()
    {
        InitializeComponent();
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement close functionality
        this.Visibility = Visibility.Collapsed;
    }

    private void QuickAction_Click(object sender, RoutedEventArgs e)
    {
        // TODO: Implement quick action handling
        if (sender is Button button && button.Tag is string action)
        {
            InputTextBox.Text = action switch
            {
                "explain" => "Can you explain this code?",
                "error" => "I'm getting an error. Can you help?",
                "hint" => "Can you give me a hint?",
                _ => ""
            };
        }
    }

    private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && !Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
        {
            e.Handled = true;
            SendMessage();
        }
    }

    private void SendButton_Click(object sender, RoutedEventArgs e)
    {
        SendMessage();
    }

    private void SendMessage()
    {
        var message = InputTextBox.Text?.Trim();
        if (string.IsNullOrEmpty(message))
            return;

        // TODO: Implement message sending to AI tutor
        InputTextBox.Text = "";
    }
}
