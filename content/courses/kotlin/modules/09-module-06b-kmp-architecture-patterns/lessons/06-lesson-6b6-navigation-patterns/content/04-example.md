---
type: "EXAMPLE"
title: "Compose Multiplatform Navigation"
---

Using Jetpack Navigation with Compose Multiplatform:

```kotlin
// ========== Navigation Setup ==========
@Composable
fun AppNavigation() {
    val navController = rememberNavController()
    
    NavHost(
        navController = navController,
        startDestination = "notes"
    ) {
        composable("notes") {
            val viewModel = remember { koinInject<NotesViewModel>() }
            NotesScreen(
                viewModel = viewModel,
                onNoteClick = { noteId -> navController.navigate("note/$noteId") },
                onAddNote = { navController.navigate("note/new") },
                onSettings = { navController.navigate("settings") }
            )
        }
        
        composable(
            route = "note/{noteId}",
            arguments = listOf(navArgument("noteId") { type = NavType.StringType })
        ) { backStackEntry ->
            val noteId = backStackEntry.arguments?.getString("noteId") ?: "new"
            val viewModel = remember { koinInject<NoteDetailViewModel>() }
            NoteDetailScreen(
                viewModel = viewModel,
                noteId = noteId,
                onBack = { navController.popBackStack() }
            )
        }
        
        composable("settings") {
            SettingsScreen(
                onBack = { navController.popBackStack() }
            )
        }
    }
}

// ========== Screen with Navigation Callbacks ==========
@Composable
fun NotesScreen(
    viewModel: NotesViewModel,
    onNoteClick: (String) -> Unit,
    onAddNote: () -> Unit,
    onSettings: () -> Unit
) {
    val state by viewModel.state.collectAsState()
    
    Scaffold(
        topBar = {
            TopAppBar(
                title = { Text("Notes") },
                actions = {
                    IconButton(onClick = onSettings) {
                        Icon(Icons.Default.Settings, "Settings")
                    }
                }
            )
        },
        floatingActionButton = {
            FloatingActionButton(onClick = onAddNote) {
                Icon(Icons.Default.Add, "Add Note")
            }
        }
    ) { padding ->
        LazyColumn(modifier = Modifier.padding(padding)) {
            items(state.notes) { note ->
                NoteCard(
                    note = note,
                    onClick = { onNoteClick(note.id) }
                )
            }
        }
    }
}
```
