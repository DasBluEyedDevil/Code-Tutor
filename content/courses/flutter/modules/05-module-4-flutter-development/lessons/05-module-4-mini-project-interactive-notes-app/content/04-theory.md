---
type: "THEORY"
title: "Enhancement Ideas"
---


Want to make it even better? Add these:

### 1. Persistent Storage

### 2. Categories/Tags
Add a category field to Note model and filter by category.

### 3. Voice Input
Use speech_to_text package for voice notes.

### 4. Rich Text Formatting
Bold, italic, bullet points using a rich text editor package.

### 5. Pin Important Notes
Add a `isPinned` field and show pinned notes at top.



```dart
import 'package:shared_preferences/shared_preferences.dart';

Future<void> _saveNotes() async {
  final prefs = await SharedPreferences.getInstance();
  final notesJson = notes.map((n) => n.toJson()).toList();
  await prefs.setString('notes', jsonEncode(notesJson));
}

Future<void> _loadNotes() async {
  final prefs = await SharedPreferences.getInstance();
  final notesString = prefs.getString('notes');
  if (notesString != null) {
    final List<dynamic> notesJson = jsonDecode(notesString);
    notes = notesJson.map((json) => Note.fromJson(json)).toList();
  }
}
```
