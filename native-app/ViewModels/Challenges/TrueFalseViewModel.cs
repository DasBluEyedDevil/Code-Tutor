using System;
using System.Reactive;
using ReactiveUI;
using CodeTutor.Native.Models.Challenges;
using CodeTutor.Native.Services;

namespace CodeTutor.Native.ViewModels.Challenges;

/// <summary>
/// View model for true/false challenges
/// </summary>
public class TrueFalseViewModel : ChallengeViewModelBase
{
    private readonly TrueFalseChallenge _challenge;
    private readonly IChallengeValidationService _validationService;
    private readonly IErrorHandlerService _errorHandler;
    private bool? _selectedAnswer;

    public TrueFalseViewModel(
        TrueFalseChallenge challenge,
        IChallengeValidationService validationService,
        IErrorHandlerService errorHandler)
        : base(challenge)
    {
        _challenge = challenge;
        _validationService = validationService;
        _errorHandler = errorHandler;

        SelectTrueCommand = ReactiveCommand.Create(() => SelectedAnswer = true,
            this.WhenAnyValue(x => x.HasSubmitted, submitted => !submitted));

        SelectFalseCommand = ReactiveCommand.Create(() => SelectedAnswer = false,
            this.WhenAnyValue(x => x.HasSubmitted, submitted => !submitted));

        SubmitCommand = ReactiveCommand.Create(Submit,
            this.WhenAnyValue(x => x.SelectedAnswer, x => x.HasSubmitted,
                (ans, submitted) => ans.HasValue && !submitted));
    }

    public string Statement => _challenge.Statement;

    public bool? SelectedAnswer
    {
        get => _selectedAnswer;
        set => this.RaiseAndSetIfChanged(ref _selectedAnswer, value);
    }

    public ReactiveCommand<Unit, Unit> SelectTrueCommand { get; }
    public ReactiveCommand<Unit, Unit> SelectFalseCommand { get; }
    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }

    private void Submit()
    {
        if (!SelectedAnswer.HasValue) return;

        Result = _validationService.ValidateTrueFalse(_challenge, SelectedAnswer.Value);
        HasSubmitted = true;
    }

    protected override void Reset()
    {
        base.Reset();
        SelectedAnswer = null;
    }
}
