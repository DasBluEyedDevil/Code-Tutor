using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CodeTutor.Wpf.Models;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Controls;

public partial class TutorChat : UserControl
{
    private readonly ITutorService _tutorService;
    private readonly ObservableCollection<TutorMessage> _messages = new();
    private CancellationTokenSource? _currentResponseCts;
    private TutorContext _currentContext = new();

    public event EventHandler? CloseRequested;

    public TutorChat(ITutorService tutorService)
    {
        _tutorService = tutorService;
        InitializeComponent();

        MessagesPanel.ItemsSource = _messages;

        _tutorService.LoadingProgressChanged += OnLoadingProgressChanged;

        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (!_tutorService.IsModelLoaded)
        {
            await LoadModelAsync();
        }
    }

    private async Task LoadModelAsync()
    {
        LoadingOverlay.Visibility = Visibility.Visible;
        StatusText.Text = "Loading model...";

        try
        {
            await _tutorService.LoadModelAsync();
            StatusText.Text = "Ready to help";

            // Add welcome message
            _messages.Add(new TutorMessage(
                MessageRole.Assistant,
                "Hi! I'm your AI tutor. I can help you understand code, fix errors, or explain concepts. What would you like to learn?"));
        }
        catch (Exception ex)
        {
            StatusText.Text = "Failed to load";
            _messages.Add(new TutorMessage(
                MessageRole.Assistant,
                $"Sorry, I couldn't load the AI model: {ex.Message}"));
        }
        finally
        {
            LoadingOverlay.Visibility = Visibility.Collapsed;
        }
    }

    private void OnLoadingProgressChanged(object? sender, int progress)
    {
        Dispatcher.Invoke(() =>
        {
            LoadingProgress.Value = progress;
        });
    }

    public void UpdateContext(TutorContext context)
    {
        _currentContext = context;
    }

    private async void SendButton_Click(object sender, RoutedEventArgs e)
    {
        await SendMessageAsync();
    }

    private async void InputTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter && !Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
        {
            e.Handled = true;
            await SendMessageAsync();
        }
    }

    private async Task SendMessageAsync()
    {
        var userInput = InputTextBox.Text.Trim();
        if (string.IsNullOrEmpty(userInput) || !_tutorService.IsModelLoaded)
            return;

        // Cancel and dispose any ongoing response
        _currentResponseCts?.Cancel();
        _currentResponseCts?.Dispose();
        _currentResponseCts = new CancellationTokenSource();

        // Add user message
        _messages.Add(new TutorMessage(MessageRole.User, userInput));
        InputTextBox.Text = string.Empty;
        ScrollToBottom();

        // Prepare assistant message for streaming
        var assistantMessage = new TutorMessage(MessageRole.Assistant, string.Empty);
        _messages.Add(assistantMessage);

        StatusText.Text = "Thinking...";
        SendButton.IsEnabled = false;

        try
        {
            await foreach (var token in _tutorService.SendMessageAsync(
                userInput,
                _currentContext,
                _messages.Take(_messages.Count - 1).ToList(),
                _currentResponseCts.Token))
            {
                assistantMessage.Content += token;
                // Force UI update
                var index = _messages.IndexOf(assistantMessage);
                _messages[index] = assistantMessage;
                ScrollToBottom();
            }
        }
        catch (OperationCanceledException)
        {
            assistantMessage.Content += " [Response cancelled]";
        }
        catch (Exception ex)
        {
            assistantMessage.Content = $"Sorry, something went wrong: {ex.Message}";
        }
        finally
        {
            StatusText.Text = "Ready to help";
            SendButton.IsEnabled = true;
        }
    }

    private void QuickAction_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is string action)
        {
            var prompt = action switch
            {
                "explain" => "Can you explain what this code does step by step?",
                "error" => "I'm getting an error. Can you help me understand what's wrong?",
                "hint" => "I'm stuck. Can you give me a hint without giving away the answer?",
                _ => string.Empty
            };

            if (!string.IsNullOrEmpty(prompt))
            {
                InputTextBox.Text = prompt;
                _ = SendMessageAsync();
            }
        }
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        CloseRequested?.Invoke(this, EventArgs.Empty);
    }

    private void ScrollToBottom()
    {
        MessagesScrollViewer.ScrollToEnd();
    }

    public void ClearHistory()
    {
        _messages.Clear();
    }
}
