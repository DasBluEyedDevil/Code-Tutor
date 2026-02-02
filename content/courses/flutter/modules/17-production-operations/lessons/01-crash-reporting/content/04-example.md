---
type: "EXAMPLE"
title: "Sentry Setup"
---


**Step 1: Create a Sentry Project**

1. Go to sentry.io and create a free account
2. Create a new project, select "Flutter"
3. Copy your DSN (Data Source Name) - looks like: `https://abc123@o456.ingest.sentry.io/789`

**Step 2: Add Dependencies**

```yaml
# pubspec.yaml
dependencies:
  sentry_flutter: ^8.12.0
```

**Step 3: Initialize Sentry**



```dart
// lib/main.dart
import 'package:flutter/material.dart';
import 'package:sentry_flutter/sentry_flutter.dart';

Future<void> main() async {
  await SentryFlutter.init(
    (options) {
      options.dsn = const String.fromEnvironment(
        'SENTRY_DSN',
        defaultValue: 'https://your-dsn@sentry.io/project-id',
      );
      
      // Set environment based on build mode
      options.environment = const String.fromEnvironment(
        'ENVIRONMENT',
        defaultValue: 'development',
      );
      
      // Sample rate for performance monitoring (0.0 to 1.0)
      options.tracesSampleRate = 0.2; // 20% of transactions
      
      // Capture 100% of errors
      options.sampleRate = 1.0;
      
      // Enable automatic breadcrumb collection
      options.enableAutoNativeBreadcrumbs = true;
      options.enableAutoPerformanceTracing = true;
      
      // Add app version for release tracking
      options.release = 'my-app@1.0.0+1';
    },
    appRunner: () => runApp(const MyApp()),
  );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'My App',
      // Wrap with Sentry navigator observer for breadcrumbs
      navigatorObservers: [
        SentryNavigatorObserver(),
      ],
      home: const HomeScreen(),
    );
  }
}
```
