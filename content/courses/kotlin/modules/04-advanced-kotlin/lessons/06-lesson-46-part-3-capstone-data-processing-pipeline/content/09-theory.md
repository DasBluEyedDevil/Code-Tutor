---
type: "THEORY"
title: "Step 5: Analysis Functions"
---


Create analysis functions using functional operations.


---



```kotlin
object Analytics {
    // Category analysis
    fun categoryBreakdown(records: List<SalesRecord>): Map<String, Double> {
        return records
            .groupBy { it.category }
            .mapValues { (_, sales) -> sales.totalRevenue() }
    }

    // Regional analysis
    fun regionalBreakdown(records: List<SalesRecord>): Map<String, Double> {
        return records
            .groupBy { it.region }
            .mapValues { (_, sales) -> sales.totalRevenue() }
    }

    // Top products
    fun topProducts(records: List<SalesRecord>, limit: Int = 5): List<Pair<String, Double>> {
        return records
            .groupBy { it.product }
            .mapValues { (_, sales) -> sales.totalRevenue() }
            .toList()
            .sortedByDescending { it.second }
            .take(limit)
    }

    // Top customers
    fun topCustomers(records: List<SalesRecord>, limit: Int = 5): List<Pair<String, Double>> {
        return records
            .groupBy { it.customer }
            .mapValues { (_, sales) -> sales.totalRevenue() }
            .toList()
            .sortedByDescending { it.second }
            .take(limit)
    }

    // Product statistics
    data class ProductStats(
        val totalOrders: Int,
        val totalQuantity: Int,
        val totalRevenue: Double,
        val averagePrice: Double
    )

    fun productStatistics(records: List<SalesRecord>): Map<String, ProductStats> {
        return records
            .groupBy { it.product }
            .mapValues { (_, sales) ->
                ProductStats(
                    totalOrders = sales.size,
                    totalQuantity = sales.sumOf { it.quantity },
                    totalRevenue = sales.totalRevenue(),
                    averagePrice = sales.map { it.price }.average()
                )
            }
    }
}
```
