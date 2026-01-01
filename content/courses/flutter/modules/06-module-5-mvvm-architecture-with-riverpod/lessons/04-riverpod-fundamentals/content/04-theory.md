---
type: "THEORY"
title: "watch vs read: The Critical Difference"
---

This is where most beginners make mistakes. Understanding when to use `ref.watch()` versus `ref.read()` is essential.

### ref.watch() - Rebuilds Widget on Change

Use `ref.watch()` when you want your widget to **update automatically** when the value changes.

**Where to use it:** Inside the `build()` method.

```dart
Widget build(BuildContext context, WidgetRef ref) {
  // GOOD: watch in build method
  // Widget rebuilds automatically when count changes
  final count = ref.watch(counterProvider);
  return Text('$count');
}
```

### ref.read() - Gets Value Once, No Rebuilding

Use `ref.read()` when you want to **get the value once** without subscribing to changes. Perfect for event handlers.

**Where to use it:** Inside callbacks, onPressed handlers, or anywhere outside build.

```dart
ElevatedButton(
  onPressed: () {
    // GOOD: read in callback
    // Gets the notifier to call a method - does not subscribe to changes
    ref.read(counterProvider.notifier).increment();
  },
  child: Text('Add'),
)
```

### The Golden Rules

**Rule 1:** Use `ref.watch()` in the build method to display reactive data.

**Rule 2:** Use `ref.read()` in callbacks/handlers to trigger actions.

**Rule 3:** NEVER use `ref.read()` in the build method to display data - your UI will not update!

### Examples: Right vs Wrong

```dart
class CounterScreen extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    
    // ===== DISPLAYING DATA =====
    
    // GOOD: watch in build - UI updates when count changes
    final count = ref.watch(counterProvider);
    
    // BAD: read in build - UI will NEVER update!
    // final count = ref.read(counterProvider);  // DON'T DO THIS!
    
    return Column(
      children: [
        Text('Count: $count'),  // This displays the watched value
        
        // ===== HANDLING ACTIONS =====
        
        ElevatedButton(
          onPressed: () {
            // GOOD: read in callback - fires once per tap
            ref.read(counterProvider.notifier).increment();
          },
          child: Text('Increment'),
        ),
        
        ElevatedButton(
          onPressed: () {
            // BAD: watch in callback - unnecessary subscription
            // ref.watch(counterProvider.notifier).decrement();  // DON'T DO THIS!
            
            // GOOD: read is correct here
            ref.read(counterProvider.notifier).decrement();
          },
          child: Text('Decrement'),
        ),
      ],
    );
  }
}

// Summary:
// ref.watch() = "Keep me updated when this changes" (use in build)
// ref.read()  = "Give me the value right now, just once" (use in callbacks)
```
