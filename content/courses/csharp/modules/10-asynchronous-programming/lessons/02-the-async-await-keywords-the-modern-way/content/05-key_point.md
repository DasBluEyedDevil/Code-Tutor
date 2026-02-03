---
type: "KEY_POINT"
title: "Async/Await Patterns"
---

## Key Takeaways

- **Always await async calls** -- calling an async method without `await` fires and forgets it. Exceptions are silently swallowed and the operation may not complete before your code continues.

- **Async propagates up the call stack** -- if you call an async method, your method should be async too. This "async all the way" pattern prevents deadlocks and ensures proper error handling.

- **Return `Task<T>` when the method produces a value** -- inside the method, `return value` (not `return Task.FromResult(value)`). The compiler wraps the result in a `Task<T>` for you.
