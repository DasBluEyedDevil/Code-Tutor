---
type: "EXAMPLE"
title: "Data Layer Implementation"
---

The data layer implements repository interfaces and handles data sources:

```kotlin
// ========== data/remote/NoteDto.kt ==========
// Data Transfer Object - matches API response
@Serializable
data class NoteDto(
    val id: String,
    val title: String,
    val content: String,
    @SerialName("created_at") val createdAt: Long,
    @SerialName("updated_at") val updatedAt: Long,
    @SerialName("is_pinned") val isPinned: Boolean
)

// ========== data/remote/NoteApi.kt ==========
interface NoteApi {
    suspend fun getAllNotes(): List<NoteDto>
    suspend fun getNoteById(id: String): NoteDto?
    suspend fun createNote(note: NoteDto): NoteDto
    suspend fun updateNote(note: NoteDto): NoteDto
    suspend fun deleteNote(id: String)
}

// ========== data/local/NoteDao.kt ==========
// Uses SQLDelight generated queries
class NoteDao(private val database: AppDatabase) {
    fun observeAll(): Flow<List<NoteEntity>> =
        database.noteQueries.getAllNotes().asFlow().mapToList(Dispatchers.Default)
    
    suspend fun getById(id: String): NoteEntity? =
        database.noteQueries.getNoteById(id).executeAsOneOrNull()
    
    suspend fun insert(note: NoteEntity) =
        database.noteQueries.insertNote(
            id = note.id,
            title = note.title,
            content = note.content,
            createdAt = note.createdAt,
            updatedAt = note.updatedAt,
            isPinned = note.isPinned
        )
}

// ========== data/repository/NoteRepositoryImpl.kt ==========
class NoteRepositoryImpl(
    private val noteDao: NoteDao,
    private val noteApi: NoteApi
) : NoteRepository {
    
    override fun observeAllNotes(): Flow<List<Note>> {
        return noteDao.observeAll().map { entities ->
            entities.map { it.toDomain() }
        }
    }
    
    override suspend fun getNoteById(id: String): Note? {
        return noteDao.getById(id)?.toDomain()
    }
    
    override suspend fun saveNote(note: Note) {
        noteDao.insert(note.toEntity())
    }
    
    override suspend fun deleteNote(id: String) {
        noteDao.delete(id)
    }
    
    override suspend fun searchNotes(query: String): List<Note> {
        return noteDao.search(query).map { it.toDomain() }
    }
}

// ========== data/mapper/NoteMappers.kt ==========
fun NoteEntity.toDomain() = Note(
    id = id,
    title = title,
    content = content,
    createdAt = createdAt,
    updatedAt = updatedAt,
    isPinned = isPinned == 1L
)

fun Note.toEntity() = NoteEntity(
    id = id,
    title = title,
    content = content,
    createdAt = createdAt,
    updatedAt = updatedAt,
    isPinned = if (isPinned) 1L else 0L
)
```
