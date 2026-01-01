---
type: "THEORY"
title: "Accessing the Notifier"
---

There is an important distinction between watching the **state** and accessing the **notifier** (to call methods). Understanding this is critical.

### Watching State vs Reading Notifier

```dart
// WATCH THE STATE (the value)
// Use when you need to DISPLAY data
// Widget rebuilds when state changes
final todos = ref.watch(todoProvider);
// todos is List<Todo> - the actual data

// READ THE NOTIFIER (to call methods)
// Use when you need to TRIGGER ACTIONS
// Does NOT cause rebuilds
ref.read(todoProvider.notifier).addTodo('Buy milk');
// .notifier gives you the TodoNotifier class
```

### The .notifier Accessor

The `.notifier` accessor is how you get the Notifier class itself (not just its state). You need this to call methods:

```dart
// This gets the STATE (List<Todo>)
final todos = ref.watch(todoProvider);

// This gets the NOTIFIER (TodoNotifier)
final notifier = ref.read(todoProvider.notifier);

// Now you can call methods
notifier.addTodo('Buy milk');
notifier.toggleComplete('123');
notifier.removeTodo('456');

// Or inline:
ref.read(todoProvider.notifier).addTodo('Buy milk');
```

### Golden Rules Reminder

| What You Need | Use | Where |
|---------------|-----|-------|
| Display data | `ref.watch(provider)` | In build() method |
| Call methods | `ref.read(provider.notifier).method()` | In callbacks/handlers |

### Common Pattern in Widgets

```dart
class MyWidget extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // WATCH for display (in build)
    final count = ref.watch(counterProvider);

    return Column(
      children: [
        Text('Count: $count'),
        ElevatedButton(
          // READ for actions (in callback)
          onPressed: () => ref.read(counterProvider.notifier).increment(),
          child: Text('Add'),
        ),
      ],
    );
  }
}
```