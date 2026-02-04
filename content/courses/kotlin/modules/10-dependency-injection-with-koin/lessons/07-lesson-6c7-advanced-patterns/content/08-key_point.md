---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Scoped dependencies with `scope<T>` limit instance lifetimes** to specific contexts (user sessions, feature flows). Use scopes to prevent singleton memory leaks for user-specific data.

**Lazy injection with `inject()` defers resolution** until first access, breaking circular dependencies and improving startup time. Use `by inject()` for properties that aren't always needed.

**Koin's DSL supports factories with parameters**: `factory { (id: String) -> DetailViewModel(id, get()) }`. Pass parameters at injection time: `get<DetailViewModel> { parametersOf("user-123") }`.
