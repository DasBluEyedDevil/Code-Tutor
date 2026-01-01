---
type: "WARNING"
title: "Common FP Pitfalls"
---


### Over-Engineering

```kotlin
// TOO ABSTRACT - hard to read
val result = input
    .let(::trim)
    .let(::validate)
    .let(::transform)
    .let(::format)

// BETTER - clear and simple
val trimmed = input.trim()
val validated = validate(trimmed)
val transformed = transform(validated)
val result = format(transformed)
```

### Ignoring Performance

```kotlin
// CREATES MANY INTERMEDIATE LISTS
list
    .filter { it > 0 }      // New list
    .map { it * 2 }         // New list
    .filter { it < 100 }    // New list
    .toList()

// BETTER - use sequences for large lists
list.asSequence()
    .filter { it > 0 }
    .map { it * 2 }
    .filter { it < 100 }
    .toList()  // Single list created
```

---

