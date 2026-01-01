import 'package:flutter/material.dart';
import 'package:sentry_flutter/sentry_flutter.dart';

Future<void> main() async {
  await SentryFlutter.init(
    (options) {
      options.dsn = const String.fromEnvironment(
        'SENTRY_DSN',
        defaultValue: '',
      );
      
      options.environment = const String.fromEnvironment(
        'ENVIRONMENT',
        defaultValue: 'development',
      );
      
      options.tracesSampleRate = 0.2;
      
      options.enableAutoNativeBreadcrumbs = true;
      options.enableAutoPerformanceTracing = true;
    },
    appRunner: () => runApp(const MyApp()),
  );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Sentry Demo',
      navigatorObservers: [
        SentryNavigatorObserver(),
      ],
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
      body: Center(
        child: ElevatedButton(
          onPressed: () {
            AppBreadcrumbs.logUserAction('button_pressed', data: {'button': 'demo'});
          },
          child: const Text('Log Action'),
        ),
      ),
    );
  }
}

/// Helper class for logging breadcrumbs
class AppBreadcrumbs {
  /// Log a user action breadcrumb
  static void logUserAction(String action, {Map<String, dynamic>? data}) {
    Sentry.addBreadcrumb(Breadcrumb(
      message: action,
      category: 'user_action',
      data: data,
      level: SentryLevel.info,
    ));
  }
  
  /// Log an API call breadcrumb
  static void logApiCall(String method, String path, {int? statusCode}) {
    Sentry.addBreadcrumb(Breadcrumb(
      message: '$method $path',
      category: 'http',
      data: {
        'method': method,
        'path': path,
        if (statusCode != null) 'status_code': statusCode,
      },
      level: statusCode != null && statusCode >= 400 
          ? SentryLevel.error 
          : SentryLevel.info,
    ));
  }
}