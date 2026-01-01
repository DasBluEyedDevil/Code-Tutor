---
type: "EXAMPLE"
title: "Step 1: Create the Model"
---

First, extract a proper `Note` class. This gives us:

### Benefits of a Proper Model

1. **Type safety**: No more `Map<String, String>` with potential null issues
2. **Immutability**: Cannot accidentally modify notes
3. **copyWith method**: Easy updates while maintaining immutability
4. **Clear structure**: Anyone reading the code knows what a Note contains
5. **IDE support**: Autocomplete, refactoring, error checking

### The Note Model

```dart
// models/note.dart

class Note {
  final String id;
  final String title;
  final String content;
  final DateTime createdAt;

  const Note({
    required this.id,
    required this.title,
    required this.content,
    required this.createdAt,
  });

  // Factory constructor for creating new notes
  factory Note.create({required String title, required String content}) {
    return Note(
      id: DateTime.now().millisecondsSinceEpoch.toString(),
      title: title,
      content: content,
      createdAt: DateTime.now(),
    );
  }

  // copyWith for immutable updates
  Note copyWith({
    String? id,
    String? title,
    String? content,
    DateTime? createdAt,
  }) {
    return Note(
      id: id ?? this.id,
      title: title ?? this.title,
      content: content ?? this.content,
      createdAt: createdAt ?? this.createdAt,
    );
  }

  @override
  String toString() => 'Note(id: $id, title: $title)';

  @override
  bool operator ==(Object other) =>
      identical(this, other) ||
      other is Note &&
          runtimeType == other.runtimeType &&
          id == other.id &&
          title == other.title &&
          content == other.content &&
          createdAt == other.createdAt;

  @override
  int get hashCode => Object.hash(id, title, content, createdAt);
}
```
