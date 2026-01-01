---
type: "THEORY"
title: "Accessing Map Values"
---


### Basic Access


### Safe Access Patterns


---



```kotlin
fun main() {
    val contacts = mapOf(
        "Alice" to "alice@email.com",
        "Bob" to "bob@email.com"
    )

    // Nullable return
    val aliceEmail: String? = contacts["Alice"]
    println(aliceEmail)  // alice@email.com

    // With default
    val charlieEmail = contacts.getOrElse("Charlie") { "unknown@email.com" }
    println(charlieEmail)  // unknown@email.com

    // Check before accessing
    if ("Alice" in contacts) {
        println("Alice's email: ${contacts["Alice"]}")
    }
}
```
