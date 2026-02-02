// Solution: Todo List with ListView.builder
// Shows a scrollable todo list with checkboxes and dividers

import 'package:flutter/material.dart';

void main() {
  runApp(const TodoApp());
}

class TodoApp extends StatelessWidget {
  const TodoApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('My Todo List')),
        body: const TodoList(),
      ),
    );
  }
}

class TodoList extends StatefulWidget {
  const TodoList({super.key});

  @override
  State<TodoList> createState() => _TodoListState();
}

class _TodoListState extends State<TodoList> {
  // List of todo items with completion status
  final List<Map<String, dynamic>> todos = [
    {'title': 'Learn Flutter basics', 'completed': true},
    {'title': 'Build a todo app', 'completed': false},
    {'title': 'Study ListView.builder', 'completed': true},
    {'title': 'Practice widget nesting', 'completed': false},
    {'title': 'Create a beautiful UI', 'completed': false},
    {'title': 'Deploy to app store', 'completed': false},
  ];

  @override
  Widget build(BuildContext context) {
    // ListView.builder efficiently builds items on demand
    return ListView.builder(
      itemCount: todos.length,
      itemBuilder: (context, index) {
        final todo = todos[index];
        return Column(
          children: [
            ListTile(
              // Checkbox icon based on completion
              leading: Icon(
                todo['completed']
                    ? Icons.check_box
                    : Icons.check_box_outline_blank,
                color: todo['completed'] ? Colors.green : Colors.grey,
              ),
              // Todo title with strikethrough if completed
              title: Text(
                todo['title'],
                style: TextStyle(
                  decoration: todo['completed']
                      ? TextDecoration.lineThrough
                      : TextDecoration.none,
                  color: todo['completed'] ? Colors.grey : Colors.black,
                ),
              ),
              // Tap to toggle completion
              onTap: () {
                setState(() {
                  todos[index]['completed'] = !todo['completed'];
                });
              },
            ),
            // Divider between items (bonus)
            if (index < todos.length - 1) const Divider(height: 1),
          ],
        );
      },
    );
  }
}

// Key concepts:
// - ListView.builder: Efficient for long lists
// - itemCount: Total number of items
// - itemBuilder: Function called for each visible item
// - StatefulWidget: Allows updating todo completion
// - Divider: Visual separator between items