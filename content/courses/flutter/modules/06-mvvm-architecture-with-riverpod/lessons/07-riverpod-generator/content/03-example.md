---
type: "EXAMPLE"
title: "Before and After Code Generation"
---

Let us compare the same counter implementation written manually versus using the Riverpod generator. This comparison shows exactly how much boilerplate the generator eliminates.

### The Manual Way (What You Have Been Doing)

With manual providers, you write the Notifier class AND the provider declaration separately. You must ensure the type parameters match exactly.

### The Generated Way

With the generator, you write only the Notifier class with a special annotation. The generator creates the provider automatically.

Notice several key differences:

1. **Import changes**: You import riverpod_annotation instead of flutter_riverpod in the Notifier file

2. **Part directive**: You add `part 'filename.g.dart';` to include the generated code

3. **Class naming**: Your class extends `_$ClassName` (underscore prefix, dollar sign, your class name). This is a generated base class.

4. **No provider declaration**: You do NOT write the provider. The generator creates `counterProvider` automatically based on your class name.

5. **Naming convention**: The generator converts your class name to camelCase for the provider name:
   - `Counter` class -> `counterProvider`
   - `UserProfile` class -> `userProfileProvider`
   - `ShoppingCart` class -> `shoppingCartProvider`

```dart
// =====================================================
// BEFORE: MANUAL PROVIDER DECLARATION
// =====================================================

import 'package:flutter_riverpod/flutter_riverpod.dart';

// Step 1: Write the Notifier
class CounterNotifier extends Notifier<int> {
  @override
  int build() => 0;

  void increment() => state++;
  void decrement() => state--;
  void reset() => state = 0;
}

// Step 2: Manually create the provider (easy to make mistakes here!)
final counterNotifierProvider = NotifierProvider<CounterNotifier, int>(() {
  return CounterNotifier();
});

// =====================================================
// AFTER: WITH RIVERPOD GENERATOR
// =====================================================

// File: lib/providers/counter.dart

import 'package:riverpod_annotation/riverpod_annotation.dart';

part 'counter.g.dart';  // This line includes the generated file!

@riverpod  // This annotation tells the generator to create a provider
class Counter extends _$Counter {  // Extend the generated base class
  @override
  int build() => 0;

  void increment() => state++;
  void decrement() => state--;
  void reset() => state = 0;
}

// That's it! No manual provider declaration needed!
// The generator creates 'counterProvider' automatically.

// =====================================================
// WHAT THE GENERATOR CREATES (counter.g.dart)
// =====================================================

// You never edit this file - it's auto-generated!
// part of 'counter.dart';
//
// final counterProvider = NotifierProvider<Counter, int>.internal(
//   Counter.new,
//   name: 'counterProvider',
//   debugGetCreateSourceHash: ...,
//   dependencies: null,
//   allTransitiveDependencies: null,
// );
//
// abstract class _$Counter extends Notifier<int> { }

// =====================================================
// USAGE IS IDENTICAL
// =====================================================

// In your widget (same whether manual or generated):
class CounterScreen extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final count = ref.watch(counterProvider);

    return Column(
      children: [
        Text('Count: $count'),
        ElevatedButton(
          onPressed: () => ref.read(counterProvider.notifier).increment(),
          child: Text('Increment'),
        ),
      ],
    );
  }
}
```
