---
type: "KEY_POINT"
title: "ViewModel Best Practices"
---

Following these best practices will help you write clean, maintainable ViewModels.

### 1. Keep ViewModels Focused

Each ViewModel should handle ONE feature or concern. If your ViewModel is getting too large, split it:

**Bad:**
```dart
class AppNotifier extends Notifier<AppState> {
  // Handles auth, cart, settings, notifications...
  // This is too much!
}
```

**Good:**
```dart
class AuthNotifier extends Notifier<AuthState> { /* auth only */ }
class CartNotifier extends Notifier<CartState> { /* cart only */ }
class SettingsNotifier extends Notifier<Settings> { /* settings only */ }
```

### 2. Use Descriptive Method Names

Method names should clearly describe what they do. Future you (and your teammates) will thank you:

**Bad:**
```dart
void add(Todo t) { ... }     // Add what? Where?
void update(String s) { ... } // Update what with what?
void toggle() { ... }         // Toggle what?
```

**Good:**
```dart
void addTodo(Todo todo) { ... }
void updateTodoTitle(String id, String newTitle) { ... }
void toggleTodoCompletion(String id) { ... }
```

### 3. Keep UI Code OUT of ViewModel

The ViewModel should NEVER import Flutter widgets or know about the UI:

**Bad:**
```dart
class TodoNotifier extends Notifier<List<Todo>> {
  void addTodo(String title, BuildContext context) {
    // NO! ViewModel should not know about BuildContext
    ScaffoldMessenger.of(context).showSnackBar(...);
  }
}
```

**Good:**
```dart
class TodoNotifier extends Notifier<List<Todo>> {
  void addTodo(String title) {
    state = [...state, Todo(title: title)];
    // View will react to state change and can show feedback
  }
}

// In widget:
onPressed: () {
  ref.read(todoProvider.notifier).addTodo(title);
  ScaffoldMessenger.of(context).showSnackBar(...);
}
```

### 4. Test ViewModels Independently

Because ViewModels have no UI dependencies, they are easy to test:

```dart
test('addTodo adds a new todo to the list', () {
  final container = ProviderContainer();
  final notifier = container.read(todoProvider.notifier);

  notifier.addTodo('Test todo');

  final todos = container.read(todoProvider);
  expect(todos.length, 1);
  expect(todos.first.title, 'Test todo');
});
```

### 5. State Should Be Immutable

Never mutate state directly. Always create new objects:

**Bad:**
```dart
state.add(newTodo);  // Mutating existing list - BAD!
```

**Good:**
```dart
state = [...state, newTodo];  // New list - GOOD!
```