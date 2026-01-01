---
type: "THEORY"
title: "Value Classes (Inline Classes)"
---


**Value classes** provide type safety without runtime overhead.


**Benefits**:
- Type safety: Can't accidentally pass wrong type
- Zero runtime overhead: Unwrapped at runtime
- Validation in init block

---



```kotlin
@JvmInline
value class UserId(val value: Int)

@JvmInline
value class Email(val value: String) {
    init {
        require(value.contains("@")) { "Invalid email" }
    }
}

fun sendEmail(email: Email) {
    println("Sending email to ${email.value}")
}

fun main() {
    val userId = UserId(123)
    val email = Email("alice@example.com")

    // sendEmail(UserId(456))  // ❌ Type mismatch!
    sendEmail(email)  // ✅ Correct type

    // At runtime, email is just a String (no wrapper object)
}
```
