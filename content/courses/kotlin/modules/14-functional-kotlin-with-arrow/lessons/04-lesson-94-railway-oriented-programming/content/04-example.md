---
type: "EXAMPLE"
title: "Basic Railway Pattern"
---


Building a two-track pipeline:



```kotlin
import arrow.core.*
import arrow.core.raise.either
import arrow.core.raise.ensure

// Each function returns Either - can switch tracks
sealed interface OrderError {
    data class ValidationFailed(val reason: String) : OrderError
    data class PaymentFailed(val reason: String) : OrderError
    data class InventoryError(val productId: Long) : OrderError
    data class ShippingError(val reason: String) : OrderError
}

data class Order(
    val id: Long,
    val items: List<OrderItem>,
    val customerId: Long,
    val status: OrderStatus
)

data class OrderItem(val productId: Long, val quantity: Int)

enum class OrderStatus { CREATED, VALIDATED, PAID, SHIPPED }

// Step 1: Validate order (can switch to failure track)
fun validateOrder(order: Order): Either<OrderError, Order> = either {
    ensure(order.items.isNotEmpty()) {
        OrderError.ValidationFailed("Order must have items")
    }
    ensure(order.items.all { it.quantity > 0 }) {
        OrderError.ValidationFailed("Invalid quantities")
    }
    order.copy(status = OrderStatus.VALIDATED)
}
```
