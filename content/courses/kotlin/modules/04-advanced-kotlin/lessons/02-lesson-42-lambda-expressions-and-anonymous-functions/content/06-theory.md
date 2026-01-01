---
type: "THEORY"
title: "Anonymous Functions"
---


An alternative to lambda expressions with different semantics.

### Anonymous Function Syntax


### Difference: Return Behavior

**The key difference**: `return` in lambdas vs anonymous functions.


### Labeled Returns in Lambdas

Alternative to anonymous functions:


### When to Use Anonymous Functions

**Use anonymous functions when**:
- You need explicit return statements
- You want different return behavior
- The function body is complex with multiple returns


**Use lambdas when**:
- Simple, single-expression operations
- Following common Kotlin idioms
- Working with collection operations

---



```kotlin
val numbers = listOf(1, 2, 3, 4, 5)

// Complex validation with multiple returns
val isValid = numbers.any(fun(number: Int): Boolean {
    if (number < 0) return false
    if (number > 100) return false
    if (number % 2 != 0) return false
    return true
})
```
