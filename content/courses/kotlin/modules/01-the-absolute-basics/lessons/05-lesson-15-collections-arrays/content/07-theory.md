---
type: "THEORY"
title: "Arrays"
---


Arrays are **fixed-size** collections with indexed access.

### Creating Arrays


### Accessing Array Elements


### Array vs List


**When to use Arrays vs Lists**:
- **Arrays**: Performance-critical code, fixed size, interop with Java
- **Lists**: Most Kotlin code (more flexible, better API)

---



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
