---
type: "THEORY"
title: "Exercise 1: Function Composition"
---


**Goal**: Implement function composition operators.

**Task**: Create `andThen` and `compose` operators for functions.


---



```kotlin
// TODO: Implement these
infix fun <A, B, C> ((A) -> B).andThen(other: (B) -> C): (A) -> C {
    // Your code here
}

infix fun <A, B, C> ((B) -> C).compose(other: (A) -> B): (A) -> C {
    // Your code here
}

fun main() {
    val trim: (String) -> String = { it.trim() }
    val uppercase: (String) -> String = { it.uppercase() }
    val addExclamation: (String) -> String = { "$it!" }

    // TODO: Test both operators
}
```
