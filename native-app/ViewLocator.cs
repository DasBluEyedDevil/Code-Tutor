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

        var name = data.GetType().FullName!.Replace("ViewModel", "");
        var viewName = name.Replace(".ViewModels.", ".Views.");

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
