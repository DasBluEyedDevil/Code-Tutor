---
type: "EXAMPLE"
title: "Complete Todo ViewModel"
---

Let us build a real-world example: a Todo list with full CRUD operations (Create, Read, Update, Delete). This example shows how Notifier handles complex state.

### The Model

First, we define our data structure (the Model layer in MVVM):

```dart
// =====================================
// MODEL: Todo data class
// =====================================
class Todo {
  final String id;
  final String title;
  final bool isCompleted;
  final DateTime createdAt;

  const Todo({
    required this.id,
    required this.title,
    this.isCompleted = false,
    required this.createdAt,
  });

  // Immutable update - creates new instance with changes
  Todo copyWith({
    String? id,
    String? title,
    bool? isCompleted,
    DateTime? createdAt,
  }) {
    return Todo(
      id: id ?? this.id,
      title: title ?? this.title,
      isCompleted: isCompleted ?? this.isCompleted,
      createdAt: createdAt ?? this.createdAt,
    );
  }
}

// =====================================
// VIEWMODEL: TodoNotifier
// =====================================
import 'package:flutter_riverpod/flutter_riverpod.dart';

class TodoNotifier extends Notifier<List<Todo>> {
  @override
  List<Todo> build() {
    // Initial state: empty list
    // In a real app, you might load from a database here
    return [];
  }

  // CREATE: Add a new todo
  void addTodo(String title) {
    // Validate: do not add empty todos
    if (title.trim().isEmpty) return;

    final newTodo = Todo(
      id: DateTime.now().millisecondsSinceEpoch.toString(),
      title: title.trim(),
      createdAt: DateTime.now(),
    );

    // Create NEW list with the new todo
    // Never mutate the existing list!
    state = [...state, newTodo];
  }

  // DELETE: Remove a todo by ID
  void removeTodo(String id) {
    // Filter out the todo with matching ID
    state = state.where((todo) => todo.id != id).toList();
  }

  // UPDATE: Toggle completion status
  void toggleComplete(String id) {
    state = state.map((todo) {
      if (todo.id == id) {
        // Return new Todo with flipped isCompleted
        return todo.copyWith(isCompleted: !todo.isCompleted);
      }
      return todo;  // Return unchanged
    }).toList();
  }

  // UPDATE: Edit todo title
  void editTodo(String id, String newTitle) {
    if (newTitle.trim().isEmpty) return;

    state = state.map((todo) {
      if (todo.id == id) {
        return todo.copyWith(title: newTitle.trim());
      }
      return todo;
    }).toList();
  }

  // BULK: Mark all as complete
  void completeAll() {
    state = state.map((todo) => todo.copyWith(isCompleted: true)).toList();
  }

  // BULK: Clear all completed
  void clearCompleted() {
    state = state.where((todo) => !todo.isCompleted).toList();
  }

  // GETTERS: Computed values (not state, just derived)
  int get totalCount => state.length;
  int get completedCount => state.where((t) => t.isCompleted).length;
  int get pendingCount => state.where((t) => !t.isCompleted).length;
}

// Create the provider
final todoProvider = NotifierProvider<TodoNotifier, List<Todo>>(() {
  return TodoNotifier();
});

// =====================================
// VIEW: Using the ViewModel in a Widget
// =====================================
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class TodoScreen extends ConsumerWidget {
  const TodoScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // WATCH the state - rebuilds when todos change
    final todos = ref.watch(todoProvider);
    
    // You can also watch the notifier for computed values
    final notifier = ref.watch(todoProvider.notifier);

    return Scaffold(
      appBar: AppBar(
        title: Text('Todos (${notifier.pendingCount} pending)'),
        actions: [
          IconButton(
            icon: const Icon(Icons.delete_sweep),
            onPressed: () {
              // READ the notifier to call methods
              ref.read(todoProvider.notifier).clearCompleted();
            },
          ),
        ],
      ),
      body: ListView.builder(
        itemCount: todos.length,
        itemBuilder: (context, index) {
          final todo = todos[index];
          return ListTile(
            leading: Checkbox(
              value: todo.isCompleted,
              onChanged: (_) {
                ref.read(todoProvider.notifier).toggleComplete(todo.id);
              },
            ),
            title: Text(
              todo.title,
              style: TextStyle(
                decoration: todo.isCompleted 
                    ? TextDecoration.lineThrough 
                    : null,
              ),
            ),
            trailing: IconButton(
              icon: const Icon(Icons.delete),
              onPressed: () {
                ref.read(todoProvider.notifier).removeTodo(todo.id);
              },
            ),
          );
        },
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _showAddDialog(context, ref),
        child: const Icon(Icons.add),
      ),
    );
  }

  void _showAddDialog(BuildContext context, WidgetRef ref) {
    final controller = TextEditingController();
    showDialog(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Add Todo'),
        content: TextField(
          controller: controller,
          autofocus: true,
          decoration: const InputDecoration(hintText: 'Enter todo title'),
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
          ElevatedButton(
            onPressed: () {
              ref.read(todoProvider.notifier).addTodo(controller.text);
              Navigator.pop(context);
            },
            child: const Text('Add'),
          ),
        ],
      ),
    );
  }
}
```
