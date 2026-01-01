---
type: "THEORY"
title: "Exercise 2: Nested Lambda Clarity"
---


**Goal**: Improve nested lambda readability by using named parameters.

**Task**: Rewrite with clear, named parameters:


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

    // TODO: Make this more readable
    val result = orders.map {
        it.items.filter { it.price > 50 }.map { it.name }
    }

    println(result)
}
```
