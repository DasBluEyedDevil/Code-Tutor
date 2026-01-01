---
type: "THEORY"
title: "Step 2: The Main Screen (Stateful)"
---

Our main screen needs to keep track of a list of notes. We'll use a `StatefulWidget` so the UI updates when notes are added or removed.

```dart
class NotesScreen extends StatefulWidget {
  const NotesScreen({super.key});

  @override
  State<NotesScreen> createState() => _NotesScreenState();
}

class _NotesScreenState extends State<NotesScreen> {
  final List<Note> _notes = [];

  void _addNote(String title, String content) {
    setState(() {
      _notes.add(Note(
        id: DateTime.now().toString(),
        title: title,
        content: content,
        dateTime: DateTime.now(),
      ));
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('My Notes')),
      body: _notes.isEmpty 
          ? const Center(child: Text('No notes yet!'))
          : ListView.builder(
              itemCount: _notes.length,
              itemBuilder: (context, index) => NoteTile(note: _notes[index]),
            ),
      floatingActionButton: FloatingActionButton(
        onPressed: () => _showAddNoteDialog(),
        child: const Icon(Icons.add),
      ),
    );
  }
}
```