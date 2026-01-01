---
type: "EXAMPLE"
title: "Note Service Implementation"
---


The note service handles all CRUD operations:



```dart
// lib/services/note_service.dart
import '../models/note.dart';
import 'package:uuid/uuid.dart';

class NoteService {
  // In-memory storage (use database in production)
  final Map<String, Note> _notes = {};
  final _uuid = Uuid();

  /// Get all notes for a specific user
  List<Note> getUserNotes(String userId) {
    return _notes.values
        .where((note) => note.userId == userId)
        .toList()
      ..sort((a, b) => b.updatedAt.compareTo(a.updatedAt)); // Newest first
  }

  /// Get a specific note by ID (only if owned by user)
  Note? getNoteById(String noteId, String userId) {
    final note = _notes[noteId];
    if (note == null || note.userId != userId) {
      return null;
    }
    return note;
  }

  /// Create a new note
  Note create(String userId, String title, String content) {
    final now = DateTime.now();
    final note = Note(
      id: 'note_${_uuid.v4().substring(0, 8)}',
      userId: userId,
      title: title,
      content: content,
      createdAt: now,
      updatedAt: now,
    );

    _notes[note.id] = note;
    return note;
  }

  /// Update an existing note (only if owned by user)
  /// Returns updated note, or null if not found/not owned
  Note? update(String noteId, String userId, {String? title, String? content}) {
    final note = getNoteById(noteId, userId);
    if (note == null) {
      return null;
    }

    final updatedNote = note.copyWith(
      title: title,
      content: content,
      updatedAt: DateTime.now(),
    );

    _notes[noteId] = updatedNote;
    return updatedNote;
  }

  /// Delete a note (only if owned by user)
  /// Returns true if deleted, false if not found/not owned
  bool delete(String noteId, String userId) {
    final note = getNoteById(noteId, userId);
    if (note == null) {
      return false;
    }

    _notes.remove(noteId);
    return true;
  }
}

// Global instance (in production, use dependency injection)
final noteService = NoteService();
```
