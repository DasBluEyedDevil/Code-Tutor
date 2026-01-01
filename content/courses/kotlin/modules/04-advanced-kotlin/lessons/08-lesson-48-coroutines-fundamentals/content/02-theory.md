---
type: "THEORY"
title: "Topic Introduction"
---


Traditional programming is synchronous - your code waits for each operation to complete before moving to the next one. When dealing with slow operations like network requests, file I/O, or database queries, this leads to blocked threads and poor performance.

Coroutines are Kotlin's solution to asynchronous programming. They allow you to write asynchronous code that looks and behaves like synchronous code, making it much easier to understand and maintain.

In this lesson, you'll learn:
- What coroutines are and why they matter
- Suspend functions - the building blocks of coroutines
- Launching coroutines with `launch`, `async`, and `runBlocking`
- Coroutine scopes and contexts
- Job and Deferred for managing coroutines
- Basic patterns for async operations

By the end, you'll write efficient concurrent code that's as easy to read as sequential code!

---

