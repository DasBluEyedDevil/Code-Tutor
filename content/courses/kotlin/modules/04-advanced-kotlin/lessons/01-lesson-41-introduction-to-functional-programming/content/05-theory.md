---
type: "THEORY"
title: "Higher-Order Functions"
---


Functions that work with other functions.

### Taking Functions as Parameters


### Real-World Example: Custom List Processing


### Returning Functions


---



```kotlin
fun createMultiplier(factor: Int): (Int) -> Int {
    return { number -> number * factor }
}

val double = createMultiplier(2)
val triple = createMultiplier(3)
val tenfold = createMultiplier(10)

println(double(5))    // 10
println(triple(5))    // 15
println(tenfold(5))   // 50
```
