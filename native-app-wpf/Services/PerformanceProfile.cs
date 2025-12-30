using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace CodeTutor.Wpf.Services;

public static class PerformanceProfile
{
    public static bool IsSoftwareRendering => (RenderCapability.Tier >> 16) == 0;
    public static bool IsUiThrottled { get; private set; }

    public static event EventHandler<bool>? UiThrottlingChanged;

    public static void SetUiThrottled(bool value)
    {
        if (IsUiThrottled == value)
            return;

        IsUiThrottled = value;
        UiThrottlingChanged?.Invoke(null, value);
    }

    public static void ApplyLowTierResources(ResourceDictionary resources)
    {
        if (!IsSoftwareRendering)
            return;

        DisableDropShadow(resources, "NeonGlowBlue");
        DisableDropShadow(resources, "NeonGlowBlueSubtle");
        DisableDropShadow(resources, "NeonGlowGreen");
        DisableDropShadow(resources, "NeonGlowRed");
        DisableDropShadow(resources, "NeonGlowPurple");
        DisableDropShadow(resources, "ElevationShadow");
        DisableDropShadow(resources, "CardShadow");
        DisableDropShadow(resources, "CardHoverShadow");
    }

    private static void DisableDropShadow(ResourceDictionary resources, string key)
    {
        if (!TryGetResource(resources, key, out var owner, out var resource))
            return;

        if (resource is DropShadowEffect effect)
        {
            if (effect.IsFrozen)
            {
                effect = effect.Clone();
                owner![key] = effect;
            }

            effect.BlurRadius = 0;
            effect.Opacity = 0;
            effect.ShadowDepth = 0;
        }
    }

    private static bool TryGetResource(ResourceDictionary resources, string key, out ResourceDictionary? owner, out object? value)
    {
        if (resources.Contains(key))
        {
            owner = resources;
            value = resources[key];
            return true;
        }

        foreach (var dictionary in resources.MergedDictionaries)
        {
            if (dictionary.Contains(key))
            {
                owner = dictionary;
                value = dictionary[key];
                return true;
            }
        }

        owner = null;
        value = null;
        return false;
    }
}
