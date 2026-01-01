---
type: "THEORY"
title: "Filter: Selecting Elements"
---


`filter` keeps only elements matching a predicate.

### Basic Filter


### Filter with Objects


### FilterNot: Opposite of Filter


### FilterIsInstance: Filter by Type


---



```kotlin
val mixed: List<Any> = listOf(1, "hello", 2, "world", 3.14, true)

val strings = mixed.filterIsInstance<String>()
println(strings)  // [hello, world]

val numbers = mixed.filterIsInstance<Int>()
println(numbers)  // [1, 2]
```
