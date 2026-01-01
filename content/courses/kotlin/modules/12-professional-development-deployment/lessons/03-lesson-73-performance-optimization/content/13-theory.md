---
type: "THEORY"
title: "Solution 2"
---



---



```kotlin
// 1. Add indexes
@Entity(
    tableName = "orders",
    indices = [
        Index(value = ["user_id"]),
        Index(value = ["created_at"])
    ]
)
data class Order(
    @PrimaryKey val id: String,
    val userId: String,
    val totalAmount: Double,
    val status: String,
    val createdAt: Long
)

// 2. Create joined data class
data class OrderWithDetails(
    @Embedded val order: Order,

    @Relation(
        parentColumn = "user_id",
        entityColumn = "id"
    )
    val user: User,

    @Relation(
        parentColumn = "id",
        entityColumn = "order_id"
    )
    val items: List<OrderItem>
)

// 3. Single query with JOIN
@Dao
interface OrderDao {
    @Transaction
    @Query("SELECT * FROM orders WHERE user_id = :userId ORDER BY created_at DESC")
    fun getOrdersWithDetails(userId: String): List<OrderWithDetails>

    // For pagination
    @Transaction
    @Query("SELECT * FROM orders WHERE user_id = :userId ORDER BY created_at DESC")
    fun getOrdersWithDetailsPaged(userId: String): PagingSource<Int, OrderWithDetails>
}

// Usage
fun displayUserOrders(userId: String) {
    val ordersWithDetails = orderDao.getOrdersWithDetails(userId) // Single query!

    ordersWithDetails.forEach { orderDetail ->
        println("Order ${orderDetail.order.id} by ${orderDetail.user.name}: ${orderDetail.items.size} items")
    }
}

// For large datasets, use paging
fun getOrdersPaged(userId: String): Flow<PagingData<OrderWithDetails>> {
    return Pager(
        config = PagingConfig(pageSize = 20, enablePlaceholders = false),
        pagingSourceFactory = { orderDao.getOrdersWithDetailsPaged(userId) }
    ).flow
}
```
