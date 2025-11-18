using System.Reactive;
using ReactiveUI;
using CodeTutor.Native.Models.Challenges;
using CodeTutor.Native.Services;

namespace CodeTutor.Native.ViewModels.Challenges;

/// <summary>
/// View model for conceptual text-based challenges
/// </summary>
public class ConceptualViewModel : ChallengeViewModelBase
{
    private readonly ConceptualChallenge _challenge;
    private readonly IChallengeValidationService _validationService;
    private string _answer = string.Empty;
    private bool _showSampleAnswer;

    public ConceptualViewModel(
        ConceptualChallenge challenge,
        IChallengeValidationService validationService)
        : base(challenge)
    {
        _challenge = challenge;
        _validationService = validationService;

        SubmitCommand = ReactiveCommand.Create(Submit,
            this.WhenAnyValue(x => x.Answer, x => x.HasSubmitted,
                (ans, submitted) => !string.IsNullOrWhiteSpace(ans) && !submitted));

        ShowSampleAnswerCommand = ReactiveCommand.Create(() => ShowSampleAnswer = true);
    }

    public string Question => _challenge.Question;
    public bool HasSampleAnswer => !string.IsNullOrEmpty(_challenge.SampleAnswer);
    public bool HasKeyPoints => _challenge.KeyPoints?.Count > 0;
    public int? MinWords => _challenge.MinWords;

    public string Answer
    {
        get => _answer;
        set
        {
            this.RaiseAndSetIfChanged(ref _answer, value);
            this.RaisePropertyChanged(nameof(WordCount));
        }
    }

    public int WordCount => Answer.Split(new[] { ' ', '\n', '\r', '\t' },
        StringSplitOptions.RemoveEmptyEntries).Length;

    public bool ShowSampleAnswer
    {
        get => _showSampleAnswer;
        set => this.RaiseAndSetIfChanged(ref _showSampleAnswer, value);
    }

    public string? SampleAnswer => ShowSampleAnswer ? _challenge.SampleAnswer : null;

    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowSampleAnswerCommand { get; }

    private void Submit()
    {
        Result = _validationService.ValidateConceptual(_challenge, Answer);
        HasSubmitted = true;
    }

    protected override void Reset()
    {
        base.Reset();
        Answer = string.Empty;
        ShowSampleAnswer = false;
    }
}
