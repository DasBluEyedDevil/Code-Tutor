import 'dart:async';
import 'package:flutter/foundation.dart';
import 'package:firebase_crashlytics/firebase_crashlytics.dart';

class ErrorReportingService {
  // Singleton pattern
  static final ErrorReportingService _instance = ErrorReportingService._internal();
  factory ErrorReportingService() => _instance;
  ErrorReportingService._internal();
  
  /// Initialize error reporting handlers
  /// Only enable reporting in release mode
  Future<void> initialize() async {
    if (!kReleaseMode) {
      // In debug mode, just print errors
      FlutterError.onError = (details) {
        FlutterError.presentError(details);
      };
      return;
    }
    
    // Set up FlutterError.onError handler
    FlutterError.onError = (errorDetails) {
      FirebaseCrashlytics.instance.recordFlutterFatalError(errorDetails);
    };
    
    // Set up PlatformDispatcher.instance.onError handler
    PlatformDispatcher.instance.onError = (error, stack) {
      FirebaseCrashlytics.instance.recordError(
        error,
        stack,
        fatal: true,
        reason: 'PlatformDispatcher Error',
      );
      return true;
    };
  }
  
  /// Set user identifier for crash reports
  Future<void> setUser(String userId) async {
    await FirebaseCrashlytics.instance.setUserIdentifier(userId);
  }
  
  /// Log a breadcrumb for debugging
  void logBreadcrumb(String message, {String? category}) {
    final formattedMessage = category != null 
        ? '[$category] $message' 
        : message;
    FirebaseCrashlytics.instance.log(formattedMessage);
  }
  
  /// Manually report a caught exception
  Future<void> reportError(
    dynamic exception,
    StackTrace? stackTrace, {
    String? reason,
    bool fatal = false,
  }) async {
    if (kReleaseMode) {
      await FirebaseCrashlytics.instance.recordError(
        exception,
        stackTrace,
        reason: reason,
        fatal: fatal,
      );
    } else {
      debugPrint('Error reported: $exception');
      if (stackTrace != null) {
        debugPrint('Stack trace: $stackTrace');
      }
    }
  }
}

void main() {
  print('ErrorReportingService created');
  final service = ErrorReportingService();
  print('Singleton works: ${identical(service, ErrorReportingService())}');
}