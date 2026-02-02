---
type: "WARNING"
title: "Common Riverpod Mistakes"
---

These are the mistakes that trip up almost every Riverpod beginner. Learn to recognize and avoid them:

### Mistake 1: Forgetting ProviderScope

**The Error:**
```
Bad state: No ProviderScope found
```

**The Problem:**
```dart
void main() {
  runApp(MyApp());  // Missing ProviderScope!
}
```

**The Fix:**
```dart
void main() {
  runApp(
    ProviderScope(  // Always wrap your app!
      child: MyApp(),
    ),
  );
}
```

### Mistake 2: Using ref.read() in build Method

**The Problem:**
```dart
Widget build(BuildContext context, WidgetRef ref) {
  final count = ref.read(counterProvider);  // WRONG!
  return Text('$count');  // This will NEVER update!
}
```

**The Fix:**
```dart
Widget build(BuildContext context, WidgetRef ref) {
  final count = ref.watch(counterProvider);  // Correct!
  return Text('$count');  // Now updates when count changes
}
```

### Mistake 3: Creating Providers Inside Widgets

**The Problem:**
```dart
class MyWidget extends ConsumerWidget {
  // WRONG: Provider created inside widget class
  final myProvider = StateProvider<int>((ref) => 0);
  
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Text('${ref.watch(myProvider)}');
  }
}
```
This creates a NEW provider every time the widget rebuilds!

**The Fix:**
```dart
// CORRECT: Provider defined at top level (outside any class)
final myProvider = StateProvider<int>((ref) => 0);

class MyWidget extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return Text('${ref.watch(myProvider)}');
  }
}
```

### Mistake 4: Using StatelessWidget Instead of ConsumerWidget

**The Problem:**
```dart
class MyScreen extends StatelessWidget {  // Wrong widget type!
  @override
  Widget build(BuildContext context) {  // No WidgetRef!
    // How do I access providers?
  }
}
```

**The Fix:**
```dart
class MyScreen extends ConsumerWidget {  // Correct!
  @override
  Widget build(BuildContext context, WidgetRef ref) {  // Now has ref
    final value = ref.watch(myProvider);  // Can access providers!
    return Text(value);
  }
}
```

### Mistake 5: Not Disposing Resources

If your provider creates streams, timers, or controllers, clean them up:
```dart
final timerProvider = Provider<Timer>((ref) {
  final timer = Timer.periodic(Duration(seconds: 1), (_) {});
  
  // Clean up when provider is disposed
  ref.onDispose(() {
    timer.cancel();
  });
  
  return timer;
});
```