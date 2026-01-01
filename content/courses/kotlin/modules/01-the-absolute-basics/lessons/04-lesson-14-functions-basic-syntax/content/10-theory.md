---
type: "THEORY"
title: "Extension Functions"
---


Add new functions to existing types without modifying their source code:

### Basic Extension Function


In extension functions, `this` refers to the object the function is called on.

### More Extension Examples


### Why Extension Functions?

They make code more readable:


---



```kotlin
// Without extension
val doubled = multiplyBy2(number)
val formatted = formatAsCurrency(price)

// With extension
val doubled = number.double()
val formatted = price.asCurrency()
```
