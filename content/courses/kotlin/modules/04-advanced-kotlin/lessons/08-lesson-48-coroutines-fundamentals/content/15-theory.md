---
type: "THEORY"
title: "Summary"
---


Congratulations! You've learned the fundamentals of Kotlin coroutines. Here's what you covered:

✅ **Suspend Functions** - Building blocks of coroutines
✅ **Coroutine Builders** - `launch`, `async`, `runBlocking`
✅ **Coroutine Scope** - Lifecycle and structured concurrency
✅ **Coroutine Context** - Jobs, dispatchers, and configuration
✅ **Job & Deferred** - Managing coroutines and results
✅ **Common Patterns** - Parallel execution, timeouts, retries

### Key Takeaways

1. **Suspend functions** don't block threads - they suspend and resume
2. **Use `launch`** for fire-and-forget tasks
3. **Use `async`** when you need a result
4. **`Dispatchers.IO`** for I/O, `Dispatchers.Default` for CPU work
5. **Cancellation is cooperative** - use `delay()` or check `isActive`
6. **Structured concurrency** automatically manages child coroutines

### Next Steps

In the next lesson, we'll dive into **Advanced Coroutines** - exploring Flows for reactive streams, channels for communication, exception handling, and advanced patterns!

> **Note:** This lesson and the next (Lesson 4.3) provide a self-contained introduction to coroutines within the Advanced Kotlin module. **Module 5: Coroutines & Flows** revisits these concepts in much greater depth -- covering structured concurrency patterns, dispatchers, exception propagation, Flow operators, and StateFlow/SharedFlow for production applications. Think of these two lessons as your foundation; Module 5 is where you master coroutines.

---

**Practice Challenge**: Build a download manager that downloads multiple files concurrently, shows progress for each file, and allows cancelling individual downloads or all downloads at once.

