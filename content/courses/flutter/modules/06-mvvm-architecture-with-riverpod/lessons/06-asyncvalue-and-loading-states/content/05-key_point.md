---
type: "KEY_POINT"
title: "AsyncValue.when() Pattern"
---

The `when()` method is the primary way to handle AsyncValue. It forces you to handle all three states, making your code robust.

### The when() Method

```dart
final asyncValue = ref.watch(someAsyncProvider);

asyncValue.when(
  loading: () => CircularProgressIndicator(),  // Required
  error: (error, stackTrace) => Text('Error: $error'),  // Required
  data: (value) => Text('Data: $value'),  // Required
);
```

You **must** provide all three callbacks. This ensures you never forget to handle loading or error states.

### Alternative Methods

Riverpod provides other methods for special cases:

**whenOrNull()** - Returns null for unhandled cases:
```dart
// Only handle data, return null for loading/error
final widget = asyncValue.whenOrNull(
  data: (user) => Text(user.name),
);
// widget is null while loading or on error
```

**maybeWhen()** - Provide a default fallback:
```dart
asyncValue.maybeWhen(
  data: (user) => UserCard(user),
  // 'orElse' is called for loading AND error
  orElse: () => CircularProgressIndicator(),
);
```

### Useful Properties

AsyncValue also has helpful properties:

```dart
// Check current state
asyncValue.isLoading   // true if loading
asyncValue.hasValue    // true if has data (even if also loading/error)
asyncValue.hasError    // true if has error

// Get value (nullable)
asyncValue.valueOrNull  // Returns T? (null if no data)
asyncValue.value        // Returns T (throws if no data!)

// Get error (nullable)
asyncValue.error        // Returns Object? (null if no error)
```

### Pattern: Show Previous Data While Loading

A common UX pattern is to show stale data while refreshing:

```dart
Widget build(BuildContext context, WidgetRef ref) {
  final userAsync = ref.watch(userProvider);
  
  return Stack(
    children: [
      // Show data if available (even during refresh)
      if (userAsync.hasValue)
        UserCard(userAsync.value!),
      
      // Show loading indicator on top
      if (userAsync.isLoading)
        const Center(child: CircularProgressIndicator()),
      
      // Show error message
      if (userAsync.hasError)
        ErrorBanner(userAsync.error.toString()),
    ],
  );
}
```