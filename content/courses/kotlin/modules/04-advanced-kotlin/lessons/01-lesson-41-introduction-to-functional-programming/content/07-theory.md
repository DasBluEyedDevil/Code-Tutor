---
type: "THEORY"
title: "Function Types"
---


Every function has a type, just like variables.

### Basic Function Type Syntax


### Function Type Components


### Using Function Types in Declarations


### Nullable Function Types


---



```kotlin
var operation: ((Int, Int) -> Int)? = null

operation = { a, b -> a + b }

// Safe call with nullable function
val result = operation?.invoke(5, 3)  // 8

operation = null
val result2 = operation?.invoke(5, 3)  // null
```
