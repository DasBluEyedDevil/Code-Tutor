---
type: "THEORY"
title: "Step 3: Adding Swipe to Delete"
---

Use the `Dismissible` widget to add swipe-to-delete functionality to your note items.

```dart
// Inside ListView.builder
return Dismissible(
  key: Key(note.id),
  background: Container(
    color: Colors.red,
    alignment: Alignment.centerRight,
    padding: const EdgeInsets.only(right: 20),
    child: const Icon(Icons.delete, color: Colors.white),
  ),
  direction: DismissDirection.endToStart,
  onDismissed: (direction) {
    setState(() {
      _notes.removeAt(index);
    });
    ScaffoldMessenger.of(context).showSnackBar(
      const SnackBar(content: Text('Note deleted')),
    );
  },
  child: ListTile(
    title: Text(note.title),
    subtitle: Text(note.content),
  ),
);
```