using System.Windows.Controls;
using CodeTutor.Wpf.Models;

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
            // AvalonEdit syntax highlighting can be configured here
        }
    }
}
