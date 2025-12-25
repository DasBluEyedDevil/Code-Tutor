using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace CodeTutor.Native.Converters;

/// <summary>
/// Converts a boolean to a success brush or muted brush
/// True = SuccessBrush (completed), False = MutedTextBrush (not completed)
/// </summary>
public class BoolToSuccessBrushConverter : IValueConverter
{
    private static readonly SolidColorBrush SuccessBrush = new(Color.Parse("#4EC9B0"));
    private static readonly SolidColorBrush MutedBrush = new(Color.Parse("#808080"));

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // True = SuccessBrush (completed), False = MutedTextBrush (not completed)
        return (value is bool boolValue && boolValue) ? SuccessBrush : MutedBrush;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // One-way binding only - ConvertBack not supported
        return Avalonia.Data.BindingOperations.DoNothing;
    }
}
