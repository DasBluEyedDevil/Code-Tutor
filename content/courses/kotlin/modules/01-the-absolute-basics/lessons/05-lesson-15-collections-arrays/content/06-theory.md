---
type: "THEORY"
title: "Maps"
---


Maps store **key-value pairs** (like a dictionary).

### Read-Only Maps (mapOf)


### Mutable Maps (mutableMapOf)


### Iterating Over Maps


**Output**:

### Map Operations


---



```kotlin
val grades = mapOf("Math" to 95, "English" to 88, "Science" to 92)

println(grades.size)           // 3
println(grades.isEmpty())      // false
println(grades.containsKey("Math"))    // true
println(grades.containsValue(95))      // true

// Get all keys and values
println(grades.keys)    // [Math, English, Science]
println(grades.values)  // [95, 88, 92]
```
