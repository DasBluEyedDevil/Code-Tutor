---
type: "THEORY"
title: "Introduction"
---

**Estimated Time**: 75 minutes
**Difficulty**: Advanced
**Prerequisites**: Parts 1-3, Lesson 4.1 (Generics)

Welcome to one of Kotlin's most powerful features: **Coroutines**! Coroutines are Kotlin's solution for writing asynchronous, non-blocking code that is both efficient and easy to read.

Traditional threading is expensive (each thread consumes ~1MB of memory) and hard to manage. Coroutines are lightweight (you can run thousands on a single thread) and provide structured concurrency that prevents common bugs.

In this lesson, you'll learn:

1. **What coroutines are** and why they matter for modern applications
2. **Suspend functions**: Functions that can pause and resume without blocking
3. **Coroutine builders**: `launch`, `async`, and `runBlocking`
4. **Coroutine scopes**: Managing the lifecycle of coroutines
5. **Dispatchers**: Controlling which thread(s) coroutines run on

By the end, you'll be able to write concurrent code that's both performant and maintainable.

---
