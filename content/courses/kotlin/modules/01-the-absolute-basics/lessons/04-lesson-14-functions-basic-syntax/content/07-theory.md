---
type: "THEORY"
title: "Single-Expression Functions"
---


When a function returns a single expression, you can use shorthand:

### Long Form vs Short Form


### More Examples


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
