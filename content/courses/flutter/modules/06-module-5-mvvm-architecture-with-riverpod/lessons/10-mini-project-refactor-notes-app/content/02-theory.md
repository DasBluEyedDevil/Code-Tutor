---
type: "THEORY"
title: "The Messy Starting Code"
---

Here is our starting point - a Notes app with everything mixed together. Look at all the problems:

### Problems With This Code

1. **State mixed with UI**: `_notes` list lives in the widget
2. **Business logic in widget**: `_addNote`, `_updateNote`, `_deleteNote` are in the UI class
3. **No data model**: Notes are just `Map<String, String>` - no type safety
4. **Cannot share state**: Other screens cannot access notes
5. **Cannot test logic**: Would need widget tests for everything
6. **ID generation in UI**: `DateTime.now()` logic should not be here

```dart
// THE MESSY VERSION - Everything in one StatefulWidget

import 'package:flutter/material.dart';

void main() => runApp(const MaterialApp(home: MessyNotesApp()));

class MessyNotesApp extends StatefulWidget {
  const MessyNotesApp({super.key});

  @override
  State<MessyNotesApp> createState() => _MessyNotesAppState();
}

class _MessyNotesAppState extends State<MessyNotesApp> {
  // Problem: State stored directly in widget
  final List<Map<String, String>> _notes = [];

  // Problem: Business logic mixed with UI
  void _addNote(String title, String content) {
    setState(() {
      _notes.add({
        'id': DateTime.now().millisecondsSinceEpoch.toString(),
        'title': title,
        'content': content,
        'createdAt': DateTime.now().toIso8601String(),
      });
    });
  }

  void _updateNote(String id, String title, String content) {
    setState(() {
      final index = _notes.indexWhere((n) => n['id'] == id);
      if (index != -1) {
        _notes[index] = {
          ..._notes[index],
          'title': title,
          'content': content,
        };
      }
    });
  }

  void _deleteNote(String id) {
    setState(() {
      _notes.removeWhere((n) => n['id'] == id);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('My Notes')),
      body: _notes.isEmpty
          ? const Center(child: Text('No notes yet'))
          : ListView.builder(
              itemCount: _notes.length,
              itemBuilder: (context, index) {
                final note = _notes[index];
                return ListTile(
                  title: Text(note['title'] ?? ''),
                  subtitle: Text(note['content'] ?? ''),
                  trailing: IconButton(
                    icon: const Icon(Icons.delete),
                    onPressed: () => _deleteNote(note['id']!),
                  ),
                );
              },
            ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _showAddDialog(context),
        child: const Icon(Icons.add),
      ),
    );
  }

  void _showAddDialog(BuildContext context) {
    final titleController = TextEditingController();
    final contentController = TextEditingController();
    showDialog(
      context: context,
      builder: (ctx) => AlertDialog(
        title: const Text('New Note'),
        content: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextField(controller: titleController, decoration: const InputDecoration(labelText: 'Title')),
            TextField(controller: contentController, decoration: const InputDecoration(labelText: 'Content')),
          ],
        ),
        actions: [
          TextButton(onPressed: () => Navigator.pop(ctx), child: const Text('Cancel')),
          TextButton(
            onPressed: () {
              _addNote(titleController.text, contentController.text);
              Navigator.pop(ctx);
            },
            child: const Text('Save'),
          ),
        ],
      ),
    );
  }
}
```
