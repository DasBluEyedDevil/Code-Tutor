// Project structure to create:
// routes/
//   _middleware.dart
//   auth/
//     register.dart
//     login.dart
//   notes/
//     _middleware.dart
//     index.dart
//     [id].dart
// lib/
//   models/
//     user.dart
//     note.dart
//   services/
//     auth_service.dart
//     note_service.dart
//   utils/
//     jwt_helper.dart

// Start with the User model:
class User {
  final String id;
  final String email;
  final String passwordHash;
  
  User({required this.id, required this.email, required this.passwordHash});
  
  // TODO: Add toJson method
}

// Then create Note model:
class Note {
  final String id;
  final String userId;
  final String title;
  final String content;
  
  Note({
    required this.id,
    required this.userId,
    required this.title,
    required this.content,
  });
  
  // TODO: Add toJson and copyWith methods
}

// Create AuthService:
class AuthService {
  final Map<String, User> _users = {};
  
  User? register(String email, String password) {
    // TODO: Check if email exists
    // TODO: Hash password
    // TODO: Create and store user
    return null;
  }
  
  String? login(String email, String password) {
    // TODO: Find user
    // TODO: Verify password
    // TODO: Return JWT
    return null;
  }
}

// Create NoteService:
class NoteService {
  final Map<String, Note> _notes = {};
  
  List<Note> getUserNotes(String userId) {
    // TODO: Return notes for this user
    return [];
  }
  
  Note create(String userId, String title, String content) {
    // TODO: Create and store note
    throw UnimplementedError();
  }
  
  Note? update(String noteId, String userId, {String? title, String? content}) {
    // TODO: Update note if exists and owned by user
    return null;
  }
  
  bool delete(String noteId, String userId) {
    // TODO: Delete note if exists and owned by user
    return false;
  }
}