---
type: "WARNING"
title: "Common Dispatcher Mistakes"
---

### Mistake 1: CPU work on IO
```kotlin
// ❌ Wrong - sorting is CPU, not IO
withContext(Dispatchers.IO) {
    list.sorted()
}

// ✅ Right - use Default for CPU work
withContext(Dispatchers.Default) {
    list.sorted()
}
```

### Mistake 2: UI updates off Main
```kotlin
// ❌ Crash - UI touched on wrong thread
withContext(Dispatchers.IO) {
    textView.text = "Loaded"
}

// ✅ Right - UI on Main
withContext(Dispatchers.Main) {
    textView.text = "Loaded"
}
```

### Mistake 3: Blocking Main thread
```kotlin
// ❌ Blocks UI, causes ANR
launch(Dispatchers.Main) {
    val data = File("large.txt").readText() // Blocking call!
    textView.text = data
}

// ✅ Right - read on IO, update on Main
launch(Dispatchers.Main) {
    val data = withContext(Dispatchers.IO) {
        File("large.txt").readText()
    }
    textView.text = data
}
```

### Mistake 4: Using Dispatchers.IO on iOS
```kotlin
// ❌ Dispatchers.IO may not exist on all platforms
withContext(Dispatchers.IO) { ... }

// ✅ Use Default in commonMain for portability
withContext(Dispatchers.Default) { ... }
```