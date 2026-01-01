---
type: "THEORY"
title: "Step 4: Data Transformation Pipeline"
---


Create transformation and enrichment functions.


---



```kotlin
// Extension functions for transformations
fun SalesRecord.normalize() = this.copy(
    customer = customer.trim(),
    product = product.trim(),
    category = category.trim(),
    region = region.trim().uppercase()
)

fun List<SalesRecord>.normalized() = this.map { it.normalize() }

// Revenue calculations
fun List<SalesRecord>.totalRevenue() = this.sumOf { it.revenue }

fun List<SalesRecord>.averageOrderValue() =
    if (this.isEmpty()) 0.0 else this.totalRevenue() / this.size
```
