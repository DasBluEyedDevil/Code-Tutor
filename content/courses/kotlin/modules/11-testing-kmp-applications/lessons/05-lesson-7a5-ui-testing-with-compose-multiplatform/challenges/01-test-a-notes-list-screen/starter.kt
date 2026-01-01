@Composable
fun NotesListScreen(
    viewModel: NotesListViewModel,
    onNoteClick: (Note) -> Unit
) {
    val state by viewModel.state.collectAsState()
    
    Column {
        SearchBar(
            query = state.searchQuery,
            onQueryChange = viewModel::search,
            modifier = Modifier.testTag("search_bar")
        )
        
        when {
            state.isLoading -> {
                CircularProgressIndicator(
                    modifier = Modifier.testTag("loading")
                )
            }
            state.notes.isEmpty() -> {
                Text(
                    "No notes found",
                    modifier = Modifier.testTag("empty_state")
                )
            }
            else -> {
                LazyColumn {
                    items(state.notes) { note ->
                        NoteItem(
                            note = note,
                            onClick = { onNoteClick(note) },
                            modifier = Modifier.testTag("note_${note.id}")
                        )
                    }
                }
            }
        }
    }
}

// TODO: Write tests for:
// 1. Shows loading indicator while loading
// 2. Shows empty state when no notes
// 3. Displays notes when available
// 4. Search filters notes
// 5. Clicking note calls onNoteClick with correct note