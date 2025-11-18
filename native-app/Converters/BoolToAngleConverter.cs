using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace CodeTutor.Native.Converters;

/// <summary>
/// Converts a boolean to a rotation angle for expand/collapse animations
/// True = 180 degrees (expanded), False = 0 degrees (collapsed)
/// </summary>
public class BoolToAngleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue)
        {
            return boolValue ? 180.0 : 0.0;
        }
        return 0.0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // One-way binding only - ConvertBack not supported
        return Avalonia.Data.BindingOperations.DoNothing;
    }
}
