---
type: "EXAMPLE"
title: "Firebase Performance Setup"
---


**Step 1: Add Dependencies**

```yaml
# pubspec.yaml
dependencies:
  firebase_core: ^3.9.0
  firebase_performance: ^0.10.0
```

**Step 2: Initialize Firebase Performance**



```dart
// lib/main.dart
import 'package:flutter/material.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:firebase_performance/firebase_performance.dart';
import 'firebase_options.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
  );
  
  // Enable performance collection (enabled by default)
  // You might disable in debug mode
  final performance = FirebasePerformance.instance;
  await performance.setPerformanceCollectionEnabled(true);
  
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});
  
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Performance Demo',
      home: const HomeScreen(),
    );
  }
}
```
