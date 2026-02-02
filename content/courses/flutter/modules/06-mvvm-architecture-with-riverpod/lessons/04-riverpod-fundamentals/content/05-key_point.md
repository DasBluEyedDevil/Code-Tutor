---
type: "KEY_POINT"
title: "Provider Types"
---

Riverpod offers different provider types for different needs. Here is when to use each:

### Provider - Read-Only Value
Use for values that never change or are computed from other providers.
```dart
final appNameProvider = Provider<String>((ref) => 'My App');
final doubleCountProvider = Provider<int>((ref) {
  final count = ref.watch(counterProvider);
  return count * 2;  // Computed value
});
```

### StateProvider - Simple Mutable Value
Use for simple values you need to change: booleans, numbers, strings.
```dart
final counterProvider = StateProvider<int>((ref) => 0);
final isDarkModeProvider = StateProvider<bool>((ref) => false);

// To update:
ref.read(counterProvider.notifier).state++;
ref.read(isDarkModeProvider.notifier).state = true;
```

### NotifierProvider - Complex State with Methods
Use when you need methods to modify state (the MVVM ViewModel pattern).
```dart
class CartNotifier extends Notifier<List<Item>> {
  @override
  List<Item> build() => [];  // Initial state
  
  void addItem(Item item) {
    state = [...state, item];
  }
  
  void removeItem(String id) {
    state = state.where((item) => item.id != id).toList();
  }
}

final cartProvider = NotifierProvider<CartNotifier, List<Item>>(() {
  return CartNotifier();
});
```

### FutureProvider - Async Data (API Calls)
Use for data fetched from an API or database.
```dart
final userProvider = FutureProvider<User>((ref) async {
  final response = await http.get('https://api.example.com/user');
  return User.fromJson(jsonDecode(response.body));
});

// In widget:
final userAsync = ref.watch(userProvider);
userAsync.when(
  data: (user) => Text(user.name),
  loading: () => CircularProgressIndicator(),
  error: (err, stack) => Text('Error: $err'),
);
```

### StreamProvider - Real-Time Data
Use for data that updates over time (chat messages, live prices).
```dart
final messagesProvider = StreamProvider<List<Message>>((ref) {
  return database.watchMessages();  // Returns a Stream
});
```

### Quick Reference Table

| Provider Type | Use When | Example |
|---------------|----------|----------|
| `Provider` | Read-only or computed values | App config, derived data |
| `StateProvider` | Simple values that change | Counter, toggle, filter |
| `NotifierProvider` | Complex state with methods | Cart, form, user profile |
| `FutureProvider` | One-time async fetch | API calls, database reads |
| `StreamProvider` | Real-time updates | Chat, live data, sockets |