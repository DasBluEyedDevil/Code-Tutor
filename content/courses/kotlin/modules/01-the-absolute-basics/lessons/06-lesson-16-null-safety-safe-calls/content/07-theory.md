---
type: "THEORY"
title: "Elvis Operator (?:)"
---


The Elvis operator `?:` provides a default value when something is null.

### Basic Usage


**How it works**:

### Real-World Examples


### Combining Safe Call and Elvis


### Elvis with Expressions


---



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
