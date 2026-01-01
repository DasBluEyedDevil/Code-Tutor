---
type: "THEORY"
title: "Safe Call Operator (?.)"
---


The safe call operator `?.` safely accesses properties/methods on nullable objects.

### Basic Usage


### How it Works


**If the object is null, the entire expression returns null.**

### Chaining Safe Calls


### Safe Calls with Methods


---



```kotlin
val text: String? = "  Hello  "

println(text?.trim())       // "Hello"
println(text?.uppercase())  // "HELLO"

val nullText: String? = null
println(nullText?.trim())   // null
```
