using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace CodeTutor.Native.Converters;

/// <summary>
/// Converts a boolean to a visibility state
/// True = Visible, False = Collapsed
/// </summary>
public class BoolToVisibilityConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue;
        }
        return false;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue;
        }
        return false;
    }
}
