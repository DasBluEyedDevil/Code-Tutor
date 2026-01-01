---
type: "THEORY"
title: "Exercise 3: Return Behavior"
---


**Goal**: Understand the difference between lambda and anonymous function returns.

**Task**: Fix this code so it prints all numbers except 3:


**Goal**: Fix it using:
1. Labeled return
2. Anonymous function

---



```kotlin
fun printNumbersSkippingThree() {
    val numbers = listOf(1, 2, 3, 4, 5)

    numbers.forEach {
        if (it == 3) return  // Problem: this exits the entire function!
        println(it)
    }

    println("Done!")  // This never prints!
}

fun main() {
    printNumbersSkippingThree()
}
```
