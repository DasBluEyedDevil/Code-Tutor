using System;
using System.Collections.Generic;
using TextMateSharp.Grammars;
using TextMateSharp.Registry;
using TextMateSharp.Themes;

namespace CodeTutor.Native.Services;

/// <summary>
/// Service for managing TextMate grammars and themes for syntax highlighting
/// </summary>
public interface ITextMateRegistryService
{
    /// <summary>
    /// Get the TextMate registry options
    /// </summary>
    RegistryOptions GetRegistryOptions();

    /// <summary>
    /// Get the scope name for a given language
    /// </summary>
    string GetScopeNameForLanguage(string language);

    /// <summary>
    /// Get the theme for syntax highlighting
    /// </summary>
    IRawTheme GetTheme(string themeName);
}
