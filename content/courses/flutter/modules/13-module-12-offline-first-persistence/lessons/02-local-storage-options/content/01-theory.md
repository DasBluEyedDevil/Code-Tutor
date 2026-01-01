---
type: "THEORY"
title: "SharedPreferences"
---


**SharedPreferences** is the simplest storage option for key-value pairs.

**Best For:**
- User preferences (theme, language)
- Simple flags (onboarding completed)
- Small configuration values
- Authentication tokens

**Limitations:**
- No complex queries
- No relationships
- Limited data types
- Not for large datasets



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
  
  // User token
  static String? get authToken => _prefs.getString('auth_token');
  static Future<void> setAuthToken(String token) => 
      _prefs.setString('auth_token', token);
  static Future<void> clearAuthToken() => 
      _prefs.remove('auth_token');
  
  // Onboarding flag
  static bool get hasCompletedOnboarding => 
      _prefs.getBool('onboarding_complete') ?? false;
  static Future<void> completeOnboarding() => 
      _prefs.setBool('onboarding_complete', true);
}
```
