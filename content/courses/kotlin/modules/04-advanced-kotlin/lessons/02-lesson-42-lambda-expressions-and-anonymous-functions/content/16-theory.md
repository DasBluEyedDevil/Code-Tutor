---
type: "THEORY"
title: "Solution 3: Return Behavior"
---



**Explanation**:
- **Labeled return** (`return@forEach`): Returns from the lambda only
- **Anonymous function**: `return` naturally exits only that function
- **Filter approach**: Often the most idiomatic—avoid returns altogether
- Understanding return behavior prevents subtle bugs in functional code

---



```kotlin
// Approach 1: Labeled return
fun printNumbersSkippingThreeLabeledReturn() {
    val numbers = listOf(1, 2, 3, 4, 5)

    numbers.forEach {
        if (it == 3) return@forEach  // Return from lambda only
        println(it)
    }

    println("Done!")  // This DOES print!
}

// Approach 2: Anonymous function
fun printNumbersSkippingThreeAnonymousFunction() {
    val numbers = listOf(1, 2, 3, 4, 5)

    numbers.forEach(fun(number) {
        if (number == 3) return  // Return from anonymous function only
        println(number)
    })

    println("Done!")  // This DOES print!
}

// Approach 3: Continue with different logic
fun printNumbersSkippingThreeFilter() {
    val numbers = listOf(1, 2, 3, 4, 5)

    numbers
        .filter { it != 3 }
        .forEach { println(it) }

    println("Done!")
}

fun main() {
    println("=== Labeled Return ===")
    printNumbersSkippingThreeLabeledReturn()
    // Output: 1, 2, 4, 5, Done!

    println("\n=== Anonymous Function ===")
    printNumbersSkippingThreeAnonymousFunction()
    // Output: 1, 2, 4, 5, Done!

    println("\n=== Filter Approach ===")
    printNumbersSkippingThreeFilter()
    // Output: 1, 2, 4, 5, Done!
}
```
