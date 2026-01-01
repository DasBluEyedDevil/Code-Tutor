// Todo List Challenge
// Create a scrollable todo list using ListView.builder

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

class TodoList extends StatelessWidget {
  const TodoList({super.key});

  @override
  Widget build(BuildContext context) {
    // Sample todo data
    final todos = [
      'Learn Flutter basics',
      'Build a todo app',
      'Study ListView.builder',
      'Practice widget nesting',
      'Create a beautiful UI',
    ];

    // TODO: Use ListView.builder instead of Column
    // Remember: itemCount, itemBuilder
    return ListView.builder(
      itemCount: todos.length,
      itemBuilder: (context, index) {
        // TODO: Return a ListTile with:
        // - leading: checkbox icon
        // - title: todo text
        return const Placeholder(); // Replace with ListTile
      },
    );
  }
}