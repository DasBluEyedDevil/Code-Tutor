---
type: "THEORY"
title: "Type Conversions"
---


Convert between types explicitly:

### Number Conversions
In Kotlin, smaller types (like `Int`) do not automatically convert to larger types (like `Long`). You must perform an explicit conversion.

```kotlin
val i: Int = 100
val l: Long = i.toLong()  // Explicitly convert Int to Long
```

### Common Conversion Methods
...
### Handling Conversion Errors
When you convert from a `String` to a number, the program might crash if the string doesn't contain a valid number. We use "safe" conversion methods like `toIntOrNull()` to prevent crashes.

---



```kotlin
// ❌ This will crash if input isn't a valid number
val number = readln().toInt()  // User types "abc" → NumberFormatException

// ✅ Safe conversion with default value
val number = readln().toIntOrNull() ?: 0  // Returns 0 if conversion fails

// ✅ Safe conversion with error handling
val input = readln()
val number = input.toIntOrNull()

if (number != null) {
    println("Valid number: $number")
} else {
    println("Invalid input!")
}
```
