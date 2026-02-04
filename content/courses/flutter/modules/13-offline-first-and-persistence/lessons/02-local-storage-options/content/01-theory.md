---
type: "THEORY"
title: "SharedPreferences"
---


**SharedPreferences** is the simplest storage option for key-value pairs.

**Best For:**
- User preferences (theme, language)
- Simple flags (onboarding completed)
- Small configuration values
- Non-sensitive app settings

**Limitations:**
- No complex queries
- No relationships
- Limited data types
- Not for large datasets
- **Not secure** -- stores data in plain text (use `flutter_secure_storage` for tokens and secrets)



```dart
// Add to pubspec.yaml:
// shared_preferences: ^2.2.0

import 'package:shared_preferences/shared_preferences.dart';

class PreferencesService {
  static late SharedPreferences _prefs;
  
  static Future<void> init() async {
    _prefs = await SharedPreferences.getInstance();
  }
  
  // Theme preference
  static bool get isDarkMode => _prefs.getBool('dark_mode') ?? false;
  static Future<void> setDarkMode(bool value) => 
      _prefs.setBool('dark_mode', value);
  
  // Last selected tab (non-sensitive setting)
  static int get lastTab => _prefs.getInt('last_tab') ?? 0;
  static Future<void> setLastTab(int index) =>
      _prefs.setInt('last_tab', index);

  // NOTE: For auth tokens, use flutter_secure_storage instead!
  // SharedPreferences stores data in plain text.

  // Onboarding flag
  static bool get hasCompletedOnboarding => 
      _prefs.getBool('onboarding_complete') ?? false;
  static Future<void> completeOnboarding() => 
      _prefs.setBool('onboarding_complete', true);
}
```
