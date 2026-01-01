---
type: "THEORY"
title: "Elvis Operator (?:)"
---


The Elvis operator `?:` provides a default value when something is null.

The Elvis operator `?:` (looks like Elvis's hair if you tilt your head) allows you to provide a backup value if an expression is null.

### Basic Usage
```kotlin
val name: String? = null
val displayName = name ?: "Guest" // If name is null, use "Guest"
```

**How it works**:
1. If the left side is NOT null, it uses the left side.
2. If the left side IS null, it uses the right side.

### Real-World Examples
- Providing a default username if none is set.
- Using 0 as a fallback for missing numeric data.

### Combining Safe Call and Elvis
This is one of the most common patterns in Kotlin.

```kotlin
val length = name?.length ?: 0
```

### Elvis with Expressions
...



```kotlin
fun getDiscount(customerType: String?): Double {
    return when (customerType ?: "regular") {
        "premium" -> 0.20
        "gold" -> 0.15
        else -> 0.05
    }
}

fun main() {
    println(getDiscount("premium"))  // 0.2
    println(getDiscount(null))       // 0.05 (uses default "regular")
}
```
