---
type: "THEORY"
title: "Methods"
---


**Methods** are functions that belong to a class. They define the behavior of an object.


---



```kotlin
class Calculator {
    fun add(a: Int, b: Int): Int {
        return a + b
    }

    fun subtract(a: Int, b: Int): Int {
        return a - b
    }

    fun multiply(a: Int, b: Int): Int {
        return a * b
    }

    fun divide(a: Int, b: Int): Double {
        require(b != 0) { "Cannot divide by zero" }
        return a.toDouble() / b
    }
}

fun main() {
    val calc = Calculator()

    println(calc.add(5, 3))        // 8
    println(calc.subtract(10, 4))  // 6
    println(calc.multiply(3, 7))   // 21
    println(calc.divide(15, 3))    // 5.0
}
```
