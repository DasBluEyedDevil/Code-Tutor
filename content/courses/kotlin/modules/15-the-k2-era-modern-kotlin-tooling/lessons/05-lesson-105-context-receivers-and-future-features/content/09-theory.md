---
type: "THEORY"
title: "Future Kotlin Features"
---


### Upcoming and Recent Language Features

**1. Collection Literals (Kotlin 2.1+)**
```kotlin
// Simpler collection creation
val list = [1, 2, 3]  // Instead of listOf(1, 2, 3)
val map = ["a": 1, "b": 2]  // Instead of mapOf("a" to 1, "b" to 2)
```

**2. Name-Based Destructuring (Under Consideration)**
```kotlin
// Current: position-based
val (first, second) = pair

// Future: name-based
val (x = first, y = second) = point
```

**3. Static Extensions (KEEP-348)**
```kotlin
// Add static methods to existing classes
fun String.Companion.randomAlphanumeric(length: Int): String

String.randomAlphanumeric(10)  // Static call
```

**4. Union Types (Discussion Phase)**
```kotlin
// Express "either A or B" without sealed classes
fun parse(input: String): Int | ParseError
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

