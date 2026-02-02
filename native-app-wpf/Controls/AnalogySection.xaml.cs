using System.Windows.Controls;
using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Controls;

public partial class AnalogySection : UserControl
{
    public AnalogySection(ContentSection section)
    {
        InitializeComponent();
        ContentText.Text = section.Content;
    }
}
