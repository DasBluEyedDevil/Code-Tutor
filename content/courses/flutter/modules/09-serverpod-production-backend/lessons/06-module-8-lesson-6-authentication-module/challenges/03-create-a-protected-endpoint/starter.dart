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

// Simulating Session for this exercise
class Session {
  Future<int?> get authenticatedUserId async => null;
}

class NotesEndpoint {
  // TODO: Implement getMyNotes
  // - Check authentication
  // - Return all notes where authorId equals current user's ID
  Future<List<Note>> getMyNotes(Session session) async {
    throw UnimplementedError();
  }

  // TODO: Implement createNote
  // - Check authentication
  // - Create a new note with the current user as author
  // - Return the created note
  Future<Note> createNote(
    Session session,
    String title,
    String content,
  ) async {
    throw UnimplementedError();
  }

  // TODO: Implement deleteNote
  // - Check authentication
  // - Verify the note belongs to the current user (authorization)
  // - Delete the note and return true if successful
  Future<bool> deleteNote(Session session, int noteId) async {
    throw UnimplementedError();
  }
}

void main() {
  print('NotesEndpoint implementation complete');
}