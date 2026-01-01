// Solution: Notes App with Statistics
// Displays note count, character count, and last update time

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
    Note(id: '1', title: 'Shopping List', content: 'Milk, eggs, bread, butter', updatedAt: DateTime.now().subtract(const Duration(hours: 2))),
    Note(id: '2', title: 'Meeting Notes', content: 'Discuss Q4 goals and project timeline', updatedAt: DateTime.now().subtract(const Duration(days: 1))),
    Note(id: '3', title: 'Ideas', content: 'Build a Flutter app with notes and todos', updatedAt: DateTime.now()),
  ];

  // Calculate statistics
  int get totalNotes => notes.length;
  
  int get totalCharacters => notes.fold(0, (sum, note) => sum + note.title.length + note.content.length);
  
  String get mostRecentUpdate {
    if (notes.isEmpty) return 'No notes';
    final latest = notes.reduce((a, b) => a.updatedAt.isAfter(b.updatedAt) ? a : b);
    final diff = DateTime.now().difference(latest.updatedAt);
    if (diff.inMinutes < 1) return 'Just now';
    if (diff.inHours < 1) return '${diff.inMinutes}m ago';
    if (diff.inDays < 1) return '${diff.inHours}h ago';
    return '${diff.inDays}d ago';
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // Statistics Card
        Card(
          margin: const EdgeInsets.all(16),
          child: Padding(
            padding: const EdgeInsets.all(16),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: [
                _buildStat('Notes', '$totalNotes', Icons.note),
                _buildStat('Characters', '$totalCharacters', Icons.text_fields),
                _buildStat('Updated', mostRecentUpdate, Icons.access_time),
              ],
            ),
          ),
        ),
        
        // Notes List
        Expanded(
          child: ListView.builder(
            itemCount: notes.length,
            itemBuilder: (context, index) {
              final note = notes[index];
              return ListTile(
                title: Text(note.title),
                subtitle: Text(
                  note.content,
                  maxLines: 1,
                  overflow: TextOverflow.ellipsis,
                ),
                trailing: Text(
                  _formatTime(note.updatedAt),
                  style: TextStyle(color: Colors.grey.shade600, fontSize: 12),
                ),
              );
            },
          ),
        ),
      ],
    );
  }

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

  String _formatTime(DateTime dt) {
    return '${dt.hour}:${dt.minute.toString().padLeft(2, '0')}';
  }
}

// Key concepts:
// - Computed properties (getters) for statistics
// - fold: Aggregate values across list
// - reduce: Find max/min in list
// - Duration: Calculate time differences
// - DateTime: Work with dates and times