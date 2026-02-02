---
type: "THEORY"
title: "Collecting Flows"
---

`collect` is the primary terminal operator - it triggers flow execution:

```kotlin
// Basic collection
numbersFlow().collect { value ->
    println(value)
}

// Other terminal operators
val first: Int = numbersFlow().first()
val last: Int = numbersFlow().last()
val list: List<Int> = numbersFlow().toList()
val set: Set<Int> = numbersFlow().toSet()
val count: Int = numbersFlow().count()
val sum: Int = numbersFlow().reduce { acc, value -> acc + value }
```

### collect is Suspending
```kotlin
fun main() = runBlocking {
    println("Before collect")
    
    flowOf(1, 2, 3)
        .onEach { delay(100) }
        .collect { println(it) }
    
    println("After collect") // Runs after flow completes
}
// Output:
// Before collect
// 1, 2, 3 (with delays)
// After collect
```