---
type: "THEORY"
title: "Object Expressions"
---


**Object expressions** create anonymous objects - objects of an unnamed class.

### Basic Object Expression


### Implementing Interfaces

Common use: One-time implementations of interfaces


**Real-World Example: Event Handlers**


**Output**:

### Accessing Outer Scope

Object expressions can access variables from their surrounding scope:


---



```kotlin
fun countClicks() {
    var clickCount = 0

    val button = object {
        fun click() {
            clickCount++  // Access outer variable
            println("Click count: $clickCount")
        }
    }

    button.click()  // Click count: 1
    button.click()  // Click count: 2
    button.click()  // Click count: 3
}

fun main() {
    countClicks()
}
```
