---
type: "THEORY"
title: "Step 7: Complete Pipeline"
---


Put it all together in a functional pipeline.


---



```kotlin
class SalesDataPipeline {
    private val transformations = mutableListOf<(List<SalesRecord>) -> List<SalesRecord>>()

    fun addTransformation(transform: (List<SalesRecord>) -> List<SalesRecord>) = apply {
        transformations.add(transform)
    }

    fun process(csvData: String): List<SalesRecord> {
        var records = CsvParser.parseCSV(csvData)

        // Apply all transformations in sequence
        transformations.forEach { transform ->
            records = transform(records)
        }

        return records
    }
}

// Create pipeline
fun createPipeline() = SalesDataPipeline()
    .addTransformation { it.validated() }
    .addTransformation { it.normalized() }

// Infix function for readable filtering
infix fun List<SalesRecord>.inCategory(category: String) =
    this.filter { it.category.equals(category, ignoreCase = true) }

infix fun List<SalesRecord>.inRegion(region: String) =
    this.filter { it.region.equals(region, ignoreCase = true) }

fun List<SalesRecord>.withRevenueAbove(amount: Double) =
    this.filter { it.revenue > amount }
```
