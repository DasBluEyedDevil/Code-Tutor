---
type: "THEORY"
title: "Local-First Writes"
---


**The Local-First Write Pattern:**

Every user action writes to local database first, then syncs to server.

**Benefits:**
- Instant UI feedback
- Works offline
- Reduced perceived latency
- Resilient to network issues

**Implementation Pattern:**



```dart
class NotesService {
  final AppDatabase _db;
  final NotesApi _api;
  final SyncQueue _syncQueue;
  
  NotesService(this._db, this._api, this._syncQueue);
  
  /// Create note - local first, then sync
  Future<Note> createNote(String title, String content) async {
    // 1. Generate local ID (UUID for offline-safe IDs)
    final localId = const Uuid().v4();
    
    // 2. Write to local database immediately
    final note = NotesCompanion(
      localId: Value(localId),
      title: Value(title),
      content: Value(content),
      syncStatus: Value(SyncStatus.pending),
      createdAt: Value(DateTime.now()),
    );
    
    final id = await _db.into(_db.notes).insert(note);
    final createdNote = await _db.getNoteById(id);
    
    // 3. Queue sync operation
    await _syncQueue.enqueue(
      SyncOperation(
        type: SyncType.create,
        table: 'notes',
        localId: localId,
        data: createdNote!.toJson(),
      ),
    );
    
    // 4. Trigger background sync
    _syncQueue.processQueue();
    
    // 5. Return immediately with local data
    return createdNote;
  }
  
  /// Update note - same pattern
  Future<Note> updateNote(Note note, String newTitle) async {
    // 1. Update local immediately
    final updated = note.copyWith(
      title: newTitle,
      updatedAt: Value(DateTime.now()),
      syncStatus: SyncStatus.pending,
    );
    
    await _db.update(_db.notes).replace(updated);
    
    // 2. Queue sync
    await _syncQueue.enqueue(
      SyncOperation(
        type: SyncType.update,
        table: 'notes',
        localId: note.localId,
        data: updated.toJson(),
      ),
    );
    
    _syncQueue.processQueue();
    
    return updated;
  }
}
```
