---
type: "THEORY"
title: "Variable Number of Arguments (Vararg)"
---


Accept any number of arguments:


### Practical Vararg Example


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
