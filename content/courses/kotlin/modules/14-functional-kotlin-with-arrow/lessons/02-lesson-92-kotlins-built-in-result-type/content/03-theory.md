---
type: "THEORY"
title: "The Problem with Exceptions"
---


### Hidden Control Flow

```kotlin
// What can throw? No way to know from signature!
fun processOrder(orderId: String): Order {
    val order = orderRepository.findById(orderId)  // Throws?
    val validated = validator.validate(order)       // Throws?
    val saved = orderRepository.save(validated)     // Throws?
    return saved
}

// Caller must read implementation or docs to know!
try {
    val order = processOrder("123")
} catch (e: OrderNotFoundException) {
    // Did I catch everything?
} catch (e: ValidationException) {
    // What about database errors?
}
```

### The Functional Alternative

Make errors explicit in the type system:

```kotlin
// Now errors are visible!
fun processOrder(orderId: String): Result<Order>

// Caller knows to handle errors
processOrder("123")
    .onSuccess { order -> /* use order */ }
    .onFailure { error -> /* handle error */ }
```

---

