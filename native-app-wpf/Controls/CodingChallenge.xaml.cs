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
    private readonly string _originalCode;

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

        // Hide hint button if no hints available
        if (challenge.Hints == null || challenge.Hints.Count == 0)
        {
            HintButton.Visibility = Visibility.Collapsed;
        }
    }

    private async void RunCode_Click(object sender, RoutedEventArgs e)
    {
        OutputPanel.Visibility = Visibility.Visible;
        OutputText.Text = "Running...";
        OutputText.Foreground = (System.Windows.Media.Brush)FindResource("TextPrimaryBrush");

        var result = await _executionService.ExecuteAsync(CodeEditor.Text, _challenge.Language);

        if (result.Success)
        {
            OutputText.Text = string.IsNullOrEmpty(result.Output) ? "(No output)" : result.Output;
            OutputText.Foreground = (System.Windows.Media.Brush)FindResource("TextPrimaryBrush");
        }
        else
        {
            OutputText.Text = string.IsNullOrEmpty(result.Error) ? result.Output : result.Error;
            OutputText.Foreground = (System.Windows.Media.Brush)FindResource("AccentRedBrush");
        }
    }

    private void ShowHint_Click(object sender, RoutedEventArgs e)
    {
        if (_challenge.Hints != null && _currentHintIndex < _challenge.Hints.Count)
        {
            var hint = _challenge.Hints[_currentHintIndex];
            HintText.Text = hint.Text;
            HintPanel.Visibility = Visibility.Visible;
            _currentHintIndex++;

            if (_currentHintIndex >= _challenge.Hints.Count)
            {
                HintButton.Content = new TextBlock { Text = "No More Hints" };
                HintButton.IsEnabled = false;
            }
            else
            {
                HintButton.Content = new TextBlock { Text = $"Next Hint ({_currentHintIndex + 1}/{_challenge.Hints.Count})" };
            }
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
        HintPanel.Visibility = Visibility.Collapsed;
        _currentHintIndex = 0;

        if (_challenge.Hints != null && _challenge.Hints.Count > 0)
        {
            HintButton.Content = new TextBlock { Text = "Show Hint" };
            HintButton.IsEnabled = true;
        }
    }
}
