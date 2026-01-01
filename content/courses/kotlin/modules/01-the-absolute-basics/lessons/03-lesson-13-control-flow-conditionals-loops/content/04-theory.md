---
type: "THEORY"
title: "If-Else Statements"
---


### Basic If Statement
An `if` statement executes a block of code ONLY if a condition is `true`.

```kotlin
val isRaining = true
if (isRaining) {
    println("Take an umbrella!")
}
```

### If-Else Statement
Use `else` to provide an alternative block of code if the condition is `false`.

```kotlin
val age = 15
if (age >= 18) {
    println("You can vote!")
} else {
    println("You are too young to vote.")
}
```

### If-Else-If Chain
You can chain multiple conditions together.

```kotlin
val score = 85
if (score >= 90) {
    println("Grade: A")
} else if (score >= 80) {
    println("Grade: B")
} else {
    println("Grade: C")
}
```

### If as an Expression
In Kotlin, `if` is an **expression**, meaning it returns a value. This allows you to assign the result directly to a variable.

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
