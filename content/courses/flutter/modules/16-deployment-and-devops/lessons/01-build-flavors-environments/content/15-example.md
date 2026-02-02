---
type: "EXAMPLE"
title: "Feature Flags"
---


Use environments to toggle features:



```dart
// lib/config/feature_flags.dart

class FeatureFlags {
  // Read from dart-define or set per environment
  static bool get enableNewCheckout => 
      const bool.fromEnvironment('FEATURE_NEW_CHECKOUT', defaultValue: false) ||
      AppConfig.instance.isDev; // Always on in dev
  
  static bool get enableDarkMode =>
      const bool.fromEnvironment('FEATURE_DARK_MODE', defaultValue: true);
  
  static bool get enableBetaFeatures =>
      !AppConfig.instance.isProd; // Never in prod
  
  static bool get showDevMenu =>
      AppConfig.instance.isDev;
}

// Usage in widgets:
class SettingsScreen extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return ListView(
      children: [
        if (FeatureFlags.enableDarkMode)
          const DarkModeToggle(),
        
        if (FeatureFlags.enableNewCheckout)
          const NewCheckoutOption(),
        
        if (FeatureFlags.showDevMenu)
          const DevMenuSection(),
      ],
    );
  }
}
```
