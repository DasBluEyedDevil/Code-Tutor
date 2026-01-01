---
type: "THEORY"
title: "Step 6: Report Generator"
---


Create a report generator using functional composition.


---



```kotlin
object ReportGenerator {
    fun generateSummary(records: List<SalesRecord>): String {
        return buildString {
            appendLine("=" .repeat(60))
            appendLine("SALES REPORT SUMMARY")
            appendLine("=".repeat(60))
            appendLine()

            appendLine("📊 Overall Statistics")
            appendLine("-".repeat(60))
            appendLine("Total Orders: ${records.size}")
            appendLine("Total Revenue: ${"$%.2f".format(records.totalRevenue())}")
            appendLine("Average Order Value: ${"$%.2f".format(records.averageOrderValue())}")
            appendLine()

            val categoryData = Analytics.categoryBreakdown(records)
            appendLine("📦 Category Breakdown")
            appendLine("-".repeat(60))
            categoryData
                .toList()
                .sortedByDescending { it.second }
                .forEach { (category, revenue) ->
                    appendLine("  $category: ${"$%.2f".format(revenue)}")
                }
            appendLine()

            val regionalData = Analytics.regionalBreakdown(records)
            appendLine("🌍 Regional Breakdown")
            appendLine("-".repeat(60))
            regionalData
                .toList()
                .sortedByDescending { it.second }
                .forEach { (region, revenue) ->
                    appendLine("  $region: ${"$%.2f".format(revenue)}")
                }
            appendLine()

            appendLine("🏆 Top 5 Products")
            appendLine("-".repeat(60))
            Analytics.topProducts(records, 5)
                .forEachIndexed { index, (product, revenue) ->
                    appendLine("  ${index + 1}. $product: ${"$%.2f".format(revenue)}")
                }
            appendLine()

            appendLine("👥 Top 5 Customers")
            appendLine("-".repeat(60))
            Analytics.topCustomers(records, 5)
                .forEachIndexed { index, (customer, revenue) ->
                    appendLine("  ${index + 1}. $customer: ${"$%.2f".format(revenue)}")
                }
            appendLine()

            appendLine("=".repeat(60))
        }
    }

    fun generateDetailedReport(records: List<SalesRecord>): String {
        return buildString {
            appendLine(generateSummary(records))
            appendLine()
            appendLine("📊 DETAILED PRODUCT STATISTICS")
            appendLine("=".repeat(60))

            Analytics.productStatistics(records)
                .toList()
                .sortedByDescending { it.second.totalRevenue }
                .forEach { (product, stats) ->
                    appendLine()
                    appendLine("Product: $product")
                    appendLine("  Orders: ${stats.totalOrders}")
                    appendLine("  Quantity Sold: ${stats.totalQuantity}")
                    appendLine("  Total Revenue: ${"$%.2f".format(stats.totalRevenue)}")
                    appendLine("  Average Price: ${"$%.2f".format(stats.averagePrice)}")
                }
        }
    }
}
```
