---
type: "EXAMPLE"
title: "Handling at the End"
---


Process the result at the end of the railway:



```kotlin
fun handleOrder(order: Order) {
    processOrder(order).fold(
        ifLeft = { error ->
            when (error) {
                is OrderError.ValidationFailed -> {
                    showValidationError(error.reason)
                }
                is OrderError.PaymentFailed -> {
                    logPaymentError(error.reason)
                    suggestRetry(order)
                }
                is OrderError.InventoryError -> {
                    suggestAlternatives(error.productId)
                }
                is OrderError.ShippingError -> {
                    queueForManualReview(order)
                    notifyOperations(error.reason)
                }
            }
        },
        ifRight = { completedOrder ->
            sendConfirmationEmail(completedOrder)
            updateDashboard(completedOrder)
            notifyWarehouse(completedOrder)
        }
    )
}

// Or using when for pattern matching
fun handleOrderResult(result: Either<OrderError, Order>): String = when (result) {
    is Either.Left -> "Order failed: ${describeError(result.value)}"
    is Either.Right -> "Order ${result.value.id} completed!"
}
```
