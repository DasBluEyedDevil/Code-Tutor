---
type: "THEORY"
title: "Arrays"
---


Arrays are **fixed-size** collections. Once you create an array, you cannot change its size (add or remove items), but you can modify the items inside it.

### Creating Arrays
```kotlin
val numbers = arrayOf(1, 2, 3, 4, 5)
val nulls = arrayOfNulls<String>(3) // Array of 3 null strings
```

### Accessing Array Elements
Like lists, arrays use index-based access.

```kotlin
println(numbers[0]) // 1
numbers[1] = 10     // Modify the second element
```

### Array vs List
While they look similar, lists are generally preferred in Kotlin because they provide a much richer set of functions and support immutability (read-only).

**When to use Arrays vs Lists**:

| Use Arrays when... | Use Lists when... |
|-------------------|-------------------|
| Working with Java interop | Building typical Kotlin apps |
| Performance-critical numeric operations | You want rich collection functions |
| Fixed-size data is guaranteed | Size may change or vary |
| Using primitive arrays (IntArray, etc.) | Readability and safety matter most |

In most Kotlin code, prefer `List` over `Array`.



```kotlin
// Array (fixed size, mutable elements)
val array = arrayOf(1, 2, 3)
array[0] = 10  // ✅ OK
// array.add(4)  // ❌ Error: Can't change size

// List (immutable)
val list = listOf(1, 2, 3)
// list[0] = 10  // ❌ Error: Can't modify
// list.add(4)   // ❌ Error: Can't add

// Mutable list (flexible)
val mutableList = mutableListOf(1, 2, 3)
mutableList[0] = 10  // ✅ OK
mutableList.add(4)   // ✅ OK
```
