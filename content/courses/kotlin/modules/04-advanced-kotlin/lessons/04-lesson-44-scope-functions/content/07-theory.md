---
type: "THEORY"
title: "run: Execute and Return Result"
---


`run` uses `this` as context and returns the lambda result.

### Basic Usage


### Object Configuration + Computation


### Multiple Operations, Single Result


### Real-World Example: Complex Calculation


---



```kotlin
data class Order(
    val items: List<Item>,
    val discount: Double,
    val taxRate: Double
)

data class Item(val price: Double, val quantity: Int)

fun Order.calculateTotal() = run {
    val subtotal = items.sumOf { it.price * it.quantity }
    val afterDiscount = subtotal * (1 - discount)
    val withTax = afterDiscount * (1 + taxRate)
    withTax
}

val order = Order(
    items = listOf(
        Item(10.0, 2),
        Item(5.0, 3)
    ),
    discount = 0.1,
    taxRate = 0.08
)

println("Total: ${"%.2f".format(order.calculateTotal())}")
// Total: 30.02
```
