// File: lib/providers/todo_list.dart
// STARTER: Manual Notifier - Convert this to use code generation!

import 'package:flutter_riverpod/flutter_riverpod.dart';

// Todo Model
class Todo {
  final String id;
  final String title;
  final bool isCompleted;

  Todo({
    required this.id,
    required this.title,
    this.isCompleted = false,
  });

  Todo copyWith({String? title, bool? isCompleted}) {
    return Todo(
      id: id,
      title: title ?? this.title,
      isCompleted: isCompleted ?? this.isCompleted,
    );
  }
}

// MANUAL NOTIFIER - Convert this to use @riverpod!
class TodoNotifier extends Notifier<List<Todo>> {
  @override
  List<Todo> build() {
    // Start with some sample todos
    return [
      Todo(id: '1', title: 'Learn Riverpod basics'),
      Todo(id: '2', title: 'Understand code generation'),
      Todo(id: '3', title: 'Build an awesome app'),
    ];
  }

  void addTodo(String title) {
    final newTodo = Todo(
      id: DateTime.now().millisecondsSinceEpoch.toString(),
      title: title,
    );
    state = [...state, newTodo];
  }

  void removeTodo(String id) {
    state = state.where((todo) => todo.id != id).toList();
  }

  void toggleTodo(String id) {
    state = state.map((todo) {
      if (todo.id == id) {
        return todo.copyWith(isCompleted: !todo.isCompleted);
      }
      return todo;
    }).toList();
  }

  void clearCompleted() {
    state = state.where((todo) => !todo.isCompleted).toList();
  }
}

// MANUAL PROVIDER DECLARATION - This should be removed!
// The generator will create this automatically.
final todoNotifierProvider = NotifierProvider<TodoNotifier, List<Todo>>(() {
  return TodoNotifier();
});

// TODO: Convert the above to use code generation:
// 1. Change import to riverpod_annotation
// 2. Add: part 'todo_list.g.dart';
// 3. Add @riverpod annotation to the class
// 4. Extend _$TodoList instead of Notifier<List<Todo>>
// 5. Delete the manual provider declaration
// 6. The generated provider will be named: todoListProvider