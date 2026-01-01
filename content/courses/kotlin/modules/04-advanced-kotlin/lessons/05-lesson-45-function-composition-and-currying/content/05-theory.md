---
type: "THEORY"
title: "Currying"
---


Transforming a function with multiple parameters into a sequence of functions, each taking a single parameter.

### Basic Currying


### Generic Currying Helper


### Three-Parameter Currying


### Practical Example: Configuration Builder


---



```kotlin
// Regular function with many parameters
fun sendEmail(
    to: String,
    subject: String,
    body: String,
    priority: String,
    attachments: List<String>
) {
    println("Sending email:")
    println("  To: $to")
    println("  Subject: $subject")
    println("  Body: $body")
    println("  Priority: $priority")
    println("  Attachments: $attachments")
}

// Curried version for reusability
fun emailSender(to: String) = { subject: String ->
    { body: String ->
        { priority: String ->
            { attachments: List<String> ->
                sendEmail(to, subject, body, priority, attachments)
            }
        }
    }
}

// Create specialized senders
val sendToAdmin = emailSender("admin@example.com")
val sendAlertToAdmin = sendToAdmin("ALERT")

// Use it
sendAlertToAdmin("System down")("HIGH")(emptyList())

// Or create even more specialized versions
val sendHighPriorityAlert = sendToAdmin("ALERT")("System issue")("HIGH")
sendHighPriorityAlert(listOf("log.txt"))
```
