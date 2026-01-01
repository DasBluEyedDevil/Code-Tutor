---
type: "THEORY"
title: "Variable Number of Arguments (Vararg)"
---


Sometimes you don't know exactly how many arguments will be passed to a function. The `vararg` keyword allows a function to accept any number of arguments of a specific type.

```kotlin
fun sumAll(vararg numbers: Int): Int {
    var sum = 0
    for (num in numbers) {
        sum += num
    }
    return sum
}

fun main() {
    println(sumAll(1, 2, 3))        // 6
    println(sumAll(10, 20, 30, 40)) // 100
}
```

### Practical Vararg Example
Inside the function, the `vararg` parameter behaves like an array.

---



```kotlin
fun printAll(vararg messages: String) {
    for (message in messages) {
        println("- $message")
    }
}

fun main() {
    printAll("Apple", "Banana", "Cherry")
    // Output:
    // - Apple
    // - Banana
    // - Cherry
}
```
