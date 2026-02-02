// SOLUTION: Proper MVVM Architecture

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

void main() {
  runApp(const ProviderScope(child: MyApp()));
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'MVVM Counter',
      home: const CounterScreen(),
    );
  }
}

// =====================================
// MODEL: Pure data class
// =====================================
class CounterState {
  final int count;
  final DateTime? lastModified;

  const CounterState({
    this.count = 0,
    this.lastModified,
  });

  // Immutable update
  CounterState copyWith({
    int? count,
    DateTime? lastModified,
  }) {
    return CounterState(
      count: count ?? this.count,
      lastModified: lastModified ?? this.lastModified,
    );
  }
}

// =====================================
// VIEWMODEL: Business logic and state
// =====================================
class CounterViewModel extends Notifier<CounterState> {
  @override
  CounterState build() {
    // Initial state
    return const CounterState();
  }

  void increment() {
    // Business rule: max 50
    if (state.count < 50) {
      state = state.copyWith(
        count: state.count + 1,
        lastModified: DateTime.now(),
      );
    }
  }

  void decrement() {
    // Business rule: min 0
    if (state.count > 0) {
      state = state.copyWith(
        count: state.count - 1,
        lastModified: DateTime.now(),
      );
    }
  }

  void reset() {
    state = const CounterState();
  }
}

// Provider for the ViewModel
final counterViewModelProvider =
    NotifierProvider<CounterViewModel, CounterState>(() {
  return CounterViewModel();
});

// =====================================
// VIEW: UI only, no logic
// =====================================
class CounterScreen extends ConsumerWidget {
  const CounterScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Watch state - rebuilds automatically when state changes
    final state = ref.watch(counterViewModelProvider);

    return Scaffold(
      appBar: AppBar(
        title: const Text('MVVM Counter'),
        actions: [
          IconButton(
            icon: const Icon(Icons.refresh),
            onPressed: () {
              // Call ViewModel - no logic here
              ref.read(counterViewModelProvider.notifier).reset();
            },
          ),
        ],
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            // Display state
            Text(
              '${state.count}',
              style: const TextStyle(fontSize: 48, fontWeight: FontWeight.bold),
            ),
            // Conditional display based on state
            if (state.lastModified != null)
              Text(
                'Modified: ${state.lastModified!.hour}:${state.lastModified!.minute.toString().padLeft(2, '0')}',
                style: const TextStyle(color: Colors.grey),
              ),
            const SizedBox(height: 20),
            // Buttons that call ViewModel methods
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                ElevatedButton(
                  onPressed: () {
                    ref.read(counterViewModelProvider.notifier).decrement();
                  },
                  child: const Text('-'),
                ),
                const SizedBox(width: 20),
                ElevatedButton(
                  onPressed: () {
                    ref.read(counterViewModelProvider.notifier).increment();
                  },
                  child: const Text('+'),
                ),
              ],
            ),
            const SizedBox(height: 20),
            // Display bounds info
            Text(
              'Range: 0 - 50',
              style: TextStyle(color: Colors.grey[600]),
            ),
          ],
        ),
      ),
    );
  }
}

// KEY IMPROVEMENTS:
// 
// 1. MODEL is pure Dart - can be tested without Flutter
// 2. VIEWMODEL has all logic - can be unit tested easily
// 3. VIEW only displays - no business rules in widgets
// 4. State is immutable - copyWith creates new instances
// 5. State persists - survives navigation (unlike StatefulWidget)
// 6. Reusable - ViewModel can be used by multiple Views