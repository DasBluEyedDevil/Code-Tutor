---
type: "WARNING"
title: "Platform Considerations"
---

### Memory Management on iOS
```kotlin
// Always cancel scope when view disappears
class IOSWrapper {
    private val viewModel = AuthViewModel(repository)
    
    fun onAppear() {
        // Start collecting
    }
    
    fun onDisappear() {
        viewModel.onCleared() // Prevents memory leaks!
    }
}
```

### Main Dispatcher Differences
```kotlin
// Dispatchers.Main works differently per platform:
// - Android: Main looper
// - iOS: Main dispatch queue
// - JVM: Requires explicit setup or doesn't exist

// Safe approach in commonMain:
expect val mainDispatcher: CoroutineDispatcher

// androidMain
actual val mainDispatcher = Dispatchers.Main

// iosMain  
actual val mainDispatcher = Dispatchers.Main

// jvmMain (desktop/server)
actual val mainDispatcher = Dispatchers.Default // No UI thread
```

### Frozen State on iOS (Legacy)
With Kotlin/Native's old memory model (pre-1.7.20), objects shared across threads needed to be frozen. The new memory model (default since Kotlin 1.9, and the only option in Kotlin 2.0+) eliminates this entirely. If you're following this course with Kotlin 2.x, frozen state is not a concern.