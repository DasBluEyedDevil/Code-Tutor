using System.Windows.Controls;
using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Controls;

public partial class TheorySection : UserControl
{
    public TheorySection(ContentSection section)
    {
        InitializeComponent();
        SectionTitle.Text = section.Title;
        ContentText.Text = section.Content;
    }
}
