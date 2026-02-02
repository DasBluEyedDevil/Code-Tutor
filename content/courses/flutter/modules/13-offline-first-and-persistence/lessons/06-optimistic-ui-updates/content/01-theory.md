---
type: "THEORY"
title: "Immediate Local Updates"
---


**Optimistic UI** means updating the UI immediately based on expected success, rather than waiting for server confirmation.

**Why Optimistic?**
- Most operations succeed (>99%)
- Waiting feels slow to users
- Local-first data makes this natural

**The Pattern:**
1. User takes action
2. Update UI immediately (optimistic)
3. Sync to server in background
4. If sync fails, rollback or show error



```dart
class OptimisticNotesProvider extends ChangeNotifier {
  final NotesService _service;
  List<Note> _notes = [];
  final Map<String, Note> _pendingUpdates = {};
  
  List<Note> get notes => _notes;
  
  /// Optimistically add a note
  Future<void> addNote(String title, String content) async {
    // 1. Create optimistic note with temporary ID
    final tempId = 'temp_${DateTime.now().millisecondsSinceEpoch}';
    final optimisticNote = Note(
      localId: tempId,
      title: title,
      content: content,
      createdAt: DateTime.now(),
      syncStatus: SyncStatus.pending,
    );
    
    // 2. Add to list immediately
    _notes.insert(0, optimisticNote);
    _pendingUpdates[tempId] = optimisticNote;
    notifyListeners();
    
    try {
      // 3. Actually create note (writes to DB and queues sync)
      final realNote = await _service.createNote(title, content);
      
      // 4. Replace optimistic with real note
      final index = _notes.indexWhere((n) => n.localId == tempId);
      if (index != -1) {
        _notes[index] = realNote;
        _pendingUpdates.remove(tempId);
        notifyListeners();
      }
    } catch (e) {
      // 5. Rollback on failure
      _notes.removeWhere((n) => n.localId == tempId);
      _pendingUpdates.remove(tempId);
      notifyListeners();
      rethrow;
    }
  }
  
  /// Optimistically delete a note
  Future<void> deleteNote(Note note) async {
    // 1. Remove from list immediately
    final index = _notes.indexWhere((n) => n.localId == note.localId);
    if (index == -1) return;
    
    final removedNote = _notes.removeAt(index);
    notifyListeners();
    
    try {
      // 2. Actually delete
      await _service.deleteNote(note.localId);
    } catch (e) {
      // 3. Rollback - re-insert at same position
      _notes.insert(index, removedNote);
      notifyListeners();
      rethrow;
    }
  }
}
```
