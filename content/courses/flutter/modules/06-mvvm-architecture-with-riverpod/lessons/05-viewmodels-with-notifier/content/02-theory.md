---
type: "THEORY"
title: "Anatomy of a Notifier"
---

Let us break down every part of a Notifier class. Understanding each piece is crucial for building proper ViewModels.

### The Complete Structure

Here is a complete Notifier with annotations explaining each part:

```dart
import 'package:flutter_riverpod/flutter_riverpod.dart';

// Step 1: Create your Notifier class
// - Extends Notifier<T> where T is the STATE TYPE
// - This class holds your state and all methods to modify it
class CounterNotifier extends Notifier<int> {
  
  // Step 2: Override the build() method
  // - Called when provider is FIRST ACCESSED
  // - Returns the INITIAL STATE
  // - Think of it as your constructor
  @override
  int build() {
    // You can access other providers here using ref
    // final someValue = ref.watch(otherProvider);
    
    // Return initial state
    return 0;
  }

  // Step 3: Create methods to modify state
  // - Use 'state' property to read current value
  // - Assign to 'state' to update (triggers rebuilds)
  
  void increment() {
    // 'state' is the current value (int in this case)
    // Assigning to 'state' updates it and notifies listeners
    state = state + 1;
  }

  void decrement() {
    state = state - 1;
  }

  void reset() {
    state = 0;
  }
  
  // You can add validation and complex logic
  void setValue(int value) {
    // Business logic: only allow positive values
    if (value >= 0) {
      state = value;
    }
  }
  
  // You can have getters for derived values
  bool get isZero => state == 0;
  bool get isPositive => state > 0;
}

// Step 4: Create the NotifierProvider
// - NotifierProvider<NotifierClass, StateType>
// - The () => NotifierClass() creates a new instance
final counterProvider = NotifierProvider<CounterNotifier, int>(() {
  return CounterNotifier();
});
```
