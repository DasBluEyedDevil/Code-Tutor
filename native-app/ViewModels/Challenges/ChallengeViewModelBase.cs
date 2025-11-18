using System;
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
    private int _hintsUsed = 0;

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

    public int HintsUsed
    {
        get => _hintsUsed;
        private set => this.RaiseAndSetIfChanged(ref _hintsUsed, value);
    }

    public string? CurrentHint => CurrentHintIndex >= 0 && CurrentHintIndex < Challenge.Hints.Count
        ? Challenge.Hints[CurrentHintIndex]
        : null;

    public bool HasMoreHints => CurrentHintIndex < Challenge.Hints.Count - 1;
    public bool HasHints => Challenge.Hints.Count > 0;

    public ReactiveCommand<Unit, Unit> ShowHintCommand { get; }
    public ReactiveCommand<Unit, Unit> ResetCommand { get; }

    /// <summary>
    /// Event raised when a hint is shown. LessonPageViewModel should handle this to update progress.
    /// </summary>
    public event EventHandler? HintShown;

    private void ShowNextHint()
    {
        if (HasMoreHints)
        {
            CurrentHintIndex++;
            HintsUsed++;
            this.RaisePropertyChanged(nameof(CurrentHint));
            this.RaisePropertyChanged(nameof(HasMoreHints));
            this.RaisePropertyChanged(nameof(HintsUsed));

            // Raise event for parent to handle
            HintShown?.Invoke(this, EventArgs.Empty);
        }
    }

    protected virtual void Reset()
    {
        HasSubmitted = false;
        Result = null;
        CurrentHintIndex = -1;
        HintsUsed = 0;
        this.RaisePropertyChanged(nameof(CurrentHint));
        this.RaisePropertyChanged(nameof(HasMoreHints));
        this.RaisePropertyChanged(nameof(HintsUsed));
    }
}
