---
type: "THEORY"
title: "Exercise 2: Optimize Database Queries"
---


Optimize this slow order fetching code.

### Initial Code (Slow)


---



```kotlin
@Dao
interface OrderDao {
    @Query("SELECT * FROM orders")
    fun getAllOrders(): List<Order>

    @Query("SELECT * FROM orders WHERE user_id = :userId")
    fun getOrdersByUser(userId: String): List<Order>
}

// Usage
fun displayUserOrders(userId: String) {
    val orders = orderDao.getOrdersByUser(userId)

    orders.forEach { order ->
        val user = userDao.getById(order.userId) // N+1 query!
        val items = orderItemDao.getByOrderId(order.id) // N+1 query!

        println("Order ${order.id} by ${user.name}: ${items.size} items")
    }
}
```
