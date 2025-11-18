using System.Reactive;
using ReactiveUI;
using CodeTutor.Native.Models;
using CodeTutor.Native.Models.Challenges;

namespace CodeTutor.Native.ViewModels.Challenges;

/// <summary>
/// Base view model for all challenge types
/// </summary>
public abstract class ChallengeViewModelBase : ViewModelBase
{
    private bool _hasSubmitted;
    private ChallengeResult? _result;
    private int _currentHintIndex = -1;

    protected ChallengeViewModelBase(Challenge challenge)
    {
        Challenge = challenge;

        ShowHintCommand = ReactiveCommand.Create(ShowNextHint);
        ResetCommand = ReactiveCommand.Create(Reset);
    }

    public Challenge Challenge { get; }

    public bool HasSubmitted
    {
        get => _hasSubmitted;
        protected set => this.RaiseAndSetIfChanged(ref _hasSubmitted, value);
    }

    public ChallengeResult? Result
    {
        get => _result;
        protected set => this.RaiseAndSetIfChanged(ref _result, value);
    }

    public int CurrentHintIndex
    {
        get => _currentHintIndex;
        private set => this.RaiseAndSetIfChanged(ref _currentHintIndex, value);
    }

    public string? CurrentHint => CurrentHintIndex >= 0 && CurrentHintIndex < Challenge.Hints.Count
        ? Challenge.Hints[CurrentHintIndex]
        : null;

    public bool HasMoreHints => CurrentHintIndex < Challenge.Hints.Count - 1;

    public ReactiveCommand<Unit, Unit> ShowHintCommand { get; }
    public ReactiveCommand<Unit, Unit> ResetCommand { get; }

    private void ShowNextHint()
    {
        if (HasMoreHints)
        {
            CurrentHintIndex++;
            this.RaisePropertyChanged(nameof(CurrentHint));
            this.RaisePropertyChanged(nameof(HasMoreHints));
        }
    }

    protected virtual void Reset()
    {
        HasSubmitted = false;
        Result = null;
        CurrentHintIndex = -1;
        this.RaisePropertyChanged(nameof(CurrentHint));
        this.RaisePropertyChanged(nameof(HasMoreHints));
    }
}
