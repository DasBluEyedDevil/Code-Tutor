---
type: "THEORY"
title: "Type-Safe Navigation Arguments"
---

### Avoiding String-Based Routes

```kotlin
// ❌ String-based - error prone
navController.navigate("note/$noteId")
navController.navigate("note/" + noteId) // Different?

// ✅ Type-safe routes
sealed class Screen(val route: String) {
    data object Notes : Screen("notes")
    data class NoteDetail(val noteId: String) : Screen("note/$noteId") {
        companion object {
            const val ROUTE = "note/{noteId}"
        }
    }
    data object Settings : Screen("settings")
}

// Usage
navController.navigate(Screen.NoteDetail(noteId).route)
```

### With Kotlin Serialization (Recommended)

```kotlin
@Serializable
sealed interface Route {
    @Serializable
    data object Notes : Route
    
    @Serializable
    data class NoteDetail(val noteId: String) : Route
    
    @Serializable
    data object Settings : Route
}

// Navigation Compose 2.8+ supports this directly
NavHost(startDestination = Route.Notes) {
    composable<Route.Notes> { NotesScreen(...) }
    composable<Route.NoteDetail> { backStackEntry ->
        val route: Route.NoteDetail = backStackEntry.toRoute()
        NoteDetailScreen(noteId = route.noteId)
    }
}
```