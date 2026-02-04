---
type: "EXAMPLE"
title: "Transaction Context Pattern"
---


A real-world pattern for database transactions:



```kotlin
// Transaction context for database operations
interface TransactionContext {
    fun execute(sql: String): Int
    fun query(sql: String): List<Map<String, Any>>
    fun rollback()
}

class Transaction(
    private val connection: Connection
) : TransactionContext {
    override fun execute(sql: String): Int {
        return connection.prepareStatement(sql).executeUpdate()
    }

    override fun query(sql: String): List<Map<String, Any>> {
        // Implementation
        return emptyList()
    }

    override fun rollback() {
        connection.rollback()
    }
}

// Functions that require transaction context
context(tx: TransactionContext)
fun transferMoney(from: Long, to: Long, amount: BigDecimal) {
    tx.execute("UPDATE accounts SET balance = balance - $amount WHERE id = $from")
    tx.execute("UPDATE accounts SET balance = balance + $amount WHERE id = $to")
}

context(tx: TransactionContext)
fun createOrder(userId: Long, items: List<OrderItem>): Long {
    tx.execute("INSERT INTO orders (user_id, created_at) VALUES ($userId, NOW())")
    val orderId = tx.query("SELECT LAST_INSERT_ID()").first()["id"] as Long

    items.forEach { item ->
        tx.execute(
            "INSERT INTO order_items (order_id, product_id, quantity) " +
            "VALUES ($orderId, ${item.productId}, ${item.quantity})"
        )
    }

    return orderId
}

// Transaction helper
fun <T> transaction(block: context(TransactionContext) () -> T): T {
    val tx = Transaction(getConnection())
    return try {
        with(tx) {
            val result = block()
            execute("COMMIT")
            result
        }
    } catch (e: Exception) {
        tx.rollback()
        throw e
    }
}

// Usage
fun main() {
    transaction {
        transferMoney(from = 1, to = 2, amount = 100.toBigDecimal())
        createOrder(userId = 1, items = listOf(OrderItem(productId = 42, quantity = 2)))
    }
}
```
