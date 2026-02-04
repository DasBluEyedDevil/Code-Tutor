---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Coroutines are lightweight concurrency primitives** that enable writing asynchronous code in a sequential style. Unlike threads, you can launch millions of coroutines without exhausting system resources.

**The `suspend` keyword marks functions that can pause execution** without blocking the underlying thread. This is the foundation of Kotlin's approach to async programming—suspension instead of callbacks or futures.

**Never use `GlobalScope.launch` in production code**—it creates unmanaged coroutines that can leak. Always launch coroutines within a structured scope that controls their lifetime and handles cancellation.
