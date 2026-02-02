import 'package:flutter/material.dart';
import 'package:sentry_flutter/sentry_flutter.dart';

Future<void> main() async {
  // TODO: Initialize Sentry with SentryFlutter.init()
  // - Read DSN from String.fromEnvironment('SENTRY_DSN')
  // - Set environment from String.fromEnvironment('ENVIRONMENT', defaultValue: 'development')
  // - Set tracesSampleRate to 0.2
  // - Use appRunner parameter to run the app
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  
  @override
  Widget build(BuildContext context) {
    // TODO: Add SentryNavigatorObserver to navigatorObservers
    return MaterialApp(
      title: 'Sentry Demo',
      home: const HomeScreen(),
    );
  }
}

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Home')),
      body: const Center(child: Text('Sentry Demo')),
    );
  }
}

/// Helper class for logging breadcrumbs
class AppBreadcrumbs {
  /// Log a user action breadcrumb
  static void logUserAction(String action, {Map<String, dynamic>? data}) {
    // TODO: Add breadcrumb with category 'user_action'
  }
  
  /// Log an API call breadcrumb
  static void logApiCall(String method, String path, {int? statusCode}) {
    // TODO: Add breadcrumb with category 'http'
    // Include method, path, and optional statusCode in data
  }
}