---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Effect systems make side effects explicit in types**, tracking operations that do I/O, mutate state, or throw exceptions. Arrow's Raise captures errors in types, ensuring callers handle them.

**`context(raise: Raise<E>)` marks functions that can raise typed errors**. The context parameter requirement forces callers to provide error handling at compile-time—errors can't be ignored.

**Effect systems provide structured error handling** without exception overhead. Errors propagate through return types, not call stacks, making error flows explicit and composable.
