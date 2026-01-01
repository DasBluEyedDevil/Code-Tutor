---
type: "EXAMPLE"
title: "Parallel Tracks"
---


When operations are independent, run them in parallel:



```kotlin
import arrow.core.*
import arrow.core.raise.either
import kotlinx.coroutines.*

// Independent operations can run in parallel
suspend fun processOrderParallel(order: Order): Either<OrderError, ProcessedOrder> = either {
    // Validate first (must complete before parallel work)
    val validated = validateOrder(order).bind()
    
    // These are independent - run in parallel
    coroutineScope {
        val inventoryDeferred = async { checkInventory(validated) }
        val customerDeferred = async { verifyCustomer(validated.customerId) }
        val fraudDeferred = async { checkFraud(validated) }
        
        // Await all and bind
        val inventory = inventoryDeferred.await().bind()
        val customer = customerDeferred.await().bind()
        val fraudClear = fraudDeferred.await().bind()
        
        // Continue with sequential steps
        val paid = processPayment(validated, customer).bind()
        val shipped = arrangeShipping(paid).bind()
        
        ProcessedOrder(shipped, inventory, customer)
    }
}

// Using parZip for cleaner parallel execution
suspend fun processOrderParZip(order: Order): Either<OrderError, ProcessedOrder> = either {
    val validated = validateOrder(order).bind()
    
    // parZip runs operations in parallel and combines results
    parZip(
        { checkInventory(validated).bind() },
        { verifyCustomer(validated.customerId).bind() },
        { checkFraud(validated).bind() }
    ) { inventory, customer, _ ->
        Triple(inventory, customer, validated)
    }.let { (inventory, customer, validated) ->
        val paid = processPayment(validated, customer).bind()
        val shipped = arrangeShipping(paid).bind()
        ProcessedOrder(shipped, inventory, customer)
    }
}
```
