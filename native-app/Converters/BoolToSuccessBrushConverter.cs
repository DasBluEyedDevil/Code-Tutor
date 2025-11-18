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
        if (value is bool boolValue && boolValue)
        {
            // Return success color
            return Application.Current?.FindResource("SuccessBrush") as IBrush
                   ?? new SolidColorBrush(Color.Parse("#4EC9B0"));
        }
        else
        {
            // Return muted color
            return Application.Current?.FindResource("MutedTextBrush") as IBrush
                   ?? new SolidColorBrush(Color.Parse("#808080"));
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
