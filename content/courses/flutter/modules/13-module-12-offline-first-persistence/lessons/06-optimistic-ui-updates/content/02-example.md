---
type: "EXAMPLE"
title: "Rollback on Failure"
---


**Graceful Rollback Pattern:**



```dart
class RollbackManager<T> {
  final List<_RollbackEntry<T>> _history = [];
  
  /// Save state before mutation
  void saveState(String operationId, T state) {
    _history.add(_RollbackEntry(
      operationId: operationId,
      state: state,
      timestamp: DateTime.now(),
    ));
    
    // Keep only recent entries
    if (_history.length > 100) {
      _history.removeAt(0);
    }
  }
  
  /// Rollback to previous state
  T? rollback(String operationId) {
    final entry = _history.firstWhereOrNull(
      (e) => e.operationId == operationId,
    );
    
    if (entry != null) {
      _history.remove(entry);
      return entry.state;
    }
    return null;
  }
  
  /// Clear entry on success
  void confirm(String operationId) {
    _history.removeWhere((e) => e.operationId == operationId);
  }
}

class _RollbackEntry<T> {
  final String operationId;
  final T state;
  final DateTime timestamp;
  
  _RollbackEntry({
    required this.operationId,
    required this.state,
    required this.timestamp,
  });
}

// Usage in provider
class NotesProvider extends ChangeNotifier {
  final RollbackManager<List<Note>> _rollback = RollbackManager();
  List<Note> _notes = [];
  
  Future<void> updateNote(Note note, String newTitle) async {
    final opId = 'update_${note.localId}_${DateTime.now().millisecondsSinceEpoch}';
    
    // Save current state
    _rollback.saveState(opId, List.from(_notes));
    
    // Apply optimistic update
    final index = _notes.indexWhere((n) => n.localId == note.localId);
    _notes[index] = note.copyWith(title: newTitle);
    notifyListeners();
    
    try {
      await _service.updateNote(note.localId, newTitle);
      _rollback.confirm(opId);
    } catch (e) {
      // Rollback to previous state
      final previousState = _rollback.rollback(opId);
      if (previousState != null) {
        _notes = previousState;
        notifyListeners();
      }
      rethrow;
    }
  }
}
```
