---
type: "EXAMPLE"
title: "Your First Riverpod App"
---

Let us build a complete, working Riverpod app step by step. This example shows every piece you need to get started.

**What this app does:** Displays a greeting message using Riverpod. Simple, but it demonstrates all the core concepts.

```dart
// STEP 1: Import Riverpod
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

// STEP 2: Define a Provider
// This is a global constant - defined outside any class
// It holds a String value: 'Hello, Riverpod!'
final greetingProvider = Provider<String>((ref) {
  return 'Hello, Riverpod!';
});

// STEP 3: Wrap Your App in ProviderScope
void main() {
  runApp(
    ProviderScope(  // This MUST wrap your entire app
      child: MyApp(),
    ),
  );
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Riverpod Demo',
      home: GreetingScreen(),
    );
  }
}

// STEP 4: Use ConsumerWidget to Access Providers
// Notice: extends ConsumerWidget, NOT StatelessWidget
class GreetingScreen extends ConsumerWidget {
  @override
  // Notice: build method has TWO parameters: context AND ref
  Widget build(BuildContext context, WidgetRef ref) {
    // STEP 5: Read the Provider Using ref.watch()
    // ref.watch() reads the value AND rebuilds this widget when it changes
    final greeting = ref.watch(greetingProvider);

    return Scaffold(
      appBar: AppBar(title: Text('My First Riverpod App')),
      body: Center(
        child: Text(
          greeting,  // Display the value from the provider
          style: TextStyle(fontSize: 24),
        ),
      ),
    );
  }
}

// THAT'S IT! A complete Riverpod app in under 50 lines.
//
// Key pieces:
// 1. Provider defined globally (greetingProvider)
// 2. ProviderScope wrapping the app (in main)
// 3. ConsumerWidget to access providers (GreetingScreen)
// 4. ref.watch() to read provider value
```
