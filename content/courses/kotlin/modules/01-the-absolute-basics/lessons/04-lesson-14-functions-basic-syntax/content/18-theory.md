---
type: "THEORY"
title: "Solution 3: Simple Banking Functions"
---



**Solution Code**:

```kotlin
fun deposit(balance: Double, amount: Double): Double {
    println("Deposited: $$amount")
    return balance + amount
}

fun withdraw(balance: Double, amount: Double): Double {
    if (amount > balance) {
        println("Insufficient funds! Current balance: $$balance")
        return balance
    }
    println("Withdrawn: $$amount")
    return balance - amount
}

fun displayBalance(balance: Double) {
    println("Current Balance: $%.2f".format(balance))
}

fun main() {
    println("=== Simple Banking System ===")
    var balance = 1000.0
    displayBalance(balance)

    println("\nDepositing $500...")
    balance = deposit(balance, 500.0)
    displayBalance(balance)

    println("\nWithdrawing $200...")
    balance = withdraw(balance, 200.0)
    displayBalance(balance)

    println("\nAttempting to withdraw $2000...")
    balance = withdraw(balance, 2000.0)
    displayBalance(balance)
}
```

**Sample Output**:
