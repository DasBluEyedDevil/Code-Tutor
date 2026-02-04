---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Generics enable type-safe reusable code** by parameterizing types. Write `class Box<T>(val value: T)` once and use it for any type, with full compile-time type checking instead of casting.

**Variance annotations control subtyping relationships**: `out T` for producers (covariant), `in T` for consumers (contravariant), and no annotation for mutable types (invariant). Understanding variance is key to designing flexible generic APIs.

**Use-site variance (`List<out Animal>`) is more flexible than declaration-site variance** when you need to override the default behavior for a specific use case. Kotlin supports both patterns for maximum expressiveness.
