---
type: "THEORY"
title: "Exercise: Refactor to Functional Style"
---


**Goal**: Transform imperative code to functional style.

**Starting Code**:
```kotlin
fun findTopCustomers(orders: List<Order>): List<Customer> {
    val customerTotals = mutableMapOf<Customer, Double>()
    for (order in orders) {
        val customer = order.customer
        val total = customerTotals.getOrDefault(customer, 0.0)
        customerTotals[customer] = total + order.total
    }
    val result = mutableListOf<Customer>()
    for ((customer, total) in customerTotals) {
        if (total > 1000.0) {
            result.add(customer)
        }
    }
    result.sortByDescending { customerTotals[it] }
    return result.take(10)
}
```

**Requirements**:
1. Use only immutable data structures
2. Use collection operations (groupBy, filter, sortedBy, etc.)
3. Keep it readable

---

