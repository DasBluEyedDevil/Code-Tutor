---
type: "THEORY"
title: "Properties"
---


**Properties** are variables that belong to a class. They define the state of an object.

**Two Types**:
- **`val`** (immutable): Cannot be changed after initialization
- **`var`** (mutable): Can be changed


---



```kotlin
class BankAccount {
    val accountNumber: String = "123456"  // Can't change
    var balance: Double = 0.0              // Can change

    fun deposit(amount: Double) {
        balance += amount
    }

    fun withdraw(amount: Double) {
        if (amount <= balance) {
            balance -= amount
        } else {
            println("Insufficient funds!")
        }
    }
}

fun main() {
    val account = BankAccount()
    println(account.balance)  // 0.0

    account.deposit(100.0)
    println(account.balance)  // 100.0

    account.withdraw(30.0)
    println(account.balance)  // 70.0

    // account.accountNumber = "999999"  // ❌ Error: Val cannot be reassigned
}
```
