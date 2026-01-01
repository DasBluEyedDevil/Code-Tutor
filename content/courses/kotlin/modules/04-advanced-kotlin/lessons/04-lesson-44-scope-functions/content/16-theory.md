---
type: "THEORY"
title: "Solution 1: Refactor with Scope Functions"
---



**Explanation**:
- `apply`: Configure the email object
- `also`: Log without breaking the chain
- `takeIf`: Conditional processing
- Chainable, readable, and expressive!

---



```kotlin
data class Email(
    var to: String = "",
    var subject: String = "",
    var body: String = "",
    var sent: Boolean = false
)

fun sendEmailRefactored() {
    Email()
        .apply {
            // Configure email
            to = "user@example.com"
            subject = "Welcome"
            body = "Welcome to our service!"
        }
        .also {
            // Side effect: log
            println("Sending email to: ${it.to}")
        }
        .takeIf { it.to.isNotEmpty() && it.subject.isNotEmpty() }
        ?.apply {
            // Mark as sent
            sent = true
        }
        ?.also {
            // Side effect: confirm
            println("Email sent successfully")
        }
        ?: println("Email validation failed")
}

fun main() {
    sendEmailRefactored()
    // Sending email to: user@example.com
    // Email sent successfully
}
```
