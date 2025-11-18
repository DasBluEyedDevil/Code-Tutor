using System;
using System.Reactive;
using ReactiveUI;
using CodeTutor.Native.Models.Challenges;
using CodeTutor.Native.Services;

namespace CodeTutor.Native.ViewModels.Challenges;

/// <summary>
/// View model for multiple choice challenges
/// </summary>
public class MultipleChoiceViewModel : ChallengeViewModelBase
{
    private readonly MultipleChoiceChallenge _challenge;
    private readonly IChallengeValidationService _validationService;
    private readonly IErrorHandlerService _errorHandler;
    private int? _selectedOption;

    public MultipleChoiceViewModel(
        MultipleChoiceChallenge challenge,
        IChallengeValidationService validationService,
        IErrorHandlerService errorHandler)
        : base(challenge)
    {
        _challenge = challenge;
        _validationService = validationService;
        _errorHandler = errorHandler;

        SubmitCommand = ReactiveCommand.Create(Submit,
            this.WhenAnyValue(x => x.SelectedOption, x => x.HasSubmitted,
                (opt, submitted) => opt.HasValue && !submitted));
    }

    public string Question => _challenge.Question;
    public List<string> Options => _challenge.Options;

    public int? SelectedOption
    {
        get => _selectedOption;
        set => this.RaiseAndSetIfChanged(ref _selectedOption, value);
    }

    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }

    private void Submit()
    {
        if (!SelectedOption.HasValue) return;

        Result = _validationService.ValidateMultipleChoice(_challenge, SelectedOption.Value);
        HasSubmitted = true;
    }

    protected override void Reset()
    {
        base.Reset();
        SelectedOption = null;
    }
}
