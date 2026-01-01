---
type: "THEORY"
title: "Flows - Reactive Streams"
---


Flows represent asynchronous streams of values.

### Basic Flow


### Flow Builders


### Flow Operators


### Flow Context

Flows preserve the context of the collector:


### `flowOn` - Change Flow Context


### Buffer and Conflate


### Combining Flows


### Flow Completion


---



```kotlin
fun main() = runBlocking {
    (1..3).asFlow()
        .onEach { println("Emitting $it") }
        .onCompletion { println("Flow completed") }
        .collect { println("Collected $it") }

    // With exception handling
    flow {
        emit(1)
        throw RuntimeException("Error!")
    }
        .onCompletion { cause ->
            if (cause != null) {
                println("Completed with error: ${cause.message}")
            }
        }
        .catch { println("Caught: ${it.message}") }
        .collect()
}
```
