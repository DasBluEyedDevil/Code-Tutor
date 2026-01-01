---
type: "THEORY"
title: "What Are Dispatchers?"
---

A **Dispatcher** determines which thread or thread pool a coroutine runs on:

```kotlin
launch(Dispatchers.Default) {
    // Runs on a shared pool optimized for CPU work
}

launch(Dispatchers.IO) {
    // Runs on a pool optimized for blocking I/O
}

launch(Dispatchers.Main) {
    // Runs on the main/UI thread (Android, iOS, Desktop)
}
```

Without a dispatcher, coroutines inherit from their parent scope:
```kotlin
CoroutineScope(Dispatchers.Main).launch {
    // Runs on Main
    launch {
        // Also runs on Main (inherited)
    }
}
```