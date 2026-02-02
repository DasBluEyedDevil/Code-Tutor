---
type: "EXAMPLE"
title: "Domain Layer Implementation"
---

The domain layer contains your core business logic:

```kotlin
// ========== domain/model/Note.kt ==========
// Pure domain entity - no framework dependencies
data class Note(
    val id: String,
    val title: String,
    val content: String,
    val createdAt: Long,
    val updatedAt: Long,
    val isPinned: Boolean = false
) {
    // Domain logic can live here
    fun isRecent(): Boolean {
        val oneDayAgo = Clock.System.now().toEpochMilliseconds() - 86_400_000
        return updatedAt > oneDayAgo
    }
}

// ========== domain/repository/NoteRepository.kt ==========
// Interface - implementation details hidden
interface NoteRepository {
    fun observeAllNotes(): Flow<List<Note>>
    suspend fun getNoteById(id: String): Note?
    suspend fun saveNote(note: Note)
    suspend fun deleteNote(id: String)
    suspend fun searchNotes(query: String): List<Note>
}

// ========== domain/usecase/GetNotesUseCase.kt ==========
// Encapsulates a single business operation
class GetNotesUseCase(
    private val noteRepository: NoteRepository
) {
    operator fun invoke(sortBy: SortOption = SortOption.UPDATED): Flow<List<Note>> {
        return noteRepository.observeAllNotes().map { notes ->
            when (sortBy) {
                SortOption.UPDATED -> notes.sortedByDescending { it.updatedAt }
                SortOption.CREATED -> notes.sortedByDescending { it.createdAt }
                SortOption.TITLE -> notes.sortedBy { it.title }
                SortOption.PINNED_FIRST -> notes.sortedByDescending { it.isPinned }
            }
        }
    }
}

enum class SortOption { UPDATED, CREATED, TITLE, PINNED_FIRST }
```
