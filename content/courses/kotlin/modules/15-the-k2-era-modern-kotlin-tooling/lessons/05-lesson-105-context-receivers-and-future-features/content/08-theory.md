---
type: "THEORY"
title: "Future Kotlin Features"
---


### Upcoming Language Features

**1. Name-Based Destructuring (Under Consideration)**
```kotlin
// Current: position-based
val (first, second) = pair

// Future: name-based
val (x = first, y = second) = point
```

**2. Static Extensions (KEEP-348)**
```kotlin
// Add static methods to existing classes
fun String.Companion.randomAlphanumeric(length: Int): String

String.randomAlphanumeric(10)  // Static call
```

**3. Union Types (Discussion Phase)**
```kotlin
// Express "either A or B" without sealed classes
fun parse(input: String): Int | ParseError
```

**4. Collection Literals (Proposed)**
```kotlin
// Simpler collection creation
val list = [1, 2, 3]  // Instead of listOf(1, 2, 3)
val map = {"a": 1, "b": 2}  // Instead of mapOf("a" to 1, "b" to 2)
```

**5. Explicit Backing Fields (Under Development)**
```kotlin
class Counter {
    var count: Int = 0
        field = 0  // Explicit backing field
        set(value) {
            require(value >= 0)
            field = value
        }
}
```

---

