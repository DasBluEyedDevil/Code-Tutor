using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CodeTutor.Wpf.Behaviors;

/// <summary>
/// Provides a typewriter effect for TextBlock elements, making text appear character by character
/// like a retro terminal. This creates a satisfying "juice" effect for output display.
/// </summary>
public static class TypewriterBehavior
{
    private static readonly object _lockObject = new();

    #region Dependency Properties

    public static readonly DependencyProperty EnableTypewriterProperty =
        DependencyProperty.RegisterAttached(
            "EnableTypewriter",
            typeof(bool),
            typeof(TypewriterBehavior),
            new PropertyMetadata(false));

    public static readonly DependencyProperty TypewriterTextProperty =
        DependencyProperty.RegisterAttached(
            "TypewriterText",
            typeof(string),
            typeof(TypewriterBehavior),
            new PropertyMetadata(string.Empty, OnTypewriterTextChanged));

    public static readonly DependencyProperty CharacterDelayProperty =
        DependencyProperty.RegisterAttached(
            "CharacterDelay",
            typeof(int),
            typeof(TypewriterBehavior),
            new PropertyMetadata(15)); // 15ms per character = ~66 chars/sec

    public static readonly DependencyProperty TypewriterCancellationProperty =
        DependencyProperty.RegisterAttached(
            "TypewriterCancellation",
            typeof(CancellationTokenSource),
            typeof(TypewriterBehavior),
            new PropertyMetadata(null));

    public static readonly DependencyProperty ShowCursorProperty =
        DependencyProperty.RegisterAttached(
            "ShowCursor",
            typeof(bool),
            typeof(TypewriterBehavior),
            new PropertyMetadata(true));

    #endregion

    #region Getters and Setters

    public static bool GetEnableTypewriter(DependencyObject obj)
        => (bool)obj.GetValue(EnableTypewriterProperty);

    public static void SetEnableTypewriter(DependencyObject obj, bool value)
        => obj.SetValue(EnableTypewriterProperty, value);

    public static string GetTypewriterText(DependencyObject obj)
        => (string)obj.GetValue(TypewriterTextProperty);

    public static void SetTypewriterText(DependencyObject obj, string value)
        => obj.SetValue(TypewriterTextProperty, value);

    public static int GetCharacterDelay(DependencyObject obj)
        => (int)obj.GetValue(CharacterDelayProperty);

    public static void SetCharacterDelay(DependencyObject obj, int value)
        => obj.SetValue(CharacterDelayProperty, value);

    public static CancellationTokenSource? GetTypewriterCancellation(DependencyObject obj)
        => (CancellationTokenSource?)obj.GetValue(TypewriterCancellationProperty);

    public static void SetTypewriterCancellation(DependencyObject obj, CancellationTokenSource? value)
        => obj.SetValue(TypewriterCancellationProperty, value);

    public static bool GetShowCursor(DependencyObject obj)
        => (bool)obj.GetValue(ShowCursorProperty);

    public static void SetShowCursor(DependencyObject obj, bool value)
        => obj.SetValue(ShowCursorProperty, value);

    #endregion

    #region Logic

    private static void OnTypewriterTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not TextBlock textBlock)
            return;

        if (!GetEnableTypewriter(textBlock))
        {
            // Typewriter disabled - just set the text normally
            textBlock.Text = e.NewValue as string ?? string.Empty;
            return;
        }

        var newText = e.NewValue as string ?? string.Empty;

        // Cancel any existing animation
        var existingCts = GetTypewriterCancellation(textBlock);
        existingCts?.Cancel();

        // Start new typewriter animation
        var cts = new CancellationTokenSource();
        SetTypewriterCancellation(textBlock, cts);

        _ = AnimateTypewriter(textBlock, newText, cts.Token);
    }

    private static async Task AnimateTypewriter(TextBlock textBlock, string fullText, CancellationToken ct)
    {
        var characterDelay = GetCharacterDelay(textBlock);
        var showCursor = GetShowCursor(textBlock);
        var cursorChar = showCursor ? "_" : "";

        try
        {
            // Clear text initially
            textBlock.Dispatcher.Invoke(() => textBlock.Text = cursorChar);

            // Type each character
            for (int i = 0; i < fullText.Length; i++)
            {
                if (ct.IsCancellationRequested)
                {
                    // If cancelled, show full text immediately
                    textBlock.Dispatcher.Invoke(() => textBlock.Text = fullText);
                    return;
                }

                var currentIndex = i;

                await textBlock.Dispatcher.InvokeAsync(() =>
                {
                    textBlock.Text = fullText[..(currentIndex + 1)] + cursorChar;
                });

                // Variable delay for more natural feel
                var delay = characterDelay;

                // Slightly longer pause after punctuation
                if (i < fullText.Length && ".!?,:;".Contains(fullText[i]))
                {
                    delay = characterDelay * 3;
                }
                // Slightly longer pause after newlines
                else if (i < fullText.Length && fullText[i] == '\n')
                {
                    delay = characterDelay * 2;
                }

                await Task.Delay(delay, ct);
            }

            // Remove cursor when done
            if (showCursor)
            {
                await Task.Delay(300, ct); // Brief pause before removing cursor
                textBlock.Dispatcher.Invoke(() => textBlock.Text = fullText);
            }
        }
        catch (TaskCanceledException)
        {
            // Animation was cancelled, show full text
            textBlock.Dispatcher.Invoke(() => textBlock.Text = fullText);
        }
        catch (OperationCanceledException)
        {
            // Animation was cancelled, show full text
            textBlock.Dispatcher.Invoke(() => textBlock.Text = fullText);
        }
    }

    /// <summary>
    /// Cancels any ongoing typewriter animation and shows the full text immediately.
    /// </summary>
    public static void CancelAndShowFull(TextBlock textBlock)
    {
        var cts = GetTypewriterCancellation(textBlock);
        cts?.Cancel();

        var fullText = GetTypewriterText(textBlock);
        textBlock.Text = fullText;
    }

    #endregion
}

/// <summary>
/// Extension methods for easier typewriter effect usage in code-behind.
/// </summary>
public static class TypewriterExtensions
{
    /// <summary>
    /// Displays text with a typewriter effect on a TextBlock.
    /// </summary>
    public static void TypeText(this TextBlock textBlock, string text, int characterDelayMs = 15, bool showCursor = true)
    {
        TypewriterBehavior.SetEnableTypewriter(textBlock, true);
        TypewriterBehavior.SetCharacterDelay(textBlock, characterDelayMs);
        TypewriterBehavior.SetShowCursor(textBlock, showCursor);
        TypewriterBehavior.SetTypewriterText(textBlock, text);
    }

    /// <summary>
    /// Stops any ongoing typewriter animation and shows the full text.
    /// </summary>
    public static void StopTypewriter(this TextBlock textBlock)
    {
        TypewriterBehavior.CancelAndShowFull(textBlock);
    }
}
