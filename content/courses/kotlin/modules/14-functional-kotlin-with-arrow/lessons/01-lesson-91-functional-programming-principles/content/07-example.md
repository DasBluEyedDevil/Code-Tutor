---
type: "EXAMPLE"
title: "Immutable Collections"
---


Kotlin provides both mutable and immutable collections:



```kotlin
// Immutable by default
val numbers: List<Int> = listOf(1, 2, 3)
// numbers.add(4)  // Won't compile!

// "Adding" creates new list
val moreNumbers: List<Int> = numbers + 4
println(numbers)      // [1, 2, 3] - unchanged
println(moreNumbers)  // [1, 2, 3, 4] - new list

// Transforming immutably
val doubled: List<Int> = numbers.map { it * 2 }
val filtered: List<Int> = numbers.filter { it > 1 }

// Immutable maps
val config: Map<String, String> = mapOf(
    "host" to "localhost",
    "port" to "8080"
)
val updated: Map<String, String> = config + ("debug" to "true")

// For performance with many operations, use builders
val built: List<Int> = buildList {
    add(1)
    add(2)
    addAll(listOf(3, 4, 5))
}  // Returns immutable List<Int>
```
