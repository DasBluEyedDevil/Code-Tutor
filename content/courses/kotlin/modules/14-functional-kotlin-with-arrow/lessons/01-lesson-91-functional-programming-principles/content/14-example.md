---
type: "EXAMPLE"
title: "Solution: Functional Refactoring"
---




```kotlin
// FUNCTIONAL SOLUTION
fun findTopCustomers(orders: List<Order>): List<Customer> =
    orders
        .groupBy { it.customer }  // Map<Customer, List<Order>>
        .mapValues { (_, orders) -> orders.sumOf { it.total } }  // Map<Customer, Double>
        .filter { (_, total) -> total > 1000.0 }
        .entries
        .sortedByDescending { it.value }
        .take(10)
        .map { it.key }

// ALTERNATIVE with data class for clarity
data class CustomerTotal(val customer: Customer, val total: Double)

fun findTopCustomersAlt(orders: List<Order>): List<Customer> =
    orders
        .groupBy { it.customer }
        .map { (customer, orders) ->
            CustomerTotal(customer, orders.sumOf { it.total })
        }
        .filter { it.total > 1000.0 }
        .sortedByDescending { it.total }
        .take(10)
        .map { it.customer }
```
