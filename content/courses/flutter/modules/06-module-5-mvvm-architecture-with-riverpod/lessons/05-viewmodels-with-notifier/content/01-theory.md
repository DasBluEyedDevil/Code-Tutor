---
type: "THEORY"
title: "What is a Notifier?"
---

In the previous lesson, you learned about StateProvider for simple values like counters and toggles. StateProvider is great for holding a single value, but what happens when you need more than just a value? What if you need **methods** to modify that value in specific ways?

This is where **Notifier** comes in.

### StateProvider Limitations

StateProvider is like a simple box that holds a value. You can read it and replace it, but that is all:

```dart
// StateProvider - just holds a value
final counterProvider = StateProvider<int>((ref) => 0);

// To update, you directly modify state
ref.read(counterProvider.notifier).state++;
ref.read(counterProvider.notifier).state = 0;  // Reset
```

This works, but notice the problems:
- Logic is scattered in widgets (where do you put validation?)
- No encapsulation (anyone can set any value)
- Hard to test (logic is in UI code)

### Notifier = State + Methods

A **Notifier** is a class that holds state AND provides methods to modify it. Think of it as a smart box that knows HOW its contents should change.

```dart
// Notifier - holds state AND has methods
class CounterNotifier extends Notifier<int> {
  @override
  int build() => 0;  // Initial state

  void increment() => state = state + 1;
  void decrement() => state = state - 1;
  void reset() => state = 0;
}
```

Now all your logic is in one place, encapsulated, and testable.

### Notifier IS Your ViewModel

Remember MVVM from earlier lessons? The **Notifier** is your **ViewModel**! It sits between your Model (data) and your View (widgets), managing state and business logic.

```
+------------------+
|      VIEW        |  <- Flutter Widgets
+--------+---------+
         |
         v
+--------+---------+
|     NOTIFIER     |  <- Your ViewModel
|  (state + logic) |
+--------+---------+
         |
         v
+--------+---------+
|      MODEL       |  <- Data classes
+------------------+
```

The pattern is: **Notifier + NotifierProvider**. The Notifier class holds your logic, and the NotifierProvider makes it available throughout your app.