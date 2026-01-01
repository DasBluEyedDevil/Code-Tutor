---
type: "EXAMPLE"
title: "Testing a Simple Notifier"
---


Test business logic without any Flutter dependencies:



```dart
// lib/features/counter/counter_notifier.dart
class CounterNotifier extends Notifier<int> {
  @override
  int build() => 0;

  void increment() => state++;
  void decrement() => state--;
  void reset() => state = 0;
}

// test/features/counter/counter_notifier_test.dart
import 'package:flutter_test/flutter_test.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

void main() {
  late ProviderContainer container;
  late CounterNotifier notifier;

  setUp(() {
    container = ProviderContainer();
    notifier = container.read(counterProvider.notifier);
  });

  tearDown(() => container.dispose());

  group('CounterNotifier', () {
    test('initial state is 0', () {
      expect(container.read(counterProvider), 0);
    });

    test('increment increases state by 1', () {
      notifier.increment();
      expect(container.read(counterProvider), 1);
    });

    test('decrement decreases state by 1', () {
      notifier.increment();
      notifier.decrement();
      expect(container.read(counterProvider), 0);
    });
  });
}
```
