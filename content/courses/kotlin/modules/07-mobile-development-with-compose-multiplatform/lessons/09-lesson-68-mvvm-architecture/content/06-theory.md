---
type: "THEORY"
title: "Dependency Injection with Hilt (Android-Only)"
---

> **Android-only:** Hilt is an Android DI framework built on Dagger. It does NOT work in `commonMain`. For cross-platform DI, use **Koin** (covered in Module 10: Dependency Injection with Koin).

### Setup

Add in `build.gradle.kts` (project level):


Add in `build.gradle.kts` (app level):


### Application Class


Update `AndroidManifest.xml`:


### Provide Dependencies


### Inject into ViewModel


### Use in Composable


---



```kotlin
// androidMain -- Hilt is Android only
import androidx.hilt.navigation.compose.hiltViewModel

@Composable
fun TasksScreen(
    viewModel: TasksViewModel = hiltViewModel()
) {
    val tasks by viewModel.tasks.collectAsState()

    LazyColumn {
        items(tasks) { task ->
            Text(task.title)
        }
    }
}
```
