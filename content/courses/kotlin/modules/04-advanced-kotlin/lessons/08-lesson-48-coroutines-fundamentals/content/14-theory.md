---
type: "THEORY"
title: "Checkpoint Quiz"
---


### Question 1: Suspend Functions

What is true about suspend functions?

**A)** They always run on a background thread
**B)** They can only be called from coroutines or other suspend functions
**C)** They block the calling thread
**D)** They must always use delay()

**Answer**: **B** - Suspend functions can only be called from coroutines or other suspend functions. They don't necessarily run on background threads and don't block threads.

---

### Question 2: Coroutine Builders

What's the difference between `launch` and `async`?

**A)** `launch` returns a result, `async` doesn't
**B)** `launch` is for sequential code, `async` for concurrent
**C)** `launch` returns Job (no result), `async` returns Deferred (with result)
**D)** They are identical

**Answer**: **C** - `launch` returns a `Job` for fire-and-forget tasks, while `async` returns a `Deferred<T>` that can provide a result via `await()`.

---

### Question 3: Dispatchers

Which dispatcher should you use for network requests?

**A)** Dispatchers.Default
**B)** Dispatchers.Main
**C)** Dispatchers.IO
**D)** Dispatchers.Unconfined

**Answer**: **C** - `Dispatchers.IO` is optimized for I/O operations like network requests, file operations, and database queries.

---

### Question 4: Cancellation

Why doesn't this coroutine cancel properly?


**A)** Missing job.join()
**B)** Thread.sleep doesn't check for cancellation
**C)** while(true) prevents cancellation
**D)** launch doesn't support cancellation

**Answer**: **B** - `Thread.sleep()` doesn't check for cancellation. Use `delay()` or check `isActive` in the loop.

---

### Question 5: Structured Concurrency

What happens when a parent coroutine is cancelled?

**A)** Child coroutines continue running
**B)** Only the parent is cancelled
**C)** All child coroutines are automatically cancelled
**D)** An exception is thrown

**Answer**: **C** - Structured concurrency ensures that when a parent coroutine is cancelled, all its children are automatically cancelled too.

---



```kotlin
val job = launch {
    while (true) {
        Thread.sleep(500)
        println("Working")
    }
}
job.cancel()
```
