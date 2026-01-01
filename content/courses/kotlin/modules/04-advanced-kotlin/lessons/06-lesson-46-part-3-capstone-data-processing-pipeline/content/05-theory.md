---
type: "THEORY"
title: "Step 1: Data Model"
---


First, define your data structures.


---



```kotlin
data class SalesRecord(
    val orderId: Int,
    val date: String,
    val customer: String,
    val product: String,
    val category: String,
    val quantity: Int,
    val price: Double,
    val region: String
) {
    val revenue: Double
        get() = quantity * price
}

// Result types for functional error handling
sealed class ParseResult {
    data class Success(val records: List<SalesRecord>) : ParseResult()
    data class Error(val message: String, val lineNumber: Int) : ParseResult()
}
```
