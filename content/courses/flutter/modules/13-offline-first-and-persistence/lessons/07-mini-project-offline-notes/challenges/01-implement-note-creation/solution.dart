import 'package:uuid/uuid.dart';

class NotesService {
  final AppDatabase _db;
  final SyncService _sync;
  
  NotesService(this._db, this._sync);
  
  Future<Note> createNote({
    required String title,
    required String content,
  }) async {
    // 1. Generate UUID
    final id = const Uuid().v4();
    final now = DateTime.now();
    
    // 2. Create note companion for insertion
    final noteCompanion = NotesCompanion(
      id: Value(id),
      title: Value(title),
      content: Value(content),
      createdAt: Value(now),
      syncStatus: Value(SyncStatus.pending),
    );
    
    // 3. Insert into local database
    await _db.insertNote(noteCompanion);
    
    // 4. Queue sync operation
    await _sync.queueOperation(
      type: 'create',
      noteId: id,
      data: {
        'id': id,
        'title': title,
        'content': content,
        'createdAt': now.toIso8601String(),
      },
    );
    
    // 5. Return created note
    final note = await _db.getNoteById(id);
    return note!;
  }
}