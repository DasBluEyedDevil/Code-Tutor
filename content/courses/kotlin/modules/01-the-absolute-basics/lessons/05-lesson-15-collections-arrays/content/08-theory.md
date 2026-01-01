---
type: "THEORY"
title: "Common Collection Operations"
---


Kotlin collections come with a powerful set of functions that let you manipulate data without using manual loops.

### forEach - Execute action for each element
```kotlin
val items = listOf("A", "B", "C")
items.forEach { println("Item: $it") }
```

### filter - Select elements matching a condition
`filter` creates a new list containing only the items that pass a test.

```kotlin
val numbers = listOf(1, 10, 5, 20)
val largeNumbers = numbers.filter { it > 10 } // [20]
```

### map - Transform each element
`map` creates a new list where every item from the original list has been changed.

```kotlin
val squared = numbers.map { it * it } // [1, 100, 25, 400]
```

### Combining Operations
You can chain these operations together to perform complex tasks in a single line.

```kotlin
val results = numbers
    .filter { it > 5 }
    .map { "Score: $it" }
```

### More Useful Operations

Here are additional collection functions you'll use frequently:



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
