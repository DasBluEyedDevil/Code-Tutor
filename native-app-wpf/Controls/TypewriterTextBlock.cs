using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace CodeTutor.Wpf.Controls;

public class TypewriterTextBlock : TextBlock
{
    public static readonly DependencyProperty TypewriterTextProperty =
        DependencyProperty.Register(nameof(TypewriterText), typeof(string), typeof(TypewriterTextBlock),
            new PropertyMetadata(string.Empty, OnTypewriterTextChanged));

    public static readonly DependencyProperty CharacterDelayProperty =
        DependencyProperty.Register(nameof(CharacterDelay), typeof(int), typeof(TypewriterTextBlock),
            new PropertyMetadata(18));

    public static readonly DependencyProperty ShowCursorProperty =
        DependencyProperty.Register(nameof(ShowCursor), typeof(bool), typeof(TypewriterTextBlock),
            new PropertyMetadata(true));

    public static readonly DependencyProperty CursorBlinkIntervalProperty =
        DependencyProperty.Register(nameof(CursorBlinkInterval), typeof(int), typeof(TypewriterTextBlock),
            new PropertyMetadata(300));

    public static readonly DependencyProperty CursorBlinkCountProperty =
        DependencyProperty.Register(nameof(CursorBlinkCount), typeof(int), typeof(TypewriterTextBlock),
            new PropertyMetadata(3));

    public static readonly DependencyProperty CursorCharacterProperty =
        DependencyProperty.Register(nameof(CursorCharacter), typeof(string), typeof(TypewriterTextBlock),
            new PropertyMetadata("|"));

    public static readonly DependencyProperty EnableIncrementalProperty =
        DependencyProperty.Register(nameof(EnableIncremental), typeof(bool), typeof(TypewriterTextBlock),
            new PropertyMetadata(true));

    private CancellationTokenSource? _typingCts;
    private DispatcherTimer? _cursorTimer;
    private string _currentText = string.Empty;
    private bool _cursorVisible;

    public string TypewriterText
    {
        get => (string)GetValue(TypewriterTextProperty);
        set => SetValue(TypewriterTextProperty, value);
    }

    public int CharacterDelay
    {
        get => (int)GetValue(CharacterDelayProperty);
        set => SetValue(CharacterDelayProperty, value);
    }

    public bool ShowCursor
    {
        get => (bool)GetValue(ShowCursorProperty);
        set => SetValue(ShowCursorProperty, value);
    }

    public int CursorBlinkInterval
    {
        get => (int)GetValue(CursorBlinkIntervalProperty);
        set => SetValue(CursorBlinkIntervalProperty, value);
    }

    public int CursorBlinkCount
    {
        get => (int)GetValue(CursorBlinkCountProperty);
        set => SetValue(CursorBlinkCountProperty, value);
    }

    public string CursorCharacter
    {
        get => (string)GetValue(CursorCharacterProperty);
        set => SetValue(CursorCharacterProperty, value);
    }

    public bool EnableIncremental
    {
        get => (bool)GetValue(EnableIncrementalProperty);
        set => SetValue(EnableIncrementalProperty, value);
    }

    private static void OnTypewriterTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is TypewriterTextBlock textBlock)
        {
            textBlock.StartTyping(e.NewValue as string ?? string.Empty);
        }
    }

    private void StartTyping(string newText)
    {
        _typingCts?.Cancel();
        _typingCts = new CancellationTokenSource();
        StopCursorBlink();

        var incremental = EnableIncremental && newText.StartsWith(_currentText, StringComparison.Ordinal);
        var startIndex = incremental ? _currentText.Length : 0;

        if (!incremental)
        {
            _currentText = string.Empty;
            Text = string.Empty;
        }

        _ = AnimateTypewriter(newText, startIndex, _typingCts.Token);
    }

    private async Task AnimateTypewriter(string fullText, int startIndex, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(fullText))
        {
            await Dispatcher.InvokeAsync(() => Text = string.Empty);
            _currentText = string.Empty;
            return;
        }

        for (int i = startIndex; i < fullText.Length; i++)
        {
            if (ct.IsCancellationRequested)
                return;

            _currentText = fullText[..(i + 1)];
            var snapshot = _currentText;

            await Dispatcher.InvokeAsync(() =>
            {
                Text = ShowCursor ? snapshot + CursorCharacter : snapshot;
            });

            var delay = CharacterDelay;
            if (".!?,:;".Contains(fullText[i]))
            {
                delay *= 3;
            }
            else if (fullText[i] == '\n')
            {
                delay *= 2;
            }

            await Task.Delay(delay, ct);
        }

        _currentText = fullText;

        if (ShowCursor)
        {
            await Dispatcher.InvokeAsync(() => Text = fullText + CursorCharacter);
            StartCursorBlink(fullText);
        }
        else
        {
            await Dispatcher.InvokeAsync(() => Text = fullText);
        }
    }

    private void StartCursorBlink(string fullText)
    {
        StopCursorBlink();

        if (CursorBlinkCount <= 0)
        {
            Text = fullText;
            return;
        }

        var remainingToggles = CursorBlinkCount * 2;
        _cursorVisible = true;

        _cursorTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(CursorBlinkInterval)
        };

        _cursorTimer.Tick += (_, _) =>
        {
            remainingToggles--;
            _cursorVisible = !_cursorVisible;
            Text = fullText + (_cursorVisible ? CursorCharacter : string.Empty);

            if (remainingToggles <= 0)
            {
                StopCursorBlink();
                Text = fullText;
            }
        };

        _cursorTimer.Start();
    }

    private void StopCursorBlink()
    {
        if (_cursorTimer == null)
            return;

        _cursorTimer.Stop();
        _cursorTimer = null;
    }
}
