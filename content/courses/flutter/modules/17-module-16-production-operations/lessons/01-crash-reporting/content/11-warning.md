---
type: "WARNING"
title: "Common Crash Reporting Mistakes"
---


**1. Not Testing in Release Mode**

Crashlytics behaves differently in debug vs release. Always test your crash reporting in release mode:

```bash
flutter run --release
```

**2. Missing Native Crash Support**

Flutter errors are caught, but native crashes need additional setup:
- iOS: dSYM files must be uploaded
- Android: ProGuard mapping files must be uploaded

**3. Over-Reporting Non-Fatal Errors**

Don't report expected errors as crashes:

```dart
// BAD - Reports normal validation as error
try {
  validateEmail(email);
} catch (e) {
  Crashlytics.instance.recordError(e, stack); // Don't do this!
}

// GOOD - Only report unexpected errors
try {
  await api.fetchUser();
} catch (e) {
  if (e is! NetworkException) {
    Crashlytics.instance.recordError(e, stack);
  }
  rethrow;
}
```

**4. Not Linking to Source Maps**

Without proper symbolication, you'll see obfuscated stack traces. Always:
- Upload dSYM files for iOS
- Upload ProGuard mappings for Android
- Configure source maps for web

**5. Ignoring Crash Trends After Releases**

Always check your dashboard within 24 hours of a release. New crashes often surface immediately.

