---
type: "EXAMPLE"
title: "Testing Offline Behavior"
---


**Testing Strategies:**



```dart
// test/offline_notes_test.dart
import 'package:flutter_test/flutter_test.dart';
import 'package:drift/drift.dart';
import 'package:drift/native.dart';

void main() {
  late AppDatabase db;
  late NotesService notesService;
  late MockSyncService mockSync;
  
  setUp(() {
    // Use in-memory database for testing
    db = AppDatabase.forTesting(
      NativeDatabase.memory(),
    );
    mockSync = MockSyncService();
    notesService = NotesService(db, mockSync);
  });
  
  tearDown(() async {
    await db.close();
  });
  
  group('Offline Note Creation', () {
    test('creates note locally without network', () async {
      // Simulate offline
      mockSync.isOnline = false;
      
      // Create note
      final note = await notesService.createNote(
        title: 'Offline Note',
        content: 'Created while offline',
      );
      
      // Verify local storage
      expect(note.id, isNotEmpty);
      expect(note.title, 'Offline Note');
      expect(note.syncStatus, SyncStatus.pending);
      
      // Verify in database
      final fromDb = await db.getNoteById(note.id);
      expect(fromDb, isNotNull);
      expect(fromDb!.title, 'Offline Note');
    });
    
    test('queues sync operation when offline', () async {
      mockSync.isOnline = false;
      
      await notesService.createNote(
        title: 'Offline Note',
        content: 'Content',
      );
      
      // Verify sync queue has operation
      final queue = await db.getPendingSyncOps();
      expect(queue.length, 1);
      expect(queue.first.operationType, 'create');
    });
    
    test('syncs when connection restored', () async {
      mockSync.isOnline = false;
      
      final note = await notesService.createNote(
        title: 'Offline Note',
        content: 'Content',
      );
      
      // Restore connection
      mockSync.isOnline = true;
      await mockSync.sync();
      
      // Verify note marked as synced
      final synced = await db.getNoteById(note.id);
      expect(synced!.syncStatus, SyncStatus.synced);
      
      // Verify queue is empty
      final queue = await db.getPendingSyncOps();
      expect(queue, isEmpty);
    });
  });
  
  group('Conflict Resolution', () {
    test('handles server conflict with merge', () async {
      // Create local note
      final localNote = await notesService.createNote(
        title: 'Local Title',
        content: 'Local content',
      );
      
      // Simulate server having different version
      mockSync.serverNotes[localNote.id] = Note(
        id: localNote.id,
        title: 'Server Title',
        content: 'Server content',
        updatedAt: DateTime.now().add(const Duration(hours: 1)),
        syncStatus: SyncStatus.synced,
      );
      
      // Trigger sync with conflict
      await mockSync.sync();
      
      // Verify merge happened (server title wins due to later timestamp)
      final merged = await db.getNoteById(localNote.id);
      expect(merged!.title, 'Server Title');
    });
  });
  
  group('Rollback on Failure', () {
    test('rollbacks optimistic update on error', () async {
      final note = await notesService.createNote(
        title: 'Original',
        content: 'Content',
      );
      
      // Make sync fail
      mockSync.shouldFail = true;
      
      // Try to update
      try {
        await notesService.updateNote(note.id, title: 'Updated');
      } catch (_) {}
      
      // Verify rollback
      final fromDb = await db.getNoteById(note.id);
      expect(fromDb!.title, 'Original');
    });
  });
}
```
