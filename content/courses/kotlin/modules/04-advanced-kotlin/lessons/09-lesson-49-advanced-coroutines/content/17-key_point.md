---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Dispatchers determine which thread pool executes your coroutine**: `Dispatchers.Default` for CPU-intensive work, `Dispatchers.IO` for blocking I/O, and `Dispatchers.Main` for UI updates. Choose correctly to avoid blocking critical threads.

**Channels enable communication between coroutines** with structured data flow. They're Kotlin's answer to actor-model concurrency, allowing safe state sharing through message passing instead of locks.

**Error handling in coroutines uses try-catch plus CoroutineExceptionHandler** for uncaught exceptions. Structured concurrency propagates errors up the scope hierarchy, ensuring failures don't go silent.
