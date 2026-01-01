---
type: "KEY_POINT"
title: "The `it` Keyword"
---


`it` is a shorthand for the single parameter in a lambda.

### When `it` Is Available


### `it` vs Named Parameters


### When to Use `it`

**✅ Use `it` when**:
- The operation is simple and obvious
- The lambda is short (1-2 lines)
- Context makes the parameter clear


**❌ Avoid `it` when**:
- The lambda is complex
- Multiple nested lambdas
- Parameter type isn't obvious


### Nested Lambdas and `it`


---



```kotlin
data class Order(val id: Int, val items: List<Item>)
data class Item(val name: String, val price: Double)

val orders = listOf(
    Order(1, listOf(Item("Book", 15.0), Item("Laptop", 1200.0))),
    Order(2, listOf(Item("Phone", 800.0), Item("Case", 25.0)))
)

// ❌ Confusing: nested 'it'
val expensive = orders.map {
    it.items.filter { it.price > 100 }  // Both 'it'?!
}

// ✅ Clear: name parameters
val expensiveItems = orders.map { order ->
    order.items.filter { item -> item.price > 100 }
}

println(expensiveItems)
// [[Item(name=Laptop, price=1200.0)], [Item(name=Phone, price=800.0)]]
```
