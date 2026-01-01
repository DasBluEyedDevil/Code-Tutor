---
type: "THEORY"
title: "Chaining Scope Functions"
---


Combining scope functions creates fluent APIs.

### Example 1: Data Processing Pipeline


### Example 2: Building Complex Objects


### Example 3: Conditional Processing


---



```kotlin
fun processOrder(orderId: Int): String {
    return fetchOrder(orderId)
        ?.let { order ->
            // Transform order
            order.apply {
                items = items.filter { it.inStock }
            }
        }
        ?.takeIf { it.items.isNotEmpty() }
        ?.also { validateOrder(it) }
        ?.run { "Order ${this.id} processed successfully" }
        ?: "Order not found or invalid"
}

data class Order(val id: Int, var items: List<Item>)
data class Item(val name: String, val inStock: Boolean)

fun fetchOrder(id: Int): Order? = Order(id, listOf(
    Item("Book", true),
    Item("Pen", false),
    Item("Notebook", true)
))

fun validateOrder(order: Order) {
    println("Validating order ${order.id}")
}
```
