---
type: "THEORY"
title: "Map: Transforming Elements"
---


`map` transforms each element using a function.

### Basic Map


### Map with Objects


### MapIndexed: Transform with Index


### MapNotNull: Transform and Filter Nulls


---



```kotlin
val input = listOf("1", "2", "abc", "3", "xyz")

val numbers = input.mapNotNull { it.toIntOrNull() }
println(numbers)  // [1, 2, 3]
```
