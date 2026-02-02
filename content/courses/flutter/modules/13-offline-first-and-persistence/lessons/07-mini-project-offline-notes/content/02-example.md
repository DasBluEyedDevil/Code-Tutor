---
type: "EXAMPLE"
title: "Local Storage Implementation"
---


**Database Setup with Drift:**



```dart
// lib/database/database.dart
import 'dart:io';
import 'package:drift/drift.dart';
import 'package:drift/native.dart';
import 'package:path_provider/path_provider.dart';
import 'package:path/path.dart' as p;

part 'database.g.dart';

class Notes extends Table {
  TextColumn get id => text()(); // UUID
  TextColumn get serverId => text().nullable()();
  TextColumn get title => text().withLength(min: 1, max: 200)();
  TextColumn get content => text()();
  DateTimeColumn get createdAt => dateTime()();
  DateTimeColumn get updatedAt => dateTime().nullable()();
  IntColumn get syncStatus => intEnum<SyncStatus>()();
  
  @override
  Set<Column> get primaryKey => {id};
}

class SyncQueue extends Table {
  TextColumn get id => text()();
  TextColumn get operationType => text()();
  TextColumn get noteId => text()();
  TextColumn get payload => text()();
  DateTimeColumn get createdAt => dateTime()();
  IntColumn get retryCount => integer().withDefault(const Constant(0))();
  
  @override
  Set<Column> get primaryKey => {id};
}

enum SyncStatus { synced, pending, syncing, failed }

@DriftDatabase(tables: [Notes, SyncQueue])
class AppDatabase extends _$AppDatabase {
  AppDatabase() : super(_openConnection());
  
  @override
  int get schemaVersion => 1;
  
  // Note operations
  Future<List<Note>> getAllNotes() =>
      (select(notes)..orderBy([(n) => OrderingTerm.desc(n.updatedAt ?? n.createdAt)]))
          .get();
  
  Stream<List<Note>> watchAllNotes() =>
      (select(notes)..orderBy([(n) => OrderingTerm.desc(n.updatedAt ?? n.createdAt)]))
          .watch();
  
  Future<Note?> getNoteById(String id) =>
      (select(notes)..where((n) => n.id.equals(id))).getSingleOrNull();
  
  Future<void> insertNote(NotesCompanion note) =>
      into(notes).insert(note);
  
  Future<void> updateNote(Note note) =>
      update(notes).replace(note);
  
  Future<void> deleteNoteById(String id) =>
      (delete(notes)..where((n) => n.id.equals(id))).go();
  
  // Sync queue operations
  Future<List<SyncQueueData>> getPendingSyncOps() =>
      (select(syncQueue)..orderBy([(q) => OrderingTerm.asc(q.createdAt)]))
          .get();
  
  Future<void> addToSyncQueue(SyncQueueCompanion op) =>
      into(syncQueue).insert(op);
  
  Future<void> removeSyncOp(String id) =>
      (delete(syncQueue)..where((q) => q.id.equals(id))).go();
}

LazyDatabase _openConnection() {
  return LazyDatabase(() async {
    final dbFolder = await getApplicationDocumentsDirectory();
    final file = File(p.join(dbFolder.path, 'offline_notes.db'));
    return NativeDatabase.createInBackground(file);
  });
}
```
