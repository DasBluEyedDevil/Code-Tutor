---
type: "THEORY"
title: "Reduce and Fold: Accumulating Values"
---


Reduce/fold combine all elements into a single value.

### Reduce


### Fold: Reduce with Initial Value


### Practical Example: Complex Accumulation


---



```kotlin
data class Transaction(val amount: Double, val type: String)

val transactions = listOf(
    Transaction(100.0, "income"),
    Transaction(50.0, "expense"),
    Transaction(200.0, "income"),
    Transaction(30.0, "expense"),
    Transaction(150.0, "income")
)

// Calculate net balance
val balance = transactions.fold(0.0) { acc, transaction ->
    when (transaction.type) {
        "income" -> acc + transaction.amount
        "expense" -> acc - transaction.amount
        else -> acc
    }
}
println("Balance: $$balance")  // Balance: $370.0
```
