---
type: "EXAMPLE"
title: "Value Classes for Domain Modeling"
---


### Type-Safe IDs Prevent Mixing Entity IDs

One of the most powerful uses of value classes is creating type-safe IDs:


### Validated Primitives

Value classes can validate data at construction time:


**Key Benefits**:
- **Zero runtime overhead**: Compiled to primitives
- **Type safety**: Compiler prevents mixing different ID types
- **Validation**: Business rules enforced at creation
- **Self-documenting**: Code expresses intent clearly

---



```kotlin
// Type-safe IDs prevent mixing different entity IDs
@JvmInline
value class UserId(val value: Long)

@JvmInline
value class OrderId(val value: Long)

@JvmInline
value class ProductId(val value: Long)

// Compiler prevents mistakes!
fun getOrder(orderId: OrderId): Order { /* ... */ }

val userId = UserId(123)
// getOrder(userId)  // Compile error: Type mismatch!
//                   // Required: OrderId, Found: UserId

val orderId = OrderId(456)
getOrder(orderId)  // Works correctly

// Validated primitives with business rules
@JvmInline
value class Email(val address: String) {
    init {
        require(address.contains("@") && address.contains(".")) {
            "Invalid email format: $address"
        }
    }
}

@JvmInline
value class PositiveInt(val value: Int) {
    init {
        require(value > 0) { "Value must be positive, got: $value" }
    }
}

@JvmInline
value class Percentage(val value: Double) {
    init {
        require(value in 0.0..100.0) { "Percentage must be 0-100, got: $value" }
    }
}

// Usage - validation happens at construction
val email = Email("user@example.com")  // OK
// val badEmail = Email("invalid")     // Throws IllegalArgumentException

val discount = Percentage(15.0)  // OK
// val badDiscount = Percentage(150.0)  // Throws IllegalArgumentException
```
