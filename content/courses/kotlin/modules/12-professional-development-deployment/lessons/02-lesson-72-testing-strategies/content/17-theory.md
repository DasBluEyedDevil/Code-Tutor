---
type: "THEORY"
title: "Solution 3"
---


**Tests First**:

**Implementation**:

---



```kotlin
data class Item(val name: String, val price: Double, val quantity: Int = 1)

class ShoppingCart {
    private val items = mutableMapOf<String, Item>()
    private var discountPercent = 0.0

    fun addItem(name: String, price: Double) {
        val existing = items[name]
        if (existing != null) {
            items[name] = existing.copy(quantity = existing.quantity + 1)
        } else {
            items[name] = Item(name, price, 1)
        }
    }

    fun removeItem(name: String) {
        items.remove(name)
    }

    fun getItemCount(name: String): Int {
        return items[name]?.quantity ?: 0
    }

    fun getTotal(): Double {
        val subtotal = items.values.sumOf { it.price * it.quantity }
        return subtotal * (1 - discountPercent / 100)
    }

    fun applyDiscount(code: String): Result<Unit> {
        if (items.isEmpty()) {
            return Result.failure(Exception("Cannot apply discount to empty cart"))
        }

        val discount = when (code) {
            "SAVE10" -> 10.0
            "SAVE20" -> 20.0
            "SAVE50" -> 50.0
            else -> return Result.failure(Exception("Invalid discount code"))
        }

        discountPercent = discount
        return Result.success(Unit)
    }
}
```
