using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CodeTutor.Native.ViewModels;

namespace CodeTutor.Native;

/// <summary>
/// Locates and creates views for view models automatically
/// Converts "FooViewModel" to "FooView" by convention
/// </summary>
public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        var fullName = data.GetType().FullName!;

        // First replace the namespace, then remove "ViewModel" suffix from class name
        // Order matters! If we remove "ViewModel" first, it corrupts "ViewModels" namespace
        var viewName = fullName.Replace(".ViewModels.", ".Views.");

        // Only remove "ViewModel" from the end (the class name suffix)
        if (viewName.EndsWith("ViewModel"))
        {
            viewName = viewName.Substring(0, viewName.Length - "ViewModel".Length);
        }

        var type = Type.GetType(viewName);

        if (type != null)
        {
            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = data;
            return control;
        }

        return new TextBlock
        {
            Text = $"View not found: {viewName}"
        };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
