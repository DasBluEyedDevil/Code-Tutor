---
type: "EXAMPLE"
title: "Enriching Crash Reports with User Context"
---


Crashes are more useful when you know who experienced them and what they were doing:



```dart
// lib/core/error_reporting.dart

class ErrorReporting {
  /// Set user information for crash reports
  static Future<void> setUser({
    required String userId,
    String? email,
    String? name,
  }) async {
    // Firebase Crashlytics
    await FirebaseCrashlytics.instance.setUserIdentifier(userId);
    
    // For Sentry, you'd use:
    // Sentry.configureScope((scope) {
    //   scope.setUser(SentryUser(
    //     id: userId,
    //     email: email,
    //     username: name,
    //   ));
    // });
  }
  
  /// Clear user info (call on logout)
  static Future<void> clearUser() async {
    await FirebaseCrashlytics.instance.setUserIdentifier('');
    
    // Sentry:
    // Sentry.configureScope((scope) => scope.setUser(null));
  }
  
  /// Add custom key-value pairs to crash reports
  static Future<void> setCustomKey(String key, dynamic value) async {
    await FirebaseCrashlytics.instance.setCustomKey(key, value.toString());
    
    // Sentry:
    // Sentry.configureScope((scope) {
    //   scope.setTag(key, value.toString());
    // });
  }
  
  /// Set multiple custom keys at once
  static Future<void> setCustomKeys(Map<String, dynamic> keys) async {
    for (final entry in keys.entries) {
      await FirebaseCrashlytics.instance.setCustomKey(
        entry.key,
        entry.value.toString(),
      );
    }
  }
}

// Usage in your app:
class AuthService {
  Future<void> signIn(User user) async {
    // ... sign in logic ...
    
    // Set user context for crash reports
    await ErrorReporting.setUser(
      userId: user.id,
      email: user.email,
      name: user.displayName,
    );
    
    // Add relevant context
    await ErrorReporting.setCustomKeys({
      'subscription_tier': user.subscriptionTier,
      'account_age_days': user.accountAgeDays,
      'feature_flags': user.enabledFeatures.join(','),
    });
  }
  
  Future<void> signOut() async {
    await ErrorReporting.clearUser();
    // ... sign out logic ...
  }
}
```
