---
type: "THEORY"
title: "Suspend Functions"
---


Suspend functions are the foundation of coroutines. They can be paused and resumed without blocking a thread.

### Basic Suspend Function


### Suspend Functions Can Call Other Suspend Functions


### Why Suspend?

The `suspend` keyword tells the compiler:
- This function may take time
- It can be paused and resumed
- It doesn't block the thread
- It can only be called from a coroutine or another suspend function


---



```kotlin
suspend fun example() {
    // Can call:
    delay(1000)           // ✅ Suspend function
    fetchData()           // ✅ Suspend function
    println("Hello")      // ✅ Regular function
    val x = 1 + 2         // ✅ Regular code

    // Thread.sleep(1000) // ⚠️ Works but blocks thread (avoid!)
}
```
