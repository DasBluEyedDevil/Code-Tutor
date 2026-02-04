---
type: "THEORY"
title: "Key Takeaways"
---


- `Either<E, A>` provides typed error handling with explicit error types
- Use `either { }` builder with `bind()` for clean chaining
- `zipOrAccumulate` collects all errors (use for form validation)
- `EitherNel<E, A>` is a typealias for `Either<NonEmptyList<E>, A>`
- `Option<A>` makes optionality explicit
- Combine validation with processing for robust error handling
- Arrow Core 2.2.x is the current stable release

---

