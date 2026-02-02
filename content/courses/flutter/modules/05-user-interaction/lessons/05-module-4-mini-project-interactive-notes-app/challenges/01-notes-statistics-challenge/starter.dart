// Notes Statistics Challenge
// Display stats: note count, total characters, last update

import 'package:flutter/material.dart';

void main() {
  runApp(const NotesApp());
}

class NotesApp extends StatelessWidget {
  const NotesApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Notes')),
        body: const NotesScreen(),
      ),
    );
  }
}

class Note {
  final String id;
  final String title;
  final String content;
  final DateTime updatedAt;

  Note({
    required this.id,
    required this.title,
    required this.content,
    required this.updatedAt,
  });
}

class NotesScreen extends StatefulWidget {
  const NotesScreen({super.key});

  @override
  State<NotesScreen> createState() => _NotesScreenState();
}

class _NotesScreenState extends State<NotesScreen> {
  final List<Note> notes = [
    Note(id: '1', title: 'Shopping', content: 'Milk, eggs, bread', updatedAt: DateTime.now().subtract(const Duration(hours: 2))),
    Note(id: '2', title: 'Meeting', content: 'Discuss Q4 goals', updatedAt: DateTime.now()),
  ];

  // TODO 1: Create getter 'totalNotes' that returns notes.length
  
  // TODO 2: Create getter 'totalCharacters' using fold()
  // Hint: notes.fold(0, (sum, note) => sum + note.title.length + note.content.length)
  
  // TODO 3: Create getter 'mostRecentUpdate' that returns a String
  // Use reduce() to find the note with latest updatedAt
  // Format as 'Just now', 'Xm ago', 'Xh ago', or 'Xd ago'

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // TODO 4: Build a statistics Card
        Card(
          margin: const EdgeInsets.all(16),
          child: Padding(
            padding: const EdgeInsets.all(16),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: [
                // TODO: Add stat columns for Notes, Characters, Updated
                // Use _buildStat helper method
              ],
            ),
          ),
        ),
        
        // Notes list
        Expanded(
          child: ListView.builder(
            itemCount: notes.length,
            itemBuilder: (context, index) {
              final note = notes[index];
              return ListTile(
                title: Text(note.title),
                subtitle: Text(note.content, maxLines: 1, overflow: TextOverflow.ellipsis),
              );
            },
          ),
        ),
      ],
    );
  }

  // Helper to build a stat column
  Widget _buildStat(String label, String value, IconData icon) {
    return Column(
      children: [
        Icon(icon, color: Colors.blue),
        const SizedBox(height: 4),
        Text(value, style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 18)),
        Text(label, style: TextStyle(color: Colors.grey.shade600, fontSize: 12)),
      ],
    );
  }
}