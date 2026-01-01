---
type: "THEORY"
title: "If-Else Statements"
---


### Basic If Statement


**Structure**:

### If-Else Statement


### If-Else-If Chain


### If as an Expression

In Kotlin, `if` returns a value:


---



```kotlin
val age = 20
val status = if (age >= 18) "Adult" else "Minor"
println(status)  // "Adult"

// Multi-line
val message = if (age >= 18) {
    "You can vote"
} else {
    "You cannot vote yet"
}
```
