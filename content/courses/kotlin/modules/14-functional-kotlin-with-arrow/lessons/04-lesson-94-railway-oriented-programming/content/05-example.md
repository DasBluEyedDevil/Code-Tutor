---
type: "EXAMPLE"
title: "Building the Railway"
---


Complete order processing pipeline:



```kotlin
// Each step can fail, switching to failure track

fun checkInventory(order: Order): Either<OrderError, Order> = either {
    order.items.forEach { item ->
        val available = inventoryService.getStock(item.productId)
        ensure(available >= item.quantity) {
            OrderError.InventoryError(item.productId)
        }
    }
    order
}

fun processPayment(order: Order): Either<OrderError, Order> = either {
    val result = paymentService.charge(order.customerId, calculateTotal(order))
    ensure(result.isSuccess) {
        OrderError.PaymentFailed(result.message)
    }
    order.copy(status = OrderStatus.PAID)
}

fun arrangeShipping(order: Order): Either<OrderError, Order> = either {
    val tracking = shippingService.createShipment(order)
    ensure(tracking != null) {
        OrderError.ShippingError("Shipment creation failed")
    }
    order.copy(status = OrderStatus.SHIPPED)
}

// The complete railway - chain all steps
fun processOrder(order: Order): Either<OrderError, Order> = either {
    val validated = validateOrder(order).bind()
    val inventoryChecked = checkInventory(validated).bind()
    val paid = processPayment(inventoryChecked).bind()
    val shipped = arrangeShipping(paid).bind()
    shipped
}
```
