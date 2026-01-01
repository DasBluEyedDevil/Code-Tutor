// STARTER CODE: The Messy Notes App
// Your task: Refactor this into clean MVVM architecture

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:riverpod_annotation/riverpod_annotation.dart';

// TODO: Add 'part' directive for generated code
// part 'main.g.dart';

void main() {
  runApp(const ProviderScope(child: MaterialApp(home: NotesScreen())));
}

// ============================================
// TODO 1: Create the Note Model
// ============================================
// class Note {
//   final String id;
//   final String title;
//   final String content;
//   final DateTime createdAt;
//
//   const Note({...});
//
//   factory Note.create({required String title, required String content}) {...}
//
//   Note copyWith({...}) {...}
// }

// ============================================
// TODO 2: Create the NotesViewModel
// ============================================
// @riverpod
// class NotesViewModel extends _$NotesViewModel {
//   @override
//   List<Note> build() => [];
//
//   void addNote({required String title, required String content}) {...}
//   void updateNote({required String id, required String title, required String content}) {...}
//   void deleteNote(String id) {...}
// }

// ============================================
// TODO 3: Refactor this Messy Widget into a Clean ConsumerWidget
// ============================================

// THIS IS THE MESSY CODE - REFACTOR IT!
class NotesScreen extends StatefulWidget {
  const NotesScreen({super.key});

  @override
  State<NotesScreen> createState() => _NotesScreenState();
}

class _NotesScreenState extends State<NotesScreen> {
  // Problem: State in widget
  final List<Map<String, String>> _notes = [];

  // Problem: Business logic in widget
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