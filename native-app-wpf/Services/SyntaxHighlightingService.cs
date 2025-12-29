using System.Windows.Media;
using ICSharpCode.AvalonEdit.Highlighting;

namespace CodeTutor.Wpf.Services;

public static class SyntaxHighlightingService
{
    public static IHighlightingDefinition? GetHighlightingForLanguage(string language)
    {
        var langLower = language.ToLower();
        var definition = langLower switch
        {
            "python" => HighlightingManager.Instance.GetDefinition("Python"),
            "javascript" or "js" => HighlightingManager.Instance.GetDefinition("JavaScript"),
            "csharp" or "c#" => HighlightingManager.Instance.GetDefinition("C#"),
            "java" => HighlightingManager.Instance.GetDefinition("Java"),
            "kotlin" => HighlightingManager.Instance.GetDefinition("Java"), // Close enough
            "rust" => HighlightingManager.Instance.GetDefinition("C#"), // Rust uses C-like syntax
            "dart" or "flutter" => HighlightingManager.Instance.GetDefinition("C#"), // Close enough
            _ => null
        };

        // Apply dark theme colors
        if (definition != null)
        {
            ApplyDarkThemeColors(definition);
        }

        return definition;
    }

    private static void ApplyDarkThemeColors(IHighlightingDefinition definition)
    {
        // Dark theme color palette (VS Code Dark+ inspired)
        var keywordColor = Color.FromRgb(197, 134, 192);      // Purple/magenta for keywords
        var typeColor = Color.FromRgb(78, 201, 176);          // Teal for types
        var stringColor = Color.FromRgb(206, 145, 120);       // Orange/brown for strings
        var commentColor = Color.FromRgb(106, 153, 85);       // Green for comments
        var numberColor = Color.FromRgb(181, 206, 168);       // Light green for numbers
        var methodColor = Color.FromRgb(220, 220, 170);       // Light yellow for methods
        var punctuationColor = Color.FromRgb(212, 212, 212);  // Light gray for punctuation

        foreach (var color in definition.NamedHighlightingColors)
        {
            var nameLower = color.Name.ToLower();

            if (nameLower.Contains("keyword") || nameLower.Contains("modifier") ||
                nameLower.Contains("visibility") || nameLower.Contains("parameter"))
            {
                color.Foreground = new SimpleHighlightingBrush(keywordColor);
            }
            else if (nameLower.Contains("class") || nameLower.Contains("type") ||
                     nameLower.Contains("namespace") || nameLower.Contains("struct") ||
                     nameLower.Contains("interface") || nameLower.Contains("enum"))
            {
                color.Foreground = new SimpleHighlightingBrush(typeColor);
            }
            else if (nameLower.Contains("string") || nameLower.Contains("char"))
            {
                color.Foreground = new SimpleHighlightingBrush(stringColor);
            }
            else if (nameLower.Contains("comment"))
            {
                color.Foreground = new SimpleHighlightingBrush(commentColor);
            }
            else if (nameLower.Contains("number") || nameLower.Contains("digit"))
            {
                color.Foreground = new SimpleHighlightingBrush(numberColor);
            }
            else if (nameLower.Contains("method") || nameLower.Contains("function"))
            {
                color.Foreground = new SimpleHighlightingBrush(methodColor);
            }
            else if (nameLower.Contains("punctuation") || nameLower.Contains("bracket"))
            {
                color.Foreground = new SimpleHighlightingBrush(punctuationColor);
            }
        }
    }
}
