---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Properties in Kotlin are not fields—they're encapsulated by getters and setters** that execute when you read or write the property. This allows you to add validation, logging, or computed values without changing the public API.

**Use `init` blocks for validation and complex initialization logic** that can't be expressed in property declarations. The `init` block runs immediately after the primary constructor, giving you a place to enforce invariants.

**Backing fields with `field` keyword enable custom getters/setters** while storing data. Write `set(value) { field = value.trim() }` to normalize data on assignment without infinite recursion.
