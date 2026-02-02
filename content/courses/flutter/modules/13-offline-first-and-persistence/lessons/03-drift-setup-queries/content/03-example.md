---
type: "EXAMPLE"
title: "Type-Safe Queries"
---


**Basic CRUD Operations:**



```dart
// After running: dart run build_runner build

extension NotesDao on AppDatabase {
  // CREATE
  Future<int> createNote(NotesCompanion note) {
    return into(notes).insert(note);
  }
  
  // READ - Get all
  Future<List<Note>> getAllNotes() {
    return select(notes).get();
  }
  
  // READ - Get by ID
  Future<Note?> getNoteById(int id) {
    return (select(notes)..where((n) => n.id.equals(id)))
        .getSingleOrNull();
  }
  
  // READ - Filtered query
  Future<List<Note>> getActiveNotes() {
    return (select(notes)
      ..where((n) => n.isArchived.equals(false))
      ..orderBy([(n) => OrderingTerm.desc(n.createdAt)]))
        .get();
  }
  
  // READ - Search
  Future<List<Note>> searchNotes(String query) {
    return (select(notes)
      ..where((n) => n.title.like('%$query%') | n.content.like('%$query%')))
        .get();
  }
  
  // UPDATE
  Future<bool> updateNote(Note note) {
    return update(notes).replace(note);
  }
  
  // UPDATE - Partial
  Future<int> archiveNote(int id) {
    return (update(notes)..where((n) => n.id.equals(id)))
        .write(NotesCompanion(isArchived: Value(true)));
  }
  
  // DELETE
  Future<int> deleteNote(int id) {
    return (delete(notes)..where((n) => n.id.equals(id))).go();
  }
  
  // DELETE - Batch
  Future<int> deleteArchivedNotes() {
    return (delete(notes)..where((n) => n.isArchived.equals(true))).go();
  }
}
```
