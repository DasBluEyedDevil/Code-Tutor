---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Lists are ordered collections** that can contain duplicate elements. Use `listOf()` for immutable lists (read-only) and `mutableListOf()` when you need to add or remove elements. Default to immutable lists unless mutability is required.

**Arrays have fixed size and better performance**, but Lists are more flexible and Kotlin-idiomatic for most use cases. Prefer Lists unless you're working with JVM interop or performance-critical code.

**Functional collection operations like `map`, `filter`, and `forEach`** are the Kotlin way to process collections. They're more expressive than loops and align with functional programming principles you'll use throughout advanced Kotlin.
