---
type: "THEORY"
title: "Common Collection Operations"
---


### forEach - Execute action for each element


### filter - Select elements matching a condition


### map - Transform each element


### Combining Operations


### More Useful Operations


---



```kotlin
val numbers = listOf(1, 2, 3, 4, 5)

// sum
println(numbers.sum())  // 15

// average
println(numbers.average())  // 3.0

// max and min
println(numbers.max())  // 5
println(numbers.min())  // 1

// count
println(numbers.count { it > 3 })  // 2 (elements: 4, 5)

// any - check if any element matches
println(numbers.any { it > 4 })  // true

// all - check if all elements match
println(numbers.all { it > 0 })  // true

// none - check if no elements match
println(numbers.none { it < 0 })  // true

// find - get first matching element
println(numbers.find { it > 3 })  // 4

// take - get first n elements
println(numbers.take(3))  // [1, 2, 3]

// drop - skip first n elements
println(numbers.drop(2))  // [3, 4, 5]
```
