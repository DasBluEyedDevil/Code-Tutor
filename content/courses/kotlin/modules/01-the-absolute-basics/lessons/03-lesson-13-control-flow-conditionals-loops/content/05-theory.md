---
type: "THEORY"
title: "When Expression"
---


Kotlin's `when` is like a powerful `switch` statement:

### Basic When


### When as Expression


### When with Ranges


### When with Multiple Conditions


### When with Boolean Conditions


---



```kotlin
val temperature = 25

when {
    temperature < 0 -> println("Freezing")
    temperature < 15 -> println("Cold")
    temperature < 25 -> println("Moderate")
    else -> println("Warm")
}
```
