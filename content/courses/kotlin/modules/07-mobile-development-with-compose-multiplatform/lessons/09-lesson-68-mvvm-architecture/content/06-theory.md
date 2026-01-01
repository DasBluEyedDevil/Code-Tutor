---
type: "THEORY"
title: "Dependency Injection with Hilt"
---


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
