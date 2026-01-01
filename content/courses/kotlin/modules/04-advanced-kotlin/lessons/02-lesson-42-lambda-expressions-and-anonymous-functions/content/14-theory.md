---
type: "THEORY"
title: "Solution 2: Nested Lambda Clarity"
---



**Explanation**:
- Named parameters (`order`, `item`) eliminate confusion
- Breaking onto multiple lines improves readability
- Extracting helper functions can simplify complex chains
- Member references work great after extraction

---



```kotlin
data class Order(val id: Int, val items: List<Item>)
data class Item(val name: String, val price: Double, val quantity: Int)

fun main() {
    val orders = listOf(
        Order(1, listOf(
            Item("Laptop", 1200.0, 1),
            Item("Mouse", 25.0, 2)
        )),
        Order(2, listOf(
            Item("Monitor", 300.0, 1),
            Item("Keyboard", 75.0, 1)
        ))
    )

    // Original (confusing):
    // val result = orders.map { it.items.filter { it.price > 50 }.map { it.name } }

    // Improved: Named parameters for clarity
    val expensiveItemNames = orders.map { order ->
        order.items
            .filter { item -> item.price > 50 }
            .map { item -> item.name }
    }

    println("Expensive items per order: $expensiveItemNames")
    // [[Laptop], [Monitor, Keyboard]]

    // Alternative: Extract helper function
    fun Order.getExpensiveItemNames(): List<String> {
        return items
            .filter { it.price > 50 }
            .map { it.name }
    }

    val expensiveItems2 = orders.map { it.getExpensiveItemNames() }
    println("Alternative result: $expensiveItems2")
    // [[Laptop], [Monitor, Keyboard]]

    // Or with extension and member reference
    val expensiveItems3 = orders.map(Order::getExpensiveItemNames)
    println("With member reference: $expensiveItems3")
    // [[Laptop], [Monitor, Keyboard]]
}
```
