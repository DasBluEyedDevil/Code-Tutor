---
type: "EXAMPLE"
title: "Practical Examples"
---


### Example 1: Data Processing Pipeline


### Example 2: Validation Framework


### Example 3: Event System


---



```kotlin
class EventBus {
    private val handlers = mutableMapOf<String, MutableList<(Any) -> Unit>>()

    fun on(event: String, handler: (Any) -> Unit) {
        handlers.getOrPut(event) { mutableListOf() }.add(handler)
    }

    fun emit(event: String, data: Any) {
        handlers[event]?.forEach { it(data) }
    }
}

// Usage
val bus = EventBus()

// Lambda with named parameter
bus.on("user_login") { data ->
    val user = data as String
    println("User logged in: $user")
}

// Lambda with 'it'
bus.on("user_logout") {
    println("User logged out: $it")
}

// Function reference
fun handleError(error: Any) {
    println("Error occurred: $error")
}
bus.on("error", ::handleError)

// Emit events
bus.emit("user_login", "Alice")
bus.emit("user_logout", "Bob")
bus.emit("error", "Connection failed")
```
