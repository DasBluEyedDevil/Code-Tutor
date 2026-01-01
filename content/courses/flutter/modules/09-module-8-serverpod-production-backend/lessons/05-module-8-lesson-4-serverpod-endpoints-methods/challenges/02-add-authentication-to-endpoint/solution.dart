import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class NoteEndpoint extends Endpoint {

  // Require authentication for all methods
  @override
  bool get requireLogin => true;

  /// Get all notes for the current user.
  Future<List<Note>> getMyNotes(Session session) async {
    final userId = await session.auth.authenticatedUserId;
    // userId guaranteed non-null due to requireLogin

    return await Note.db.find(
      session,
      where: (t) => t.authorId.equals(userId!),
      orderBy: (t) => t.createdAt,
      orderDescending: true,
    );
  }

  /// Create a note for the current user.
  Future<Note> createNote(Session session, Note note) async {
    final userId = await session.auth.authenticatedUserId;

    if (note.content.trim().isEmpty) {
      throw ArgumentError('Note content cannot be empty');
    }

    final noteToCreate = note.copyWith(
      authorId: userId!,
      createdAt: DateTime.now(),
    );

    return await Note.db.insertRow(session, noteToCreate);
  }

  /// Delete a note (only if owned by current user).
  Future<bool> deleteNote(Session session, int noteId) async {
    final userId = await session.auth.authenticatedUserId;

    final note = await Note.db.findById(session, noteId);
    if (note == null) {
      return false;
    }

    // Security check: only delete own notes
    if (note.authorId != userId) {
      throw UnauthorizedException(
        'You can only delete your own notes',
      );
    }

    await Note.db.deleteRow(session, note);
    return true;
  }
}

// Custom exception for unauthorized access
class UnauthorizedException implements Exception {
  final String message;
  UnauthorizedException(this.message);

  @override
  String toString() => 'UnauthorizedException: $message';
}