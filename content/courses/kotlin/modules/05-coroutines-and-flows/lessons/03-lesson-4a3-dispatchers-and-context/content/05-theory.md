---
type: "THEORY"
title: "Platform-Specific Dispatchers in KMP"
---

### The Problem
`Dispatchers.Main` needs platform-specific setup:
- Android: Works automatically
- iOS: Needs `kotlinx-coroutines-core` native
- JVM: No Main dispatcher by default

### Solution: Platform-Specific Expect/Actual

```kotlin
// In commonMain
expect val mainDispatcher: CoroutineDispatcher

// In androidMain
actual val mainDispatcher: CoroutineDispatcher = Dispatchers.Main

// In iosMain
actual val mainDispatcher: CoroutineDispatcher = Dispatchers.Main

// In jvmMain (for desktop/server)
actual val mainDispatcher: CoroutineDispatcher = Dispatchers.Default
```

### KMP ViewModel Pattern
```kotlin
// In commonMain
open class BaseViewModel {
    protected val scope = CoroutineScope(mainDispatcher + SupervisorJob())
    
    protected fun launchOnMain(block: suspend CoroutineScope.() -> Unit) {
        scope.launch(mainDispatcher, block = block)
    }
    
    protected fun launchOnIO(block: suspend CoroutineScope.() -> Unit) {
        scope.launch(Dispatchers.Default, block = block) // IO doesn't exist on all platforms
    }
    
    fun onCleared() {
        scope.cancel()
    }
}
```