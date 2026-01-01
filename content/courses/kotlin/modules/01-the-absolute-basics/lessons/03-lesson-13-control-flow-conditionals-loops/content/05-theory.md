---
type: "THEORY"
title: "When Expression"
---


Kotlin's `when` is like a powerful `switch` statement:

### Basic When
The `when` expression defines a conditional expression with multiple branches. It's much cleaner than a long `if-else-if` chain.

```kotlin
val day = 3
when (day) {
    1 -> println("Monday")
    2 -> println("Tuesday")
    3 -> println("Wednesday")
    else -> println("Unknown day")
}
```

### When as Expression
Like `if`, `when` can return a value.

```kotlin
val result = when (day) {
    1 -> "Weekday"
    else -> "Invalid"
}
```

### When with Ranges
You can check if a value is within a range using the `in` keyword.

```kotlin
val age = 15
when (age) {
    in 0..12 -> println("Child")
    in 13..19 -> println("Teenager")
    else -> println("Adult")
}
```

### When with Multiple Conditions
You can combine multiple values in a single branch using a comma.

```kotlin
when (day) {
    1, 2, 3, 4, 5 -> println("Weekday")
    6, 7 -> println("Weekend")
}
```

### When with Boolean Conditions
You can use `when` without an argument to act like an `if-else-if` chain.

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
