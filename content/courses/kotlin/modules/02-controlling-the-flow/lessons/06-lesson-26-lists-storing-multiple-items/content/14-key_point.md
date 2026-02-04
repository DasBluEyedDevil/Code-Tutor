---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Lists are Kotlin's primary ordered collection type**. Default to `listOf()` for immutability and use `mutableListOf()` only when you need to add, remove, or modify elements after creation.

**List indices are zero-based**: the first element is at index 0, the last at `size - 1`. Use `.last()` and `.first()` methods instead of manual indexing when accessing endpoints.

**Functional operations transform lists without mutation**. Methods like `map`, `filter`, `sorted`, and `distinct` return new lists, preserving the original—a key principle of functional programming that prevents bugs from unexpected state changes.
