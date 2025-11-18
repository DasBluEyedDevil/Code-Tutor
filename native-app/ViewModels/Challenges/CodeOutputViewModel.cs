using System;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using CodeTutor.Native.Models.Challenges;
using CodeTutor.Native.Services;

namespace CodeTutor.Native.ViewModels.Challenges;

/// <summary>
/// View model for code output prediction challenges
/// </summary>
public class CodeOutputViewModel : ChallengeViewModelBase
{
    private readonly CodeOutputChallenge _challenge;
    private readonly IChallengeValidationService _validationService;
    private readonly ICodeExecutor _codeExecutor;
    private readonly IErrorHandlerService _errorHandler;
    private string _userOutput = string.Empty;
    private string _actualOutput = string.Empty;
    private bool _isRunning;

    public CodeOutputViewModel(
        CodeOutputChallenge challenge,
        IChallengeValidationService validationService,
        ICodeExecutor codeExecutor,
        IErrorHandlerService errorHandler)
        : base(challenge)
    {
        _challenge = challenge;
        _validationService = validationService;
        _codeExecutor = codeExecutor;
        _errorHandler = errorHandler;

        RunCodeCommand = ReactiveCommand.CreateFromTask(RunCodeAsync,
            this.WhenAnyValue(x => x.IsRunning, x => x.AllowRun,
                (running, allow) => !running && allow));

        SubmitCommand = ReactiveCommand.Create(Submit,
            this.WhenAnyValue(x => x.UserOutput, x => x.HasSubmitted,
                (output, submitted) => !string.IsNullOrWhiteSpace(output) && !submitted));
    }

    public string Code => _challenge.Code;
    public string Language => _challenge.Language;
    public bool AllowRun => _challenge.AllowRun;

    public string UserOutput
    {
        get => _userOutput;
        set => this.RaiseAndSetIfChanged(ref _userOutput, value);
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
        ActualOutput = string.Empty;

        try
        {
            var result = await _codeExecutor.ExecuteAsync(_challenge.Language, _challenge.Code);

            if (result.Success)
            {
                ActualOutput = result.Output;
            }
            else
            {
                ActualOutput = $"Error: {result.Error}";
            }
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            await _errorHandler.HandleErrorAsync(ex, "Code execution", showToUser: false);
            ActualOutput = $"Execution failed: {_errorHandler.GetUserFriendlyMessage(ex)}";
        }
        finally
        {
            IsRunning = false;
        }
    }

    private void Submit()
    {
        Result = _validationService.ValidateCodeOutput(_challenge, UserOutput);
        HasSubmitted = true;
    }

    protected override void Reset()
    {
        base.Reset();
        UserOutput = string.Empty;
        ActualOutput = string.Empty;
    }
}
