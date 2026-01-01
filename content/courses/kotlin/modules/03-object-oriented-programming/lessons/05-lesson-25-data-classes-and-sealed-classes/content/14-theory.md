---
type: "THEORY"
title: "Solution: Product Catalog"
---



---



```kotlin
data class Product(
    val id: Int,
    val name: String,
    val price: Double,
    val category: String,
    val inStock: Boolean = true
)

data class Order(
    val orderId: String,
    val products: List<Product>,
    val discount: Double = 0.0
) {
    val subtotal: Double
        get() = products.sumOf { it.price }

    val total: Double
        get() = subtotal - discount

    fun applyDiscount(discountAmount: Double): Order {
        return copy(discount = discountAmount)
    }

    fun displayOrder() {
        println("\n=== Order $orderId ===")
        products.forEach { product ->
            println("${product.name} - $${product.price}")
        }
        println("---")
        println("Subtotal: $$subtotal")
        if (discount > 0) {
            println("Discount: -$$discount")
        }
        println("Total: $$total")
        println("===================\n")
    }
}

fun main() {
    val products = listOf(
        Product(1, "Laptop", 999.99, "Electronics"),
        Product(2, "Mouse", 29.99, "Electronics"),
        Product(3, "Keyboard", 79.99, "Electronics"),
        Product(4, "Monitor", 299.99, "Electronics"),
        Product(5, "Desk Lamp", 39.99, "Furniture", inStock = false)
    )

    // Filter in-stock products
    val availableProducts = products.filter { it.inStock }

    // Create order
    val order = Order(
        orderId = "ORD-2025-001",
        products = listOf(
            products[0],  // Laptop
            products[1],  // Mouse
            products[2]   // Keyboard
        )
    )

    order.displayOrder()

    // Apply discount
    val discountedOrder = order.applyDiscount(50.0)
    discountedOrder.displayOrder()

    // Destructuring
    val (orderId, items, discount) = discountedOrder
    println("Order ID: $orderId")
    println("Number of items: ${items.size}")
    println("Discount: $$discount")
}
```
