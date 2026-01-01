---
type: "THEORY"
title: "Extension Challenges"
---


Take the project further with these challenges!

### Challenge 1: Date-Based Analysis

Add time-series analysis:


### Challenge 2: Customer Segmentation

Classify customers by spending:


### Challenge 3: Product Recommendations

Find frequently bought together items:


### Challenge 4: Export to Different Formats

Add JSON/CSV export:


### Challenge 5: Sequence Optimization

Use sequences for large datasets:


---



```kotlin
fun processLargeDataset(csvData: String): List<SalesRecord> {
    return csvData.lineSequence()  // Sequence instead of lines()
        .drop(1)
        .filter { it.isNotBlank() }
        .mapNotNull { CsvParser.parseLine(it) }
        .filter(Validators::validateRecord)
        .map { it.normalize() }
        .toList()
}
```
