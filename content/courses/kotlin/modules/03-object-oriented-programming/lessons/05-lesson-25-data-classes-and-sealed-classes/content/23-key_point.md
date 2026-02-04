---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Data classes automatically generate `equals()`, `hashCode()`, `toString()`, and `copy()`** based on primary constructor properties. Use them for simple value holders to eliminate boilerplate and ensure consistent behavior.

**Sealed classes restrict inheritance to a known set of subclasses**, enabling exhaustive `when` expressions. This is Kotlin's alternative to enums for modeling state machines and discriminated unions with associated data.

**Use `copy()` for immutable updates** to data classes. Instead of mutating a user, write `val updated = user.copy(age = 31)` to create a modified version while preserving the original—a cornerstone of functional programming.
