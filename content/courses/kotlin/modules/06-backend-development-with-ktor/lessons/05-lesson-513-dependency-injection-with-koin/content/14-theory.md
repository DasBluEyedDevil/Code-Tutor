---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) `single` creates one instance (singleton), `factory` creates new instances each time**


Use `single` for stateless services (UserService, repositories).
Use `factory` for stateful objects (request data, messages).

---

**Question 2: B) Resolves a dependency from Koin**


Koin uses type inference to determine what to inject.

---

**Question 3: B) It allows swapping real implementations with mocks**


Tests use mock implementations without changing service code!

---

**Question 4: C) Resolving dependencies from Koin when first accessed**


This is more efficient than eager resolution.

---

**Question 5: B) When you need per-request or per-session instances**

**Singleton** (shared state):
- Database connections
- Configuration
- Stateless services

**Scoped** (isolated state):
- Request context (tenant ID, user session)
- Transaction boundaries
- Per-request caches

Multi-tenant applications are a perfect use case for scopes!

---



```kotlin
val userService by inject<UserService>()
// Lazy: userService is resolved when first accessed
// Subsequent accesses return the same instance (for singletons)
```
