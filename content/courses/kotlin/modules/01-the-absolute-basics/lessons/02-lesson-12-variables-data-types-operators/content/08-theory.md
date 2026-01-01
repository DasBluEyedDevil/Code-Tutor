---
type: "THEORY"
title: "Type Conversions"
---


Convert between types explicitly:

### Number Conversions


### Common Conversion Methods

| Method | From → To | Example |
|--------|-----------|---------|
| `toInt()` | Any number/String → Int | `"42".toInt()` → 42 |
| `toDouble()` | Any number/String → Double | `42.toDouble()` → 42.0 |
| `toLong()` | Any number/String → Long | `42.toLong()` → 42L |
| `toFloat()` | Any number/String → Float | `42.toFloat()` → 42.0f |
| `toString()` | Any type → String | `42.toString()` → "42" |
| `toBoolean()` | String → Boolean | `"true".toBoolean()` → true |

### Handling Conversion Errors


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
