---
type: "THEORY"
title: "Isar Introduction"
---


**Isar** is a super-fast, cross-platform NoSQL database for Flutter.

**Best For:**
- High-performance requirements
- Full-text search
- Complex queries without SQL
- Cross-platform (including web)

**Features:**
- Extremely fast (benchmarks show 10x faster than alternatives)
- ACID compliant
- Full-text search
- Composite indexes
- Async and sync APIs



```dart
// Add to pubspec.yaml:
// isar: ^3.1.0
// isar_flutter_libs: ^3.1.0
// dev_dependencies:
//   isar_generator: ^3.1.0
//   build_runner: ^2.4.0

import 'package:isar/isar.dart';

part 'note.g.dart';

@collection
class Note {
  Id id = Isar.autoIncrement;
  
  @Index(type: IndexType.value)
  late String title;
  
  late String content;
  
  @Index()
  late DateTime createdAt;
  
  late bool isSynced;
  
  // Full-text search index
  @Index(type: IndexType.value, caseSensitive: false)
  List<String> get titleWords => title.split(' ');
}

// Initialize Isar
Future<Isar> initIsar() async {
  final dir = await getApplicationDocumentsDirectory();
  return await Isar.open(
    [NoteSchema],
    directory: dir.path,
  );
}

// Query examples
class NotesRepository {
  final Isar isar;
  
  NotesRepository(this.isar);
  
  Future<List<Note>> getAll() => isar.notes.where().findAll();
  
  Future<List<Note>> search(String query) => isar.notes
      .filter()
      .titleContains(query, caseSensitive: false)
      .findAll();
  
  Future<List<Note>> getUnsynced() => isar.notes
      .filter()
      .isSyncedEqualTo(false)
      .findAll();
  
  Stream<List<Note>> watchAll() => isar.notes.where().watch();
}
```
