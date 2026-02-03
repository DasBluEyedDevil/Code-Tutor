---
type: "THEORY"
title: "Key Takeaways"
---


- `Either<E, A>` provides typed error handling with explicit error types
- Use `either { }` builder with `bind()` for clean chaining
- Error accumulation collects all errors (use for form validation)
- `Option<A>` makes optionality explicit
- Combine validation with processing for robust error handling
- Arrow Core 2.2.1 is the latest stable release

> **Arrow 2.x Update**: `Validated` has been replaced by `EitherNel` (typealias for `Either<NonEmptyList<E>, A>`) and the `zipOrAccumulate`/`mapOrAccumulate` functions in the `Raise` DSL. The concepts are identical — only the API surface changed.

---

