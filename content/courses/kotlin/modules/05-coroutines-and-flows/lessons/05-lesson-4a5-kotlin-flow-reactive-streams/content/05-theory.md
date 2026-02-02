---
type: "THEORY"
title: "Intermediate Operators"
---

Operators transform flows without triggering execution:

### map - Transform Each Value
```kotlin
numbersFlow()
    .map { it * 2 } // 2, 4, 6
    .collect { println(it) }
```

### filter - Keep Matching Values
```kotlin
numbersFlow()
    .filter { it % 2 == 0 } // Only even numbers
    .collect { println(it) }
```

### transform - Emit Multiple Values
```kotlin
flowOf(1, 2, 3)
    .transform { value ->
        emit(value)
        emit(value * 10)
    }
    .collect { println(it) } // 1, 10, 2, 20, 3, 30
```

### take / drop
```kotlin
flowOf(1, 2, 3, 4, 5)
    .take(3) // First 3
    .collect { println(it) } // 1, 2, 3

flowOf(1, 2, 3, 4, 5)
    .drop(2) // Skip first 2
    .collect { println(it) } // 3, 4, 5
```

### onEach - Side Effects
```kotlin
numbersFlow()
    .onEach { println("Processing: $it") }
    .map { it * 2 }
    .collect { println("Result: $it") }
```