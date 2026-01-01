---
type: "EXAMPLE"
title: "Alternative: flatMap Chain"
---


The same railway using flatMap:



```kotlin
// Using flatMap - equivalent to either { } with bind()
fun processOrderChain(order: Order): Either<OrderError, Order> =
    validateOrder(order)
        .flatMap { validated -> checkInventory(validated) }
        .flatMap { checked -> processPayment(checked) }
        .flatMap { paid -> arrangeShipping(paid) }

// Even more concise with function references
fun processOrderConcise(order: Order): Either<OrderError, Order> =
    validateOrder(order)
        .flatMap(::checkInventory)
        .flatMap(::processPayment)
        .flatMap(::arrangeShipping)

// Visual representation:
//
// validateOrder --+-> checkInventory --+-> processPayment --+-> arrangeShipping --+-> Success!
//                 |                    |                    |                     |
//                 +-> ValidationFailed +-> InventoryError   +-> PaymentFailed     +-> ShippingError
//                          |                   |                    |                    |
//                          +===================================================+==+===+=> Failure!
```
