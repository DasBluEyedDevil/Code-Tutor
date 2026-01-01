---
type: "EXAMPLE"
title: "Composing Multiple Error Types"
---


Handling functions with different error types:



```kotlin
import arrow.core.raise.*
import arrow.core.*

sealed interface UserError { /* ... */ }
sealed interface PaymentError { /* ... */ }
sealed interface OrderError { /* ... */ }

// Union error type
sealed interface AppError {
    data class User(val error: UserError) : AppError
    data class Payment(val error: PaymentError) : AppError
    data class Order(val error: OrderError) : AppError
}

// Functions with different error types
context(Raise<UserError>)
fun getUser(id: Long): User = /* ... */

context(Raise<PaymentError>)
fun processPayment(amount: Double): Receipt = /* ... */

// Compose by mapping errors
context(Raise<AppError>)
fun processOrder(userId: Long, amount: Double): OrderResult {
    // withError maps the error type
    val user = withError({ AppError.User(it) }) {
        getUser(userId)
    }
    
    val receipt = withError({ AppError.Payment(it) }) {
        processPayment(amount)
    }
    
    return OrderResult(user, receipt)
}

// Usage
either<AppError, OrderResult> {
    processOrder(123, 99.99)
}.fold(
    ifLeft = { error ->
        when (error) {
            is AppError.User -> handleUserError(error.error)
            is AppError.Payment -> handlePaymentError(error.error)
            is AppError.Order -> handleOrderError(error.error)
        }
    },
    ifRight = { result -> handleSuccess(result) }
)
```
