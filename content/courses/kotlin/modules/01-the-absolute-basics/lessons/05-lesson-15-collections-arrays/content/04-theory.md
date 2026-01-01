---
type: "THEORY"
title: "Lists"
---


Lists are ordered collections that can contain duplicates. They are one of the most commonly used collection types.

### Read-Only Lists (listOf)
By default, lists created with `listOf` are read-only. You cannot add or remove items after the list is created.

```kotlin
val fruit = listOf("Apple", "Banana", "Cherry")
// fruit.add("Date") ❌ Error: No add() method on read-only list
```

### Accessing List Elements
You can access elements by their index, starting from 0.

```kotlin
println(fruit[0]) // Apple
println(fruit.first()) // Apple
println(fruit.last()) // Cherry
```

### Mutable Lists (mutableListOf)
If you need to change the list later, use `mutableListOf`.

```kotlin
val names = mutableListOf("Alice", "Bob")
names.add("Carol") // ✅ OK
names.removeAt(0)  // ✅ Removes "Alice"
```

### List Operations
...



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
