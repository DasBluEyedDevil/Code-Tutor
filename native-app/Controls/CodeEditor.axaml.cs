using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using AvaloniaEdit;
using AvaloniaEdit.TextMate;
using TextMateSharp.Grammars;
using CodeTutor.Native.Services;
using CodeTutor.Native.Models;

namespace CodeTutor.Native.Controls;

/// <summary>
/// Enhanced code editor control with syntax highlighting
/// </summary>
public partial class CodeEditor : UserControl
{
    private TextMate.Installation? _textMateInstallation;
    private readonly ITextMateRegistryService? _registryService;
    private readonly IEditorConfigurationService? _configService;

    public static readonly StyledProperty<string> TextProperty =
        AvaloniaProperty.Register<CodeEditor, string>(nameof(Text), string.Empty);

    public static readonly StyledProperty<string> LanguageProperty =
        AvaloniaProperty.Register<CodeEditor, string>(nameof(Language), "text");

    public static readonly StyledProperty<bool> IsReadOnlyProperty =
        AvaloniaProperty.Register<CodeEditor, bool>(nameof(IsReadOnly), false);

    public CodeEditor()
    {
        InitializeComponent();

        // Try to get services from App's service provider if available
        if (Application.Current is App app)
        {
            // Services will be null in design mode
            _registryService = app.Services?.GetService(typeof(ITextMateRegistryService)) as ITextMateRegistryService;
            _configService = app.Services?.GetService(typeof(IEditorConfigurationService)) as IEditorConfigurationService;
        }

        Loaded += OnLoaded;
    }

    public string Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Language
    {
        get => GetValue(LanguageProperty);
        set => SetValue(LanguageProperty, value);
    }

    public bool IsReadOnly
    {
        get => GetValue(IsReadOnlyProperty);
        set => SetValue(IsReadOnlyProperty, value);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == TextProperty)
        {
            if (TextEditor != null && TextEditor.Text != Text)
            {
                TextEditor.Text = Text;
            }
        }
        else if (change.Property == LanguageProperty)
        {
            UpdateSyntaxHighlighting();
        }
        else if (change.Property == IsReadOnlyProperty)
        {
            if (TextEditor != null)
            {
                TextEditor.IsReadOnly = IsReadOnly;
            }
        }
    }

    private void OnLoaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (TextEditor == null) return;

        // Set up text changed event
        TextEditor.TextChanged += (s, e) =>
        {
            if (TextEditor.Text != Text)
            {
                Text = TextEditor.Text;
            }
        };

        // Apply configuration
        ApplyConfiguration();

        // Set up syntax highlighting
        UpdateSyntaxHighlighting();

        // Set initial text
        if (!string.IsNullOrEmpty(Text))
        {
            TextEditor.Text = Text;
        }

        // Set read-only state
        TextEditor.IsReadOnly = IsReadOnly;
    }

    private void ApplyConfiguration()
    {
        if (TextEditor == null || _configService == null) return;

        try
        {
            var config = _configService.GetConfiguration();

            TextEditor.FontFamily = FontFamily.Parse(config.FontFamily);
            TextEditor.FontSize = config.FontSize;
            TextEditor.ShowLineNumbers = config.ShowLineNumbers;
            TextEditor.Options.ConvertTabsToSpaces = config.ConvertTabsToSpaces;
            TextEditor.Options.IndentationSize = config.TabSize;
            TextEditor.Options.EnableVirtualSpace = config.EnableVirtualSpace;
            TextEditor.Options.ShowEndOfLine = config.ShowEndOfLine;
            TextEditor.Options.ShowTabs = config.ShowTabs;
            TextEditor.Options.ShowSpaces = config.ShowSpaces;
            TextEditor.WordWrap = config.WordWrap;
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            System.Diagnostics.Debug.WriteLine($"Failed to apply editor configuration: {ex.Message}");
        }
    }

    private void UpdateSyntaxHighlighting()
    {
        if (TextEditor == null || _registryService == null || string.IsNullOrEmpty(Language))
            return;

        try
        {
            // Dispose existing installation
            _textMateInstallation?.Dispose();

            // Get registry options
            var registryOptions = _registryService.GetRegistryOptions();

            // Install TextMate with the language grammar
            _textMateInstallation = TextEditor.InstallTextMate(registryOptions);

            // Get scope name for language
            var scopeName = _registryService.GetScopeNameForLanguage(Language);

            // Set the grammar
            _textMateInstallation.SetGrammar(scopeName);
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            // Syntax highlighting failed, but editor still works
            System.Diagnostics.Debug.WriteLine($"Failed to set up syntax highlighting for {Language}: {ex.Message}");
        }
    }
}
