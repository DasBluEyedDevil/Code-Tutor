---
type: "EXAMPLE"
title: "Complete Feature: Notes List"
---

Full implementation from data to UI:

```kotlin
// ========== domain/model/Note.kt ==========
data class Note(
    val id: String,
    val title: String,
    val content: String,
    val createdAt: Long,
    val updatedAt: Long,
    val isPinned: Boolean = false
)

// ========== domain/repository/NoteRepository.kt ==========
interface NoteRepository {
    fun observeAllNotes(): Flow<List<Note>>
    suspend fun getNoteById(id: String): Note?
    suspend fun saveNote(note: Note)
    suspend fun deleteNote(id: String)
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
        noteDao.upsert(note.toEntity())
    }
    
    override suspend fun deleteNote(id: String) {
        noteDao.delete(id)
    }
}

// ========== presentation/viewmodel/NotesViewModel.kt ==========
class NotesViewModel(
    private val noteRepository: NoteRepository
) : BaseViewModel() {
    
    private val _state = MutableStateFlow(NotesUiState())
    val state: StateFlow<NotesUiState> = _state.asStateFlow()
    
    init {
        loadNotes()
    }
    
    private fun loadNotes() {
        scope.launch {
            _state.update { it.copy(isLoading = true) }
            
            noteRepository.observeAllNotes()
                .catch { e ->
                    _state.update { it.copy(
                        error = e.message ?: "Unknown error",
                        isLoading = false
                    )}
                }
                .collect { notes ->
                    val sorted = notes.sortedWith(
                        compareByDescending<Note> { it.isPinned }
                            .thenByDescending { it.updatedAt }
                    )
                    _state.update { it.copy(
                        notes = sorted.map { note -> note.toUiModel() },
                        isLoading = false
                    )}
                }
        }
    }
    
    fun deleteNote(noteId: String) {
        scope.launch {
            noteRepository.deleteNote(noteId)
        }
    }
    
    fun togglePin(noteId: String) {
        scope.launch {
            val note = noteRepository.getNoteById(noteId) ?: return@launch
            noteRepository.saveNote(note.copy(
                isPinned = !note.isPinned,
                updatedAt = Clock.System.now().toEpochMilliseconds()
            ))
        }
    }
}

// ========== ui/screens/NotesScreen.kt ==========
@Composable
fun NotesScreen(
    viewModel: NotesViewModel,
    onNoteClick: (String) -> Unit,
    onAddClick: () -> Unit
) {
    val state by viewModel.state.collectAsState()
    
    Scaffold(
        topBar = {
            TopAppBar(title = { Text("My Notes") })
        },
        floatingActionButton = {
            FloatingActionButton(onClick = onAddClick) {
                Icon(Icons.Default.Add, "Add Note")
            }
        }
    ) { padding ->
        Box(modifier = Modifier.padding(padding).fillMaxSize()) {
            when {
                state.isLoading -> {
                    CircularProgressIndicator(
                        modifier = Modifier.align(Alignment.Center)
                    )
                }
                state.error != null -> {
                    Text(
                        text = state.error!!,
                        color = MaterialTheme.colorScheme.error,
                        modifier = Modifier.align(Alignment.Center)
                    )
                }
                state.notes.isEmpty() -> {
                    Text(
                        text = "No notes yet. Tap + to create one!",
                        modifier = Modifier.align(Alignment.Center)
                    )
                }
                else -> {
                    LazyColumn {
                        items(state.notes, key = { it.id }) { note ->
                            NoteCard(
                                note = note,
                                onClick = { onNoteClick(note.id) },
                                onDelete = { viewModel.deleteNote(note.id) },
                                onTogglePin = { viewModel.togglePin(note.id) },
                                modifier = Modifier.animateItem()
                            )
                        }
                    }
                }
            }
        }
    }
}
```
