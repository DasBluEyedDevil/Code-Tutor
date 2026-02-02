---
type: "THEORY"
title: "Step 4: The Add Note Dialog"
---

Use a `showDialog` with a `Form` and two `TextFields` to collect user input.

```dart
void _showAddNoteDialog() {
  final titleController = TextEditingController();
  final contentController = TextEditingController();

  showDialog(
    context: context,
    builder: (context) => AlertDialog(
      title: const Text('New Note'),
      content: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          TextField(controller: titleController, decoration: const InputDecoration(labelText: 'Title')),
          TextField(controller: contentController, decoration: const InputDecoration(labelText: 'Content')),
        ],
      ),
      actions: [
        TextButton(onPressed: () => Navigator.pop(context), child: const Text('Cancel')),
        ElevatedButton(
          onPressed: () {
            _addNote(titleController.text, contentController.text);
            Navigator.pop(context);
          },
          child: const Text('Save'),
        ),
      ],
    ),
  );
}
```