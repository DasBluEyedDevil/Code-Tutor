---
type: "THEORY"
title: "Hive Basics"
---


**Hive** is a lightweight, fast NoSQL database written in pure Dart.

**Best For:**
- Structured data without complex relationships
- Fast read/write operations
- Cross-platform consistency
- Simple caching

**Features:**
- No native dependencies
- AES-256 encryption
- Custom type adapters
- Lazy loading support



```dart
// Add to pubspec.yaml:
// hive: ^2.2.3
// hive_flutter: ^1.1.0
// dev_dependencies:
//   hive_generator: ^2.0.1
//   build_runner: ^2.4.0

import 'package:hive_flutter/hive_flutter.dart';

// Define a model with Hive annotations
@HiveType(typeId: 0)
class Note extends HiveObject {
  @HiveField(0)
  late String id;
  
  @HiveField(1)
  late String title;
  
  @HiveField(2)
  late String content;
  
  @HiveField(3)
  late DateTime createdAt;
  
  @HiveField(4)
  late DateTime updatedAt;
}

// Initialize Hive
Future<void> initHive() async {
  await Hive.initFlutter();
  Hive.registerAdapter(NoteAdapter()); // Generated
  await Hive.openBox<Note>('notes');
}

// CRUD operations
class NotesRepository {
  final Box<Note> _box = Hive.box<Note>('notes');
  
  List<Note> getAll() => _box.values.toList();
  
  Note? getById(String id) => _box.get(id);
  
  Future<void> save(Note note) async {
    await _box.put(note.id, note);
  }
  
  Future<void> delete(String id) async {
    await _box.delete(id);
  }
  
  // Watch for changes
  Stream<BoxEvent> watch() => _box.watch();
}
```
