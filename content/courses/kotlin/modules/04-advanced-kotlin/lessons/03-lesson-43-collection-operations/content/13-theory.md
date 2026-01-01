---
type: "THEORY"
title: "Solution 1: Sales Data Analysis"
---



**Explanation**:
- `sumOf` calculates total with transformation
- `count` with predicate counts matches
- `groupBy` + `mapValues` aggregates by key
- `maxByOrNull` finds maximum based on criteria

---



```kotlin
data class Sale(val product: String, val amount: Double, val quantity: Int)

fun main() {
    val sales = listOf(
        Sale("Laptop", 1200.0, 2),
        Sale("Mouse", 25.0, 10),
        Sale("Keyboard", 75.0, 5),
        Sale("Monitor", 300.0, 3),
        Sale("Laptop", 1200.0, 1),
        Sale("Mouse", 25.0, 15)
    )

    // 1. Total revenue
    val totalRevenue = sales.sumOf { it.amount * it.quantity }
    println("Total revenue: $${"%.2f".format(totalRevenue)}")
    // Total revenue: $5500.00

    // 2. Number of sales over $100 total
    val bigSales = sales.count { it.amount * it.quantity > 100 }
    println("Sales over $100: $bigSales")
    // Sales over $100: 5

    // 3. Average sale amount
    val avgSale = sales.map { it.amount * it.quantity }.average()
    println("Average sale: $${"%.2f".format(avgSale)}")
    // Average sale: $916.67

    // 4. Best-selling product (by quantity)
    val bestSeller = sales
        .groupBy { it.product }
        .mapValues { (_, sales) -> sales.sumOf { it.quantity } }
        .maxByOrNull { it.value }

    println("Best seller: ${bestSeller?.key} (${bestSeller?.value} units)")
    // Best seller: Mouse (25 units)

    // Bonus: Revenue by product
    val revenueByProduct = sales
        .groupBy { it.product }
        .mapValues { (_, sales) ->
            sales.sumOf { it.amount * it.quantity }
        }
        .toList()
        .sortedByDescending { it.second }

    println("\nRevenue by product:")
    revenueByProduct.forEach { (product, revenue) ->
        println("  $product: $${"%.2f".format(revenue)}")
    }
    // Laptop: $3600.00
    // Monitor: $900.00
    // Mouse: $625.00
    // Keyboard: $375.00
}
```
