---
type: "THEORY"
title: "Collecting in Compose"
---

### collectAsState
```kotlin
@Composable
fun CounterScreen(viewModel: CounterViewModel) {
    val count by viewModel.count.collectAsState()
    
    Column {
        Text("Count: $count")
        Button(onClick = { viewModel.increment() }) {
            Text("+1")
        }
    }
}
```

### collectAsStateWithLifecycle (Recommended)
```kotlin
import androidx.lifecycle.compose.collectAsStateWithLifecycle

@Composable
fun CounterScreen(viewModel: CounterViewModel) {
    val count by viewModel.count.collectAsStateWithLifecycle()
    
    // Same usage, but respects lifecycle
    Text("Count: $count")
}
```

### Handling Events with LaunchedEffect
```kotlin
@Composable
fun ProfileScreen(viewModel: ProfileViewModel) {
    val context = LocalContext.current
    
    // Collect events in a lifecycle-aware way
    LaunchedEffect(Unit) {
        viewModel.events.collect { event ->
            when (event) {
                is ProfileEvent.ShowToast -> {
                    Toast.makeText(context, event.message, Toast.LENGTH_SHORT).show()
                }
                is ProfileEvent.Navigate -> {
                    navController.navigate(event.route)
                }
            }
        }
    }
}
```