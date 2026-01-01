---
type: "THEORY"
title: "Why SQLDelight for KMP?"
---

### The Problem with Platform-Specific Solutions

**Android: Room**
```kotlin
// Room is excellent but Android-only
@Dao
interface NoteDao {
    @Query("SELECT * FROM notes")
    fun getAll(): List<NoteEntity>
}
```
- ❌ Only works on Android
- ❌ No iOS support
- ❌ Requires separate iOS implementation

**iOS: Core Data**
```swift
// Core Data is powerful but iOS/macOS only
let request = NSFetchRequest<Note>(entityName: "Note")
let notes = try context.fetch(request)
```
- ❌ Only works on Apple platforms
- ❌ Different paradigm (object graph vs SQL)
- ❌ Requires separate Android implementation

### SQLDelight: One Codebase, All Platforms

```kotlin
// In commonMain - works everywhere
class NoteRepository(private val database: AppDatabase) {
    fun getAllNotes(): List<Note> {
        return database.noteQueries.getAllNotes().executeAsList()
    }
}
```
- ✅ Same code for Android, iOS, Desktop, Web
- ✅ Type-safe at compile time
- ✅ SQL you already know
- ✅ Native SQLite performance on each platform