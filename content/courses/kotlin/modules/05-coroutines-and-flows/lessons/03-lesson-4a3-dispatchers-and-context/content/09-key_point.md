---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Dispatchers determine which thread pool executes your coroutine**: `Dispatchers.Default` for CPU work (sized to CPU cores), `Dispatchers.IO` for blocking operations (expandable pool), `Dispatchers.Main` for UI updates (platform main thread).

**Use `withContext` to switch dispatchers mid-coroutine** for operations requiring different execution contexts. Write `withContext(Dispatchers.IO) { readFile() }` to perform blocking I/O without tying up the default pool.

**CoroutineContext is a composable set of elements** including dispatcher, job, exception handler, and custom elements. Contexts merge when launching child coroutines, inheriting parent configuration with override capability.
