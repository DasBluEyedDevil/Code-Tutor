---
type: "THEORY"
title: "What is Flow?"
---

A **Flow** is a cold stream of values computed asynchronously:

```kotlin
import kotlinx.coroutines.flow.*

fun numbersFlow(): Flow<Int> = flow {
    for (i in 1..3) {
        delay(100)
        emit(i) // Send value downstream
    }
}

fun main() = runBlocking {
    numbersFlow().collect { value ->
        println(value) // 1, 2, 3 (with delays)
    }
}
```

### Key Characteristics

| Feature | Description |
|---------|-------------|
| **Cold** | Nothing happens until collected |
| **Asynchronous** | Emits values over time |
| **Cancellable** | Respects structured concurrency |
| **Composable** | Chain operators like map, filter |
| **Backpressure** | Collector controls emission speed |