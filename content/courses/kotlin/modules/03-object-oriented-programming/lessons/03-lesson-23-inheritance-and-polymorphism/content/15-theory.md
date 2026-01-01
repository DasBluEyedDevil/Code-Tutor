---
type: "THEORY"
title: "Solution: Bank Account Hierarchy"
---



---



```kotlin
open class BankAccount(val accountNumber: String, val holder: String) {
    protected var balance: Double = 0.0

    open fun deposit(amount: Double) {
        require(amount > 0) { "Deposit amount must be positive" }
        balance += amount
        println("Deposited $$amount. New balance: $$balance")
    }

    open fun withdraw(amount: Double): Boolean {
        require(amount > 0) { "Withdrawal amount must be positive" }

        return if (amount <= balance) {
            balance -= amount
            println("Withdrew $$amount. New balance: $$balance")
            true
        } else {
            println("Insufficient funds! Balance: $$balance")
            false
        }
    }

    fun displayBalance() {
        println("Account: $accountNumber ($holder)")
        println("Balance: $$balance")
    }
}

class SavingsAccount(
    accountNumber: String,
    holder: String,
    val interestRate: Double
) : BankAccount(accountNumber, holder) {

    private var withdrawalsThisMonth = 0
    private val maxWithdrawals = 3

    override fun withdraw(amount: Double): Boolean {
        if (withdrawalsThisMonth >= maxWithdrawals) {
            println("Withdrawal limit reached! Maximum $maxWithdrawals withdrawals per month.")
            return false
        }

        val success = super.withdraw(amount)
        if (success) {
            withdrawalsThisMonth++
            println("Withdrawals remaining this month: ${maxWithdrawals - withdrawalsThisMonth}")
        }
        return success
    }

    fun applyInterest() {
        val interest = balance * interestRate / 100
        balance += interest
        println("Interest applied: $$interest. New balance: $$balance")
    }

    fun resetMonthlyWithdrawals() {
        withdrawalsThisMonth = 0
        println("Monthly withdrawal limit reset")
    }
}

class CheckingAccount(
    accountNumber: String,
    holder: String,
    val overdraftLimit: Double
) : BankAccount(accountNumber, holder) {

    override fun withdraw(amount: Double): Boolean {
        require(amount > 0) { "Withdrawal amount must be positive" }

        val availableFunds = balance + overdraftLimit

        return if (amount <= availableFunds) {
            balance -= amount
            println("Withdrew $$amount. New balance: $$balance")
            if (balance < 0) {
                println("⚠️ Account overdrawn by $${-balance}")
            }
            true
        } else {
            println("Exceeds overdraft limit! Available: $$availableFunds")
            false
        }
    }
}

fun main() {
    println("=== Savings Account ===")
    val savings = SavingsAccount("SAV001", "Alice Johnson", 2.5)
    savings.deposit(1000.0)
    savings.applyInterest()
    savings.withdraw(100.0)
    savings.withdraw(100.0)
    savings.withdraw(100.0)
    savings.withdraw(100.0)  // Should fail (limit reached)
    savings.displayBalance()

    println("\n=== Checking Account ===")
    val checking = CheckingAccount("CHK001", "Bob Smith", 500.0)
    checking.deposit(1000.0)
    checking.withdraw(1200.0)  // Uses overdraft
    checking.withdraw(400.0)   // Should fail (exceeds overdraft limit)
    checking.displayBalance()
}
```
