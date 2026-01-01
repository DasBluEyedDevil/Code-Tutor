---
type: "THEORY"
title: "Solution 2: Currying Implementation"
---



**Explanation**:
- Currying transforms multi-parameter functions into chains
- Creates specialized versions by fixing parameters
- Useful for configuration and creating function families

---



```kotlin
fun <A, B, C> curry(f: (A, B) -> C): (A) -> (B) -> C {
    return { a -> { b -> f(a, b) } }
}

// Bonus: Uncurry
fun <A, B, C> uncurry(f: (A) -> (B) -> C): (A, B) -> C {
    return { a, b -> f(a)(b) }
}

fun main() {
    val add = { a: Int, b: Int -> a + b }
    val multiply = { a: Int, b: Int -> a * b }

    // Curry add
    val curriedAdd = curry(add)
    val add10 = curriedAdd(10)
    println(add10(5))   // 15
    println(add10(20))  // 30

    // Curry multiply
    val curriedMultiply = curry(multiply)
    val double = curriedMultiply(2)
    val triple = curriedMultiply(3)
    println(double(7))  // 14
    println(triple(7))  // 21

    // Practical: Specialized formatters
    val format = { prefix: String, value: String -> "$prefix: $value" }
    val curriedFormat = curry(format)

    val errorFormatter = curriedFormat("ERROR")
    val infoFormatter = curriedFormat("INFO")

    println(errorFormatter("Something went wrong"))  // ERROR: Something went wrong
    println(infoFormatter("Process started"))        // INFO: Process started

    // Uncurry example
    val uncurriedAdd = uncurry(curriedAdd)
    println(uncurriedAdd(5, 3))  // 8
}
```
