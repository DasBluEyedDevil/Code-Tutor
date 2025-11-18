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
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // True = SuccessBrush (completed), False = MutedTextBrush (not completed)
        return (value is bool boolValue && boolValue)
            ? Application.Current?.FindResource("SuccessBrush") as IBrush ?? new SolidColorBrush(Color.Parse("#4EC9B0"))
            : Application.Current?.FindResource("MutedTextBrush") as IBrush ?? new SolidColorBrush(Color.Parse("#808080"));
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // One-way binding only - ConvertBack not supported
        return Avalonia.Data.BindingOperations.DoNothing;
    }
}
