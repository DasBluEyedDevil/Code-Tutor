---
type: "EXAMPLE"
title: "The either { } Builder"
---


Cleaner syntax for chaining operations:



```kotlin
import arrow.core.*
import arrow.core.raise.either

// The either { } builder provides bind() for short-circuit evaluation
fun updateUserEmail(userId: Long, newEmail: String): Either<UserError, User> =
    either {
        // bind() extracts Right value or short-circuits with Left
        val user = getUser(userId).bind()
        val validEmail = validateEmail(newEmail).bind()
        
        // Continue with the happy path
        val updatedUser = user.copy(email = validEmail)
        updateInDatabase(updatedUser).bind()
    }

// Equivalent to nested flatMap, but much cleaner!
fun updateUserEmailFlatMap(userId: Long, newEmail: String): Either<UserError, User> =
    getUser(userId).flatMap { user ->
        validateEmail(newEmail).flatMap { validEmail ->
            val updatedUser = user.copy(email = validEmail)
            updateInDatabase(updatedUser)
        }
    }

// Complex example
fun processOrder(orderId: Long): Either<OrderError, Receipt> = either {
    val order = findOrder(orderId).bind()
    val customer = getCustomer(order.customerId).bind()
    val inventory = checkInventory(order.items).bind()
    val payment = processPayment(customer, order.total).bind()
    val shipment = createShipment(order, customer.address).bind()
    
    Receipt(order, customer, payment, shipment)
}
```
