---
type: "EXAMPLE"
title: "Step 3: Create the View"
---

Finally, clean up the UI. The View should ONLY handle:

### What the View Does

1. **Display data**: Shows the list of notes
2. **Capture user input**: Text fields, buttons
3. **Delegate actions**: Calls ViewModel methods
4. **Navigation**: Dialogs, screens (if applicable)

### What the View Does NOT Do

- No business logic
- No state management (except local UI state like text controllers)
- No data transformation
- No ID generation

### The Clean NotesScreen

```dart
// views/notes_screen.dart

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/note.dart';
import '../viewmodels/notes_viewmodel.dart';

class NotesScreen extends ConsumerWidget {
  const NotesScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Watch the notes list - rebuilds when it changes
    final notes = ref.watch(notesViewModelProvider);

    return Scaffold(
      appBar: AppBar(
        title: const Text('My Notes'),
        actions: [
          // Show note count
          Center(
            child: Padding(
              padding: const EdgeInsets.only(right: 16),
              child: Text('${notes.length} notes'),
            ),
          ),
        ],
      ),
      body: notes.isEmpty
          ? const _EmptyState()
          : _NotesList(notes: notes),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _showAddNoteDialog(context, ref),
        child: const Icon(Icons.add),
      ),
    );
  }

  void _showAddNoteDialog(BuildContext context, WidgetRef ref) {
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
              // Delegate to ViewModel - no logic here!
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

// Separate widget for empty state
class _EmptyState extends StatelessWidget {
  const _EmptyState();

  @override
  Widget build(BuildContext context) {
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
}

// Separate widget for notes list
class _NotesList extends ConsumerWidget {
  final List<Note> notes;

  const _NotesList({required this.notes});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    return ListView.builder(
      itemCount: notes.length,
      itemBuilder: (context, index) {
        final note = notes[index];
        return _NoteCard(
          note: note,
          onDelete: () {
            ref.read(notesViewModelProvider.notifier).deleteNote(note.id);
          },
        );
      },
    );
  }
}

// Separate widget for individual note card
class _NoteCard extends StatelessWidget {
  final Note note;
  final VoidCallback onDelete;

  const _NoteCard({required this.note, required this.onDelete});

  @override
  Widget build(BuildContext context) {
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
              _formatDate(note.createdAt),
              style: TextStyle(
                fontSize: 12,
                color: Colors.grey.shade500,
              ),
            ),
          ],
        ),
        trailing: IconButton(
          icon: const Icon(Icons.delete_outline),
          onPressed: onDelete,
        ),
        isThreeLine: note.content.isNotEmpty,
      ),
    );
  }

  String _formatDate(DateTime date) {
    return '${date.day}/${date.month}/${date.year}';
  }
}
```
