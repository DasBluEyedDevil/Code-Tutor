using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CodeTutor.Wpf.Models;
using CodeTutor.Wpf.Services;

namespace CodeTutor.Wpf.Controls;

public partial class CodingChallenge : UserControl
{
    private readonly Challenge _challenge;
    private readonly ICodeExecutionService _executionService;
    private int _currentHintIndex = 0;
    private int _failedAttempts = 0;
    private readonly string _originalCode;
    private CancellationTokenSource? _executionCts;

    public event EventHandler<string>? ChallengeCompleted;
    public bool IsCompleted { get; private set; }
    public string ChallengeId => _challenge.Id;

    public CodingChallenge(Challenge challenge)
    {
        InitializeComponent();
        _challenge = challenge;
        _originalCode = challenge.StarterCode;
        _executionService = new CodeExecutionService();

        ChallengeTitle.Text = challenge.Title;
        Description.Text = challenge.Description;
        Instructions.Text = challenge.Instructions;
        CodeEditor.Text = challenge.StarterCode;

        // Set syntax highlighting based on language
        var highlighting = SyntaxHighlightingService.GetHighlightingForLanguage(challenge.Language);
        if (highlighting != null)
        {
            CodeEditor.SyntaxHighlighting = highlighting;
        }

        // Set up status bar
        StatusBar.SetLanguage(challenge.Language);
        StatusBar.SetStatus("Ready", true);

        // Update position on caret change
        CodeEditor.TextArea.Caret.PositionChanged += (s, e) =>
        {
            StatusBar.UpdatePosition(
                CodeEditor.TextArea.Caret.Line,
                CodeEditor.TextArea.Caret.Column);
        };

        UpdateHintButtonState();

        // Check runtime availability asynchronously
        _ = CheckRuntimeAsync();
    }

    private async Task CheckRuntimeAsync()
    {
        var runtimeInfo = await _executionService.GetRuntimeInfoAsync(_challenge.Language);
        StatusBar.SetRuntimeStatus(runtimeInfo.IsAvailable, runtimeInfo.Version);

        if (!runtimeInfo.IsAvailable && !string.IsNullOrEmpty(runtimeInfo.InstallHint))
        {
            // Show a subtle warning in the output panel
            OutputPanel.Visibility = Visibility.Visible;
            OutputText.Text = $"Note: {runtimeInfo.InstallHint}";
            OutputText.Foreground = (System.Windows.Media.Brush)FindResource("AccentYellowBrush");
        }
    }

    private void UpdateHintButtonState()
    {
        if (_challenge.Hints == null || _challenge.Hints.Count == 0)
        {
            HintButton.Visibility = Visibility.Collapsed;
            return;
        }

        // Require at least 1 failed attempt to show first hint
        if (_failedAttempts == 0)
        {
            HintButton.Content = new TextBlock { Text = "Hint (run code first)" };
            HintButton.IsEnabled = false;
        }
        else if (_currentHintIndex >= _challenge.Hints.Count)
        {
            HintButton.Content = new TextBlock { Text = "No More Hints" };
            HintButton.IsEnabled = false;
        }
        else
        {
            int hintsAvailable = Math.Min(_failedAttempts, _challenge.Hints.Count);
            if (_currentHintIndex < hintsAvailable)
            {
                HintButton.Content = new TextBlock { Text = $"Show Hint ({_currentHintIndex + 1}/{hintsAvailable})" };
                HintButton.IsEnabled = true;
            }
            else
            {
                HintButton.Content = new TextBlock { Text = "Try again to unlock more hints" };
                HintButton.IsEnabled = false;
            }
        }
    }

    private async void RunCode_Click(object sender, RoutedEventArgs e)
    {
        _executionCts?.Cancel();
        _executionCts = new CancellationTokenSource();

        RunningIndicator.Visibility = Visibility.Visible;
        StopButton.Visibility = Visibility.Visible;

        StatusBar.SetStatus("Running...", false);

        try
        {
            OutputPanel.Visibility = Visibility.Visible;
            TestResultsPanel.Visibility = Visibility.Collapsed;
            OutputText.Text = "Running...";
            OutputText.Foreground = (System.Windows.Media.Brush)FindResource("TextPrimaryBrush");

            // Reset to default border while running
            OutputPanel.BorderBrush = (System.Windows.Media.Brush)FindResource("BorderDefaultBrush");
            OutputPanel.Background = (System.Windows.Media.Brush)FindResource("BackgroundDarkBrush");

            var result = await _executionService.ExecuteAsync(CodeEditor.Text, _challenge.Language);

            if (result.Success)
            {
                OutputText.Text = string.IsNullOrEmpty(result.Output) ? "(No output)" : result.Output;
                OutputText.Foreground = (System.Windows.Media.Brush)FindResource("TextPrimaryBrush");

                // Set success styling
                OutputPanel.BorderBrush = (System.Windows.Media.Brush)FindResource("AccentGreenBrush");
                OutputPanel.Background = (System.Windows.Media.Brush)FindResource("SuccessBackgroundBrush");

                // Run test case validation if test cases exist
                if (_challenge.TestCases != null && _challenge.TestCases.Count > 0)
                {
                    ValidateTestCases(result.Output);
                }

                StatusBar.SetStatus("Ready", true);
            }
            else
            {
                OutputText.Text = string.IsNullOrEmpty(result.Error) ? result.Output : result.Error;
                OutputText.Foreground = (System.Windows.Media.Brush)FindResource("AccentRedBrush");

                // Set error styling
                OutputPanel.BorderBrush = (System.Windows.Media.Brush)FindResource("AccentRedBrush");
                OutputPanel.Background = (System.Windows.Media.Brush)FindResource("ErrorBackgroundBrush");

                _failedAttempts++;
                UpdateHintButtonState();

                StatusBar.SetStatus("Ready", true);
            }
        }
        catch (Exception ex)
        {
            OutputText.Text = $"Execution failed: {ex.Message}";
            OutputText.Foreground = (System.Windows.Media.Brush)FindResource("AccentRedBrush");
            OutputPanel.BorderBrush = (System.Windows.Media.Brush)FindResource("AccentRedBrush");
            OutputPanel.Background = (System.Windows.Media.Brush)FindResource("ErrorBackgroundBrush");

            StatusBar.SetStatus("Ready", true);
        }
        finally
        {
            RunningIndicator.Visibility = Visibility.Collapsed;
            StopButton.Visibility = Visibility.Collapsed;
        }
    }

    private void StopExecution_Click(object sender, RoutedEventArgs e)
    {
        _executionCts?.Cancel();
        OutputText.Text = "Execution cancelled.";
        OutputText.Foreground = (System.Windows.Media.Brush)FindResource("AccentOrangeBrush");
        OutputPanel.BorderBrush = (System.Windows.Media.Brush)FindResource("AccentOrangeBrush");
        OutputPanel.Background = (System.Windows.Media.Brush)FindResource("WarningBackgroundBrush");

        RunningIndicator.Visibility = Visibility.Collapsed;
        StopButton.Visibility = Visibility.Collapsed;
        StatusBar.SetStatus("Cancelled", true);
    }

    private void ValidateTestCases(string actualOutput)
    {
        var results = new List<object>();
        int passed = 0;
        int total = 0;

        foreach (var testCase in _challenge.TestCases)
        {
            if (!testCase.IsVisible) continue;
            total++;

            bool testPassed = string.IsNullOrEmpty(testCase.ExpectedOutput) ||
                              actualOutput.Contains(testCase.ExpectedOutput.Trim());

            if (testPassed) passed++;

            results.Add(new
            {
                Description = testCase.Description,
                Status = testPassed ? "\u2713" : "\u2717",
                Color = testPassed
                    ? (System.Windows.Media.Brush)FindResource("AccentGreenBrush")
                    : (System.Windows.Media.Brush)FindResource("AccentRedBrush")
            });
        }

        TestResultsPanel.Visibility = Visibility.Visible;
        TestResultsList.ItemsSource = results;

        bool allPassed = passed == total;
        TestSummary.Text = $"{passed}/{total} tests passed";
        TestSummary.Foreground = allPassed
            ? (System.Windows.Media.Brush)FindResource("AccentGreenBrush")
            : (System.Windows.Media.Brush)FindResource("AccentRedBrush");

        // Set panel border and background based on overall result
        TestResultsPanel.BorderBrush = allPassed
            ? (System.Windows.Media.Brush)FindResource("AccentGreenBrush")
            : (System.Windows.Media.Brush)FindResource("AccentRedBrush");
        TestResultsPanel.Background = allPassed
            ? (System.Windows.Media.Brush)FindResource("SuccessBackgroundBrush")
            : (System.Windows.Media.Brush)FindResource("ErrorBackgroundBrush");

        if (!allPassed)
        {
            _failedAttempts++;
            UpdateHintButtonState();
        }

        // Fire completion event if all tests pass
        if (allPassed && !IsCompleted)
        {
            IsCompleted = true;
            ChallengeCompleted?.Invoke(this, _challenge.Id);
        }
    }

    private void ShowHint_Click(object sender, RoutedEventArgs e)
    {
        if (_challenge.Hints != null && _currentHintIndex < _challenge.Hints.Count && _currentHintIndex < _failedAttempts)
        {
            var hint = _challenge.Hints[_currentHintIndex];
            HintText.Text = hint.Text;
            HintPanel.Visibility = Visibility.Visible;
            _currentHintIndex++;
            UpdateHintButtonState();
        }
    }

    private void ShowSolution_Click(object sender, RoutedEventArgs e)
    {
        var result = MessageBox.Show(
            "Are you sure you want to see the solution? Try solving it yourself first!",
            "Show Solution",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            CodeEditor.Text = _challenge.Solution;
        }
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        CodeEditor.Text = _originalCode;
        OutputPanel.Visibility = Visibility.Collapsed;
        TestResultsPanel.Visibility = Visibility.Collapsed;
        HintPanel.Visibility = Visibility.Collapsed;
        _currentHintIndex = 0;
        _failedAttempts = 0;

        UpdateHintButtonState();
    }
}
