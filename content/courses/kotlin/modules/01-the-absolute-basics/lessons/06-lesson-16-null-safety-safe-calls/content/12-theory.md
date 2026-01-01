---
type: "THEORY"
title: "Null Safety Patterns"
---


### Pattern 1: Safe Call with Default
This is the most common pattern. Use it when you want to use a value if it exists, or a default if it doesn't.

```kotlin
val count = data?.size ?: 0
```

### Pattern 2: Explicit Null Check
If you check for null using `if`, Kotlin "smart casts" the variable to a non-null type inside the block.

```kotlin
if (name != null) {
    println(name.length) // No ?. needed here!
}
```

### Pattern 3: Early Return
Use this to stop a function early if essential data is missing.

```kotlin
fun process(data: String?) {
    val nonNullData = data ?: return
    println("Processing $nonNullData")
}
```

### Pattern 4: let for Complex Logic

Use `let` when you need to perform multiple operations on a non-null value. The variable is named (here `id`) for clarity:



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
