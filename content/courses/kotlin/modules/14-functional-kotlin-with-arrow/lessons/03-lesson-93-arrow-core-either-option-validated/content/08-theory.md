---
type: "THEORY"
title: "Option - Explicit Nullability"
---


### What is Option?

`Option<A>` explicitly represents an optional value:
- `Some(value)` - value is present
- `None` - value is absent

### Why Option When Kotlin Has Nullable Types?

```kotlin
// Nullable types work great most of the time
val user: User? = findUser(id)
user?.let { println(it.name) }

// But Option provides:
// 1. Chaining with flatMap/map
// 2. Better interop with Either
// 3. No null ambiguity (is null intentional or a bug?)
```

---

