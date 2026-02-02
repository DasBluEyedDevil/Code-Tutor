using System.Windows.Controls;
using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Controls;

public partial class WarningSection : UserControl
{
    public WarningSection(ContentSection section)
    {
        InitializeComponent();
        ContentText.Text = section.Content;
    }
}
