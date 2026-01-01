import 'package:uuid/uuid.dart';

class NotesService {
  final AppDatabase _db;
  final SyncService _sync;
  
  NotesService(this._db, this._sync);
  
  Future<Note> createNote({
    required String title,
    required String content,
  }) async {
    // TODO: Implement offline-first note creation
    throw UnimplementedError();
  }
}