---
type: "THEORY"
title: "Custom Getters and Setters"
---


### Custom Getters

A **custom getter** computes a value every time the property is accessed.

**Example: Computed Properties**


**Why use a custom getter instead of a method?**
- More natural syntax: `rect.area` vs `rect.getArea()`
- Semantic: it looks like a property because it behaves like one
- Lightweight computation that doesn't change the object state

**Example: Derived Properties**


### Custom Setters

A **custom setter** validates or transforms values when they're assigned.

**Example: Input Validation**


**Key Points**:
- `set(value)` defines custom logic when the property is assigned
- `field` refers to the **backing field** (the actual stored value)
- Use `field` to avoid infinite recursion (don't use the property name inside its own setter!)

### Visibility Modifiers for Setters

You can make a property readable publicly but only writable internally:


---



```kotlin
class BankAccount(initialBalance: Double) {
    var balance: Double = initialBalance
        private set  // Can only be modified inside the class

    fun deposit(amount: Double) {
        require(amount > 0) { "Amount must be positive" }
        balance += amount
    }

    fun withdraw(amount: Double) {
        require(amount > 0 && amount <= balance) { "Invalid withdrawal" }
        balance -= amount
    }
}

fun main() {
    val account = BankAccount(1000.0)

    println(account.balance)  // ✅ Can read: 1000.0
    account.deposit(500.0)
    println(account.balance)  // 1500.0

    // account.balance = 9999.0  // ❌ Error: Cannot assign to 'balance': the setter is private
}
```
