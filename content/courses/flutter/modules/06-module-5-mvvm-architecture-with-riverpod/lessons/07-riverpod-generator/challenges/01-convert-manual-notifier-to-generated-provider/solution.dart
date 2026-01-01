// File: lib/providers/todo_list.dart
// SOLUTION: Converted to use Riverpod code generation

import 'package:riverpod_annotation/riverpod_annotation.dart';

part 'todo_list.g.dart';  // Include the generated file

// Todo Model (unchanged)
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

// CONVERTED: Now uses @riverpod annotation!
@riverpod
class TodoList extends _$TodoList {  // Extends generated base class
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

// NO MANUAL PROVIDER DECLARATION NEEDED!
// The generator creates: todoListProvider
// Type: NotifierProvider<TodoList, List<Todo>>

// =====================================================
// WHAT THE GENERATOR CREATES (todo_list.g.dart)
// =====================================================
// This file is auto-generated. Do not edit.
//
// part of 'todo_list.dart';
//
// final todoListProvider =
//     NotifierProvider<TodoList, List<Todo>>.internal(
//   TodoList.new,
//   name: 'todoListProvider',
//   debugGetCreateSourceHash: ...,
//   dependencies: null,
//   allTransitiveDependencies: null,
// );
//
// abstract class _$TodoList extends Notifier<List<Todo>> { }

// =====================================================
// USAGE IN WIDGETS (identical to manual version!)
// =====================================================
/*
class TodoScreen extends ConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Watch the provider (note: it's now todoListProvider, not todoNotifierProvider)
    final todos = ref.watch(todoListProvider);

    return ListView.builder(
      itemCount: todos.length,
      itemBuilder: (context, index) {
        final todo = todos[index];
        return ListTile(
          title: Text(
            todo.title,
            style: TextStyle(
              decoration: todo.isCompleted
                  ? TextDecoration.lineThrough
                  : null,
            ),
          ),
          leading: Checkbox(
            value: todo.isCompleted,
            onChanged: (_) {
              ref.read(todoListProvider.notifier).toggleTodo(todo.id);
            },
          ),
          trailing: IconButton(
            icon: Icon(Icons.delete),
            onPressed: () {
              ref.read(todoListProvider.notifier).removeTodo(todo.id);
            },
          ),
        );
      },
    );
  }
}
*/

// KEY CHANGES SUMMARY:
// 1. Import: flutter_riverpod -> riverpod_annotation
// 2. Added: part 'todo_list.g.dart';
// 3. Added: @riverpod annotation
// 4. Changed: extends Notifier<List<Todo>> -> extends _$TodoList
// 5. Renamed class: TodoNotifier -> TodoList (cleaner name for generated provider)
// 6. Removed: Manual provider declaration
// 7. Provider name changed: todoNotifierProvider -> todoListProvider