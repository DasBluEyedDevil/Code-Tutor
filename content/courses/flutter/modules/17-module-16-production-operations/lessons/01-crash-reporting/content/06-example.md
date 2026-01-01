---
type: "EXAMPLE"
title: "Complete Error Handling Setup"
---


Here's a production-ready error handling configuration that captures all types of errors:



```dart
// lib/core/error_reporting.dart
import 'dart:async';
import 'package:flutter/foundation.dart';
import 'package:firebase_crashlytics/firebase_crashlytics.dart';

class ErrorReporting {
  static Future<void> initialize() async {
    // Only enable in release mode
    if (!kReleaseMode) {
      // In debug mode, just print errors
      FlutterError.onError = (details) {
        FlutterError.presentError(details);
      };
      return;
    }
    
    // Pass Flutter framework errors to Crashlytics
    FlutterError.onError = (errorDetails) {
      FirebaseCrashlytics.instance.recordFlutterFatalError(errorDetails);
    };
    
    // Pass platform dispatcher errors to Crashlytics
    PlatformDispatcher.instance.onError = (error, stack) {
      FirebaseCrashlytics.instance.recordError(
        error,
        stack,
        fatal: true,
        reason: 'Platform Dispatcher Error',
      );
      return true;
    };
  }
  
  /// Call this to wrap your app with zone-based error capture
  static void runWithErrorReporting(Widget app) {
    runZonedGuarded(
      () => runApp(app),
      (error, stackTrace) {
        if (kReleaseMode) {
          FirebaseCrashlytics.instance.recordError(
            error,
            stackTrace,
            reason: 'Uncaught Zone Error',
          );
        } else {
          // In debug, print the error
          debugPrint('Zone Error: $error');
          debugPrint('Stack: $stackTrace');
        }
      },
    );
  }
  
  /// Manually report a non-fatal error
  static void reportError(
    dynamic error,
    StackTrace? stackTrace, {
    String? reason,
    bool fatal = false,
  }) {
    if (kReleaseMode) {
      FirebaseCrashlytics.instance.recordError(
        error,
        stackTrace,
        reason: reason,
        fatal: fatal,
      );
    } else {
      debugPrint('Reported Error: $error');
    }
  }
}

// lib/main.dart
void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(options: DefaultFirebaseOptions.currentPlatform);
  await ErrorReporting.initialize();
  
  ErrorReporting.runWithErrorReporting(const MyApp());
}
```
