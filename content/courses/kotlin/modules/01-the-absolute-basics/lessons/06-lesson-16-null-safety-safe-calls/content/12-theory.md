---
type: "THEORY"
title: "Null Safety Patterns"
---


### Pattern 1: Safe Call with Default


### Pattern 2: Explicit Null Check


### Pattern 3: Early Return


### Pattern 4: let for Complex Logic


---



```kotlin
fun processOrder(orderId: String?) {
    orderId?.let { id ->
        println("Processing order: $id")
        // Multiple operations on id
        val order = findOrder(id)
        sendConfirmation(id)
        updateInventory(id)
    }
}
```
