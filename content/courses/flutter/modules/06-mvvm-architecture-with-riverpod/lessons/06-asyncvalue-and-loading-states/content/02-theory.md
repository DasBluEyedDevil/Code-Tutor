---
type: "THEORY"
title: "AsyncValue to the Rescue"
---

Riverpod provides **AsyncValue<T>** to elegantly handle all three states. It is a sealed class that wraps your data type and tells you whether you are loading, have data, or encountered an error.

### The Three States

**AsyncValue** has three possible states:

```dart
// 1. LOADING - Data is being fetched
AsyncValue<User>.loading()

// 2. DATA - Success! Here's your data
AsyncValue<User>.data(user)

// 3. ERROR - Something went wrong
AsyncValue<User>.error(exception, stackTrace)
```

### How Riverpod Creates AsyncValue

When you use **FutureProvider** or **AsyncNotifier**, Riverpod automatically wraps your data in AsyncValue:

```dart
// FutureProvider returns AsyncValue automatically
final userProvider = FutureProvider<User>((ref) async {
  final response = await http.get(Uri.parse('https://api.example.com/user'));
  return User.fromJson(jsonDecode(response.body));
});

// When you watch it, you get AsyncValue<User>
final userAsync = ref.watch(userProvider);  // Type: AsyncValue<User>
```

### The Benefits

1. **Type-safe**: Compiler ensures you handle all states
2. **No boilerplate**: No manual isLoading, hasError variables
3. **Consistent pattern**: Same approach everywhere
4. **Easy to use**: The `when()` method handles everything
5. **Testable**: Logic is separate from UI