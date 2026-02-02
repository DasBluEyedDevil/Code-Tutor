---
type: "WARNING"
title: "Common Notifier Mistakes"
---

These mistakes trip up almost everyone when starting with Notifier. Learn to recognize and avoid them.

### Mistake 1: Calling Methods in build() Without User Action

**The Problem:**
```dart
class MyWidget extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // WRONG! This calls loadData on EVERY rebuild!
    ref.read(dataProvider.notifier).loadData();
    
    return Text('...');
  }
}
```

This creates an infinite loop: load data -> state changes -> widget rebuilds -> load data again...

**The Fix:**
Load data in the Notifier's build() method, or use a ref.listen:
```dart
class DataNotifier extends Notifier<Data> {
  @override
  Data build() {
    // Load data when provider is first created
    _loadData();
    return Data.empty();
  }
  
  Future<void> _loadData() async { ... }
}
```

### Mistake 2: Modifying State Directly (Bypassing Notifier)

**The Problem:**
```dart
// Getting the list and trying to modify it directly
final todos = ref.read(todoProvider);
todos.add(newTodo);  // This does NOTHING!
```

This does not update state because you are just modifying a local copy. Widgets will not rebuild.

**The Fix:**
Always go through the Notifier:
```dart
ref.read(todoProvider.notifier).addTodo(newTodo);
```

### Mistake 3: Making the Notifier Too Large

**The Problem:**
```dart
class EverythingNotifier extends Notifier<AppState> {
  void login() { ... }
  void logout() { ... }
  void addToCart() { ... }
  void removeFromCart() { ... }
  void updateProfile() { ... }
  void changeTheme() { ... }
  void sendMessage() { ... }
  // 50 more methods...
}
```

This becomes unmaintainable and hard to test.

**The Fix:**
Split into focused Notifiers:
```dart
class AuthNotifier extends Notifier<AuthState> { ... }
class CartNotifier extends Notifier<CartState> { ... }
class ProfileNotifier extends Notifier<ProfileState> { ... }
class ThemeNotifier extends Notifier<ThemeData> { ... }
```

### Mistake 4: Forgetting That State Must Be Replaced

**The Problem:**
```dart
void updateUser(String name) {
  state.name = name;  // This does NOT work!
  // State object was not replaced, so no notification happens
}
```

**The Fix:**
Create a new state object:
```dart
void updateUser(String name) {
  state = state.copyWith(name: name);  // New object, notifications sent
}
```