---
type: "EXAMPLE"
title: "Your First Coroutine"
---

Let's write your first coroutine:

```kotlin
import kotlinx.coroutines.*

fun main() = runBlocking {
    println("Starting on ${Thread.currentThread().name}")
    
    launch {
        delay(1000L) // Non-blocking delay for 1 second
        println("World! on ${Thread.currentThread().name}")
    }
    
    println("Hello,")
}

// Output:
// Starting on main
// Hello,
// World! on main
```
