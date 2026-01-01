---
type: "THEORY"
title: "When to Use Result"
---


### Good Use Cases

1. **Operations that commonly fail**: Parsing, I/O, network calls
2. **Public API boundaries**: Make errors explicit to callers
3. **Functional pipelines**: Chain operations cleanly
4. **When you want exhaustive handling**: Compiler helps catch unhandled cases

### When to Stick with Exceptions

1. **Programming errors**: NullPointerException, IndexOutOfBounds
2. **Unrecoverable situations**: OutOfMemory, StackOverflow
3. **Deep call stacks**: Result doesn't propagate automatically
4. **Library integration**: When libraries throw exceptions

### Best Practice

```kotlin
// Use exceptions at boundaries, Result internally
class MyService {
    // Public API can throw
    fun doWork(): Output {
        return doWorkSafe().getOrThrow()
    }
    
    // Internal operations use Result
    private fun doWorkSafe(): Result<Output> = runCatching {
        // ...
    }
}
```

---

