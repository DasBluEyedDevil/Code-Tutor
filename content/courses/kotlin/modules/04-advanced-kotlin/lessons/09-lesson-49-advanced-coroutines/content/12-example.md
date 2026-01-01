---
type: "EXAMPLE"
title: "SharedFlow with Replay"
---

The replay parameter determines how many past values new collectors receive. With replay=2, late collectors get the last 2 emitted values immediately. This is useful for caching recent events or ensuring collectors don't miss important updates.

```kotlin
import kotlinx.coroutines.*
import kotlinx.coroutines.flow.*

fun main() = runBlocking {
    // SharedFlow with replay = 2 keeps last 2 values
    val sharedFlow = MutableSharedFlow<Int>(replay = 2)
    
    // Emit values before any collectors
    sharedFlow.emit(1)
    sharedFlow.emit(2)
    sharedFlow.emit(3)
    
    println("=== Collector 1 joins (gets replay) ===")
    val job1 = launch {
        sharedFlow.collect { println("Collector 1: $it") }
    }
    
    delay(100)
    
    println("\n=== Emitting new value ===")
    sharedFlow.emit(4)
    
    delay(100)
    
    println("\n=== Collector 2 joins (gets replay) ===")
    val job2 = launch {
        sharedFlow.collect { println("Collector 2: $it") }
    }
    
    delay(100)
    
    println("\n=== Emitting another value ===")
    sharedFlow.emit(5)
    
    delay(100)
    job1.cancel()
    job2.cancel()
}
// Output:
// === Collector 1 joins (gets replay) ===
// Collector 1: 2
// Collector 1: 3
//
// === Emitting new value ===
// Collector 1: 4
//
// === Collector 2 joins (gets replay) ===
// Collector 2: 3
// Collector 2: 4
//
// === Emitting another value ===
// Collector 1: 5
// Collector 2: 5
```
