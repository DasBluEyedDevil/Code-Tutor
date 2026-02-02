---
type: "EXAMPLE"
title: "A Complete MVVM Example: Counter App"
---

Let us build a simple counter app using proper MVVM architecture with Riverpod. This example is complete and runnable. Study each layer carefully.

Notice how each file has a single responsibility. The Model knows nothing about the ViewModel. The ViewModel knows nothing about specific widgets. The View just displays what it is told.

```dart
// ============================================
// FILE: lib/models/counter_state.dart
// LAYER: MODEL - Pure data, no logic
// ============================================

class CounterState {
  final int count;
  final DateTime lastUpdated;

  // Constructor with default values
  const CounterState({
    this.count = 0,
    DateTime? lastUpdated,
  }) : lastUpdated = lastUpdated ?? const DateTime(2024);

  // Immutable update method - creates new instance
  CounterState copyWith({
    int? count,
    DateTime? lastUpdated,
  }) {
    return CounterState(
      count: count ?? this.count,
      lastUpdated: lastUpdated ?? this.lastUpdated,
    );
  }
}

// ============================================
// FILE: lib/viewmodels/counter_viewmodel.dart
// LAYER: VIEWMODEL - Business logic and state
// ============================================

import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/counter_state.dart';

// The Notifier holds and manages state
class CounterViewModel extends Notifier<CounterState> {
  @override
  CounterState build() {
    // Initial state when provider is first accessed
    return const CounterState();
  }

  // Business logic: increment with validation
  void increment() {
    // Rule: Cannot exceed 100
    if (state.count < 100) {
      state = state.copyWith(
        count: state.count + 1,
        lastUpdated: DateTime.now(),
      );
    }
  }

  // Business logic: decrement with validation
  void decrement() {
    // Rule: Cannot go below 0
    if (state.count > 0) {
      state = state.copyWith(
        count: state.count - 1,
        lastUpdated: DateTime.now(),
      );
    }
  }

  // Business logic: reset to zero
  void reset() {
    state = const CounterState();
  }
}

// Provider that exposes the ViewModel
final counterViewModelProvider =
    NotifierProvider<CounterViewModel, CounterState>(() {
  return CounterViewModel();
});

// ============================================
// FILE: lib/views/counter_screen.dart
// LAYER: VIEW - UI only, no business logic
// ============================================

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../viewmodels/counter_viewmodel.dart';

class CounterScreen extends ConsumerWidget {
  const CounterScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Watch state - rebuilds when state changes
    final counterState = ref.watch(counterViewModelProvider);

    return Scaffold(
      appBar: AppBar(
        title: const Text('MVVM Counter'),
        actions: [
          // Reset button
          IconButton(
            icon: const Icon(Icons.refresh),
            onPressed: () {
              // Call ViewModel method - NO logic here!
              ref.read(counterViewModelProvider.notifier).reset();
            },
          ),
        ],
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            // Display count from state
            Text(
              '${counterState.count}',
              style: const TextStyle(fontSize: 72, fontWeight: FontWeight.bold),
            ),
            const SizedBox(height: 16),
            // Display last updated time
            Text(
              'Last updated: ${_formatTime(counterState.lastUpdated)}',
              style: const TextStyle(color: Colors.grey),
            ),
            const SizedBox(height: 32),
            // Increment and Decrement buttons
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                FloatingActionButton(
                  heroTag: 'decrement',
                  onPressed: () {
                    ref.read(counterViewModelProvider.notifier).decrement();
                  },
                  child: const Icon(Icons.remove),
                ),
                const SizedBox(width: 24),
                FloatingActionButton(
                  heroTag: 'increment',
                  onPressed: () {
                    ref.read(counterViewModelProvider.notifier).increment();
                  },
                  child: const Icon(Icons.add),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }

  // Helper method for formatting - this is OK in View
  // because it is purely presentational
  String _formatTime(DateTime time) {
    return '${time.hour}:${time.minute.toString().padLeft(2, '0')}';
  }
}

// ============================================
// FILE: lib/main.dart
// App entry point
// ============================================

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'views/counter_screen.dart';

void main() {
  runApp(
    // ProviderScope is required for Riverpod
    const ProviderScope(
      child: MyApp(),
    ),
  );
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'MVVM Counter',
      theme: ThemeData(primarySwatch: Colors.blue),
      home: const CounterScreen(),
    );
  }
}
```
