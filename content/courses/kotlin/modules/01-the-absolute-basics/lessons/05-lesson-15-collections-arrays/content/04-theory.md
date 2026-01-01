---
type: "THEORY"
title: "Lists"
---


Lists are ordered collections that can contain duplicates.

### Read-Only Lists (listOf)


### Accessing List Elements


### Mutable Lists (mutableListOf)


### List Operations


---



```kotlin
val numbers = listOf(1, 2, 3, 4, 5)

// Check if contains
println(numbers.contains(3))     // true
println(3 in numbers)            // true (same thing)
println(10 in numbers)           // false

// Get index
println(numbers.indexOf(3))      // 2
println(numbers.indexOf(10))     // -1 (not found)

// Sublist
println(numbers.subList(1, 4))   // [2, 3, 4]

// Reverse
println(numbers.reversed())      // [5, 4, 3, 2, 1]

// Sort (returns new list)
val unsorted = listOf(5, 2, 8, 1, 9)
println(unsorted.sorted())       // [1, 2, 5, 8, 9]
println(unsorted.sortedDescending())  // [9, 8, 5, 2, 1]
```
