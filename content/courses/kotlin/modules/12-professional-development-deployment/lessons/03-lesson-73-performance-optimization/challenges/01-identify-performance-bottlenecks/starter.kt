data class Order(
    val id: Int,
    val customerId: Int,
    val items: List<OrderItem>,
    val status: String // "pending", "shipped", "delivered", "cancelled"
)

data class OrderItem(
    val productId: Int,
    val name: String,
    val price: Double,
    val quantity: Int
)

// PROBLEM: This function has multiple performance bottlenecks.
// Fix them while keeping the same output.
fun processOrders(orders: List<Order>): Pair<Double, Int> {
    // Bottleneck 1: Creates full intermediate list just to filter
    val activeOrders = orders.filter { it.status != "cancelled" }

    // Bottleneck 2: Flattens ALL items then filters (two full passes)
    val allItems = activeOrders.flatMap { it.items }
    val expensiveItems = allItems.filter { it.price * it.quantity > 50.0 }

    // Bottleneck 3: Maps to totals then sums (two passes over expensiveItems)
    val totals = expensiveItems.map { it.price * it.quantity }
    val grandTotal = totals.sum()

    // Bottleneck 4: Counts by iterating again
    val count = expensiveItems.size

    return Pair(grandTotal, count)
}

fun main() {
    val orders = listOf(
        Order(1, 100, listOf(
            OrderItem(1, "Widget", 10.0, 2),   // 20.0 -- excluded
            OrderItem(2, "Gadget", 45.0, 2)     // 90.0 -- included
        ), "shipped"),
        Order(2, 101, listOf(
            OrderItem(3, "Gizmo", 100.0, 1)     // 100.0 -- included
        ), "pending"),
        Order(3, 102, listOf(
            OrderItem(4, "Doohickey", 80.0, 1)  // 80.0 -- included
        ), "cancelled"),  // excluded
        Order(4, 100, listOf(
            OrderItem(5, "Thingamajig", 5.0, 3) // 15.0 -- excluded
        ), "delivered")
    )

    val (total, count) = processOrders(orders)
    println("Total: $total, Count: $count")
}
