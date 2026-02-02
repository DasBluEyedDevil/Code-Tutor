// SOLUTION: Clean MVVM Notes App with Riverpod

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:riverpod_annotation/riverpod_annotation.dart';

part 'main.g.dart';

void main() {
  runApp(const ProviderScope(child: MaterialApp(home: NotesScreen())));
}

// ============================================
// MODEL: Pure data class
// ============================================

class Note {
  final String id;
  final String title;
  final String content;
  final DateTime createdAt;

  const Note({
    required this.id,
    required this.title,
    required this.content,
    required this.createdAt,
  });

  /// Factory for creating new notes with auto-generated ID
  factory Note.create({required String title, required String content}) {
    return Note(
      id: DateTime.now().millisecondsSinceEpoch.toString(),
      title: title,
      content: content,
      createdAt: DateTime.now(),
    );
  }

  /// Immutable update via copyWith
  Note copyWith({
    String? id,
    String? title,
    String? content,
    DateTime? createdAt,
  }) {
    return Note(
      id: id ?? this.id,
      title: title ?? this.title,
      content: content ?? this.content,
      createdAt: createdAt ?? this.createdAt,
    );
  }
}

// ============================================
// VIEWMODEL: Business logic with Riverpod
// ============================================

@riverpod
class NotesViewModel extends _$NotesViewModel {
  @override
  List<Note> build() {
    // Initial state: empty list
    return [];
  }

  /// Add a new note
  void addNote({required String title, required String content}) {
    if (title.trim().isEmpty) return;

    final note = Note.create(
      title: title.trim(),
      content: content.trim(),
    );

    state = [...state, note];
  }

  /// Update an existing note
  void updateNote({
    required String id,
    required String title,
    required String content,
  }) {
    state = state.map((note) {
      if (note.id == id) {
        return note.copyWith(
          title: title.trim(),
          content: content.trim(),
        );
      }
      return note;
    }).toList();
  }

  /// Delete a note by ID
  void deleteNote(String id) {
    state = state.where((note) => note.id != id).toList();
  }
}

// ============================================
// VIEW: Clean UI with ConsumerWidget
// ============================================

class NotesScreen extends ConsumerWidget {
  const NotesScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Watch state - rebuilds when notes change
    final notes = ref.watch(notesViewModelProvider);

    return Scaffold(
      appBar: AppBar(
        title: const Text('My Notes'),
        actions: [
          Center(
            child: Padding(
              padding: const EdgeInsets.only(right: 16),
              child: Text('${notes.length} notes'),
            ),
          ),
        ],
      ),
      body: notes.isEmpty
          ? _buildEmptyState(context)
          : _buildNotesList(notes, ref),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _showAddDialog(context, ref),
        child: const Icon(Icons.add),
      ),
    );
  }

  Widget _buildEmptyState(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Icon(Icons.note_outlined, size: 64, color: Colors.grey.shade400),
          const SizedBox(height: 16),
          Text(
            'No notes yet',
            style: Theme.of(context).textTheme.titleLarge?.copyWith(
                  color: Colors.grey.shade600,
                ),
          ),
          const SizedBox(height: 8),
          Text(
            'Tap + to add your first note',
            style: TextStyle(color: Colors.grey.shade500),
          ),
        ],
      ),
    );
  }

  Widget _buildNotesList(List<Note> notes, WidgetRef ref) {
    return ListView.builder(
      itemCount: notes.length,
      itemBuilder: (context, index) {
        final note = notes[index];
        return Card(
          margin: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
          child: ListTile(
            title: Text(
              note.title,
              style: const TextStyle(fontWeight: FontWeight.bold),
            ),
            subtitle: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                if (note.content.isNotEmpty) Text(note.content),
                const SizedBox(height: 4),
                Text(
                  '${note.createdAt.day}/${note.createdAt.month}/${note.createdAt.year}',
                  style: TextStyle(fontSize: 12, color: Colors.grey.shade500),
                ),
              ],
            ),
            trailing: IconButton(
              icon: const Icon(Icons.delete_outline),
              onPressed: () {
                // Delegate to ViewModel - no logic here!
                ref.read(notesViewModelProvider.notifier).deleteNote(note.id);
              },
            ),
            isThreeLine: note.content.isNotEmpty,
          ),
        );
      },
    );
  }

  void _showAddDialog(BuildContext context, WidgetRef ref) {
    final titleController = TextEditingController();
    final contentController = TextEditingController();

    showDialog(
      context: context,
      builder: (ctx) => AlertDialog(
        title: const Text('New Note'),
        content: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            TextField(
              controller: titleController,
              decoration: const InputDecoration(labelText: 'Title'),
              autofocus: true,
            ),
            const SizedBox(height: 8),
            TextField(
              controller: contentController,
              decoration: const InputDecoration(labelText: 'Content'),
              maxLines: 3,
            ),
          ],
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(ctx),
            child: const Text('Cancel'),
          ),
          FilledButton(
            onPressed: () {
              // Delegate to ViewModel!
              ref.read(notesViewModelProvider.notifier).addNote(
                    title: titleController.text,
                    content: contentController.text,
                  );
              Navigator.pop(ctx);
            },
            child: const Text('Save'),
          ),
        ],
      ),
    );
  }
}

// ============================================
// KEY ACHIEVEMENTS:
// ============================================
//
// 1. MODEL (Note):
//    - Pure data class with type safety
//    - Immutable with copyWith
//    - Factory constructor for creation
//
// 2. VIEWMODEL (NotesViewModel):
//    - All business logic centralized
//    - State management via Riverpod
//    - Easy to test with unit tests
//
// 3. VIEW (NotesScreen):
//    - Only handles UI rendering
//    - Delegates all actions to ViewModel
//    - Clean, readable, maintainable
//
// BENEFITS:
// - Each piece can be tested separately
// - Easy to add features (search, edit, persist)
// - Teams can work on different layers
// - State is shared across the entire app