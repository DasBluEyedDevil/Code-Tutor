using System.Windows.Controls;
using CodeTutor.Wpf.Models;

namespace CodeTutor.Wpf.Controls;

public partial class KeyPointSection : UserControl
{
    public KeyPointSection(ContentSection section)
    {
        InitializeComponent();
        ContentText.Text = section.Content;
    }
}
