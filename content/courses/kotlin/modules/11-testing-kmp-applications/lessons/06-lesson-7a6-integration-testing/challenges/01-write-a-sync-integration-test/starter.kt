// Sync manager that fetches from API and saves to DB
class NotesSyncManager(
    private val api: NotesApiClient,
    private val repository: NoteRepository
) {
    suspend fun sync(): Result<Int> = runCatching {
        val remoteNotes = api.fetchNotes().getOrThrow()
        
        remoteNotes.forEach { noteDto ->
            val existingNote = repository.getById(noteDto.id)
            if (existingNote == null) {
                repository.insert(noteDto.toNote())
            } else if (noteDto.updatedAt > existingNote.updatedAt) {
                repository.update(noteDto.toNote())
            }
        }
        
        remoteNotes.size
    }
}

// TODO: Write integration tests for:
// 1. Sync adds new notes to empty database
// 2. Sync updates existing notes if remote is newer
// 3. Sync doesn't overwrite if local is newer
// 4. Sync handles API errors gracefully