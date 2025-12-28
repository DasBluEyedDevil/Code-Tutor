using System.Windows.Controls;
using System.Xml;
using CodeTutor.Wpf.Models;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace CodeTutor.Wpf.Controls;

public partial class CodeExampleSection : UserControl
{
    public CodeExampleSection(ContentSection section)
    {
        InitializeComponent();
        ExampleTitle.Text = section.Title;
        CodeEditor.Text = section.Code ?? string.Empty;
        Description.Text = section.Content;

        // Set syntax highlighting based on language
        if (!string.IsNullOrEmpty(section.Language))
        {
            var highlighting = GetHighlightingForLanguage(section.Language);
            if (highlighting != null)
            {
                CodeEditor.SyntaxHighlighting = highlighting;
            }
        }
    }

    private static IHighlightingDefinition? GetHighlightingForLanguage(string language)
    {
        var langLower = language.ToLower();
        return langLower switch
        {
            "python" => HighlightingManager.Instance.GetDefinition("Python"),
            "javascript" or "js" => HighlightingManager.Instance.GetDefinition("JavaScript"),
            "csharp" or "c#" => HighlightingManager.Instance.GetDefinition("C#"),
            "java" => HighlightingManager.Instance.GetDefinition("Java"),
            "kotlin" => HighlightingManager.Instance.GetDefinition("Java"), // Close enough
            "dart" or "flutter" => HighlightingManager.Instance.GetDefinition("C#"), // Close enough
            _ => null
        };
    }
}
