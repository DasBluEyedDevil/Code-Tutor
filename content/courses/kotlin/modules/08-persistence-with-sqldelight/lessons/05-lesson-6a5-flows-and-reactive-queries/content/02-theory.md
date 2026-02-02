---
type: "THEORY"
title: "The Reactive Pattern"
---

### Traditional Approach (Pull)

```kotlin
// Manually refresh when you think data might have changed
fun loadNotes() {
    val notes = repository.getAllNotes()
    _uiState.value = notes
}

// Call this everywhere data might change:
fun addNote(note: Note) {
    repository.addNote(note)
    loadNotes() // Don't forget to refresh!
}

fun deleteNote(id: Long) {
    repository.deleteNote(id)
    loadNotes() // Don't forget again!
}
```

**Problems:**
- Easy to forget to refresh
- Multiple sources can modify data
- UI can get out of sync

### Reactive Approach (Push)

```kotlin
// Observe once, updates come automatically
init {
    viewModelScope.launch {
        repository.observeAllNotes().collect { notes ->
            _uiState.value = notes
        }
    }
}

// Just modify data - UI updates automatically!
fun addNote(note: Note) = repository.addNote(note)
fun deleteNote(id: Long) = repository.deleteNote(id)
```

**Benefits:**
- UI always shows current data
- No manual refresh needed
- Works with any data source