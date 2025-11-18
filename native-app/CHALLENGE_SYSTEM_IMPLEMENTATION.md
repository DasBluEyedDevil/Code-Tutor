# Challenge System Implementation Guide

## Overview

This document provides complete specifications for implementing all 6 challenge types in the native C#/Avalonia application.

---

## Challenge Type Architecture

### Base Challenge Contract

```csharp
namespace CodeTutor.Native.Models;

public abstract class Challenge
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public abstract string Type { get; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("hints")]
    public List<string> Hints { get; set; } = new();

    [JsonPropertyName("commonMistakes")]
    public List<CommonMistake>? CommonMistakes { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; } = 100;
}

public class CommonMistake
{
    [JsonPropertyName("pattern")]
    public string Pattern { get; set; } = string.Empty;

    [JsonPropertyName("explanation")]
    public string Explanation { get; set; } = string.Empty;

    [JsonPropertyName("fix")]
    public string Fix { get; set; } = string.Empty;
}
```

---

## Challenge Type 1: Multiple Choice

### Data Model
```csharp
public class MultipleChoiceChallenge : Challenge
{
    public override string Type => "MULTIPLE_CHOICE";

    [JsonPropertyName("question")]
    public string Question { get; set; } = string.Empty;

    [JsonPropertyName("options")]
    public List<string> Options { get; set; } = new();

    [JsonPropertyName("correctAnswer")]
    public int CorrectAnswer { get; set; }

    [JsonPropertyName("explanation")]
    public string Explanation { get; set; } = string.Empty;
}
```

### ViewModel
```csharp
public class MultipleChoiceViewModel : ChallengeViewModelBase
{
    private readonly MultipleChoiceChallenge _challenge;
    private int? _selectedOption;
    private bool _hasSubmitted;
    private bool _isCorrect;

    public MultipleChoiceViewModel(MultipleChoiceChallenge challenge)
    {
        _challenge = challenge;

        SubmitCommand = ReactiveCommand.Create(Submit,
            this.WhenAnyValue(x => x.SelectedOption, opt => opt.HasValue));
    }

    public List<string> Options => _challenge.Options;

    public int? SelectedOption
    {
        get => _selectedOption;
        set => this.RaiseAndSetIfChanged(ref _selectedOption, value);
    }

    public bool HasSubmitted
    {
        get => _hasSubmitted;
        set => this.RaiseAndSetIfChanged(ref _hasSubmitted, value);
    }

    public bool IsCorrect
    {
        get => _isCorrect;
        set => this.RaiseAndSetIfChanged(ref _isCorrect, value);
    }

    public string Explanation => _challenge.Explanation;

    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }

    private void Submit()
    {
        HasSubmitted = true;
        IsCorrect = SelectedOption == _challenge.CorrectAnswer;

        OnChallengeCompleted(new ChallengeResult
        {
            Passed = IsCorrect,
            Score = IsCorrect ? _challenge.Points : 0,
            HintsUsed = HintsRevealed
        });
    }
}
```

### XAML View
```xml
<UserControl xmlns="https://github.com/avaloniaui"
             x:Class="CodeTutor.Native.Views.Challenges.MultipleChoiceView">
    <StackPanel Spacing="16">
        <!-- Question -->
        <TextBlock Text="{Binding Question}"
                   FontSize="18"
                   FontWeight="Bold"
                   TextWrapping="Wrap" />

        <!-- Options (Radio Buttons) -->
        <ItemsControl ItemsSource="{Binding Options}">
            <ItemsControl.ItemTemplate>
                <DataTemplate>
                    <RadioButton GroupName="MultipleChoice"
                                 IsChecked="{Binding IsSelected}"
                                 IsEnabled="{Binding !HasSubmitted}"
                                 Margin="0,8">
                        <StackPanel Orientation="Horizontal" Spacing="8">
                            <!-- A, B, C, D Label -->
                            <Border Background="#3A3A3A"
                                    CornerRadius="4"
                                    Padding="8,4"
                                    Width="32" Height="32">
                                <TextBlock Text="{Binding Label}"
                                          FontWeight="Bold"
                                          HorizontalAlignment="Center"
                                          VerticalAlignment="Center" />
                            </Border>

                            <!-- Option Text -->
                            <TextBlock Text="{Binding Text}"
                                      TextWrapping="Wrap"
                                      VerticalAlignment="Center" />
                        </StackPanel>
                    </RadioButton>
                </DataTemplate>
            </ItemsControl.ItemTemplate>
        </ItemsControl>

        <!-- Submit Button -->
        <Button Content="Submit Answer"
                Command="{Binding SubmitCommand}"
                IsVisible="{Binding !HasSubmitted}" />

        <!-- Result Display -->
        <Border IsVisible="{Binding HasSubmitted}"
                Background="{Binding IsCorrect, Converter={StaticResource ResultToBrushConverter}}"
                Padding="16"
                CornerRadius="8">
            <StackPanel Spacing="8">
                <TextBlock Text="{Binding IsCorrect, Converter={StaticResource BoolToResultTextConverter}}"
                          FontSize="16"
                          FontWeight="Bold" />

                <TextBlock Text="{Binding Explanation}"
                          TextWrapping="Wrap"
                          Opacity="0.9" />
            </StackPanel>
        </Border>

        <!-- Hints Panel (from base) -->
        <ContentControl Content="{Binding HintsPanel}" />
    </StackPanel>
</UserControl>
```

---

## Challenge Type 2: True/False

### Data Model
```csharp
public class TrueFalseChallenge : Challenge
{
    public override string Type => "TRUE_FALSE";

    [JsonPropertyName("question")]
    public string Question { get; set; } = string.Empty;

    [JsonPropertyName("correctAnswer")]
    public bool CorrectAnswer { get; set; }

    [JsonPropertyName("explanation")]
    public string Explanation { get; set; } = string.Empty;
}
```

### ViewModel
```csharp
public class TrueFalseViewModel : ChallengeViewModelBase
{
    private readonly TrueFalseChallenge _challenge;
    private bool? _userAnswer;
    private bool _hasSubmitted;

    public TrueFalseViewModel(TrueFalseChallenge challenge)
    {
        _challenge = challenge;

        SubmitTrueCommand = ReactiveCommand.Create(() => Submit(true));
        SubmitFalseCommand = ReactiveCommand.Create(() => Submit(false));
    }

    public ReactiveCommand<Unit, Unit> SubmitTrueCommand { get; }
    public ReactiveCommand<Unit, Unit> SubmitFalseCommand { get; }

    private void Submit(bool answer)
    {
        _userAnswer = answer;
        _hasSubmitted = true;

        var isCorrect = answer == _challenge.CorrectAnswer;

        OnChallengeCompleted(new ChallengeResult
        {
            Passed = isCorrect,
            Score = isCorrect ? _challenge.Points : 0
        });
    }
}
```

### XAML View
```xml
<StackPanel Spacing="24">
    <TextBlock Text="{Binding Question}"
               FontSize="18"
               TextWrapping="Wrap" />

    <!-- Large True/False Buttons -->
    <Grid ColumnDefinitions="*,16,*">
        <!-- True Button -->
        <Button Grid.Column="0"
                Command="{Binding SubmitTrueCommand}"
                IsEnabled="{Binding !HasSubmitted}"
                Height="100">
            <StackPanel Spacing="8">
                <Path Data="{StaticResource CheckmarkIcon}"
                      Fill="#4EC9B0"
                      Width="32" Height="32" />
                <TextBlock Text="TRUE"
                          FontSize="20"
                          FontWeight="Bold" />
            </StackPanel>
        </Button>

        <!-- False Button -->
        <Button Grid.Column="2"
                Command="{Binding SubmitFalseCommand}"
                IsEnabled="{Binding !HasSubmitted}"
                Height="100">
            <StackPanel Spacing="8">
                <Path Data="{StaticResource XIcon}"
                      Fill="#F48771"
                      Width="32" Height="32" />
                <TextBlock Text="FALSE"
                          FontSize="20"
                          FontWeight="Bold" />
            </StackPanel>
        </Button>
    </Grid>

    <!-- Result Display -->
    <Border IsVisible="{Binding HasSubmitted}">
        <!-- Same as Multiple Choice -->
    </Border>
</StackPanel>
```

---

## Challenge Type 3: Code Output

### Data Model
```csharp
public class CodeOutputChallenge : Challenge
{
    public override string Type => "CODE_OUTPUT";

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = "python";

    [JsonPropertyName("expectedOutput")]
    public string ExpectedOutput { get; set; } = string.Empty;

    [JsonPropertyName("allowRunning")]
    public bool AllowRunning { get; set; } = true;
}
```

### ViewModel
```csharp
public class CodeOutputViewModel : ChallengeViewModelBase
{
    private readonly CodeOutputChallenge _challenge;
    private readonly ICodeExecutor _codeExecutor;
    private string _userAnswer = string.Empty;
    private bool _hasSubmitted;
    private bool _isRunning;
    private string _actualOutput = string.Empty;

    public CodeOutputViewModel(CodeOutputChallenge challenge, ICodeExecutor codeExecutor)
    {
        _challenge = challenge;
        _codeExecutor = codeExecutor;

        RunCodeCommand = ReactiveCommand.CreateFromTask(RunCodeAsync,
            this.WhenAnyValue(x => x.IsRunning, running => !running && _challenge.AllowRunning));

        SubmitCommand = ReactiveCommand.Create(Submit,
            this.WhenAnyValue(x => x.UserAnswer, answer => !string.IsNullOrWhiteSpace(answer)));
    }

    public string Code => _challenge.Code;
    public string Language => _challenge.Language;

    public string UserAnswer
    {
        get => _userAnswer;
        set => this.RaiseAndSetIfChanged(ref _userAnswer, value);
    }

    public string ActualOutput
    {
        get => _actualOutput;
        set => this.RaiseAndSetIfChanged(ref _actualOutput, value);
    }

    public bool IsRunning
    {
        get => _isRunning;
        set => this.RaiseAndSetIfChanged(ref _isRunning, value);
    }

    public ReactiveCommand<Unit, Unit> RunCodeCommand { get; }
    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }

    private async Task RunCodeAsync()
    {
        IsRunning = true;
        try
        {
            var result = await _codeExecutor.ExecuteAsync(_challenge.Language, _challenge.Code);
            ActualOutput = result.Success ? result.Output : $"Error: {result.Error}";
        }
        finally
        {
            IsRunning = false;
        }
    }

    private void Submit()
    {
        _hasSubmitted = true;

        var normalizedUser = NormalizeOutput(UserAnswer);
        var normalizedExpected = NormalizeOutput(_challenge.ExpectedOutput);

        var isCorrect = normalizedUser == normalizedExpected;

        OnChallengeCompleted(new ChallengeResult
        {
            Passed = isCorrect,
            Score = isCorrect ? _challenge.Points : 0
        });
    }

    private string NormalizeOutput(string output)
    {
        return output.Trim()
            .Replace("\r\n", "\n")
            .Replace("\r", "\n");
    }
}
```

### XAML View
```xml
<Grid RowDefinitions="Auto,*,Auto,*">
    <!-- Code Display (Read-only) -->
    <Border Grid.Row="0" Background="#1E1E1E" Padding="16">
        <StackPanel Spacing="8">
            <TextBlock Text="Code:"
                      FontWeight="Bold"
                      Foreground="#9CDCFE" />

            <avaloniaedit:TextEditor Text="{Binding Code}"
                                     IsReadOnly="True"
                                     FontFamily="Consolas"
                                     Background="#1E1E1E"
                                     Foreground="#D4D4D4" />

            <Button Content="▶ Run to See Output"
                    Command="{Binding RunCodeCommand}"
                    IsVisible="{Binding AllowRunning}" />
        </StackPanel>
    </Border>

    <!-- Actual Output (if run) -->
    <Border Grid.Row="1"
            IsVisible="{Binding ActualOutput, Converter={StaticResource StringNotEmptyConverter}}"
            Background="#252526"
            Padding="16">
        <StackPanel Spacing="8">
            <TextBlock Text="Actual Output:"
                      FontWeight="Bold" />
            <TextBlock Text="{Binding ActualOutput}"
                      FontFamily="Consolas"
                      TextWrapping="Wrap" />
        </StackPanel>
    </Border>

    <!-- User Input -->
    <StackPanel Grid.Row="2" Spacing="8" Margin="16">
        <TextBlock Text="What will this code output?"
                  FontWeight="Bold" />

        <TextBox Text="{Binding UserAnswer}"
                 Watermark="Enter the expected output..."
                 AcceptsReturn="True"
                 Height="120"
                 FontFamily="Consolas" />

        <Button Content="Submit Answer"
                Command="{Binding SubmitCommand}" />
    </StackPanel>

    <!-- Result -->
    <Border Grid.Row="3" IsVisible="{Binding HasSubmitted}">
        <!-- Result display -->
    </Border>
</Grid>
```

---

## Challenge Type 4: Free Coding

### Data Model
```csharp
public class FreeCodingChallenge : Challenge
{
    public override string Type => "FREE_CODING";

    [JsonPropertyName("instructions")]
    public string Instructions { get; set; } = string.Empty;

    [JsonPropertyName("starterCode")]
    public string StarterCode { get; set; } = string.Empty;

    [JsonPropertyName("solution")]
    public string Solution { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = "python";

    [JsonPropertyName("testCases")]
    public List<TestCase> TestCases { get; set; } = new();

    [JsonPropertyName("bonusChallenges")]
    public List<string>? BonusChallenges { get; set; }
}

public class TestCase
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("input")]
    public string? Input { get; set; }

    [JsonPropertyName("expectedOutput")]
    public string ExpectedOutput { get; set; } = string.Empty;

    [JsonPropertyName("isVisible")]
    public bool IsVisible { get; set; }

    [JsonPropertyName("points")]
    public int Points { get; set; } = 10;
}
```

### ViewModel
```csharp
public class FreeCodingViewModel : ChallengeViewModelBase
{
    private readonly FreeCodingChallenge _challenge;
    private readonly IChallengeValidator _validator;
    private string _code;
    private bool _isRunningTests;
    private bool _showSolution;
    private ObservableCollection<TestResult> _testResults = new();

    public FreeCodingViewModel(FreeCodingChallenge challenge, IChallengeValidator validator)
    {
        _challenge = challenge;
        _validator = validator;
        _code = challenge.StarterCode;

        RunTestsCommand = ReactiveCommand.CreateFromTask(RunTestsAsync,
            this.WhenAnyValue(x => x.IsRunningTests, running => !running));

        ToggleSolutionCommand = ReactiveCommand.Create(() => ShowSolution = !ShowSolution);

        ResetCommand = ReactiveCommand.Create(() => Code = _challenge.StarterCode);
    }

    public string Instructions => _challenge.Instructions;
    public string Language => _challenge.Language;

    public string Code
    {
        get => _code;
        set => this.RaiseAndSetIfChanged(ref _code, value);
    }

    public bool IsRunningTests
    {
        get => _isRunningTests;
        set => this.RaiseAndSetIfChanged(ref _isRunningTests, value);
    }

    public bool ShowSolution
    {
        get => _showSolution;
        set => this.RaiseAndSetIfChanged(ref _showSolution, value);
    }

    public string Solution => _challenge.Solution;

    public ObservableCollection<TestResult> TestResults
    {
        get => _testResults;
        set => this.RaiseAndSetIfChanged(ref _testResults, value);
    }

    public ReactiveCommand<Unit, Unit> RunTestsCommand { get; }
    public ReactiveCommand<Unit, Unit> ToggleSolutionCommand { get; }
    public ReactiveCommand<Unit, Unit> ResetCommand { get; }

    private async Task RunTestsAsync()
    {
        IsRunningTests = true;
        TestResults.Clear();

        try
        {
            var result = await _validator.ValidateAsync(_challenge, Code);

            TestResults = new ObservableCollection<TestResult>(result.TestResults);

            var allPassed = result.TestResults.All(t => t.Passed);

            if (allPassed)
            {
                OnChallengeCompleted(new ChallengeResult
                {
                    Passed = true,
                    Score = result.Score,
                    TestResults = result.TestResults
                });
            }
        }
        finally
        {
            IsRunningTests = false;
        }
    }
}
```

### XAML View
```xml
<Grid ColumnDefinitions="*,400">
    <!-- Left: Code Editor -->
    <Grid Grid.Column="0" RowDefinitions="Auto,*,Auto">
        <!-- Instructions -->
        <Border Grid.Row="0" Background="#2D2D30" Padding="16">
            <TextBlock Text="{Binding Instructions}"
                      TextWrapping="Wrap" />
        </Border>

        <!-- Code Editor -->
        <avaloniaedit:TextEditor Grid.Row="1"
                                 Name="CodeEditor"
                                 Text="{Binding Code}"
                                 SyntaxHighlighting="{Binding Language, Converter={StaticResource LanguageToSyntaxConverter}}"
                                 FontFamily="Consolas"
                                 FontSize="14"
                                 ShowLineNumbers="True"
                                 Background="#1E1E1E"
                                 Foreground="#D4D4D4"
                                 Margin="16" />

        <!-- Toolbar -->
        <StackPanel Grid.Row="2"
                   Orientation="Horizontal"
                   Spacing="8"
                   Padding="16">
            <Button Content="▶ Run Tests"
                    Command="{Binding RunTestsCommand}"
                    Background="#0E639C"
                    Foreground="White" />

            <Button Content="👁 Show Solution"
                    Command="{Binding ToggleSolutionCommand}" />

            <Button Content="↺ Reset"
                    Command="{Binding ResetCommand}" />
        </StackPanel>
    </Grid>

    <!-- Right: Test Results -->
    <Grid Grid.Column="1" RowDefinitions="Auto,*,Auto" Background="#252526">
        <TextBlock Grid.Row="0"
                  Text="Test Results"
                  FontSize="16"
                  FontWeight="Bold"
                  Padding="16" />

        <!-- Test Results List -->
        <ScrollViewer Grid.Row="1">
            <ItemsControl ItemsSource="{Binding TestResults}">
                <ItemsControl.ItemTemplate>
                    <DataTemplate>
                        <Border Background="{Binding Passed, Converter={StaticResource PassedToBrushConverter}}"
                                Margin="8"
                                Padding="12"
                                CornerRadius="4">
                            <StackPanel Spacing="4">
                                <StackPanel Orientation="Horizontal" Spacing="8">
                                    <Path Data="{Binding Passed, Converter={StaticResource PassedToIconConverter}}"
                                          Fill="White"
                                          Width="16" Height="16" />

                                    <TextBlock Text="{Binding Description}"
                                              FontWeight="Bold" />
                                </StackPanel>

                                <TextBlock Text="{Binding ActualOutput}"
                                          FontFamily="Consolas"
                                          FontSize="12"
                                          IsVisible="{Binding !Passed}" />

                                <TextBlock Text="{Binding Error}"
                                          Foreground="#F48771"
                                          IsVisible="{Binding Error, Converter={StaticResource StringNotEmptyConverter}}" />
                            </StackPanel>
                        </Border>
                    </DataTemplate>
                </ItemsControl.ItemTemplate>
            </ItemsControl>
        </ScrollViewer>

        <!-- Solution Panel -->
        <Border Grid.Row="2"
               IsVisible="{Binding ShowSolution}"
               Background="#1E1E1E"
               BorderBrush="#3A3A3A"
               BorderThickness="0,1,0,0"
               Padding="16">
            <StackPanel Spacing="8">
                <TextBlock Text="Solution:"
                          FontWeight="Bold"
                          Foreground="#9CDCFE" />

                <avaloniaedit:TextEditor Text="{Binding Solution}"
                                        IsReadOnly="True"
                                        FontFamily="Consolas"
                                        FontSize="12"
                                        Background="#1E1E1E"
                                        Foreground="#D4D4D4"
                                        MaxHeight="200" />
            </StackPanel>
        </Border>
    </Grid>
</Grid>
```

---

## Challenge Type 5: Code Completion

### Data Model
```csharp
public class CodeCompletionChallenge : Challenge
{
    public override string Type => "CODE_COMPLETION";

    [JsonPropertyName("instructions")]
    public string Instructions { get; set; } = string.Empty;

    [JsonPropertyName("starterCode")]
    public string StarterCode { get; set; } = string.Empty;

    [JsonPropertyName("solution")]
    public string Solution { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public string Language { get; set; } = "python";

    [JsonPropertyName("testCases")]
    public List<TestCase> TestCases { get; set; } = new();
}
```

### ViewModel
```csharp
// Very similar to FreeCodingViewModel, but highlights TODO markers

public class CodeCompletionViewModel : ChallengeViewModelBase
{
    // Same as FreeCodingViewModel, with additional:

    public void HighlightTodoMarkers()
    {
        // Find all "TODO" or "// ..." in code
        // Highlight them in the editor
        // Auto-focus first TODO when challenge loads
    }
}
```

---

## Challenge Type 6: Conceptual

### Data Model
```csharp
public class ConceptualChallenge : Challenge
{
    public override string Type => "CONCEPTUAL";

    [JsonPropertyName("question")]
    public string Question { get; set; } = string.Empty;

    [JsonPropertyName("sampleAnswer")]
    public string SampleAnswer { get; set; } = string.Empty;

    [JsonPropertyName("minWords")]
    public int MinWords { get; set; } = 50;
}
```

### ViewModel
```csharp
public class ConceptualViewModel : ChallengeViewModelBase
{
    private readonly ConceptualChallenge _challenge;
    private string _userAnswer = string.Empty;
    private bool _hasSubmitted;

    public string Question => _challenge.Question;
    public string SampleAnswer => _challenge.SampleAnswer;
    public int MinWords => _challenge.MinWords;

    public string UserAnswer
    {
        get => _userAnswer;
        set
        {
            this.RaiseAndSetIfChanged(ref _userAnswer, value);
            this.RaisePropertyChanged(nameof(WordCount));
        }
    }

    public int WordCount => UserAnswer.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;

    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }

    private void Submit()
    {
        _hasSubmitted = true;

        // No automatic validation - mark as "submitted"
        // Actual grading would be manual or AI-based

        OnChallengeCompleted(new ChallengeResult
        {
            Passed = WordCount >= MinWords,
            Score = 100, // Or based on manual review
            RequiresManualReview = true
        });
    }
}
```

### XAML View
```xml
<StackPanel Spacing="16">
    <TextBlock Text="{Binding Question}"
               FontSize="18"
               TextWrapping="Wrap" />

    <TextBox Text="{Binding UserAnswer}"
             AcceptsReturn="True"
             TextWrapping="Wrap"
             MinHeight="200"
             Watermark="Enter your answer here..."
             IsEnabled="{Binding !HasSubmitted}" />

    <StackPanel Orientation="Horizontal" Spacing="16">
        <TextBlock Text="{Binding WordCount, StringFormat='Words: {0}'}" />
        <TextBlock Text="{Binding MinWords, StringFormat='(Minimum: {0})'}"
                  Opacity="0.7" />
    </StackPanel>

    <Button Content="Submit Answer"
            Command="{Binding SubmitCommand}"
            IsEnabled="{Binding WordCount, Converter={StaticResource IntGreaterThanConverter}, ConverterParameter={Binding MinWords}}" />

    <!-- Sample Answer (revealed after submission) -->
    <Border IsVisible="{Binding HasSubmitted}"
            Background="#252526"
            Padding="16"
            CornerRadius="8">
        <StackPanel Spacing="8">
            <TextBlock Text="Sample Answer:"
                      FontWeight="Bold"
                      Foreground="#9CDCFE" />

            <TextBlock Text="{Binding SampleAnswer}"
                      TextWrapping="Wrap" />
        </StackPanel>
    </Border>
</StackPanel>
```

---

## Validation Service

### Interface
```csharp
public interface IChallengeValidator
{
    Task<ValidationResult> ValidateAsync(Challenge challenge, string userAnswer);
    Task<TestResult> RunTestCaseAsync(TestCase testCase, string code, string language);
}
```

### Implementation
```csharp
public class ChallengeValidator : IChallengeValidator
{
    private readonly ICodeExecutor _codeExecutor;

    public ChallengeValidator(ICodeExecutor codeExecutor)
    {
        _codeExecutor = codeExecutor;
    }

    public async Task<ValidationResult> ValidateAsync(Challenge challenge, string userAnswer)
    {
        return challenge switch
        {
            FreeCodingChallenge coding => await ValidateFreeCodingAsync(coding, userAnswer),
            CodeCompletionChallenge completion => await ValidateCodeCompletionAsync(completion, userAnswer),
            MultipleChoiceChallenge mc => ValidateMultipleChoice(mc, userAnswer),
            TrueFalseChallenge tf => ValidateTrueFalse(tf, userAnswer),
            CodeOutputChallenge co => ValidateCodeOutput(co, userAnswer),
            ConceptualChallenge concept => ValidateConceptual(concept, userAnswer),
            _ => throw new NotSupportedException($"Challenge type {challenge.Type} not supported")
        };
    }

    private async Task<ValidationResult> ValidateFreeCodingAsync(FreeCodingChallenge challenge, string code)
    {
        var testResults = new List<TestResult>();
        var totalPoints = 0;
        var earnedPoints = 0;

        foreach (var testCase in challenge.TestCases)
        {
            var result = await RunTestCaseAsync(testCase, code, challenge.Language);
            testResults.Add(result);

            totalPoints += testCase.Points;
            if (result.Passed)
            {
                earnedPoints += testCase.Points;
            }
        }

        var score = totalPoints > 0 ? (earnedPoints * 100) / totalPoints : 0;

        return new ValidationResult
        {
            Passed = testResults.All(t => t.Passed),
            Score = score,
            TestResults = testResults
        };
    }

    public async Task<TestResult> RunTestCaseAsync(TestCase testCase, string code, string language)
    {
        try
        {
            // Inject input if needed
            var codeWithInput = InjectInput(code, testCase.Input, language);

            var execution = await _codeExecutor.ExecuteAsync(language, codeWithInput);

            if (!execution.Success)
            {
                return new TestResult
                {
                    Description = testCase.Description,
                    Passed = false,
                    Error = execution.Error
                };
            }

            var actualOutput = NormalizeOutput(execution.Output);
            var expectedOutput = NormalizeOutput(testCase.ExpectedOutput);

            var passed = CompareOutputs(actualOutput, expectedOutput);

            return new TestResult
            {
                Description = testCase.Description,
                Passed = passed,
                ActualOutput = actualOutput,
                ExpectedOutput = expectedOutput
            };
        }
        catch (Exception ex)
        {
            return new TestResult
            {
                Description = testCase.Description,
                Passed = false,
                Error = $"Test execution failed: {ex.Message}"
            };
        }
    }

    private bool CompareOutputs(string actual, string expected)
    {
        // Exact match (default)
        if (actual == expected) return true;

        // Try with trimmed whitespace
        if (actual.Trim() == expected.Trim()) return true;

        // Try case-insensitive
        if (actual.Equals(expected, StringComparison.OrdinalIgnoreCase)) return true;

        return false;
    }

    private string NormalizeOutput(string output)
    {
        return output.Trim()
            .Replace("\r\n", "\n")
            .Replace("\r", "\n");
    }

    private string InjectInput(string code, string? input, string language)
    {
        if (string.IsNullOrEmpty(input)) return code;

        // For Python: replace input() calls
        if (language == "python")
        {
            // Simple approach: prepend input simulation
            return $"# Simulated input\n_input_data = {JsonSerializer.Serialize(input.Split('\n'))}\n_input_index = 0\ndef input(prompt=''):\n    global _input_index\n    result = _input_data[_input_index]\n    _input_index += 1\n    return result\n\n{code}";
        }

        return code;
    }
}
```

---

## Test Result Models

```csharp
public class ValidationResult
{
    public bool Passed { get; set; }
    public int Score { get; set; }
    public List<TestResult> TestResults { get; set; } = new();
    public string? CompilationError { get; set; }
    public string? RuntimeError { get; set; }
    public bool RequiresManualReview { get; set; }
}

public class TestResult
{
    public string Description { get; set; } = string.Empty;
    public bool Passed { get; set; }
    public string? ActualOutput { get; set; }
    public string? ExpectedOutput { get; set; }
    public string? Error { get; set; }
    public int ExecutionTimeMs { get; set; }
}

public class ChallengeResult
{
    public bool Passed { get; set; }
    public int Score { get; set; }
    public int HintsUsed { get; set; }
    public List<TestResult>? TestResults { get; set; }
    public bool RequiresManualReview { get; set; }
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
}
```

---

## Challenge Factory

```csharp
public interface IChallengeFactory
{
    ChallengeViewModelBase Create(Challenge challenge);
}

public class ChallengeFactory : IChallengeFactory
{
    private readonly ICodeExecutor _codeExecutor;
    private readonly IChallengeValidator _validator;

    public ChallengeFactory(ICodeExecutor codeExecutor, IChallengeValidator validator)
    {
        _codeExecutor = codeExecutor;
        _validator = validator;
    }

    public ChallengeViewModelBase Create(Challenge challenge)
    {
        return challenge switch
        {
            MultipleChoiceChallenge mc => new MultipleChoiceViewModel(mc),
            TrueFalseChallenge tf => new TrueFalseViewModel(tf),
            CodeOutputChallenge co => new CodeOutputViewModel(co, _codeExecutor),
            FreeCodingChallenge fc => new FreeCodingViewModel(fc, _validator),
            CodeCompletionChallenge cc => new CodeCompletionViewModel(cc, _validator),
            ConceptualChallenge concept => new ConceptualViewModel(concept),
            _ => throw new NotSupportedException($"Challenge type {challenge.Type} not supported")
        };
    }
}
```

---

## Integration with Lesson Page

```csharp
public class LessonPageViewModel : ViewModelBase
{
    private readonly IChallengeFactory _challengeFactory;
    private ChallengeViewModelBase? _currentChallenge;

    public LessonPageViewModel(IChallengeFactory challengeFactory)
    {
        _challengeFactory = challengeFactory;
    }

    public void LoadChallenge(Challenge challenge)
    {
        CurrentChallenge = _challengeFactory.Create(challenge);
        CurrentChallenge.ChallengeCompleted += OnChallengeCompleted;
    }

    public ChallengeViewModelBase? CurrentChallenge
    {
        get => _currentChallenge;
        set => this.RaiseAndSetIfChanged(ref _currentChallenge, value);
    }

    private void OnChallengeCompleted(object? sender, ChallengeResult result)
    {
        // Save progress
        // Update UI
        // Unlock achievements
        // Move to next challenge
    }
}
```

---

**Document Version:** 1.0
**Last Updated:** 2025-11-18
