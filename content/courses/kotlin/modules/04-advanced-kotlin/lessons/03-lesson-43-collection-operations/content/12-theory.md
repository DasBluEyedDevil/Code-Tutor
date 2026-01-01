---
type: "THEORY"
title: "Exercise 1: Sales Data Analysis"
---


**Goal**: Analyze sales data using collection operations.

**Task**: Given sales data, calculate:
1. Total revenue
2. Number of sales over $100
3. Average sale amount
4. Best-selling product


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

    // TODO: Implement analysis
}
```
