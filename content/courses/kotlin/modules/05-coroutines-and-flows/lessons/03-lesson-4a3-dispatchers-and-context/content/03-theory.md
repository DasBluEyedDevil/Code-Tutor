---
type: "THEORY"
title: "The Standard Dispatchers"
---

### Dispatchers.Default
- **Thread Pool Size**: Number of CPU cores (minimum 2)
- **Use For**: CPU-intensive work
- **Examples**: Sorting large lists, JSON parsing, complex calculations, image processing

```kotlin
launch(Dispatchers.Default) {
    val sorted = hugeList.sorted() // CPU-bound
    val parsed = Json.decodeFromString<Data>(jsonString)
}
```

### Dispatchers.IO
- **Thread Pool Size**: Up to 64 threads (or more based on demand)
- **Use For**: Blocking I/O operations
- **Examples**: File operations, network requests (blocking APIs), database queries

```kotlin
launch(Dispatchers.IO) {
    val file = File("data.txt").readText() // Blocking I/O
    val response = httpClient.get(url)      // Blocking network
}
```

### Dispatchers.Main
- **Thread**: Single main/UI thread
- **Use For**: UI updates
- **Platform-specific**: Requires platform setup (Android, iOS, Desktop)

```kotlin
launch(Dispatchers.Main) {
    textView.text = "Updated!"  // UI must be touched on Main
    _uiState.value = newState   // StateFlow updates
}
```

### Dispatchers.Unconfined
- **Behavior**: Starts in caller's thread, resumes in suspension point's thread
- **Use For**: Almost never - testing only
- **Warning**: Unpredictable thread behavior

```kotlin
// ⚠️ Avoid in production code
launch(Dispatchers.Unconfined) {
    println("Start: ${Thread.currentThread().name}") // Caller's thread
    delay(100)
    println("After: ${Thread.currentThread().name}") // Different thread!
}
```