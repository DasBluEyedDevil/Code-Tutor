---
type: "THEORY"
title: "Observable Properties"
---


Observable delegates notify you when a property changes.

### Delegates.observable


### Delegates.vetoable

Veto (reject) property changes based on a condition:


---



```kotlin
class Account {
    var balance: Double by Delegates.vetoable(0.0) { _, oldValue, newValue ->
        println("Attempting to change balance from $oldValue to $newValue")

        // Veto negative balances
        if (newValue < 0) {
            println("❌ Rejected: balance cannot be negative")
            false  // Reject change
        } else {
            println("✅ Accepted")
            true  // Accept change
        }
    }
}

fun main() {
    val account = Account()

    account.balance = 100.0  // ✅ Accepted
    println("Balance: ${account.balance}")

    account.balance = -50.0  // ❌ Rejected
    println("Balance: ${account.balance}")  // Still 100.0

    account.balance = 200.0  // ✅ Accepted
    println("Balance: ${account.balance}")
}
```
