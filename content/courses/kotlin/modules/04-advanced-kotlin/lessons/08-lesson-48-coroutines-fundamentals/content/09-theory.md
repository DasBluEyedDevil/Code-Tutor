---
type: "THEORY"
title: "Coroutine Context"
---


Every coroutine has a context that includes:
- **Job** - manages lifecycle
- **Dispatcher** - determines which thread(s) to use
- **CoroutineName** - for debugging
- **Exception handler** - handles errors

### Dispatchers

Dispatchers determine which thread pool a coroutine runs on:


**Common Dispatchers**:
- `Dispatchers.Default` - CPU-intensive tasks (sorting, calculations)
- `Dispatchers.IO` - I/O operations (network, database, files)
- `Dispatchers.Main` - UI updates (Android, JavaFX)
- `Dispatchers.Unconfined` - not confined to specific thread

### Switching Contexts with `withContext`


---



```kotlin
suspend fun fetchAndProcess() = withContext(Dispatchers.IO) {
    // Fetch data on IO dispatcher
    val data = fetchDataFromNetwork()

    withContext(Dispatchers.Default) {
        // Process on Default dispatcher
        processData(data)
    }
}

suspend fun fetchDataFromNetwork(): String {
    delay(1000)
    return "Network data"
}

suspend fun processData(data: String): String {
    delay(500)
    return "Processed: $data"
}

fun main() = runBlocking {
    val result = fetchAndProcess()
    println(result)
}
```
