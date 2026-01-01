---
type: "THEORY"
title: "Exercise 1: Refactor with Scope Functions"
---


**Goal**: Refactor imperative code using scope functions.

**Task**: Rewrite this code using appropriate scope functions:


---



```kotlin
data class Email(
    var to: String = "",
    var subject: String = "",
    var body: String = "",
    var sent: Boolean = false
)

fun sendEmail() {
    val email = Email()
    email.to = "user@example.com"
    email.subject = "Welcome"
    email.body = "Welcome to our service!"

    println("Sending email to: ${email.to}")

    if (email.to.isNotEmpty() && email.subject.isNotEmpty()) {
        email.sent = true
        println("Email sent successfully")
    }
}
```
