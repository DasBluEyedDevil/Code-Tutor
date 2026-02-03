---
type: "KEY_POINT"
title: "Sync vs Async Mental Model"
---

## Key Takeaways

- **Synchronous blocks the thread; asynchronous releases it** -- `Thread.Sleep(5000)` freezes your program for 5 seconds. `await Task.Delay(5000)` waits 5 seconds but lets the thread handle other work.

- **`async Task` marks methods as asynchronous** -- the `async` keyword enables `await` inside the method. Return `Task` (no result) or `Task<T>` (with result).

- **`Task.WhenAll()` runs operations in parallel** -- instead of awaiting three API calls sequentially (15 seconds total), `await Task.WhenAll(call1, call2, call3)` runs them simultaneously (5 seconds total).
