---
type: "EXAMPLE"
title: "Applying FP to Real Code"
---


Transforming imperative code to functional style:



```kotlin
// IMPERATIVE STYLE
fun processOrdersImperative(orders: List<Order>): Double {
    var total = 0.0
    for (order in orders) {
        if (order.status == OrderStatus.COMPLETED) {
            for (item in order.items) {
                total += item.price * item.quantity
            }
        }
    }
    return total
}

// FUNCTIONAL STYLE
fun processOrdersFunctional(orders: List<Order>): Double =
    orders
        .filter { it.status == OrderStatus.COMPLETED }
        .flatMap { it.items }
        .sumOf { it.price * it.quantity }

// Even more composable
val isCompleted: (Order) -> Boolean = { it.status == OrderStatus.COMPLETED }
val itemTotal: (OrderItem) -> Double = { it.price * it.quantity }

fun processOrders(orders: List<Order>): Double =
    orders
        .filter(isCompleted)
        .flatMap(Order::items)
        .sumOf(itemTotal)
```
