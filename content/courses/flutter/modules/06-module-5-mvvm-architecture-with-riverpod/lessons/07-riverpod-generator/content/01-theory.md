---
type: "THEORY"
title: "Why Code Generation?"
---

As you have been building providers manually, you may have noticed a pattern. Every time you create a Notifier, you write similar boilerplate code:

1. Create the Notifier class with proper type parameters
2. Implement the build() method
3. Create a separate provider declaration with matching types
4. Ensure the provider factory returns your Notifier instance

This repetitive process has several problems:

**Problem 1: Boilerplate is tedious**
Every Notifier requires the same structural code. For a large app with dozens of providers, this adds up to hundreds of lines of repetitive code that you must write, maintain, and debug.

**Problem 2: Type parameter errors**
The NotifierProvider requires you to specify both the Notifier type AND the state type. Getting these wrong causes confusing errors:

```dart
// Easy to make mistakes like this:
final counterProvider = NotifierProvider<CounterNotifier, String>(() {  // Wrong! Should be int
  return CounterNotifier();
});
```

**Problem 3: Inconsistency**
Without a standard pattern, different developers write providers differently. Some use different naming conventions, some forget parts of the boilerplate.

**The Solution: Code Generation**

Riverpod offers a code generation package called **riverpod_generator** that writes all this boilerplate for you. You write a simple annotated class, run a command, and the generator creates perfectly typed providers automatically.

Code generation is a common pattern in Dart. You may have used it with json_serializable for JSON parsing or freezed for immutable classes. Riverpod's generator follows the same approach: you write the essential code, and the generator fills in the rest.

```dart
// THE BOILERPLATE PROBLEM
// Look at all this code for a simple counter:

// Step 1: Create the Notifier class
class CounterNotifier extends Notifier<int> {
  @override
  int build() => 0;

  void increment() => state++;
  void decrement() => state--;
}

// Step 2: Create the provider with matching types
final counterNotifierProvider = NotifierProvider<CounterNotifier, int>(() {
  return CounterNotifier();
});

// For EVERY Notifier, you repeat this pattern.
// With 20 features, that's 20 provider declarations with type parameters.
// Miss one type? Runtime error.
// Typo in the provider name? Hard to find bug.
```
