---
type: "THEORY"
title: "Key Takeaways"
---


- `Raise<E>` provides imperative-style error handling with functional safety
- Use `ensure` and `ensureNotNull` for validation
- `raise(error)` immediately fails with the error
- `either { }` provides `Raise<E>` context and returns `Either<E, A>`
- `withError` maps between different error types
- `catch` converts exceptions to typed errors
- Context receivers are experimental; `either { }` blocks work reliably

---

