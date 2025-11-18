namespace CodeTutor.Native.Models;

/// <summary>
/// Configuration settings for the code editor
/// </summary>
public class EditorConfiguration
{
    public string FontFamily { get; set; } = "Cascadia Code, Consolas, Courier New, monospace";
    public int FontSize { get; set; } = 14;
    public int TabSize { get; set; } = 4;
    public bool ConvertTabsToSpaces { get; set; } = true;
    public bool ShowLineNumbers { get; set; } = true;
    public bool EnableCodeFolding { get; set; } = true;
    public bool EnableAutoIndentation { get; set; } = true;
    public bool EnableBracketMatching { get; set; } = true;
    public bool WordWrap { get; set; } = false;
    public bool ShowEndOfLine { get; set; } = false;
    public bool ShowTabs { get; set; } = false;
    public bool ShowSpaces { get; set; } = false;
    public bool EnableVirtualSpace { get; set; } = false;
    public bool EnableLigatures { get; set; } = true;
    public string ThemeName { get; set; } = "vs-dark";
}
