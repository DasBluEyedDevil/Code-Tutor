using System;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using CodeTutor.Native.Models;
using CodeTutor.Native.Models.Challenges;
using CodeTutor.Native.Services;

namespace CodeTutor.Native.ViewModels.Challenges;

/// <summary>
/// View model for code completion challenges
/// </summary>
public class CodeCompletionViewModel : ChallengeViewModelBase
{
    private readonly CodeCompletionChallenge _challenge;
    private readonly IChallengeValidationService _validationService;
    private readonly IErrorHandlerService _errorHandler;
    private string _code;
    private bool _isValidating;
    private bool _showSolution;

    public CodeCompletionViewModel(
        CodeCompletionChallenge challenge,
        IChallengeValidationService validationService,
        IErrorHandlerService errorHandler)
        : base(challenge)
    {
        _challenge = challenge;
        _validationService = validationService;
        _errorHandler = errorHandler;
        _code = challenge.IncompleteCode;

        SubmitCommand = ReactiveCommand.CreateFromTask(SubmitAsync,
            this.WhenAnyValue(x => x.IsValidating, x => x.Code,
                (validating, code) => !validating && !string.IsNullOrWhiteSpace(code)));

        ShowSolutionCommand = ReactiveCommand.Create(() => { ShowSolution = true; });
    }

    public string Language => _challenge.Language;
    public bool HasSolution => !string.IsNullOrEmpty(_challenge.Solution);
    public bool HasTodoMarkers => _challenge.TodoMarkers?.Count > 0;

    public string Code
    {
        get => _code;
        set => this.RaiseAndSetIfChanged(ref _code, value);
    }

    public bool IsValidating
    {
        get => _isValidating;
        set => this.RaiseAndSetIfChanged(ref _isValidating, value);
    }

    public bool ShowSolution
    {
        get => _showSolution;
        set => this.RaiseAndSetIfChanged(ref _showSolution, value);
    }

    public string? Solution => ShowSolution ? _challenge.Solution : null;

    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowSolutionCommand { get; }

    private async Task SubmitAsync()
    {
        IsValidating = true;

        try
        {
            Result = await _validationService.ValidateCodeCompletionAsync(_challenge, Code);
            HasSubmitted = true;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            await _errorHandler.HandleErrorAsync(ex, "Challenge validation", showToUser: false);
            Result = new ChallengeResult
            {
                IsCorrect = false,
                Score = 0,
                MaxScore = _challenge.Points,
                Feedback = $"Validation error: {_errorHandler.GetUserFriendlyMessage(ex)}"
            };
        }
        finally
        {
            IsValidating = false;
        }
    }

    protected override void Reset()
    {
        base.Reset();
        Code = _challenge.IncompleteCode;
        ShowSolution = false;
    }
}
