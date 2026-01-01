---
type: "THEORY"
title: "Exercise 2: Custom List Filter"
---


**Goal**: Build a reusable filter function for lists.

**Requirements**:
1. Create a function `filterList` that takes a list and a predicate function
2. The predicate determines which elements to keep
3. Test with different predicates (even numbers, > 10, etc.)

**Starter Code**:

---



```kotlin
fun filterList(list: List<Int>, predicate: (Int) -> Boolean): List<Int> {
    // TODO: Implement
}

fun main() {
    val numbers = listOf(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)
    // TODO: Filter with different predicates
}
```
