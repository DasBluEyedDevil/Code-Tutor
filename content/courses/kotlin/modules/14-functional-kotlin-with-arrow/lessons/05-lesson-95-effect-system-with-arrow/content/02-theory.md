---
type: "THEORY"
title: "Topic Introduction"
---


Arrow's `Raise<E>` DSL provides a powerful effect system for error handling. Instead of returning `Either<E, A>` from every function, you declare that a function operates within a `Raise<E>` context -- making error handling feel like imperative code while maintaining functional safety.

In this lesson, you'll learn:
- Understanding `Raise<E>` for effect-based error handling
- Using `ensure` and `ensureNotNull`
- Composing effects with the `either { }` builder
- Integrating effects with coroutines

---

