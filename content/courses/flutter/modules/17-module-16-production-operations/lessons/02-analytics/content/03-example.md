---
type: "EXAMPLE"
title: "Firebase Analytics Setup"
---


**Step 1: Add Dependencies**

If you haven't set up Firebase, use FlutterFire CLI first:

```bash
# Install and configure Firebase
dart pub global activate flutterfire_cli
flutterfire configure
```

Then add the analytics package:

```yaml
# pubspec.yaml
dependencies:
  firebase_core: ^3.9.0
  firebase_analytics: ^11.4.0
```

**Step 2: Initialize Firebase Analytics**



```dart
// lib/main.dart
import 'package:flutter/material.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_analytics/firebase_analytics.dart';
import 'firebase_options.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
  );
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  
  // Create analytics instance
  static FirebaseAnalytics analytics = FirebaseAnalytics.instance;
  static FirebaseAnalyticsObserver observer = 
      FirebaseAnalyticsObserver(analytics: analytics);
  
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Analytics Demo',
      // Add observer for automatic screen tracking
      navigatorObservers: [observer],
      home: const HomeScreen(),
    );
  }
}
```
