using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CodeTutor.Native.Models.Challenges;

namespace CodeTutor.Native.Controls;

/// <summary>
/// Common Mistakes Panel - displays common coding errors and how to fix them
/// </summary>
public partial class CommonMistakesPanel : UserControl
{
    public static readonly StyledProperty<List<CommonMistake>?> CommonMistakesProperty =
        AvaloniaProperty.Register<CommonMistakesPanel, List<CommonMistake>?>(nameof(CommonMistakes));

    public static readonly StyledProperty<bool> IsExpandedProperty =
        AvaloniaProperty.Register<CommonMistakesPanel, bool>(nameof(IsExpanded), defaultValue: false);

    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<CommonMistakesPanel, string>(nameof(Title), defaultValue: "Common Mistakes");

    public List<CommonMistake>? CommonMistakes
    {
        get => GetValue(CommonMistakesProperty);
        set => SetValue(CommonMistakesProperty, value);
    }

    public bool IsExpanded
    {
        get => GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Header => CommonMistakes != null ? $"Common Mistakes ({CommonMistakes.Count})" : "Common Mistakes";

    public CommonMistakesPanel()
    {
        InitializeComponent();
        DataContext = this;

        // Update header when CommonMistakes changes
        this.GetObservable(CommonMistakesProperty).Subscribe(_ => this.RaisePropertyChanged(nameof(Header)));
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
