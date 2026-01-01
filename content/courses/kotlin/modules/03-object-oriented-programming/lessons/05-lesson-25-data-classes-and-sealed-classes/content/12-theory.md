---
type: "THEORY"
title: "Exercise: Create Value Classes for a Banking Domain"
---


**Goal**: Create type-safe value classes for a banking application.

**Requirements**:
1. Create `AccountNumber` value class (8-digit string, must start with digit)
2. Create `Money` value class (Double, must be non-negative)
3. Create `TransactionId` value class (UUID string format)
4. Write a `transfer` function that uses these types

**Starter Code**:


---



```kotlin
@JvmInline
value class AccountNumber(val value: String) {
    init {
        require(value.length == 8 && value[0].isDigit()) {
            "Account number must be 8 digits starting with a digit"
        }
    }
}

@JvmInline
value class Money(val amount: Double) {
    init {
        require(amount >= 0) { "Money cannot be negative: $amount" }
    }
}

@JvmInline
value class TransactionId(val value: String) {
    init {
        require(value.matches(Regex("[a-f0-9-]{36}"))) {
            "Invalid UUID format: $value"
        }
    }
}

fun transfer(
    from: AccountNumber,
    to: AccountNumber,
    amount: Money,
    transactionId: TransactionId
): Boolean {
    println("Transferring \$${amount.amount} from ${from.value} to ${to.value}")
    println("Transaction ID: ${transactionId.value}")
    return true
}
```
