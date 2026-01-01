---
type: "THEORY"
title: "Single-Expression Functions"
---


When a function returns a single expression, you can use a concise shorthand by omitting the curly braces and the `return` keyword.

### Long Form vs Short Form

**Long Form**:
```kotlin
fun multiply(a: Int, b: Int): Int {
    return a * b
}
```

**Short Form (Single-Expression)**:
```kotlin
fun multiply(a: Int, b: Int) = a * b
```

### More Examples
Kotlin can infer the return type of single-expression functions, so you don't even need to specify `: Int` or `: String`.

---



```kotlin
fun square(x: Int) = x * x

fun isEven(n: Int) = n % 2 == 0

fun max(a: Int, b: Int) = if (a > b) a else b

fun getDiscount(isPremium: Boolean) = if (isPremium) 0.20 else 0.10

fun main() {
    println(square(5))        // 25
    println(isEven(7))        // false
    println(max(10, 20))      // 20
    println(getDiscount(true)) // 0.2
}
```
