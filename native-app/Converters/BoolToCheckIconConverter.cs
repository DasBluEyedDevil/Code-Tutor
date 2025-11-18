using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace CodeTutor.Native.Converters;

/// <summary>
/// Converts a boolean to a check icon or circle icon
/// True = CheckIcon (completed), False = CircleIcon (not completed)
/// </summary>
public class BoolToCheckIconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool boolValue && boolValue)
        {
            // Return CheckIcon path data
            return StreamGeometry.Parse("M9 16.17L4.83 12l-1.42 1.41L9 19 21 7l-1.41-1.41z");
        }
        else
        {
            // Return CircleIcon path data
            return StreamGeometry.Parse("M12 2C6.47 2 2 6.47 2 12s4.47 10 10 10 10-4.47 10-10S17.53 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z");
        }
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
