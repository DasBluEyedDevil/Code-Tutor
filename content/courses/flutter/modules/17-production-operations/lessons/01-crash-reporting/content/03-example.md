---
type: "EXAMPLE"
title: "Firebase Crashlytics Setup"
---


**Step 1: Add Firebase to Your Project**

If you haven't already, set up Firebase using the FlutterFire CLI:

```bash
# Install FlutterFire CLI
dart pub global activate flutterfire_cli

# Configure Firebase (creates firebase_options.dart)
flutterfire configure
```

**Step 2: Add Dependencies**

```yaml
# pubspec.yaml
dependencies:
  firebase_core: ^3.9.0
  firebase_crashlytics: ^4.3.0
```

**Step 3: Initialize in Your App**



```dart
// lib/main.dart
import 'dart:async';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_crashlytics/firebase_crashlytics.dart';
import 'firebase_options.dart';

void main() async {
  // Ensure Flutter bindings are initialized
  WidgetsFlutterBinding.ensureInitialized();
  
  // Initialize Firebase
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
  );
  
  // Configure Crashlytics
  if (kReleaseMode) {
    // In release mode, report all Flutter errors to Crashlytics
    FlutterError.onError = FirebaseCrashlytics.instance.recordFlutterFatalError;
    
    // Catch errors that occur outside of Flutter's framework
    PlatformDispatcher.instance.onError = (error, stack) {
      FirebaseCrashlytics.instance.recordError(error, stack, fatal: true);
      return true;
    };
  }
  
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'My App',
      home: const HomeScreen(),
    );
  }
}
```
