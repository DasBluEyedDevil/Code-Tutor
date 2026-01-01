---
type: "THEORY"
title: "Type Inference"
---


Kotlin is smart—it can figure out types automatically! This is called **Type Inference**.

```kotlin
val name = "Alice"  // Kotlin knows this is a String
val age = 25        // Kotlin knows this is an Int
val weight = 70.5   // Kotlin knows this is a Double
```

**When to use explicit types**:
- When you want to ensure a specific type (e.g., `Long` instead of `Int`).
- When the type isn't immediately obvious from the value.
- For public APIs (in advanced development).

```kotlin
val distance: Long = 500  // Explicitly Long, even though 500 fits in Int
```

Most of the time, let Kotlin infer! It keeps your code clean and readable.

---



```kotlin
// Inference is clear
val count = 10  // Obviously Int

// Explicit might help readability
val result: Boolean = checkStatus()  // Makes intent clear
```
