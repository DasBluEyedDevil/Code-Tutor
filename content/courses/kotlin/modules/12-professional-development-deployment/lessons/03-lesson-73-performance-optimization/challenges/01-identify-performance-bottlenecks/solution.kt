data class Order(
    val id: Int,
    val customerId: Int,
    val items: List<OrderItem>,
    val status: String
)

data class OrderItem(
    val productId: Int,
    val name: String,
    val price: Double,
    val quantity: Int
)

fun processOrders(orders: List<Order>): Pair<Double, Int> {
    // Single-pass with sequence: filter -> flatMap -> filter -> fold
    var count = 0
    val total = orders.asSequence()
        .filter { it.status != "cancelled" }
        .flatMap { it.items.asSequence() }
        .filter { it.price * it.quantity > 50.0 }
        .onEach { count++ }
        .sumOf { it.price * it.quantity }

    return Pair(total, count)
}

fun main() {
    val orders = listOf(
        Order(1, 100, listOf(
            OrderItem(1, "Widget", 10.0, 2),
            OrderItem(2, "Gadget", 45.0, 2)
        ), "shipped"),
        Order(2, 101, listOf(
            OrderItem(3, "Gizmo", 100.0, 1)
        ), "pending"),
        Order(3, 102, listOf(
            OrderItem(4, "Doohickey", 80.0, 1)
        ), "cancelled"),
        Order(4, 100, listOf(
            OrderItem(5, "Thingamajig", 5.0, 3)
        ), "delivered")
    )

    val (total, count) = processOrders(orders)
    println("Total: $total, Count: $count")
}
