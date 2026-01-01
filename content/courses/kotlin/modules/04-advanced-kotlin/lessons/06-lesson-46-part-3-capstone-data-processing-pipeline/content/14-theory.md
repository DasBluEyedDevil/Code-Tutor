---
type: "THEORY"
title: "Testing Your Pipeline"
---


Create test functions to verify your implementation:


---



```kotlin
fun testPipeline() {
    val testData = """
OrderID,Date,Customer,Product,Category,Quantity,Price,Region
1,2024-01-01,Test User,Test Product,Test,1,100.00,North
2,2024-01-02,Test User,Test Product,Test,2,50.00,South
    """.trimIndent()

    val records = CsvParser.parseCSV(testData).validated().normalized()

    // Test parsing
    assert(records.size == 2) { "Should parse 2 records" }

    // Test revenue calculation
    val total = records.totalRevenue()
    assert(total == 200.0) { "Total revenue should be 200" }

    // Test filtering
    val north = records inRegion "NORTH"
    assert(north.size == 1) { "Should find 1 North region record" }

    println("✅ All tests passed!")
}
```
