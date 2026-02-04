---
type: "EXAMPLE"
title: "Code Breakdown"
---


### Dependency Resolution Flow


### get() Function

The `get()` function resolves dependencies:


Type inference determines what to inject based on parameter types.

### by inject<T>() Delegate


This is a **lazy delegate**:
- `userService` is resolved when first accessed (lazy)
- Subsequent accesses return the same instance (for singletons)
- Type-safe (compile-time checking)

---



```kotlin
val userService by inject<UserService>()
```
