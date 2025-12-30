using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using CodeTutor.Wpf.Models;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Controls;

public partial class TutorChat : UserControl
{
    private readonly ITutorService _tutorService;
    private readonly IModelDownloadService _downloadService;
    private readonly ObservableCollection<TutorMessage> _messages = new();
    private CancellationTokenSource? _currentResponseCts;
    private CancellationTokenSource? _downloadCts;
    private TutorContext _currentContext = new();
    private readonly string _modelPath;

    public event EventHandler? CloseRequested;

    public TutorChat(ITutorService tutorService, IModelDownloadService downloadService)
    {
        _tutorService = tutorService;
        _downloadService = downloadService;

        // Model path relative to application directory
        _modelPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "models", "phi4", "gpu", "gpu-int4-rtn-block-32");

        InitializeComponent();

        MessagesPanel.ItemsSource = _messages;

        _tutorService.LoadingProgressChanged += OnLoadingProgressChanged;
        _downloadService.ProgressChanged += OnDownloadProgressChanged;
        _downloadService.StatusChanged += OnDownloadStatusChanged;

        Loaded += OnLoaded;
    }

    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_tutorService.IsModelLoaded)
        {
            ShowReadyState();
            return;
        }

        // Check if model exists
        var modelExists = await _downloadService.IsModelDownloadedAsync(_modelPath);

        if (modelExists)
        {
            await LoadModelAsync();
        }
        else
        {
            ShowDownloadPrompt();
        }
    }

    private void ShowDownloadPrompt()
    {
        LoadingOverlay.Visibility = Visibility.Visible;
        DownloadPromptPanel.Visibility = Visibility.Visible;
        DownloadProgressPanel.Visibility = Visibility.Collapsed;
        LoadingModelPanel.Visibility = Visibility.Collapsed;
        StatusText.Text = "Model required";
        SetThinkingState(false);
    }

    private void ShowDownloadProgress()
    {
        LoadingOverlay.Visibility = Visibility.Visible;
        DownloadPromptPanel.Visibility = Visibility.Collapsed;
        DownloadProgressPanel.Visibility = Visibility.Visible;
        LoadingModelPanel.Visibility = Visibility.Collapsed;
        StatusText.Text = "Downloading...";
        SetThinkingState(false);
    }

    private void ShowLoadingModel()
    {
        LoadingOverlay.Visibility = Visibility.Visible;
        DownloadPromptPanel.Visibility = Visibility.Collapsed;
        DownloadProgressPanel.Visibility = Visibility.Collapsed;
        LoadingModelPanel.Visibility = Visibility.Visible;
        StatusText.Text = "Loading model...";
        SetThinkingState(false);
    }

    private void ShowReadyState()
    {
        LoadingOverlay.Visibility = Visibility.Collapsed;
        StatusText.Text = "Ready to help";
        SetThinkingState(false);

        if (_messages.Count == 0)
        {
            _messages.Add(new TutorMessage(
                MessageRole.Assistant,
                "Hi! I'm your AI tutor. I can help you understand code, fix errors, or explain concepts. What would you like to learn?"));
        }
    }

    private async void DownloadModelButton_Click(object sender, RoutedEventArgs e)
    {
        ShowDownloadProgress();

        _downloadCts = new CancellationTokenSource();

        try
        {
            var success = await _downloadService.DownloadModelAsync(_modelPath, _downloadCts.Token);

            if (success)
            {
                await LoadModelAsync();
            }
            else
            {
                ShowDownloadPrompt();
            }
        }
        catch (OperationCanceledException)
        {
            ShowDownloadPrompt();
        }
        catch (Exception ex)
        {
            _messages.Add(new TutorMessage(
                MessageRole.Assistant,
                $"Download failed: {ex.Message}"));
            ShowDownloadPrompt();
        }
        finally
        {
            _downloadCts?.Dispose();
            _downloadCts = null;
        }
    }

    private void CancelDownloadButton_Click(object sender, RoutedEventArgs e)
    {
        _downloadCts?.Cancel();
    }

    private void OnDownloadProgressChanged(object? sender, ModelDownloadProgress progress)
    {
        Dispatcher.Invoke(() =>
        {
            DownloadProgress.Value = progress.OverallProgress;

            var downloadedMB = progress.BytesDownloaded / (1024.0 * 1024.0);
            var totalMB = progress.TotalBytes / (1024.0 * 1024.0);
            DownloadDetailText.Text = $"{downloadedMB:F1} MB / {totalMB:F1} MB";
        });
    }

    private void OnDownloadStatusChanged(object? sender, string status)
    {
        Dispatcher.Invoke(() =>
        {
            DownloadStatusText.Text = status;
        });
    }

    private async Task LoadModelAsync()
    {
        ShowLoadingModel();

        try
        {
            await _tutorService.LoadModelAsync();
            ShowReadyState();
        }
        catch (Exception ex)
        {
            StatusText.Text = "Failed to load";
            _messages.Add(new TutorMessage(
                MessageRole.Assistant,
                $"Sorry, I couldn't load the AI model: {ex.Message}"));
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
        SetThinkingState(true);
        PerformanceProfile.SetUiThrottled(true);

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
            SetThinkingState(false);
            PerformanceProfile.SetUiThrottled(false);
        }
    }

    private void SetThinkingState(bool isThinking)
    {
        ThinkingIndicator.Visibility = isThinking ? Visibility.Visible : Visibility.Collapsed;

        if (Resources["ThinkingDotsStoryboard"] is Storyboard storyboard)
        {
            if (isThinking)
            {
                storyboard.Begin(this, true);
            }
            else
            {
                storyboard.Remove(this);
            }
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
