---
type: "THEORY"
title: "Step 1: The Note Model"
---

A "Note" isn't just text; it's a structured piece of information. Let's create a class to represent it.

```dart
class Note {
  String id;
  String title;
  String content;
  DateTime dateTime;
  Color color;

  Note({
    required this.id,
    required this.title,
    required this.content,
    required this.dateTime,
    this.color = Colors.white,
  });
}
```