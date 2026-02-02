---
type: "EXAMPLE"
title: "Reactive Streams"
---


**Watch for Changes:**



```dart
extension NotesStreamDao on AppDatabase {
  // Watch all notes (updates when data changes)
  Stream<List<Note>> watchAllNotes() {
    return select(notes).watch();
  }
  
  // Watch single note
  Stream<Note?> watchNoteById(int id) {
    return (select(notes)..where((n) => n.id.equals(id)))
        .watchSingleOrNull();
  }
  
  // Watch with filter
  Stream<List<Note>> watchActiveNotes() {
    return (select(notes)
      ..where((n) => n.isArchived.equals(false))
      ..orderBy([(n) => OrderingTerm.desc(n.createdAt)]))
        .watch();
  }
  
  // Watch count
  Stream<int> watchNoteCount() {
    final countExp = notes.id.count();
    return (selectOnly(notes)..addColumns([countExp]))
        .map((row) => row.read(countExp)!)
        .watchSingle();
  }
}

// Usage in a widget
class NotesScreen extends StatelessWidget {
  final AppDatabase db;
  
  const NotesScreen({required this.db});
  
  @override
  Widget build(BuildContext context) {
    return StreamBuilder<List<Note>>(
      stream: db.watchActiveNotes(),
      builder: (context, snapshot) {
        if (!snapshot.hasData) {
          return const CircularProgressIndicator();
        }
        
        final notes = snapshot.data!;
        return ListView.builder(
          itemCount: notes.length,
          itemBuilder: (context, index) {
            final note = notes[index];
            return ListTile(
              title: Text(note.title),
              subtitle: Text(note.content),
            );
          },
        );
      },
    );
  }
}
```
