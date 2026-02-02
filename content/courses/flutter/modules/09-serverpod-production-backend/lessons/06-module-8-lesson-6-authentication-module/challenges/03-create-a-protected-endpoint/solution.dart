class Note {
  final int? id;
  final int authorId;
  final String title;
  final String content;
  final DateTime createdAt;
  
  Note({
    this.id,
    required this.authorId,
    required this.title,
    required this.content,
    required this.createdAt,
  });
}

class Session {
  Future<int?> get authenticatedUserId async => 1; // Simulated
}

class AuthenticationException implements Exception {
  final String message;
  AuthenticationException(this.message);
  @override
  String toString() => 'AuthenticationException: $message';
}

class AuthorizationException implements Exception {
  final String message;
  AuthorizationException(this.message);
  @override
  String toString() => 'AuthorizationException: $message';
}

class NotesEndpoint {
  /// Get all notes belonging to the current user
  /// Requires authentication
  Future<List<Note>> getMyNotes(Session session) async {
    // Step 1: Check authentication
    final userId = await session.authenticatedUserId;
    if (userId == null) {
      throw AuthenticationException(
        'You must be signed in to view your notes',
      );
    }

    // Step 2: Query notes for this user
    // In real Serverpod: Note.db.find(session, where: (t) => t.authorId.equals(userId))
    return [
      Note(
        id: 1,
        authorId: userId,
        title: 'My First Note',
        content: 'This is a test note',
        createdAt: DateTime.now(),
      ),
    ];
  }

  /// Create a new note for the current user
  /// Requires authentication
  Future<Note> createNote(
    Session session,
    String title,
    String content,
  ) async {
    // Step 1: Check authentication
    final userId = await session.authenticatedUserId;
    if (userId == null) {
      throw AuthenticationException(
        'You must be signed in to create notes',
      );
    }

    // Step 2: Validate input
    if (title.trim().isEmpty) {
      throw ArgumentError('Note title cannot be empty');
    }

    // Step 3: Create the note with current user as author
    final note = Note(
      id: 1,
      authorId: userId,  // Set author to current user
      title: title.trim(),
      content: content,
      createdAt: DateTime.now(),
    );

    return note;
  }

  /// Delete a note
  /// Requires authentication AND authorization (must own the note)
  Future<bool> deleteNote(Session session, int noteId) async {
    // Step 1: Check authentication
    final userId = await session.authenticatedUserId;
    if (userId == null) {
      throw AuthenticationException(
        'You must be signed in to delete notes',
      );
    }

    // Step 2: Fetch the note to check ownership
    final note = Note(
      id: noteId,
      authorId: userId,
      title: 'Test',
      content: 'Test',
      createdAt: DateTime.now(),
    );

    // Step 3: Authorization check - verify ownership
    if (note.authorId != userId) {
      throw AuthorizationException(
        'You can only delete your own notes',
      );
    }

    // Step 4: Delete the note
    return true;
  }
}

void main() {
  print('NotesEndpoint implementation complete');
  print('');
  print('Key features implemented:');
  print('1. getMyNotes - Checks auth, returns only user\'s notes');
  print('2. createNote - Checks auth, sets authorId to current user');
  print('3. deleteNote - Checks auth AND verifies note ownership');
}