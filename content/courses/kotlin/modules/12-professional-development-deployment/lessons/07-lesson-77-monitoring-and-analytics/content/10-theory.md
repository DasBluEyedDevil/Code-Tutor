---
type: "THEORY"
title: "Solution 1"
---



---



```kotlin
import org.slf4j.LoggerFactory
import net.logstash.logback.argument.StructuredArguments.*

class OrderService(
    private val orderRepository: OrderRepository,
    private val paymentService: PaymentService,
    private val inventoryService: InventoryService
) {
    private val logger = LoggerFactory.getLogger(OrderService::class.java)

    suspend fun createOrder(userId: String, items: List<OrderItem>): Order {
        logger.info(
            "Creating order",
            keyValue("userId", userId),
            keyValue("itemCount", items.size)
        )

        // Validate inventory
        logger.debug("Checking inventory for ${items.size} items")

        items.forEach { item ->
            val available = inventoryService.checkStock(item.productId, item.quantity)
            if (!available) {
                logger.warn(
                    "Insufficient inventory",
                    keyValue("productId", item.productId),
                    keyValue("requested", item.quantity)
                )
                throw InsufficientInventoryException(item.productId)
            }
        }

        // Calculate total
        val total = items.sumOf { it.price * it.quantity }
        logger.debug("Order total calculated: $$total")

        // Create order
        val order = try {
            orderRepository.create(
                userId = userId,
                items = items,
                total = total,
                status = OrderStatus.PENDING
            )
        } catch (e: SQLException) {
            logger.error(
                "Database error creating order",
                e,
                keyValue("userId", userId)
            )
            throw e
        }

        logger.info(
            "Order created",
            keyValue("orderId", order.id),
            keyValue("total", total),
            keyValue("status", order.status)
        )

        // Process payment
        try {
            logger.info("Processing payment for order ${order.id}")
            paymentService.charge(userId, total, order.id)

            orderRepository.updateStatus(order.id, OrderStatus.PAID)

            logger.info(
                "Payment successful",
                keyValue("orderId", order.id),
                keyValue("amount", total)
            )
        } catch (e: PaymentException) {
            logger.error(
                "Payment failed",
                e,
                keyValue("orderId", order.id),
                keyValue("userId", userId),
                keyValue("amount", total),
                keyValue("errorCode", e.errorCode)
            )

            orderRepository.updateStatus(order.id, OrderStatus.PAYMENT_FAILED)
            throw e
        }

        // Reserve inventory
        try {
            items.forEach { item ->
                inventoryService.reserve(item.productId, item.quantity, order.id)
            }

            logger.info(
                "Inventory reserved",
                keyValue("orderId", order.id)
            )
        } catch (e: Exception) {
            logger.error(
                "Inventory reservation failed",
                e,
                keyValue("orderId", order.id)
            )
            // Rollback payment
            paymentService.refund(order.id)
            throw e
        }

        logger.info(
            "Order processed successfully",
            keyValue("orderId", order.id),
            keyValue("userId", userId),
            keyValue("total", total),
            keyValue("itemCount", items.size)
        )

        return order
    }
}
```
