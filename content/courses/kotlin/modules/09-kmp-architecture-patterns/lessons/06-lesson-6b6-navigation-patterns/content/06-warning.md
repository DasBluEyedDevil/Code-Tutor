---
type: "WARNING"
title: "Navigation Best Practices"
---

### 1. Don't Navigate from ViewModel Directly

```kotlin
// ❌ ViewModel knows about NavController
class MyViewModel(private val navController: NavController) {
    fun onItemClick() {
        navController.navigate("detail") // Tight coupling!
    }
}

// ✅ ViewModel emits events, UI handles navigation
class MyViewModel {
    val navigationEvents: Flow<NavigationEvent>
    
    fun onItemClick() {
        emitEvent(GoToDetail)
    }
}
```

### 2. Handle Configuration Changes

```kotlin
// ❌ State lost on rotation
@Composable
fun Screen() {
    var showDialog by remember { mutableStateOf(false) }
}

// ✅ State in ViewModel survives
class ViewModel {
    private val _state = MutableStateFlow(State())
    // Dialog state preserved
}
```

### 3. Deep Link Support

```kotlin
// Define deep links alongside routes
composable(
    route = "note/{noteId}",
    deepLinks = listOf(
        navDeepLink { uriPattern = "myapp://note/{noteId}" }
    )
) { ... }
```