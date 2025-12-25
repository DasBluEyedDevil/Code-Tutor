using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TextMateSharp.Grammars;
using TextMateSharp.Internal.Grammars.Reader;
using TextMateSharp.Internal.Themes.Reader;
using TextMateSharp.Registry;
using TextMateSharp.Themes;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for managing TextMate grammars and themes for syntax highlighting
/// </summary>
public class TextMateRegistryService : ITextMateRegistryService
{
    private readonly Dictionary<string, string> _languageToScopeMap;
    private readonly RegistryOptions _registryOptions;
    private readonly Dictionary<string, IRawTheme> _themes;

    public TextMateRegistryService()
    {
        _languageToScopeMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "python", "source.python" },
            { "javascript", "source.js" },
            { "typescript", "source.ts" },
            { "java", "source.java" },
            { "csharp", "source.cs" },
            { "c#", "source.cs" },
            { "rust", "source.rust" },
            { "kotlin", "source.kotlin" },
            { "dart", "source.dart" },
            { "json", "source.json" },
            { "xml", "text.xml" },
            { "html", "text.html.basic" },
            { "css", "source.css" },
            { "markdown", "text.html.markdown" }
        };

        _registryOptions = new RegistryOptions(ThemeName.DarkPlus);
        _themes = new Dictionary<string, IRawTheme>();

        // Load built-in themes
        LoadBuiltInThemes();
    }

    public RegistryOptions GetRegistryOptions()
    {
        return _registryOptions;
    }

    public string GetScopeNameForLanguage(string language)
    {
        if (_languageToScopeMap.TryGetValue(language.ToLowerInvariant(), out var scope))
        {
            return scope;
        }

        // Default to the language name as scope if not found
        return $"source.{language.ToLowerInvariant()}";
    }

    public IRawTheme GetTheme(string themeName)
    {
        if (_themes.TryGetValue(themeName, out var theme))
        {
            return theme;
        }

        // Return default dark theme
        return _registryOptions.GetTheme(ThemeName.DarkPlus.ToString());
    }

    private void LoadBuiltInThemes()
    {
        try
        {
            // TextMateSharp comes with built-in themes
            // We'll use the built-in themes from the RegistryOptions
            _themes["vs-dark"] = _registryOptions.GetTheme(ThemeName.DarkPlus.ToString());
            _themes["vs-light"] = _registryOptions.GetTheme(ThemeName.LightPlus.ToString());
            _themes["dark"] = _registryOptions.GetTheme(ThemeName.Dark.ToString());
            _themes["light"] = _registryOptions.GetTheme(ThemeName.Light.ToString());
        }
        catch (Exception ex) when (ex is not OutOfMemoryException && ex is not StackOverflowException)
        {
            // Log error but continue - syntax highlighting will be disabled
            System.Diagnostics.Debug.WriteLine($"Failed to load themes: {ex.Message}");
        }
    }
}
