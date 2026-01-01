---
type: "WARNING"
title: "Async Mistakes"
---

These are the most common mistakes when working with async state in Riverpod.

### Mistake 1: Not Showing Loading State

**The Problem:**
```dart
Widget build(BuildContext context, WidgetRef ref) {
  final userAsync = ref.watch(userProvider);
  
  // WRONG: Just grabbing value, ignoring loading/error
  final user = userAsync.value;
  return Text(user?.name ?? 'Unknown');
}
```

Users see nothing or stale data while loading. They do not know anything is happening.

**The Fix:**
```dart
return userAsync.when(
  loading: () => CircularProgressIndicator(),
  error: (e, _) => Text('Error: $e'),
  data: (user) => Text(user.name),
);
```

### Mistake 2: Ignoring Errors

**The Problem:**
```dart
// maybeWhen without error handling
return userAsync.maybeWhen(
  data: (user) => UserCard(user),
  orElse: () => CircularProgressIndicator(),
);
// Error shows as loading forever!
```

**The Fix:**
```dart
return userAsync.when(
  loading: () => CircularProgressIndicator(),
  error: (e, _) => ErrorWidget(e),  // Always handle errors!
  data: (user) => UserCard(user),
);
```

### Mistake 3: Calling Async Methods in build() Without Guard

**The Problem:**
```dart
class MyNotifier extends AsyncNotifier<Data> {
  Future<void> refresh() async {
    state = AsyncValue.loading();
    
    // WRONG: No error handling!
    final data = await fetchData();
    state = AsyncValue.data(data);
    // If fetchData throws, state is stuck on loading!
  }
}
```

**The Fix:**
```dart
Future<void> refresh() async {
  state = const AsyncValue.loading();
  
  // RIGHT: guard() handles errors automatically
  state = await AsyncValue.guard(() => fetchData());
}
```

### Mistake 4: Not Using AsyncValue.guard()

**The Problem:**
```dart
// Manual try-catch is verbose and error-prone
try {
  final result = await someAsyncOperation();
  state = AsyncValue.data(result);
} catch (e, stack) {
  state = AsyncValue.error(e, stack);
}
```

**The Fix:**
```dart
// guard() does the same thing in one line
state = await AsyncValue.guard(() => someAsyncOperation());
```

### Mistake 5: Forgetting to Set Loading State Before Async Operations

**The Problem:**
```dart
Future<void> updateData() async {
  // Forgot to set loading!
  final newData = await fetchNewData();
  state = AsyncValue.data(newData);
}
// User does not know update is in progress
```

**The Fix:**
```dart
Future<void> updateData() async {
  state = const AsyncValue.loading();  // Tell users something is happening
  state = await AsyncValue.guard(() => fetchNewData());
}
```