---
type: "THEORY"
title: "Maps"
---


Maps store **key-value pairs**. Each key is unique and maps to exactly one value. Think of a phone book: the person's name is the **key**, and their phone number is the **value**.

### Read-Only Maps (mapOf)
```kotlin
val inventory = mapOf("Apples" to 10, "Bananas" to 5)
println(inventory["Apples"]) // 10
```

### Mutable Maps (mutableMapOf)
```kotlin
val users = mutableMapOf("alice123" to "Alice Smith")
users["bob456"] = "Bob Jones" // Add new entry
users["alice123"] = "Alice Peterson" // Update existing entry
```

### Iterating Over Maps
You can loop through maps easily to get both keys and values.

```kotlin
for ((key, value) in inventory) {
    println("$key: $value items")
}
```

**Output**:
```text
Apples: 10 items
Bananas: 5 items
```

### Map Operations
...



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
