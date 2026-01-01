---
type: "THEORY"
title: "Complete Solution"
---


Here's the full working solution:


---



```kotlin
// Data Model
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
    val revenue: Double get() = quantity * price
}

// CSV Parser
object CsvParser {
    fun parseLine(line: String): SalesRecord? {
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
            null
        }
    }

    fun parseCSV(csvData: String): List<SalesRecord> {
        return csvData.lines()
            .drop(1)
            .filter { it.isNotBlank() }
            .mapNotNull { parseLine(it) }
    }
}

// Validators
object Validators {
    val validQuantity: (SalesRecord) -> Boolean = { it.quantity > 0 }
    val validPrice: (SalesRecord) -> Boolean = { it.price > 0 }
    val validCustomer: (SalesRecord) -> Boolean = { it.customer.isNotBlank() }

    fun validateRecord(record: SalesRecord): Boolean =
        listOf(validQuantity, validPrice, validCustomer).all { it(record) }
}

// Extensions
fun SalesRecord.normalize() = copy(
    customer = customer.trim(),
    product = product.trim(),
    category = category.trim(),
    region = region.trim().uppercase()
)

fun List<SalesRecord>.validated() = filter(Validators::validateRecord)
fun List<SalesRecord>.normalized() = map { it.normalize() }
fun List<SalesRecord>.totalRevenue() = sumOf { it.revenue }
fun List<SalesRecord>.averageOrderValue() =
    if (isEmpty()) 0.0 else totalRevenue() / size

infix fun List<SalesRecord>.inCategory(category: String) =
    filter { it.category.equals(category, ignoreCase = true) }

infix fun List<SalesRecord>.inRegion(region: String) =
    filter { it.region.equals(region, ignoreCase = true) }

// Analytics
object Analytics {
    fun categoryBreakdown(records: List<SalesRecord>) =
        records.groupBy { it.category }
            .mapValues { (_, sales) -> sales.totalRevenue() }

    fun regionalBreakdown(records: List<SalesRecord>) =
        records.groupBy { it.region }
            .mapValues { (_, sales) -> sales.totalRevenue() }

    fun topProducts(records: List<SalesRecord>, limit: Int = 5) =
        records.groupBy { it.product }
            .mapValues { (_, sales) -> sales.totalRevenue() }
            .toList()
            .sortedByDescending { it.second }
            .take(limit)

    fun topCustomers(records: List<SalesRecord>, limit: Int = 5) =
        records.groupBy { it.customer }
            .mapValues { (_, sales) -> sales.totalRevenue() }
            .toList()
            .sortedByDescending { it.second }
            .take(limit)
}

// Report Generator
object ReportGenerator {
    fun generate(records: List<SalesRecord>): String = buildString {
        appendLine("=" .repeat(60))
        appendLine("SALES REPORT")
        appendLine("=".repeat(60))
        appendLine()

        appendLine("📊 Overall Statistics")
        appendLine("Total Orders: ${records.size}")
        appendLine("Total Revenue: ${"$%.2f".format(records.totalRevenue())}")
        appendLine("Average Order: ${"$%.2f".format(records.averageOrderValue())}")
        appendLine()

        appendLine("📦 Category Breakdown")
        Analytics.categoryBreakdown(records)
            .toList()
            .sortedByDescending { it.second }
            .forEach { (cat, rev) ->
                appendLine("  $cat: ${"$%.2f".format(rev)}")
            }
        appendLine()

        appendLine("🌍 Regional Breakdown")
        Analytics.regionalBreakdown(records)
            .toList()
            .sortedByDescending { it.second }
            .forEach { (reg, rev) ->
                appendLine("  $reg: ${"$%.2f".format(rev)}")
            }
        appendLine()

        appendLine("🏆 Top 5 Products")
        Analytics.topProducts(records, 5)
            .forEachIndexed { i, (prod, rev) ->
                appendLine("  ${i + 1}. $prod: ${"$%.2f".format(rev)}")
            }
        appendLine()

        appendLine("👥 Top 5 Customers")
        Analytics.topCustomers(records, 5)
            .forEachIndexed { i, (cust, rev) ->
                appendLine("  ${i + 1}. $cust: ${"$%.2f".format(rev)}")
            }
    }
}

// Main Application
fun main() {
    val csvData = """
OrderID,Date,Customer,Product,Category,Quantity,Price,Region
1001,2024-01-15,Alice Johnson,Laptop,Electronics,1,1200.00,North
1002,2024-01-16,Bob Smith,Mouse,Electronics,2,25.00,South
1003,2024-01-17,Alice Johnson,Keyboard,Electronics,1,75.00,North
1004,2024-01-18,Charlie Brown,Desk,Furniture,1,300.00,East
1005,2024-01-19,Diana Prince,Chair,Furniture,2,150.00,West
1006,2024-01-20,Bob Smith,Monitor,Electronics,1,400.00,South
1007,2024-01-21,Alice Johnson,Lamp,Furniture,3,50.00,North
1008,2024-01-22,Eve Davis,Laptop,Electronics,1,1200.00,East
1009,2024-01-23,Frank Miller,Mouse,Electronics,5,25.00,West
1010,2024-01-24,Charlie Brown,Desk,Furniture,1,300.00,East
1011,2024-01-25,Alice Johnson,Monitor,Electronics,1,400.00,North
1012,2024-01-26,Bob Smith,Keyboard,Electronics,2,75.00,South
1013,2024-01-27,Diana Prince,Laptop,Electronics,1,1200.00,West
1014,2024-01-28,Eve Davis,Chair,Furniture,2,150.00,East
1015,2024-01-29,Frank Miller,Lamp,Furniture,1,50.00,West
    """.trimIndent()

    // Process data through functional pipeline
    val allRecords = CsvParser.parseCSV(csvData)
        .validated()
        .normalized()

    println("Processed ${allRecords.size} records\n")

    // Generate full report
    println(ReportGenerator.generate(allRecords))

    // Demonstrate functional filtering
    println("\n" + "=".repeat(60))
    println("CUSTOM ANALYSIS EXAMPLES")
    println("=".repeat(60))

    // Electronics in North region
    val northElectronics = allRecords inCategory "Electronics" inRegion "NORTH"
    println("\nElectronics in North Region:")
    println("  Orders: ${northElectronics.size}")
    println("  Revenue: ${"$%.2f".format(northElectronics.totalRevenue())}")

    // Furniture analysis
    val furniture = allRecords inCategory "Furniture"
    println("\nFurniture Sales:")
    println("  Orders: ${furniture.size}")
    println("  Revenue: ${"$%.2f".format(furniture.totalRevenue())}")
    println("  Average Order: ${"$%.2f".format(furniture.averageOrderValue())}")

    // High-value orders
    val highValue = allRecords.filter { it.revenue > 500 }
    println("\nHigh-Value Orders (>$500):")
    println("  Count: ${highValue.size}")
    println("  Total: ${"$%.2f".format(highValue.totalRevenue())}")
}
```
