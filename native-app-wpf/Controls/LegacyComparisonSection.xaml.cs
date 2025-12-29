using System.Windows.Controls;
using CodeTutor.Wpf.Models;
using ICSharpCode.AvalonEdit.Highlighting;

namespace CodeTutor.Wpf.Controls;

public partial class LegacyComparisonSection : UserControl
{
    public LegacyComparisonSection(ContentSection section)
    {
        InitializeComponent();

        // Set header title with legacy framework name
        var legacyName = GetLegacyDisplayName(section.Legacy);
        HeaderTitle.Text = !string.IsNullOrEmpty(section.Title)
            ? section.Title
            : $"Legacy: {legacyName}";

        // Set content text
        ContentText.Text = section.Content;

        // Set code with syntax highlighting
        CodeEditor.Text = section.Code ?? string.Empty;

        if (!string.IsNullOrEmpty(section.Language))
        {
            var highlighting = GetHighlightingForLanguage(section.Language);
            if (highlighting != null)
            {
                CodeEditor.SyntaxHighlighting = highlighting;
            }
        }
    }

    private static string GetLegacyDisplayName(string? legacy)
    {
        if (string.IsNullOrEmpty(legacy))
            return "Legacy Code";

        return legacy.ToLower() switch
        {
            "express" => "Express.js",
            "node" or "nodejs" => "Node.js",
            "vitest" => "Vitest",
            "jest" => "Jest",
            "mocha" => "Mocha",
            "react" => "React",
            "vue" => "Vue.js",
            "angular" => "Angular",
            _ => legacy
        };
    }

    private static IHighlightingDefinition? GetHighlightingForLanguage(string language)
    {
        var langLower = language.ToLower();
        return langLower switch
        {
            "python" => HighlightingManager.Instance.GetDefinition("Python"),
            "javascript" or "js" => HighlightingManager.Instance.GetDefinition("JavaScript"),
            "typescript" or "ts" => HighlightingManager.Instance.GetDefinition("JavaScript"),
            "csharp" or "c#" => HighlightingManager.Instance.GetDefinition("C#"),
            "java" => HighlightingManager.Instance.GetDefinition("Java"),
            "kotlin" => HighlightingManager.Instance.GetDefinition("Java"),
            "dart" or "flutter" => HighlightingManager.Instance.GetDefinition("C#"),
            _ => null
        };
    }
}
