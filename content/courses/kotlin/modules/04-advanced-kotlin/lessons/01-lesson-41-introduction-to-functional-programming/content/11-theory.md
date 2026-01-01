---
type: "THEORY"
title: "Solution 1: Function Calculator"
---



**Explanation**:
- We define operation functions as lambda expressions
- Each lambda takes two Ints and returns an Int
- The `calculate` function is generic—it works with any operation
- We can pass pre-defined operations or create them inline

---



```kotlin
fun calculate(a: Int, b: Int, operation: (Int, Int) -> Int): Int {
    return operation(a, b)
}

fun main() {
    // Define operations as lambdas
    val add = { a: Int, b: Int -> a + b }
    val subtract = { a: Int, b: Int -> a - b }
    val multiply = { a: Int, b: Int -> a * b }
    val divide = { a: Int, b: Int -> if (b != 0) a / b else 0 }

    val x = 20
    val y = 4

    println("$x + $y = ${calculate(x, y, add)}")         // 24
    println("$x - $y = ${calculate(x, y, subtract)}")    // 16
    println("$x * $y = ${calculate(x, y, multiply)}")    // 80
    println("$x / $y = ${calculate(x, y, divide)}")      // 5

    // Can also use lambdas directly
    println("$x % $y = ${calculate(x, y) { a, b -> a % b }}")  // 0
}
```
