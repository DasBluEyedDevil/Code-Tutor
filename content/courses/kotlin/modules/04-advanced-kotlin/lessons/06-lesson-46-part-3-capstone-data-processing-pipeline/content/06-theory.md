---
type: "THEORY"
title: "Step 2: CSV Parser"
---


Create a functional CSV parser.


---



```kotlin
object CsvParser {
    fun parseLine(line: String, lineNumber: Int): SalesRecord? {
        return try {
            val parts = line.split(",")
            if (parts.size != 8) return null

            SalesRecord(
                orderId = parts[0].toInt(),
                date = parts[1],
                customer = parts[2],
                product = parts[3],
                category = parts[4],
                quantity = parts[5].toInt(),
                price = parts[6].toDouble(),
                region = parts[7]
            )
        } catch (e: Exception) {
            println("Error parsing line $lineNumber: ${e.message}")
            null
        }
    }

    fun parseCSV(csvData: String): List<SalesRecord> {
        return csvData
            .lines()
            .drop(1)  // Skip header
            .filter { it.isNotBlank() }
            .mapIndexedNotNull { index, line -> parseLine(line, index + 2) }
    }
}
```
