---
type: "THEORY"
title: "Advanced Context Switching"
---


### `withContext` - Temporary Context Switch


### Context Elements


---



```kotlin
fun main() = runBlocking {
    val context = CoroutineName("MyCoroutine") + Dispatchers.Default

    launch(context) {
        println("Running in: ${coroutineContext[CoroutineName]?.name}")
        println("On thread: ${Thread.currentThread().name}")
    }

    delay(100)
}
```
