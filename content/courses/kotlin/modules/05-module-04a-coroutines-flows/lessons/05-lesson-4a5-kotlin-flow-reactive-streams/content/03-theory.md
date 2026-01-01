---
type: "THEORY"
title: "Creating Flows"
---

### flow { } Builder
```kotlin
fun fetchUpdates(): Flow<Update> = flow {
    while (true) {
        val update = api.checkForUpdates()
        emit(update)
        delay(5000) // Check every 5 seconds
    }
}
```

### flowOf() - From Values
```kotlin
val numbers: Flow<Int> = flowOf(1, 2, 3)
val names: Flow<String> = flowOf("Alice", "Bob", "Charlie")
```

### asFlow() - From Collections
```kotlin
val listFlow: Flow<Int> = listOf(1, 2, 3).asFlow()
val rangeFlow: Flow<Int> = (1..100).asFlow()
```

### channelFlow - Concurrent Emissions
```kotlin
fun fetchFromMultipleSources(): Flow<Data> = channelFlow {
    launch { send(fetchFromApi1()) }
    launch { send(fetchFromApi2()) }
    launch { send(fetchFromApi3()) }
}
```