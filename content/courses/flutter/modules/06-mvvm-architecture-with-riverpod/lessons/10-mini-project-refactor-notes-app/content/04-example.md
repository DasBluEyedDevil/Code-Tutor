---
type: "EXAMPLE"
title: "Step 2: Create the ViewModel"
---

Next, move all business logic into a Riverpod ViewModel. This handles:

### What the ViewModel Does

1. **Holds state**: The list of notes lives here
2. **Provides actions**: `addNote()`, `updateNote()`, `deleteNote()`
3. **Encapsulates logic**: ID generation, validation, etc.
4. **Enables testing**: Can test all logic without UI

### The NotesViewModel

```dart
// viewmodels/notes_viewmodel.dart

import 'package:riverpod_annotation/riverpod_annotation.dart';
import '../models/note.dart';

part 'notes_viewmodel.g.dart';

@riverpod
class NotesViewModel extends _$NotesViewModel {
  @override
  List<Note> build() {
    // Initial state: empty list
    // In a real app, you might load from a database here
    return [];
  }

  /// Add a new note
  void addNote({required String title, required String content}) {
    // Validate input
    if (title.trim().isEmpty) return;

    // Create new note using factory
    final note = Note.create(title: title.trim(), content: content.trim());

    // Update state immutably
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

  /// Get a single note by ID (useful for detail screens)
  Note? getNoteById(String id) {
    try {
      return state.firstWhere((note) => note.id == id);
    } catch (e) {
      return null;
    }
  }
}

// Generated code will create:
// - notesViewModelProvider
// - _$NotesViewModel base class
```
