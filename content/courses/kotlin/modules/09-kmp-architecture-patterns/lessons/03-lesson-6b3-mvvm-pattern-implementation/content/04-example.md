---
type: "EXAMPLE"
title: "Connecting ViewModel to Compose UI"
---

Here's how to use the ViewModel in Compose Multiplatform:

```kotlin
@Composable
fun NotesScreen(
    viewModel: NotesViewModel,
    onNoteClick: (String) -> Unit
) {
    val state by viewModel.state.collectAsState()
    
    Column(modifier = Modifier.fillMaxSize()) {
        // Filter chips
        FilterChips(
            selected = state.selectedFilter,
            onFilterSelected = viewModel::onFilterChanged
        )
        
        // Content based on state
        when {
            state.isLoading -> {
                Box(Modifier.fillMaxSize(), contentAlignment = Alignment.Center) {
                    CircularProgressIndicator()
                }
            }
            state.error != null -> {
                ErrorMessage(
                    message = state.error!!,
                    onDismiss = viewModel::clearError
                )
            }
            state.notes.isEmpty() -> {
                EmptyState(message = "No notes yet")
            }
            else -> {
                NotesList(
                    notes = state.notes,
                    onNoteClick = onNoteClick,
                    onDeleteNote = viewModel::onDeleteNote,
                    onTogglePin = viewModel::onTogglePin
                )
            }
        }
    }
}

@Composable
fun NotesList(
    notes: List<NoteUiModel>,
    onNoteClick: (String) -> Unit,
    onDeleteNote: (String) -> Unit,
    onTogglePin: (String) -> Unit
) {
    LazyColumn {
        items(notes, key = { it.id }) { note ->
            NoteCard(
                note = note,
                onClick = { onNoteClick(note.id) },
                onDelete = { onDeleteNote(note.id) },
                onTogglePin = { onTogglePin(note.id) }
            )
        }
    }
}

@Composable
fun NoteCard(
    note: NoteUiModel,
    onClick: () -> Unit,
    onDelete: () -> Unit,
    onTogglePin: () -> Unit
) {
    Card(
        modifier = Modifier
            .fillMaxWidth()
            .padding(8.dp)
            .clickable(onClick = onClick)
    ) {
        Row(
            modifier = Modifier.padding(16.dp),
            verticalAlignment = Alignment.CenterVertically
        ) {
            Column(modifier = Modifier.weight(1f)) {
                Row(verticalAlignment = Alignment.CenterVertically) {
                    Text(
                        text = note.title,
                        style = MaterialTheme.typography.titleMedium
                    )
                    if (note.isPinned) {
                        Icon(
                            Icons.Default.PushPin,
                            contentDescription = "Pinned",
                            modifier = Modifier.size(16.dp)
                        )
                    }
                }
                Text(
                    text = note.preview,
                    style = MaterialTheme.typography.bodyMedium,
                    color = MaterialTheme.colorScheme.onSurfaceVariant
                )
                Text(
                    text = note.formattedDate,
                    style = MaterialTheme.typography.bodySmall
                )
            }
            IconButton(onClick = onTogglePin) {
                Icon(
                    if (note.isPinned) Icons.Filled.PushPin else Icons.Outlined.PushPin,
                    contentDescription = "Toggle pin"
                )
            }
            IconButton(onClick = onDelete) {
                Icon(Icons.Default.Delete, contentDescription = "Delete")
            }
        }
    }
}
```
