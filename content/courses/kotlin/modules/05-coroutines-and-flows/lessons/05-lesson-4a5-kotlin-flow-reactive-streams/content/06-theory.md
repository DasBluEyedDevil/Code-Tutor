---
type: "THEORY"
title: "Combining Flows"
---

### combine - Latest from Each
```kotlin
val names = flowOf("Alice", "Bob").onEach { delay(100) }
val ages = flowOf(25, 30).onEach { delay(150) }

names.combine(ages) { name, age ->
    "$name is $age"
}.collect { println(it) }
// Alice is 25
// Bob is 25
// Bob is 30
```

### zip - Pair Up Values
```kotlin
val names = flowOf("Alice", "Bob", "Charlie")
val ages = flowOf(25, 30, 35)

names.zip(ages) { name, age ->
    "$name is $age"
}.collect { println(it) }
// Alice is 25
// Bob is 30
// Charlie is 35
```

### merge - Interleave Emissions
```kotlin
val flow1 = flowOf(1, 2, 3).onEach { delay(100) }
val flow2 = flowOf(10, 20, 30).onEach { delay(150) }

merge(flow1, flow2).collect { println(it) }
// 1, 10, 2, 3, 20, 30 (interleaved based on timing)
```

### flatMapConcat / flatMapMerge / flatMapLatest
```kotlin
// flatMapConcat - process sequentially
flowOf(1, 2, 3)
    .flatMapConcat { value ->
        flow {
            emit("$value start")
            delay(100)
            emit("$value end")
        }
    }
    .collect { println(it) }
// 1 start, 1 end, 2 start, 2 end, 3 start, 3 end
```