---
type: "THEORY"
title: "Key Takeaways"
---


- `Raise<E>` provides imperative-style error handling with functional safety
- Use `raise.ensure` and `raise.ensureNotNull` for validation
- `raise.raise(error)` immediately fails with the error
- `either { }` provides `Raise<E>` context and returns `Either<E, A>`
- `raise.withError` maps between different error types
- `catch` converts exceptions to typed errors
- Context parameters (`-Xcontext-parameters`) are Beta since Kotlin 2.2; `either { }` blocks work without compiler flags

---

