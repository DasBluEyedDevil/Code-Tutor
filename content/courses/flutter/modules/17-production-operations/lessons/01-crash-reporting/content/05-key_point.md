---
type: "KEY_POINT"
title: "Capturing Different Types of Errors"
---


**Flutter Framework Errors**

These are errors caught by Flutter's error handling (widget build errors, layout errors, etc.):

```dart
// Crashlytics
FlutterError.onError = FirebaseCrashlytics.instance.recordFlutterFatalError;

// Sentry - handled automatically by SentryFlutter.init()
```

**Dart Async Errors (Zone Errors)**

Errors from async operations not caught by try/catch:

```dart
// Using runZonedGuarded for comprehensive error capture
runZonedGuarded(
  () => runApp(const MyApp()),
  (error, stackTrace) {
    // Crashlytics
    FirebaseCrashlytics.instance.recordError(error, stackTrace);
    
    // Sentry
    Sentry.captureException(error, stackTrace: stackTrace);
  },
);
```

**Platform/Isolate Errors**

Errors from platform channels or isolates:

```dart
PlatformDispatcher.instance.onError = (error, stack) {
  // Report to your crash service
  return true; // Prevents the error from propagating
};
```

