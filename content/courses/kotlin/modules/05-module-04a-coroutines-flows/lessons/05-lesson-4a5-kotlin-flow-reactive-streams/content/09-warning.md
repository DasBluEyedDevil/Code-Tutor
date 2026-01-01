---
type: "WARNING"
title: "Cold vs Hot Flows"
---

### Cold Flows (flow { })
- Start fresh for each collector
- Values computed on demand
- Multiple collectors = multiple executions

```kotlin
val coldFlow = flow {
    println("Flow started")
    emit(1)
}

coldFlow.collect { println(it) } // "Flow started", 1
coldFlow.collect { println(it) } // "Flow started", 1 (runs again!)
```

### Hot Flows (StateFlow, SharedFlow)
- Exist independently of collectors
- Values shared among collectors
- Covered in next lesson

```kotlin
val hotFlow = MutableStateFlow(0)

launch { hotFlow.collect { println("A: $it") } }
launch { hotFlow.collect { println("B: $it") } }

hotFlow.value = 1 // Both A and B receive 1
```